using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SoftwareEngineering2;
using SoftwareEngineering2.Interfaces;
using SoftwareEngineering2.Repositories;
using SoftwareEngineering2.Services;
using SoftwareEngineering2.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
    policy =>
    {
        var frontendClientUrl = Environment.GetEnvironmentVariable("FRONTEND_CLIENT_URL");
        var frontendShopUrl = Environment.GetEnvironmentVariable("FRONTEND_SHOP_URL");
        var frontendDeliveryUrl = Environment.GetEnvironmentVariable("FRONTEND_DELIVERY_URL");
        var origins = new List<string>();
        if (!string.IsNullOrEmpty(frontendClientUrl)) {
            origins.Add(frontendClientUrl);
        }
        if (!string.IsNullOrEmpty(frontendShopUrl)) {
            origins.Add(frontendShopUrl);
        }
        if (!string.IsNullOrEmpty(frontendDeliveryUrl)) {
            origins.Add(frontendDeliveryUrl);
        }
        policy.WithOrigins(origins.ToArray());
    });
});

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement() { {
        new OpenApiSecurityScheme {
            Reference = new OpenApiReference {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            },
            Scheme = "oauth2",
            Name = "Bearer",
            In = ParameterLocation.Header,
        },
        new List<string>()
    } });
});

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

if (Environment.GetEnvironmentVariable("USE_IN_MEMORY_DB") == "true") {
    builder.Services.AddDbContextPool<FlowerShopContext>(options => options.UseInMemoryDatabase("FlowerShop"));
} else {
    var connectionStringBuilder = new SqlConnectionStringBuilder(Environment.GetEnvironmentVariable("CONNSTR"))
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

builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IClientModelRepository, ClientModelRepository>();
builder.Services.AddTransient<IEmployeeModelRepository, EmployeeModelRepository>();
builder.Services.AddTransient<IDeliveryManModelRepository, DeliveryManModelRepository>();
builder.Services.AddTransient<IAddressModelRepository, AddressModelRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IProductService, ProductService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "your-issuer",
            ValidAudience = "your-audience",
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("your-secret-key has to be long enough"))
        };
    });

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
    Console.WriteLine("Creating and dropping database");
    dbContext.Database.EnsureDeleted();
    dbContext.Database.EnsureCreated();
} else {
    dbContext.Database.Migrate();
}

app.UseCors(MyAllowSpecificOrigins);

app.ConfigureExceptionHandler(detailedErrors: app.Environment.IsDevelopment());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
