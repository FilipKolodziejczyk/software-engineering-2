using System.Configuration;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SoftwareEngineering2;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Repositories;
using ConfigurationManager = System.Configuration.ConfigurationManager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

var connectionString = Environment.GetEnvironmentVariable("CONNSTR");
if (string.IsNullOrEmpty(connectionString)) {
    connectionString = ConfigurationManager.ConnectionStrings["Db"].ConnectionString;
}

builder.Services.AddTransient<IDbConnection>(_ => new SqlConnection(connectionString));
builder.Services.AddDbContext<FlowerShopContext>(
    options => options.UseSqlServer(connectionString));


builder.Services.AddTransient<ISampleModelRepository, SampleModelRepository>();
builder.Services.AddTransient<ISampleModelTypeRepository, SampleModelTypeRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();