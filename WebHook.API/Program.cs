using WebHook.API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IReceiveWebhook, ConsoleWebhookReceiver>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/webhook", async (HttpContext context, IReceiveWebhook receive) =>
{
    using var stream = new StreamReader(context.Request.Body);
    return await receive.ProcessRequest(await stream.ReadToEndAsync());
});

app.Run();

/// <summary>
/// This is just a requirement for ASP .NET Core and testing this service
/// </summary>
public partial class Program { }