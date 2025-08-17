using Microsoft.Extensions.DependencyInjection;
using Oqtane.Services;
using GIBS.Module.Recipe.Services;

namespace GIBS.Module.Recipe.Startup
{
    public class ClientStartup : IClientStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IRecipeService, RecipeService>();
        }
    }
}
