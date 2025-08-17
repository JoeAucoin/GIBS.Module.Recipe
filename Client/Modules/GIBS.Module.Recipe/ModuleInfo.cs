using Oqtane.Models;
using Oqtane.Modules;

namespace GIBS.Module.Recipe
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "Recipe",
            Description = "Recipe Module for Oqtane",
            Version = "1.0.0",
            ServerManagerType = "GIBS.Module.Recipe.Manager.RecipeManager, GIBS.Module.Recipe.Server.Oqtane",
            ReleaseVersions = "1.0.0",
            Dependencies = "GIBS.Module.Recipe.Shared.Oqtane",
            PackageName = "GIBS.Module.Recipe" 
        };
    }
}
