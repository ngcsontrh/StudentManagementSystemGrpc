using Grpc.Net.Client.Web;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Shared;
using ProtoBuf.Grpc.Client;
using Microsoft.AspNetCore.Components;
using static Client.Blazor.Pages.Weather;

namespace Client.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddAntDesign();
            builder.Services.AddSingleton(sp =>
            {
                var httpHandler = new HttpClientHandler();
                httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;

                var channel = GrpcChannel.ForAddress($"http://localhost:5058", new GrpcChannelOptions { HttpHandler = httpHandler });

                return channel.CreateGrpcService<IStudentService>();
            });

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}
