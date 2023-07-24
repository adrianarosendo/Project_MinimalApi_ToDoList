using Application.Abstractions;
using Application.ToDo.Commands;
using DataAccess.Repositories;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using ToDoList.Abstraction;
using Microsoft.OpenApi.Models;

namespace ToDoList.Extension
{
    public static class ToDoListExtension
    {
        public static void RegisterServices (this WebApplicationBuilder builder) {

            string? assemblyName = typeof(SocialDBContext).Namespace;

            var cs = builder.Configuration.GetConnectionString("Default");
            builder.Services.AddDbContext<SocialDBContext>(opt => opt.UseSqlServer(cs,
                optionsBuilder => optionsBuilder.MigrationsAssembly(assemblyName))

            );
            builder.Services.AddScoped<IToDoRepository, ToDoRepository>();
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateToDo).GetTypeInfo().Assembly));


            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "ToDo list",
                        Version = "v1",
                        Description = "Projeto Lista de Tarefas",
                       
                    });
            });



        }

        public static void RegisterEndpointDefinition(this WebApplication app)
        {
            var endpointDefinitions = typeof(Program).Assembly
                .GetTypes()
                .Where(t => t.IsAssignableTo(typeof(IEndpointDefinition)) && !t.IsAbstract && !t.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<IEndpointDefinition>();
            foreach (var endpointDef in endpointDefinitions)
            {
                endpointDef.RegisterEndpoints(app);
            }

        }
    }
}

