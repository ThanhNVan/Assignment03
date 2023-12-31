using Assignment03.DataProviders;
using Assignment03.EntityProviders;
using Assignment03.LogicProviders;
using Assignment03.WebApiProviders;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Assignment03.WebApiHost;

public class Program
{
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);


        // Add services to the container.
        builder.Services.AddSqlServerProviders(builder.Configuration);
        builder.Services.AddDataProviders();
        builder.Services.AddLogicProviders();
        builder.Services.AddAuthenticationProviders(builder.Configuration);

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}