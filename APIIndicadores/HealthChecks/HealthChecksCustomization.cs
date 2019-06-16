using System;
using System.Linq;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;

namespace APIIndicadores.HealthChecks
{
    public static class HealthChecksCustomization
    {
        public static HealthCheckOptions GetJsonReturn(string healthCheckName)
        {
            return new HealthCheckOptions()
            {
                Predicate = (check) => check.Name == healthCheckName,
                ResponseWriter = async (context, report) =>
                {
                    var healthCheck = report.Entries.First();
                    var result = JsonConvert.SerializeObject(
                        new
                        {
                            HealthCheck = healthCheck.Key,
                            Status = Enum.GetName(typeof(HealthStatus), healthCheck.Value.Status)
                        });
                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    await context.Response.WriteAsync(result);
                }
            };
        }
    }
}