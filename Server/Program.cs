using Client.Blazor.Repositories;
using NHibernate;
using ProtoBuf.Grpc.Server;
using Server.Repositories;
using Server.Repositories.Interfaces;
using Server.Services;
using Shared;
using ISession = NHibernate.ISession;

namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string connectionString = builder.Configuration.GetConnectionString("Default")!;

            // Add services to the container.
            builder.Services.AddSingleton<NHibernateHelper>(provider => new NHibernateHelper(connectionString));
            builder.Services.AddScoped(provider => provider.GetService<NHibernateHelper>()!.OpenSession());

            // Add repositories
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<IClassRepository, ClassRepository>();
            builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();

            // Add gRPC services
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<IClassService, ClassService>();

            builder.Services.AddCodeFirstGrpc();
            builder.Services.AddGrpcReflection();
            
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.MapGrpcReflectionService();
            }
            
            // Configure the HTTP request pipeline.
            app.MapGrpcService<StudentService>();
            app.MapGrpcService<ClassService>();

            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}
