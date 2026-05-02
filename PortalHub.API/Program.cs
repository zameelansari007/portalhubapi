using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PortalHub.API.Common;
using PortalHub.Application.Common;
using PortalHub.Application.DTOs.Auth;
using PortalHub.Application.DTOs.Master;
using PortalHub.Application.DTOs.Portal;
using PortalHub.Application.Interfaces.Auth;
using PortalHub.Application.Interfaces.Portal;
using PortalHub.Application.Interfaces.Queries;
using PortalHub.Application.Interfaces.Repositories;
using PortalHub.Application.Services;
using PortalHub.Domain.Models.Master;
using PortalHub.Infrastructure;
using PortalHub.Infrastructure.Auth;
using PortalHub.Infrastructure.Dapper;
using PortalHub.Infrastructure.Dapper.Repositories;
using PortalHub.Infrastructure.EF.Repositories;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// DB
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// JWT Settings
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("Jwt"));

// DI
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>(); 
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped( typeof(IQueryRepository<>),typeof(DapperQueryRepository<>));
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddScoped<ICrudService<CreateSubscriptionPlanDto,UpdateSubscriptionPlanDto,SubscriptionPlanDto>,SubscriptionPlanService>();
builder.Services.AddScoped<ICrudService<CreateSupplierProfileDto,UpdateSupplierProfileDto,SupplierProfileResponseDto>,SupplierProfileService>();
builder.Services.AddScoped<ICrudService<CreateProductDto, UpdateProductDto, ProductResponseDto>,ProductService>();
builder.Services.AddScoped<ICrudService<CreateCategoryDto, UpdateCategoryDto, CategoryResponseDto>,CategoryService>();
builder.Services.AddScoped<ICrudService<CreateProductVariantDto, UpdateProductVariantDto, ProductVariantResponseDto>,ProductVariantService>();
builder.Services.AddScoped<ICrudService<CreateProductImageDto, UpdateProductImageDto, ProductImageResponseDto>, ProductImageService>();

builder.Services.AddScoped<ICatalogQueryRepository, CatalogQueryRepository>();

// automapper
builder.Services.AddAutoMapper(
    typeof(PortalHub.Application.Mappings.UserProfile).Assembly
);

//builder.Services.AddAutoMapper(cfg => { },
//    typeof(PortalHub.Application.Mappings.UserProfile).Assembly);

//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//builder.Services.AddAutoMapper(typeof(Program));

// AUTHENTICATION
var jwt = builder.Configuration
    .GetSection("Jwt")
    .Get<JwtSettings>();

//builder.Services
//    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,

//            ValidIssuer = jwt.Issuer,
//            ValidAudience = jwt.Audience,
//            IssuerSigningKey =
//                new SymmetricSecurityKey(
//                    Encoding.UTF8.GetBytes(jwt.SecretKey))
//        };
//    });

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwt.Issuer,
            ValidAudience = jwt.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey))
        };

        options.Events = new JwtBearerEvents
        {
            OnChallenge = async context =>
            {
                context.HandleResponse();
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";

                var response = new ApiResponse<object>
                {
                    Success = false,
                    Message = "Unauthorized",
                    Data = null,
                    Error = "401"
                };

                await context.Response.WriteAsJsonAsync(response);
            },
            OnForbidden = async context =>
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json";

                var response = new ApiResponse<object>
                {
                    Success = false,
                    Message = "Forbidden",
                    Data = null,
                    Error = "403"
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        };
    });




builder.Services.AddAuthorization();

//builder.Services.AddControllers();
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState
                .Where(x => x.Value.Errors.Count > 0)
                .ToDictionary(
                    x => x.Key,
                    x => x.Value.Errors.First().ErrorMessage);

            var response = new
            {
                success = false,
                message = "Validation Failed",
                errors
            };

            return new BadRequestObjectResult(response);
        };
    });
//builder.Services.AddOpenApi();
//builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.SupportNonNullableReferenceTypes();

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PortalHub API",
        Version = "v1"
    });

    // JWT Bearer auth definition
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by your JWT token.\nExample: Bearer eyJhbGciOiJIUzI1NiIs..."
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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





builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(typeof(PortalHub.Application.Services.AuthService).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //app.MapOpenApi();
    // curl https://localhost:7212/openapi/v1.json
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // Correct path for Swashbuckle
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PortalHub API v1");
        c.RoutePrefix = "swagger"; // UI available at /swagger
    });
}

app.UseHttpsRedirection();

app.UseMiddleware<IpBlockMiddleware>();

/* IMPORTANT ORDER */
app.UseMiddleware<ApiResponseMiddleware>();
app.UseAuthentication();   // MUST COME FIRST
app.UseAuthorization();    // THEN THIS

app.MapControllers();

app.Run();
