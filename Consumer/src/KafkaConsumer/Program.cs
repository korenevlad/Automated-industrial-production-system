using KafkaConsumer;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:5000");

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
