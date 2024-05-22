using Anamnese.API.ORM.Context;
using Anamnese.API.ORM.Entity;
using Anamnese.API.ORM.Seeders.PacientSeeder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao contêiner.
builder.Services.AddControllers();
builder.Services.AddCors();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "AnamneseAPI", Version = "v1" });
});

builder.Services.AddDbContext<AnamneseDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

var app = builder.Build();

// Criar e aplicar migrações
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
        Console.WriteLine("Seeder aplicado com sucesso.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Erro ao aplicar migrações e/ou seeder:");
        Console.WriteLine(ex.Message);
    }
}

#endregion migrations and seeder
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
app.UseAuthorization();
app.MapControllers();

app.Run();
