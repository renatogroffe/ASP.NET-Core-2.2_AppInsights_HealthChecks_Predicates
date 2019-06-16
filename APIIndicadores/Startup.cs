using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using APIIndicadores.Data;
using APIIndicadores.HealthChecks;

namespace APIIndicadores
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configurando o uso do Entity Framework Core
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("BaseIndicadores")));

            // Ativando o uso de cache via Redis
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration =
                    Configuration.GetConnectionString("CacheRedis");
                options.InstanceName = "Cache-APIIndicadores-";
            });

            // Verificando a disponibilidade dos bancos de dados
            // da aplicação através de Health Checks
            services.AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>(name: "baseSql")
                .AddRedis(Configuration.GetConnectionString("CacheRedis"), name: "cacheRedis");

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Ativando o Application Insights
            services.AddApplicationInsightsTelemetry(Configuration);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            // Ativando o middlweare de Health Check
            app.UseHealthChecks("/status");
            app.UseHealthChecks("/status-baseSql",
                HealthChecksCustomization.GetJsonReturn("baseSql"));              
            app.UseHealthChecks("/status-cacheRedis",
                HealthChecksCustomization.GetJsonReturn("cacheRedis"));              

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}