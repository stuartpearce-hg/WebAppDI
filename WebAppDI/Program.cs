using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using CasCap;
using CasCap.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Add Aspire services
builder.AddServiceDefaults();
builder.AddAspireDashboard();

// Configure OpenTelemetry
builder.Services.ConfigureOpenTelemetryMeterProvider(metrics => metrics
    .AddAspireMeter()
    .AddMeter("WebAppDI"));

builder.Services.ConfigureOpenTelemetryTracerProvider(tracing => tracing
    .AddAspireTracing()
    .AddSource("WebAppDI"));

// Add services to the container
builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation()
    .AddApplicationPart(typeof(StringsController).Assembly); // Register controllers from WebAppDILib
builder.Services.AddScoped<IDITestService, DITestService>();

// Enable API endpoints
builder.Services.AddEndpointsApiExplorer();

// Configure static files
builder.Services.Configure<StaticFileOptions>(options =>
{
    options.ServeUnknownFileTypes = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Map Aspire endpoints first
app.MapDefaultEndpoints(); // Aspire health checks and metrics

// Map application endpoints
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllers(); // For attribute-based routing in API controllers

app.Run();
