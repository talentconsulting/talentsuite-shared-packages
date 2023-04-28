using TalentConsulting.TalentSuite.ApiHelpers.Healthchecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks().AddCheck<InfoHealthCheckAsync>("Info", tags: new[] { "Info health check" });
builder.Services.AddHealthChecks().AddCheck<ReadinessHealthCheckAsync>("Readiness", tags: new[] { "Readiness heath check" });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/health", new HealthCheckOptions()
    {
        ResponseWriter = HealthCheckExtensions.WriteResponse
    });

    endpoints.MapHealthChecks("/health/info", new HealthCheckOptions()
    {
        Predicate = p => p.Tags.Any(t => t == "Info"),
        ResponseWriter = HealthCheckExtensions.WriteResponse
    });

    endpoints.MapHealthChecks("/health/readiness", new HealthCheckOptions()
    {
        Predicate = p => p.Tags.Any(t => t == "Readiness"),
        ResponseWriter = HealthCheckExtensions.WriteResponse
    });
});

app.Run();