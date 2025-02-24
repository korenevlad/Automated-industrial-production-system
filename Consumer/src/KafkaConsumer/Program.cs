using KafkaConsumer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddSignalR();
builder.Services.AddHostedService<KafkaConsumerService>();

var app = builder.Build();

app.UseCors("AllowAllOrigins");

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<KafkaHub>("/kafkaHub");
});

app.Run();
