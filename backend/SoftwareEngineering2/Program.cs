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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var connectionStringBuilder = new SqlConnectionStringBuilder(
    builder.Configuration.GetConnectionString("Db"));
connectionStringBuilder.UserID = builder.Configuration["DbUser"];
connectionStringBuilder.Password = builder.Configuration["DbPassword"];
var connectionString = connectionStringBuilder.ConnectionString;

builder.Services.AddTransient<IDbConnection>(_ => new SqlConnection(connectionString));
builder.Services.AddDbContextPool<FlowerShopContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<ISampleModelRepository, SampleModelRepository>();
builder.Services.AddTransient<ISampleModelTypeRepository, SampleModelTypeRepository>();

builder.Services.AddControllers();

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