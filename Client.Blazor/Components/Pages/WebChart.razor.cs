using AntDesign;
using AntDesign.Charts;
using Client.Blazor.Models;
using Microsoft.AspNetCore.Components;
using Shared;

namespace Client.Blazor.Components.Pages
{
    public partial class WebChart : ComponentBase
    {
        [Inject]
        public IStudentService StudentService { get; set; } = null!;

        [Inject]
        public NotificationService Notification {  get; set; } = null!;

        IChartComponent chart1 = null!;

        // models 
        private List<StudentProfileModel>? students;
        private List<ClassInformationModel>? classes;

        // model datatype for charts
        private List<StudentAgeChartModel> data1 = null!;
        private List<ClassChartModel> data2 = null!;

        // config charts
        private ColumnConfig config1 = null!;
        private PieConfig config2 = null!;

        bool isFirstRender = true;

        // load all students for analysis.
        private async Task LoadStudentsData()
        {
            var studentReply = await StudentService.GetAllProfilesAsync(new Shared.Empty());
            if (studentReply.Students == null)
            {
                _ = Notification.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = studentReply.Message,
                    NotificationType = NotificationType.Error
                });
            }
            else
            {
                students = studentReply.Students.Select(s => new StudentProfileModel
                {
                    Id = s.Id,
                    FullName = s.FullName,
                    Address = s.Address,
                    Birthday = s.Birthday,
                    ClassId = s.ClassId,
                    ClassName = s.ClassName,
                }).ToList();

                classes = studentReply.Students
                .Select(s => new ClassInformationModel
                {
                    Id = s.ClassId,
                    Name = s.ClassName
                })
                .DistinctBy(s => s.Id)
                .ToList();

                data1 = students!
                  .GroupBy(s => (DateTime.Now.Year - s.Birthday.Year))
                  .Select(s => new StudentAgeChartModel
                  {
                      Age = s.Key,
                      NumberOfStudent = s.Count(),
                  })
                  .OrderBy(s => s.Age)
                  .ToList();

                data2 = students!
                    .GroupBy(s => s.ClassName)
                    .Select(s => new ClassChartModel
                    {
                        ClassName = s.Key,
                        NumberOfStudent = s.Count()
                    })
                    .ToList();
            }
        }

        private async Task LoadStudentAgeClass(int classId = -1)
        {
            var temp = students!.ToList();
            if(classId != -1)
            {
                temp = temp.Where(s => s.ClassId == classId).ToList();
            }
            data1 = temp
                    .GroupBy(s => (DateTime.Now.Year - s.Birthday.Year))
                    .Select(s => new StudentAgeChartModel
                    {
                        Age = s.Key,
                        NumberOfStudent = s.Count(),
                    })
                    .ToList();
            
            await chart1.ChangeData(data1, !isFirstRender);
            
        }

        void Config()
        {
            config1 = new ColumnConfig
            {
                AutoFit = true,
                Padding = new[] { 40, 40, 40, 40 },
                XField = "age",
                YField = "numberOfStudent",
                Meta = new
                {
                    Age = new
                    {
                        Alias = "Age Of Student"
                    },
                    NumberOfStudent = new
                    {
                        Alias = "Number Of Student"
                    }
                },
                Label = new ColumnViewConfigLabel
                {
                    Visible = true,
                    Style = new TextStyle
                    {
                        FontSize = 12,
                        FontWeight = 600,
                        Opacity = 0.6,
                    }

                }
            };

            config2 = new PieConfig
            {
                AutoFit = true,
                Radius = 0.8,
                AngleField = "numberOfStudent",
                ColorField = "className",
                Label = new PieLabelConfig
                {
                    Visible = true,
                    Type = "spider"
                }
            };
        }
        protected override async Task OnInitializedAsync()
        {
            await LoadStudentsData();
            Config();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await chart1.ChangeData(data1);
            isFirstRender = false;
        }
    }
}