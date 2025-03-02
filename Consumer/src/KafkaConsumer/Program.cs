using KafkaConsumer;
using KafkaConsumer.DataAccess.Data;
using KafkaConsumer.DataAccess.Repository;
using KafkaConsumer.DataAccess.Repository.Implementation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:5000");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Services.AddSignalR();
builder.Services.AddHostedService<KafkaConsumerService>();

var app = builder.Build();

app.UseRouting();
app.UseCors("AllowFrontend");
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<KafkaHub>("/kafkaHub");
});

app.Run();
