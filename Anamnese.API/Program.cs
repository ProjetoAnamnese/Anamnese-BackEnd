using Anamnese.API.ORM.Context;
using Microsoft.EntityFrameworkCore;
using Anamnese.API.ORM.Repository;
using Anamnese.API.ORM.Entity;
using Anamnese.API.Application.Services.Profissional;
using Anamnese.API.Application.Services.Token;
using Anamnese.API.Application.Services.Pacient;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Anamnese.API.Application.Services.Report;
using System.ComponentModel.Design;
using Anamnese.API.Application.Services.Referral;
using Anamnese.API.Application.Services.ProfissionalAvailable;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
builder.Services.AddAuthorization();




#region dependecyInjection
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<BaseRepository<ProfissionalModel>>();
builder.Services.AddScoped<BaseRepository<PacientModel>>();
builder.Services.AddScoped<BaseRepository<ReportModel>>();
//builder.Services.AddScoped<BaseRepository<ReferralModel>>();
builder.Services.AddScoped<BaseRepository<ProfissionalAvailableModel>>();
builder.Services.AddScoped<BaseRepository<SpecialityModel>>();
builder.Services.AddScoped<BaseRepository<AppointmentModel>>();


builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IProfissionalAvailableService, ProfissionalAvailableService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IProfissionalService, ProfissionalService>();
builder.Services.AddScoped<IPacientService, PacientService>();
//builder.Services.AddScoped<IReferralService, ReferralService>();
#endregion dependecyInjection

#region mysqlconfig
builder.Services.AddDbContext<AnamneseDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var serverVersion = ServerVersion.AutoDetect(connectionString);
    options.UseMySql(connectionString, serverVersion);
});
#endregion mysqlconfig

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "AnamneseAPI");
        c.RoutePrefix = "swagger";
    });
}
app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
