using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MusicSite.Server.Data;
using MusicSite.Server.Options;
using MusicSite.Server.PipelineBehaviors;
using MusicSite.Server.Services;
using Serilog;
using System.Text;
using MusicSite.Server.Data.Interfaces;
using MusicSite.Server.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
var services = builder.Services;
//builder.Services.AddControllersWithViews();
//builder.Services.AddRazorPages();
services.AddControllers();
services.AddTransient(
    typeof(IPipelineBehavior<,>),
    typeof(ValidationBehavior<,>)
);

var myJwtOptions = new MyJwtOptions();
configuration.Bind(nameof(MyJwtOptions), myJwtOptions);
services.AddSingleton(myJwtOptions);

services.AddDbContext<MusicSiteServerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MusicSiteServerContext"))
);
services.AddScoped<IUnitOfWork, UnitOfWork>();

services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(x =>
    {
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                myJwtOptions.GetSecretInBytes()
            ),
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireExpirationTime = false,
            ValidateLifetime = true
        };
    });
services.AddAuthorization();

services.AddScoped<IAuthService, AuthService>();

services.AddSwaggerGen(x =>
{
    x.SwaggerDoc(
        "v1", 
        new OpenApiInfo 
        {
            Title = "MusicSite API",
            Version = "v1"
        }
    );
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme 
    { 
        Description = "JWT Auth header using bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
    x.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.UseAuthentication();

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

app.UseAuthorization();

//app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
