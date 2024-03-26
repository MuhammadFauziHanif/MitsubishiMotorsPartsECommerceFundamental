using Microsoft.EntityFrameworkCore;
using MitsubishiMotorsPartsECommerce.RESTServices.BLL;
using MitsubishiMotorsPartsECommerce.Data;
using MitsubishiMotorsPartsECommerce.Data.Interfaces;
using MitsubishiMotorsPartsECommerce.RESTServices.BLL.Interfaces;
using FluentValidation;
using MitsubishiMotorsPartsECommerce.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MitsubishiMotorsPartsECommerce.Helpers;
using System.Text;
using Microsoft.OpenApi.Models;
using MitsubishiMotorsPartsECommerce.DAL.MitsubishiMotorsPartsECommerce.DAL;
using MitsubishiMotorsPartsECommerce.BLL.DTOs.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mitsubishi Motors Parts E-Commerce Platform REST API", Version = "v1" });

    // Define security scheme for bearer token
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "JWT Authorization header using the Bearer scheme.",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    };
    c.AddSecurityDefinition("Bearer", securityScheme);

    // Add security requirement for endpoints that need authorization
    var securityRequirement = new OpenApiSecurityRequirement
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
        };
    c.AddSecurityRequirement(securityRequirement);
});

//appdbcontext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDbConnectionString"));
});

//DI
builder.Services.AddScoped<ICategoryData, CategoryData>();
builder.Services.AddScoped<ICategoryBLL, CategoryBLL>();
builder.Services.AddScoped<IProductData, ProductData>();
builder.Services.AddScoped<IProductBLL, ProductBLL>();
builder.Services.AddScoped<IAdminDAL, AdminDAL>();
builder.Services.AddScoped<MitsubishiMotorsPartsECommerce.BLL.Interfaces.IAdminBLL, MitsubishiMotorsPartsECommerce.BLL.AdminBLL>();
builder.Services.AddScoped<MitsubishiMotorsPartsECommerce.Interface.ICustomer, CustomerDAL>();
builder.Services.AddScoped<MitsubishiMotorsPartsECommerce.BLL.Interfaces.ICustomerBLL, MitsubishiMotorsPartsECommerce.BLL.CustomerBLL>();
builder.Services.AddScoped<MitsubishiMotorsPartsECommerce.Interface.ISalesOrderDAL, SalesOrderDAL>();
builder.Services.AddScoped<MitsubishiMotorsPartsECommerce.BLL.Interfaces.ISalesOrderBLL, MitsubishiMotorsPartsECommerce.BLL.SalesOrderBLL>();

//automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//add validator
builder.Services.AddValidatorsFromAssemblyContaining<CategoryCreateDTOValidator>();

var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);
var appSettings = appSettingsSection.Get<AppSettings>();
var key = Encoding.ASCII.GetBytes(appSettings.Secret);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
