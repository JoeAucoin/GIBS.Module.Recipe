using Microsoft.AspNetCore.Builder; 
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Oqtane.Infrastructure;
using GIBS.Module.Recipe.Repository;
using GIBS.Module.Recipe.Services;
using System.Text.Json.Serialization;

namespace GIBS.Module.Recipe.Startup
{
    public class ServerStartup : IServerStartup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // not implemented
        }

        public void ConfigureMvc(IMvcBuilder mvcBuilder)
        {
            // not implemented
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IRecipeService, ServerRecipeService>();
            services.AddTransient<IRecipeRepository, RecipeRepository>(); // Add this line
            services.AddDbContextFactory<RecipeContext>(opt => { }, ServiceLifetime.Transient);
        }
    }
}