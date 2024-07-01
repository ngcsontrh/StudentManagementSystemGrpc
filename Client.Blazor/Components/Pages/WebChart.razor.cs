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

        private string? errorMessage;
        private List<StudentProfile>? students;

        private List<StudentAgeChartModel> data1 = null!;
        private PieConfig config1 = null!;

        private List<ClassChartModel> data2 = null!;
        private PieConfig config2 = null!;

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
            config1 = new PieConfig
            {
                AutoFit = true,
                Radius = 0.8,
                AngleField = "numberOfStudent",
                ColorField = "age",
                Label = new PieLabelConfig
                {
                    Visible = true,
                    Type = "inner"
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
