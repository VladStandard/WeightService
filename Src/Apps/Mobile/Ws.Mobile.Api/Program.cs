var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

WebApplication app = builder.Build();

app.UseHttpsRedirection();

app.Run();