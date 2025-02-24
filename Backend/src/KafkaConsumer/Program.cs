using KafkaConsumer;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();

builder.Services.AddSingleton<KafkaHub>();
builder.Services.AddHostedService<KafkaConsumerService>();

var app = builder.Build();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<KafkaHub>("/kafkaHub");
});

await app.RunAsync();