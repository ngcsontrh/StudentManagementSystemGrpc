using Client.Blazor.Components;
using Client.Blazor.Mappers;
using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using Shared;

namespace Client.Blazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string grpcAddress = builder.Configuration.GetConnectionString("gRPCAddress1")!;
            var handler = new SocketsHttpHandler
            {
                PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
                KeepAlivePingDelay = TimeSpan.FromSeconds(60),
                KeepAlivePingTimeout = TimeSpan.FromSeconds(30),
                EnableMultipleHttp2Connections = true
            };

            // Add services to the container.
            builder.Services.AddRazorComponents().AddInteractiveServerComponents();
                
            builder.Services.AddAntDesign();
            builder.Services.AddAutoMapper(typeof(StudentMapper));
            builder.Services.AddAutoMapper(typeof(ClassMapper));

            // Add DI grpc services
            builder.Services.AddScoped(provider =>
            {
                var channel = GrpcChannel.ForAddress(grpcAddress, new GrpcChannelOptions
                {
                    HttpHandler = handler
                });
                return channel.CreateGrpcService<IStudentService>();
            });

            builder.Services.AddScoped(provider =>
            {
                var channel = GrpcChannel.ForAddress(grpcAddress, new GrpcChannelOptions
                {
                    HttpHandler = handler
                });
                return channel.CreateGrpcService<IClassService>();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
