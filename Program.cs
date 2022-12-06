var builder = WebApplication.CreateBuilder(args);


builder.Configuration.AddKeyPerFile("/etc/sampleapi", true, true);

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
