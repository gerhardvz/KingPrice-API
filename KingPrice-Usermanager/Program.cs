using System.Reflection;
using System.Text.Json.Serialization;
using KingPrice_Usermanager.Context;
using KingPrice_Usermanager.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Services.AddLogging(logging => logging.AddConsole());
// Add services to the container.
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "KingPrice - Demo Project",
        Description = "Gerhard van Zyl Technical Skills Demo Project for KingPrice",
        Contact = new OpenApiContact
        {
            Name = "Gerhard van Zyl",
            Email = "gerhard@schoongezicht.com"
        }
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});
builder.Services.AddControllers().AddJsonOptions(option =>
{
    option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
var dbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var dataSourceBuilder = new NpgsqlDataSourceBuilder(dbConnectionString);
var dataSource = dataSourceBuilder.Build();
builder.Services.AddDbContext<UserDbContext>(options => { options.UseNpgsql(dataSource, option => { }); });

builder.Services.AddScoped<IUserManagementService, UserManagementService>();
builder.Services.AddScoped<IGroupManagementService, GroupManagementService>();
builder.Services.AddScoped<IPermissionManagementService, PermissionManagementService>();
builder.Services.AddScoped<IStatsService, StatsService>();

var app = builder.Build();
var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
Console.WriteLine(Path.Combine(AppContext.BaseDirectory, xmlFilename));
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x =>
{
    x.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});
app.UseRouting();

app.UseAuthorization();
app.MapControllers();


app.Run();