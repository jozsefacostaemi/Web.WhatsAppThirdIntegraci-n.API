using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Web.Core.Business.API.Domain.Interfaces;
using Web.Core.Business.API.Infraestructure.Persistence.Repositories.EmitMessage;
using Web.WhatsAppThirdIntegración.API.Domain.Interfaces;
using Web.WhatsAppThirdIntegración.API.Infraestructure.Persistence.Entities;
using Web.WhatsAppThirdIntegración.API.Infraestructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Se agrega servicio de Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "API EMI Demo WhatsApp", Version = "v1" });
});
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));
builder.Services.AddScoped<ApplicationDbContext>();
builder.Services.AddScoped<IPatientsRepository, PatientsRepository>();
builder.Services.AddScoped<IAppoinmentsRepository, AppoinmentsRepository>();
var app = builder.Build();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "API EMI Demo v1");
    options.RoutePrefix = "swagger";
});

app.MapControllers();

app.Run();
