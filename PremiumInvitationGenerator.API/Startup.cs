namespace PremiumInvitationGenerator
{
    using CsvReader;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Microsoft.OpenApi.Models;
    using PremiumCalculator;
    using PremiumInvitationGenerator.API;
    using PremiumInvitationGenerator.API.ErrorHandling;
    using TextGenerator;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(typeof(IPremiumCalculator), typeof(PremiumCalculator));
            services.AddTransient(typeof(IPremiumCalculatorBuilder), typeof(PremiumCalculatorBuilder));
            services.AddTransient(typeof(ICsvReader), typeof(CsvReader));
            services.AddTransient(typeof(IInvitationGenerator), typeof(InvitationGenerator));
            services.AddTransient(typeof(IInvitationService), typeof(InvitationService));

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Renewal Invitation Generator API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddFile("Logs/PremiumInvitationGenerator-{Date}.txt");

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger - ui(HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Renewal Invitation Generator API V1");
            });

            app.UseRouting();

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
