using ApiTesteKognit.Ioc;
using Serilog;
try
{
    var builder = WebApplication.CreateBuilder(args);

    var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .CreateLogger();
    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog(logger);

    builder.Services.AddControllers();
    builder.Services.AddInfrastructure(builder.Configuration);
    var app = builder.Build();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Kognit");
        c.RoutePrefix = "swagger";
    });

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Fatal error, hot terminated.");
}
