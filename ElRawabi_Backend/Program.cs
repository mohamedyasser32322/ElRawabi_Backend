using Microsoft.OpenApi.Models;
using ElRawabi_Backend.Data;
using ElRawabi_Backend.Dtos;
using ElRawabi_Backend.Helpers;
using ElRawabi_Backend.Repository.Implementation;
using ElRawabi_Backend.Repository.Interfaces;
using ElRawabi_Backend.Services.Implmentation;
using ElRawabi_Backend.Services.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter JWT Bearer token **only**",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
//Database Connection
builder.Services.AddDbContext<ElRawabiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//Auto Mapper Dtos
builder.Services.AddAutoMapper(cfg => {
    cfg.AddMaps(typeof(Program));
});

//Jwt 
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => {
    var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()    
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});
// Helpers Scoped
builder.Services.AddScoped<PasswordHelper>();
builder.Services.AddScoped<JwtHelper>();

// Repos & Services Scope
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<IProjectRepository,ProjectRepository>();
builder.Services.AddScoped<IBuildingRepository,BuildingRepository>();
builder.Services.AddScoped<IApartmentRepository,ApartmentRepository>();
builder.Services.AddScoped<IBuildingImgRepository, BuildingImgRepository>();
builder.Services.AddScoped<IBuildingTimeLineRepository,BuildingTimeLineRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IBuildingService, BuildingService>();
builder.Services.AddScoped<IApartmentService, ApartmentService>();
builder.Services.AddScoped<IBuildingTimeLineService, BuildingTimeLineService>();
builder.Services.AddScoped<IClientDashboardService, ClientDashboardService>();
builder.Services.AddScoped<IEmailService, EmailService>();

var app = builder.Build();

app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();