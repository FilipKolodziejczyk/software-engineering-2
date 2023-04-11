using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SoftwareEngineering2;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Repositories;
using SoftwareEngineering2.Services;
using SoftwareEngineering2.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

if (Environment.GetEnvironmentVariable("USE_IN_MEMORY_DB") == "true") {
    builder.Services.AddDbContextPool<FlowerShopContext>(options => options.UseInMemoryDatabase("FlowerShop"));
} else {
    var connectionStringBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("Db"))
        {
            UserID = builder.Configuration["DbUser"],
            Password = builder.Configuration["DbPassword"]
        };
    var connectionString = connectionStringBuilder.ConnectionString;
    
    builder.Services.AddTransient<IDbConnection>(_ => new SqlConnection(connectionString));
    builder.Services.AddDbContextPool<FlowerShopContext>(options => options.UseSqlServer(connectionString));
}

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<ISampleModelRepository, SampleModelRepository>();
builder.Services.AddTransient<ISampleModelTypeRepository, SampleModelTypeRepository>();
builder.Services.AddTransient<ISampleService, SampleService>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<FlowerShopContext>();
if (Environment.GetEnvironmentVariable("CREATE_AND_DROP_DB") == "true") {
    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
}
else {
    dbContext.Database.Migrate();
}

app.ConfigureExceptionHandler(detailedErrors: app.Environment.IsDevelopment());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();