using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Oqtane.Modules;
using Oqtane.Models;
using Oqtane.Infrastructure;
using Oqtane.Interfaces;
using Oqtane.Enums;
using Oqtane.Repository;
using GIBS.Module.Recipe.Repository;
using System.Threading.Tasks;

namespace GIBS.Module.Recipe.Manager
{
    public class RecipeManager : MigratableModuleBase, IInstallable, IPortable, ISearchable
    {
        private readonly IRecipeRepository _RecipeRepository;
        private readonly IDBContextDependencies _DBContextDependencies;

        public RecipeManager(IRecipeRepository RecipeRepository, IDBContextDependencies DBContextDependencies)
        {
            _RecipeRepository = RecipeRepository;
            _DBContextDependencies = DBContextDependencies;
        }

        public bool Install(Tenant tenant, string version)
        {
            return Migrate(new RecipeContext(_DBContextDependencies), tenant, MigrationType.Up);
        }

        public bool Uninstall(Tenant tenant)
        {
            return Migrate(new RecipeContext(_DBContextDependencies), tenant, MigrationType.Down);
        }

        public string ExportModule(Oqtane.Models.Module module)
        {
            string content = "";
            List<Models.Recipe> Recipes = _RecipeRepository.GetRecipes(module.ModuleId).ToList();
            if (Recipes != null)
            {
                content = JsonSerializer.Serialize(Recipes);
            }
            return content;
        }

        public void ImportModule(Oqtane.Models.Module module, string content, string version)
        {
            List<Models.Recipe> Recipes = null;
            if (!string.IsNullOrEmpty(content))
            {
                Recipes = JsonSerializer.Deserialize<List<Models.Recipe>>(content);
            }
            if (Recipes != null)
            {
                foreach(var Recipe in Recipes)
                {
                    _RecipeRepository.AddRecipe(new Models.Recipe { ModuleId = module.ModuleId, Name = Recipe.Name });
                }
            }
        }

        public Task<List<SearchContent>> GetSearchContentsAsync(PageModule pageModule, DateTime lastIndexedOn)
        {
           var searchContentList = new List<SearchContent>();

           foreach (var Recipe in _RecipeRepository.GetRecipes(pageModule.ModuleId))
           {
               if (Recipe.ModifiedOn >= lastIndexedOn)
               {
                   searchContentList.Add(new SearchContent
                   {
                       EntityName = "GIBSRecipe",
                       EntityId = Recipe.RecipeId.ToString(),
                       Title = Recipe.Name,
                       Body = Recipe.Name,
                       ContentModifiedBy = Recipe.ModifiedBy,
                       ContentModifiedOn = Recipe.ModifiedOn
                   });
               }
           }

           return Task.FromResult(searchContentList);
        }
    }
}
