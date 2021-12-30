using MediatR;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MusicSite.Server.Data;
using MusicSite.Server.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.Swagger;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<MusicSiteServerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MusicSiteServerContext"))
);

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc(
        "v1", 
        new OpenApiInfo 
        {
            Title = "MusicSite API",
            Version = "v1"
        }
    );
});

builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();

    var swaggerOptions = new MySwaggerOptions();
    configuration
        .GetSection(nameof(MySwaggerOptions))
        .Bind(swaggerOptions);
    app.UseSwagger(options => 
    {
        options.RouteTemplate = swaggerOptions.JsonRoute;
    });

    app.UseSwaggerUI(options => 
    {
        options.SwaggerEndpoint(swaggerOptions.UIEndpoint, swaggerOptions.Description);
    });
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
