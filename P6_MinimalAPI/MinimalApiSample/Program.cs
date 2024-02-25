using System.Diagnostics;
using MinimalApiSample;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IRandomService, RandomService>();
var app = builder.Build();
app.Map("/", () => "Hello MinimalAPI");
app.Map("/activityId", () =>
{
    using var activitySource = new ActivitySource("MinimalApiSample");
    using var activity = activitySource.StartActivity();
    return activity?.Id ?? string.Empty;
});
app.Run();

public partial class Program {}
