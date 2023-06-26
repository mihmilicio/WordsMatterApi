using DinamicaApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader();
        policy.WithOrigins("https://localhost:3000", "https://<domainname>",
            $"https://{builder.Configuration["Auth0:Domain"]}");
        policy.AllowAnyMethod();
        policy.AllowCredentials();
    });
});

builder.Services.AddControllers();

builder.Services.AddDbContext<DinamicaContext>(opt =>
    opt.UseSqlite("Data Source=Dinamica.db"));

builder.Services.AddHttpClient<INuvemService, NuvemService>();
builder.Services.AddSingleton<RabbitMQProducer>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
