using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json;
using WeatherRequest.Api.Web;
using WeatherRequest.Infraestructure.Contexts;
using WeatherRequest.Infraestructure.Entities.Api;
using WeatherRequest.MiddleWare;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container. 

builder.Services.AddDbContext<WrContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection"));
});

builder.Services.AddAutoMapper(typeof(Program));

IoC.AddServices(builder.Services, builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin",
        builder => builder.WithOrigins("*")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Weather Request",
        Version = "v1",
        Description = "Datos Meteorològicos por ciudad"
    });
     
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

var dataPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");
var cityListPath = Path.Combine(dataPath, "city.list.json");

var cities = File.Exists(cityListPath)
           ? JsonSerializer.Deserialize<List<CityJson.Root>>(File.ReadAllText(cityListPath))
           : new List<CityJson.Root>();

using (var environment = app.Services.CreateScope())
{
    var services = environment.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<WrContext>();
        await context.Database.MigrateAsync();
        await LoadDb.InsertCities(context, cities);
    }
    catch (Exception e)
    {
        var logging = services.GetRequiredService<ILogger<Program>>();
        logging.LogError(e, "Ocurrio un error en la migración");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowOrigin");

app.UseMiddleware<ManagerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
