var builder = WebApplication.CreateBuilder(args);

const string varName = "DOTNET_RUNNING_IN_CONTAINER";
var runningInContainer = bool.TryParse(Environment.GetEnvironmentVariable(varName),
    out var isRunningInContainer)
    && isRunningInContainer;

var configFolder = builder.Configuration.GetValue<string>("ConfigurationFolder");

if (runningInContainer &&
    !string.IsNullOrWhiteSpace(configFolder) &&
    Directory.Exists(configFolder))
{
    builder.Configuration.AddKeyPerFile(configFolder, false, true);
}
else
{
    Console.WriteLine($"ConfigurationFolder set to: '{configFolder}'.");
    Console.WriteLine($"ConfigurationFolder exists: {Directory.Exists(configFolder)}");
    Console.WriteLine($"Running in Container: '{runningInContainer}'.");
    
    Console.WriteLine("Relying on default configuration");
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

Console.WriteLine("Dumping actual configuration");
app.Configuration.AsEnumerable().ToList().ForEach(f=>{
    Console.WriteLine($"- {f.Key}: {f.Value}");
});
Console.WriteLine("END Dumping actual configuration");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();
app.MapControllers();
app.Run();
