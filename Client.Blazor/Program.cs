using Client.Blazor.Components;
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

            // Add services to the container.
            builder.Services.AddRazorComponents().AddInteractiveServerComponents();
                
            builder.Services.AddAntDesign();

            AddGrpcService<IStudentService>(builder);
            AddGrpcService<IClassService>(builder);

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

        private static void AddGrpcService<T>(WebApplicationBuilder builder) where T : class
        {
            string grpcChannel = builder.Configuration.GetConnectionString("GrpcChannel")!;

            builder.Services.AddScoped(provider =>
            {
                var channel = GrpcChannel.ForAddress(grpcChannel);
                return channel.CreateGrpcService<T>();
            });
        }
    }
}
