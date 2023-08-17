using Assignment03.EntityProviders;
using  Assignment03.HttpClientProviders;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Syncfusion.Blazor;

namespace Assignment03.BlazorWebApp;

public class Program
{
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddSyncfusionBlazor();
        builder.Services.AddHttpClientProviders(builder.Configuration);
        builder.Services.AddBlazoredSessionStorage();
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor();

        var app = builder.Build();

        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(builder.Configuration["SyncfusionKey"]);
        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment()) {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseRouting();

        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        app.Run();
    }
}