using MinimalApiSample;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IRandomService, RandomService>();
var app = builder.Build();
app.Map("/", () => "Hello MinimalAPI");
app.Run();

public partial class Program {}
