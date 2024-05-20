using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WebApplication1.Data;
using WebApplication1.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<AppDBContext>(Options =>
Options.UseSqlServer(builder.Configuration.GetConnectionString("AppConnectionString")));

builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IJobRepository, JobRepository>();

builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://localhost:5001",
            ValidAudience = "https://localhost:5001",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@830gggghhhhhhhhhhhhhhrtert8785fghgvjk09978"))
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddSwaggerGen(c => {
c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });   
    // Configure Bearer token authentication                                                                          
    var securityScheme = new OpenApiSecurityScheme {       
        Name = "Authorization",  
        Type = SecuritySchemeType.Http,   
        Scheme = "Bearer",       
        BearerFormat = "JWT",      
        In = ParameterLocation.Header,  
        Description = "Enter 'Bearer' [space] and then your token in the text input below.\n\nExample: 'Bearer 12345abcdef'", };
    c.AddSecurityDefinition("Bearer", securityScheme); var securityRequirement = new OpenApiSecurityRequirement { { new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } }, Array.Empty<string>() } }; c.AddSecurityRequirement(securityRequirement);
});
var app = builder.Build();

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
