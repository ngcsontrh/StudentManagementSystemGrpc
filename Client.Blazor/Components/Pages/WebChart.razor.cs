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

        // models 
        private List<StudentProfile>? students;

        private string? errorMessage;

        // model datatype for charts
        private List<StudentAgeChartModel> data1 = null!;
        private List<ClassChartModel> data2 = null!;

        // config charts
        private PieConfig config2 = null!;
        private ColumnConfig config1 = null!;

        // load all students for analysis.
        private async Task LoadStudentsData()
        {
            var studentReply = await StudentService.GetAllProfilesAsync(new Shared.Empty());
            if (studentReply.Students == null)
            {
                errorMessage = studentReply.Message;
            }
            else
            {
                students = studentReply.Students;
            }
        }

        private void LoadStudentAgeChart()
        {
            data1 = students!
                    .GroupBy(s => (DateTime.Now.Year - s.Birthday.Year))
                    .Select(s => new StudentAgeChartModel
                    {
                        Age = s.Key,
                        NumberOfStudent = s.Count(),
                    })
                    .ToList();

            config1 = new ColumnConfig
            {
                AutoFit = true,
                Padding = "auto",
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
         }

        private void LoadClassChart()
        {
            data2 = students!
                    .GroupBy(s => s.ClassName)
                    .Select(s => new ClassChartModel
                    {
                        ClassName = s.Key,
                        NumberOfStudent = s.Count()
                    })
                    .ToList();

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
            if(students != null)
            {
                LoadStudentAgeChart();
                LoadClassChart();
            }
        }
    }
}
