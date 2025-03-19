using Anamnese.API.ORM.Context;
using Microsoft.EntityFrameworkCore;
using Anamnese.API.ORM.Repository;
using Anamnese.API.ORM.Entity;
using Anamnese.API.Application.Services.Profissional;
using Anamnese.API.Application.Services.Token;
using Anamnese.API.Application.Services.Pacient;
using System;
using Scalar.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Anamnese.API.Application.Services.Report;
using Anamnese.API.Application.Services.ProfissionalAvailable;
using Anamnese.API.Application.Services.Appointment;
using Anamnese.API.Application.Services.Anotation;
using Anamnese.API.ORM.Seeders.PacientSeeder;
using Anamnese.API.ORM.Seeders.ProfissionalSeeder;
using Google.Apis.Auth.AspNetCore3;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddCors();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
  options.CheckConsentNeeded = context => false;
  options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddAuthentication(options =>
  {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
  })
  .AddCookie("Cookies")
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
  })
  .AddGoogleOpenIdConnect(googleOptions =>
  {
    googleOptions.Scope.Add("profile");
    googleOptions.SignInScheme = JwtBearerDefaults.AuthenticationScheme;
    googleOptions.Scope.Add("email");
    googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    googleOptions.CallbackPath = "/auth/callback/google";
    googleOptions.Events.OnTokenValidated = async context =>
    {
      var accessToken = context.TokenEndpointResponse.AccessToken;
      var idToken = context.TokenEndpointResponse.IdToken;
      var user = context.Principal;
    };

    googleOptions.Events.OnRemoteFailure = context =>
    {
      Console.WriteLine(context.Failure.Message);
      context.HandleResponse();
      return Task.CompletedTask;
    };
  });
builder.Services.AddAuthorization();




#region dependecyInjection
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<BaseRepository<ProfissionalModel>>();
builder.Services.AddScoped<BaseRepository<PacientModel>>();
builder.Services.AddScoped<BaseRepository<ReportModel>>();
builder.Services.AddScoped<BaseRepository<AnotationModel>>();
builder.Services.AddScoped<BaseRepository<ProfissionalAvailableModel>>();
builder.Services.AddScoped<BaseRepository<AppointmentModel>>();


builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IProfissionalAvailableService, ProfissionalAvailableService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IProfissionalService, ProfissionalService>();
builder.Services.AddScoped<IPacientService, PacientService>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IAnotationService, AnotationService>();
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


#region migrations and seeder
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AnamneseDbContext>();

    try
    {
        context.Database.Migrate();
        Console.WriteLine("Migrations aplicadas com sucesso.");

        PacientSeeder.SeedPacients(context);
        ProfissionalSeeder.SeedProfissionais(context);
        Console.WriteLine("Seeder aplicado com sucesso.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erro ao aplicar migra��es e/ou seeder:");
        Console.WriteLine(ex.Message);
    }
}

#endregion migrations and seeder

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AnamneseDbContext>();
    try
    {
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine("An error occurred while migrating the database:");
        Console.WriteLine(ex.Message);
    }
}
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
