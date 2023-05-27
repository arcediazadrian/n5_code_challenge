using BusinessLogic;
using Data;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace PermissionsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<IPermissionUnitOfWork, PermissionUnitOfWork>();
            builder.Services.AddScoped<IPermissionTypeService, PermissionTypeService>();
            builder.Services.AddScoped<IPermissionService, PermissionService>();
            builder.Services.AddDbContext<PermissionsContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServer")));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}