using Client.Blazor.Mappers;
using Client.ConsoleApp.Controllers;
using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection;
using ProtoBuf.Grpc.Client;
using Shared;

namespace Client.ConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddScoped(provider =>
                {
                    var channel = GrpcChannel.ForAddress("https://localhost:7050");
                    return channel.CreateGrpcService<IStudentService>();
                })
                .AddScoped(provider =>
                {
                    var channel = GrpcChannel.ForAddress("https://localhost:7050");
                    return channel.CreateGrpcService<IClassService>();
                })
                .AddTransient<StudentController>()
                .AddAutoMapper(typeof(StudentMapper))
                .AddAutoMapper(typeof (ClassMapper))
                .BuildServiceProvider();

            var studentController = serviceProvider.GetService<StudentController>()!;

            await studentController.Index();
        }
    }
}
