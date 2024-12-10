using ContractManager;
using ContractManager.API;
using ContractManager.Application;
using ContractManager.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

{
    var root = Directory.GetCurrentDirectory();
    var dotenv = Path.Combine(root, ".env");
    DotEnv.Load(dotenv);
    builder.Configuration.AddEnvironmentVariables();
    
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);

    builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
}

var app = builder.Build();
{
    app.UseExceptionHandler();
    app.UseInfrastructure();
    
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.MapControllers();

    app.Run();   
}