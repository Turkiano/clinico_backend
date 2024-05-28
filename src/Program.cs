using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;
using sda_backend_teamwork.src.Controllers;
using sda_onsite_2_csharp_backend_teamwork;
using sda_onsite_2_csharp_backend_teamwork.src.Abstractions;
using sda_onsite_2_csharp_backend_teamwork.src.Controller;
using sda_onsite_2_csharp_backend_teamwork.src.Controllers;
using sda_onsite_2_csharp_backend_teamwork.src.Databases;
using sda_onsite_2_csharp_backend_teamwork.src.Enums;
using sda_onsite_2_csharp_backend_teamwork.src.Repositories;
using sda_onsite_2_csharp_backend_teamwork.src.services;
using sda_onsite_2_csharp_backend_teamwork.src.Services;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();// after the builder variable
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true); // this is to lowercase all letters


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
     options =>
    {
        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Description = "Bearer token authentication",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Scheme = "Bearer"
        }
        );

        options.OperationFilter<SecurityRequirementsOperationFilter>();
    }
);
builder.Services.AddControllers();


builder.Services.AddScoped<IUserService, UserService>(); //this is the built-in DI container for the Service
builder.Services.AddScoped<IUserRepository, UserRepository>(); //this is the built-in DI container for the Repository

builder.Services.AddScoped<IProductService, ProductService>(); //this is the built-in DI container for the Service
builder.Services.AddScoped<IProductRepository, ProductRepository>(); //this is the built-in DI container for the Repository

builder.Services.AddScoped<IOrderItemService, OrderItemService>(); //this is the built-in DI container for the Service
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>(); //this is the built-in DI container for the Repository
builder.Services.AddScoped<IOrderService, OrderServices>(); //this is the built-in DI container for the Service
builder.Services.AddScoped<IOrderRepository, OrderRepository>(); //this is the built-in DI container for the Repository

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>(); //this is the built-in DI container for the Service
builder.Services.AddScoped<ICategoryService, CategoryService>(); //this is the built-in DI container for the Repository


// builder.Services.AddScoped<ICustomerOrderService, CustomerOrderService>();
builder.Services.AddScoped<ICustomerOrderRepository, CustomerOrderRepository>();

// configuring DB
var _config = builder.Configuration;
var dataSourceBuilder = new NpgsqlDataSourceBuilder(@$"Host={_config["Db_Host"]};Username={_config["Db_Username"]};Database={_config["Db_Database"]};Password={_config["Db_Password"]}");
dataSourceBuilder.MapEnum<Role>();

var dataSource = dataSourceBuilder.Build();
builder.Services.AddDbContext<DatabaseContext>((options) =>
{
    options.UseNpgsql(dataSource).UseSnakeCaseNamingConvention();
});


AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt_Issuer"],
            ValidAudience = builder.Configuration["Jwt_Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt_SigningKey"]!))
        };
    });

// ADD Orign & CORS to allow the frontend to talk with this backend
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins(builder.Configuration["Cors_Origin"]!)
                          .AllowAnyHeader()
                            .AllowAnyMethod()
                            .SetIsOriginAllowed((host) => true)
                            .AllowCredentials();
                      });
});

var app = builder.Build();
app.MapControllers();// Should be added after the app variable



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins); //this is to invoke the Cors to access between back-end & front-end

app.UseAuthentication();
app.UseAuthorization();

app.Run();