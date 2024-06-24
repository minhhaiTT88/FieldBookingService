﻿using CommonLib;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using System.Text;
using FieldBookingService;
using FieldBookingService.Helper;
using DataAccess.Startups;
using Business.Startups;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var logger = NLogBuilder.ConfigureNLog("Config/nlog.config").GetCurrentClassLogger();

const string ConfigFolder = "Config";
var path = Path.Combine(Directory.GetCurrentDirectory(), ConfigFolder, "appsettings.json");

var config = new ConfigurationBuilder()
    //.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile(path, optional: true, reloadOnChange: true)
    .AddCommandLine(args)
    .Build();

Read_Config.Read_Config_Info(config);

// Cấu hình đọc cấu hình từ file appsettings.json
IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile(path, optional: true, reloadOnChange: true)
    .Build();

builder.Services.AddLogging(builder =>
{
    builder.AddConfiguration(configuration.GetSection("Logging"))
           .AddConsole()
           .AddDebug();
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

// Chạy load memory
builder.Services.AddSingleton<IHostedService, RunBackgroundService>();

builder.Services.Configure<FormOptions>(options =>
{
    options.ValueCountLimit = int.MaxValue; // 200 items max
    options.ValueLengthLimit = 1024 * 1024 * 100; // 100MB max len form data
    options.MultipartBoundaryLengthLimit = int.MaxValue;
    options.MultipartBodyLengthLimit = int.MaxValue;
    options.MultipartHeadersLengthLimit = int.MaxValue;
});

// If run iis
builder.Services.Configure<IISServerOptions>(options =>
{
    options.MaxRequestBodySize = int.MaxValue;
});

// If run Linux
builder.Services.Configure<KestrelServerOptions>(options =>
{
    options.Limits.MaxRequestBodySize = int.MaxValue; // if don't set default value is: 30 MB
    options.AddServerHeader = false;
});

// web socket
builder.Services.AddSingleton<IWebsocketHandler, WebsocketHandler>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = Config_Info.Jwt_Issuer,
        ValidAudience = Config_Info.Jwt_Issuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config_Info.Jwt_Key))
    };
});


// Cấu hình background worker dùng chung
DAStartup.ConfigureServices(builder.Services);
BLStartup.ConfigureServices(builder.Services);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), @"FileUpload")),
    RequestPath = new PathString("/FileUpload")
});

app.UseWebSockets(new WebSocketOptions()
{
    KeepAliveInterval = TimeSpan.FromSeconds(120),
});

app.UseHttpsRedirection();

app.UseCors(x => x
.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader());
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//var _HostUrl = configuration["HostUrl"] ?? "http://*:8008";
//app.Urls.Add(_HostUrl);
app.Run();