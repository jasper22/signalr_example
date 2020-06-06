using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebServer.Controllers;

namespace WebServer
{
    /// <summary>
    /// <c>Startup</c>
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // CORS must be defined - otherwise SignalR won't receive an request
            services.AddCors((setup) =>
            {
                setup.AddPolicy("CorsPolicy", builder => builder
                                                            .WithOrigins("http://localhost:4200")       // There's a strange situation with .AllowAnyOrigin() and .AllowCredentials() - they don't work together
                                                            .AllowAnyMethod()                           // Angular send credentials as 'included' so it rejected by ASP .NET Core if we don;t define .AllowCredentials()
                                                            .AllowAnyHeader()
                                                            .AllowCredentials());
            });

            // Add SignalR
            services.AddSignalR((hubOptions) =>
            {
                hubOptions.EnableDetailedErrors = true;

            });

            // Don't need to add my Hub implementation to some kinda repostiory
            // services.AddSingleton<ISignalRCommands, SignalRHub>();

            services.AddApplicationInsightsTelemetry();
        }

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();  // Everything is gonna be redirected to HTTPS

            app.UseCors("CorsPolicy");

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // Endpoint will be https://localhost:5001/hub
                endpoints.MapHub<SignalRHub>("/hub");

                // Please not at Controller that this endpoin defined as https://localhost:5001/api/SignalR/
                endpoints.MapControllers();
            });
        }
    }
}
