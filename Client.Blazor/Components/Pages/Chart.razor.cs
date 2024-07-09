using AntDesign;
using AntDesign.Charts;
using AutoMapper;
using Client.Blazor.DTOs;
using Microsoft.AspNetCore.Components;
using Shared;

namespace Client.Blazor.Components.Pages
{
    public partial class Chart : ComponentBase
    {
        [Inject]
        public IStudentService StudentService { get; set; } = null!;

        [Inject]
        public IClassService ClassService { get; set; } = null!;

        [Inject]
        public NotificationService Notification { get; set; } = null!;

        [Inject]
        public IMapper Mapper { get; set; } = null!;

        IChartComponent chart1 = null!;

        List<StudentAgeDTO> data1 = null!;
        List<ClassStudentCountDTO> data2 = null!;

        // config charts
        ColumnConfig config1 = null!;
        PieConfig config2 = null!;

        bool isFirstRender = true;

        List<ClassInfoDTO>? classes;

        async Task LoadClassesAsync()
        {
            var reply = await ClassService.GetAllClassesInfoAsync(new Shared.Empty());
            if (reply.Classes == null)
            {
                _ = Notification.Open(new NotificationConfig()
                {
                    Message = "Error",
                    Description = reply.Message,
                    NotificationType = NotificationType.Error
                });
            }
            else
            {
                classes = Mapper.Map<List<ClassInfoDTO>>(reply.Classes);
            }
        }

        async Task LoadStudentAgeClassAsync(int classId = -1)
        {
            var reply = await StudentService.GetStudentAgeChartAsync(new IdRequest { Id = classId });
            data1 = Mapper.Map<List<StudentAgeDTO>>(reply.ChartData);
            if (!isFirstRender)
            {
                await chart1.ChangeData(data1);
            }
        }

        async Task LoadClassChartAsync()
        {
            var classChartReply = await ClassService.GetClassChartAsync(new Shared.Empty());
            data2 = Mapper.Map<List<ClassStudentCountDTO>>(classChartReply.ChartData);
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
                AngleField = "studentPercentage",
                ColorField = "className",
                Label = new PieLabelConfig
                {
                    Visible = true,
                    Type = "outer",
                    Offset = 20,
                }
            };
        }

        protected override async Task OnInitializedAsync()
        {
            Config();
            await LoadClassesAsync();
            await LoadStudentAgeClassAsync();
            await LoadClassChartAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await chart1.ChangeData(data1);
            isFirstRender = false;
        }
    }
}