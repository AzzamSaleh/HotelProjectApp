using HotelProjectAPI.Data;
using HotelProjectAPI.MappingProfiles;
using HotelProjectAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the IoC container.

// Configure DbContext with SQL Server connection string from configuration
var connectionString = builder.Configuration.GetConnectionString("HotelProjectDbConnectionString");

builder.Services.AddDbContext<HotelProjectDbContext>(options =>
    options.UseSqlServer(connectionString));



builder.Services.AddScoped<ICountriesService,CountriesService>();
builder.Services.AddScoped<IHotelsService,HotelsService>();

builder.Services.AddAutoMapper(cfg  =>
{
    cfg.AddProfile<HotelMappingProfile>();
    cfg.AddProfile<CountryMappingProfile>();
});





builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    } );
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
