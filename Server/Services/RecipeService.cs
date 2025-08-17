using GIBS.Module.Recipe.Repository;
using Microsoft.AspNetCore.Http;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Oqtane.Models;
using Oqtane.Security;
using Oqtane.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GIBS.Module.Recipe.Models;

namespace GIBS.Module.Recipe.Services
{
    public class ServerRecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IUserPermissions _userPermissions;
        private readonly ILogManager _logger;
        private readonly IHttpContextAccessor _accessor;
        private readonly Alias _alias;

        public ServerRecipeService(IRecipeRepository recipeRepository, IUserPermissions userPermissions, ITenantManager tenantManager, ILogManager logger, IHttpContextAccessor accessor)
        {
            _recipeRepository = recipeRepository;
            _userPermissions = userPermissions;
            _logger = logger;
            _accessor = accessor;
            _alias = tenantManager.GetAlias();
        }

        public Task<List<Models.Recipe>> GetRecipesAsync(int moduleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, moduleId, PermissionNames.View))
            {
                return Task.FromResult(_recipeRepository.GetRecipes(moduleId).ToList());
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Recipe Get Attempt {ModuleId}", moduleId);
                return Task.FromResult<List<Models.Recipe>>(null);
            }
        }

        public Task<Models.Recipe> GetRecipeAsync(int recipeId, int moduleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, moduleId, PermissionNames.View))
            {
                return Task.FromResult(_recipeRepository.GetRecipe(recipeId));
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Recipe Get Attempt {RecipeId} {ModuleId}", recipeId, moduleId);
                return Task.FromResult<Models.Recipe>(null);
            }
        }

        public Task<Models.Recipe> AddRecipeAsync(Models.Recipe recipe)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, recipe.ModuleId, PermissionNames.Edit))
            {
                recipe = _recipeRepository.AddRecipe(recipe);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "Recipe Added {Recipe}", recipe);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Recipe Add Attempt {Recipe}", recipe);
                recipe = null;
            }
            return Task.FromResult(recipe);
        }

        public Task<Models.Recipe> UpdateRecipeAsync(Models.Recipe recipe)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, recipe.ModuleId, PermissionNames.Edit))
            {
                recipe = _recipeRepository.UpdateRecipe(recipe);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "Recipe Updated {Recipe}", recipe);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Recipe Update Attempt {Recipe}", recipe);
                recipe = null;
            }
            return Task.FromResult(recipe);
        }

        public Task DeleteRecipeAsync(int recipeId, int moduleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, moduleId, PermissionNames.Edit))
            {
                _recipeRepository.DeleteRecipe(recipeId);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "Recipe Deleted {RecipeId}", recipeId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Recipe Delete Attempt {RecipeId} {ModuleId}", recipeId, moduleId);
            }
            return Task.CompletedTask;
        }

        public Task<Oqtane.Models.File> ResizeImageAsync(int fileId, int width, int height, int moduleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, moduleId, PermissionNames.Edit))
            {
                return _recipeRepository.ResizeImageAsync(fileId, width, height);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Image Resize Attempt {FileId}", fileId);
                return Task.FromResult<Oqtane.Models.File>(null);
            }
        }

        public Task<List<Ingredient>> GetIngredientsAsync(int moduleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, moduleId, PermissionNames.View))
            {
                return Task.FromResult(_recipeRepository.GetIngredients(moduleId).ToList());
            }
            return Task.FromResult<List<Ingredient>>(null);
        }

        public Task<Ingredient> GetIngredientAsync(int ingredientId, int moduleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, moduleId, PermissionNames.View))
            {
                return Task.FromResult(_recipeRepository.GetIngredient(ingredientId));
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Ingredient Get Attempt {IngredientId} {ModuleId}", ingredientId, moduleId);
                return Task.FromResult<Ingredient>(null);
            }
        }

        public Task<Ingredient> AddIngredientAsync(Ingredient ingredient)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ingredient.ModuleId, PermissionNames.Edit))
            {
                return Task.FromResult(_recipeRepository.AddIngredient(ingredient));
            }
            return Task.FromResult<Ingredient>(null);
        }

        public Task<Ingredient> UpdateIngredientAsync(Ingredient ingredient)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ingredient.ModuleId, PermissionNames.Edit))
            {
                return Task.FromResult(_recipeRepository.UpdateIngredient(ingredient));
            }
            return Task.FromResult<Ingredient>(null);
        }

        public Task DeleteIngredientAsync(int ingredientId, int moduleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, moduleId, PermissionNames.Edit))
            {
                _recipeRepository.DeleteIngredient(ingredientId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Ingredient Delete Attempt {IngredientId} {ModuleId}", ingredientId, moduleId);
            }
            return Task.CompletedTask;
        }

        public Task<List<Unit>> GetUnitsAsync(int moduleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, moduleId, PermissionNames.View))
            {
                return Task.FromResult(_recipeRepository.GetUnits(moduleId).ToList());
            }
            return Task.FromResult<List<Unit>>(null);
        }

        public Task<Unit> GetUnitAsync(int unitId, int moduleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, moduleId, PermissionNames.View))
            {
                return Task.FromResult(_recipeRepository.GetUnit(unitId));
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Unit Get Attempt {UnitId} {ModuleId}", unitId, moduleId);
                return Task.FromResult<Unit>(null);
            }
        }

        public Task<List<RecipeIngredient>> GetRecipeIngredientsAsync(int recipeId, int moduleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, moduleId, PermissionNames.View))
            {
                return Task.FromResult(_recipeRepository.GetRecipeIngredients(recipeId).ToList());
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized GetRecipeIngredients Attempt for Recipe {RecipeId} in Module {ModuleId}", recipeId, moduleId);
                return Task.FromResult<List<RecipeIngredient>>(null);
            }
        }

        public Task<RecipeIngredient> GetRecipeIngredientAsync(int recipeIngredientId, int moduleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, moduleId, PermissionNames.View))
            {
                return Task.FromResult(_recipeRepository.GetRecipeIngredient(recipeIngredientId));
            }
            return Task.FromResult<RecipeIngredient>(null);
        }

        public Task<RecipeIngredient> AddRecipeIngredientAsync(RecipeIngredient recipeIngredient)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, recipeIngredient.ModuleId, PermissionNames.Edit))
            {
                return Task.FromResult(_recipeRepository.AddRecipeIngredient(recipeIngredient));
            }
            return Task.FromResult<RecipeIngredient>(null);
        }

        public Task<RecipeIngredient> UpdateRecipeIngredientAsync(RecipeIngredient recipeIngredient)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, recipeIngredient.ModuleId, PermissionNames.Edit))
            {
                return Task.FromResult(_recipeRepository.UpdateRecipeIngredient(recipeIngredient));
            }
            return Task.FromResult<RecipeIngredient>(null);
        }

        public Task DeleteRecipeIngredientAsync(int recipeIngredientId, int moduleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, moduleId, PermissionNames.Edit))
            {
                _recipeRepository.DeleteRecipeIngredient(recipeIngredientId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Recipe Ingredient Delete Attempt {RecipeIngredientId} {ModuleId}", recipeIngredientId, moduleId);
            }
            return Task.CompletedTask;
        }

        // Add these new implementations for Unit CRUD
        public Task<Unit> AddUnitAsync(Unit unit)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, unit.ModuleId, PermissionNames.Edit))
            {
                return Task.FromResult(_recipeRepository.AddUnit(unit));
            }
            return Task.FromResult<Unit>(null);
        }

        public Task<Unit> UpdateUnitAsync(Unit unit)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, unit.ModuleId, PermissionNames.Edit))
            {
                return Task.FromResult(_recipeRepository.UpdateUnit(unit));
            }
            return Task.FromResult<Unit>(null);
        }

        public Task DeleteUnitAsync(int unitId, int moduleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, moduleId, PermissionNames.Edit))
            {
                _recipeRepository.DeleteUnit(unitId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Unit Delete Attempt {UnitId} {ModuleId}", unitId, moduleId);
            }
            return Task.CompletedTask;
        }

        public Task<List<Step>> GetStepsAsync(int recipeId, int moduleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, moduleId, PermissionNames.View))
            {
                return Task.FromResult(_recipeRepository.GetSteps(recipeId).ToList());
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized GetSteps Attempt for Recipe {RecipeId} in Module {ModuleId}", recipeId, moduleId);
                return Task.FromResult<List<Step>>(null);
            }
        }

        public Task<Step> GetStepAsync(int stepId, int moduleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, moduleId, PermissionNames.View))
            {
                return Task.FromResult(_recipeRepository.GetStep(stepId));
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized GetStep Attempt {StepId} {ModuleId}", stepId, moduleId);
                return Task.FromResult<Step>(null);
            }
        }

        public Task<Step> AddStepAsync(Step step)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, step.ModuleId, PermissionNames.Edit))
            {
                step = _recipeRepository.AddStep(step);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "Step Added {Step}", step);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Step Add Attempt {Step}", step);
                step = null;
            }
            return Task.FromResult(step);
        }

        public Task<Step> UpdateStepAsync(Step step)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, step.ModuleId, PermissionNames.Edit))
            {
                step = _recipeRepository.UpdateStep(step);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "Step Updated {Step}", step);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Step Update Attempt {Step}", step);
                step = null;
            }
            return Task.FromResult(step);
        }

        public Task DeleteStepAsync(int stepId, int moduleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, moduleId, PermissionNames.Edit))
            {
                _recipeRepository.DeleteStep(stepId);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "Step Deleted {StepId}", stepId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Step Delete Attempt {StepId} {ModuleId}", stepId, moduleId);
            }
            return Task.CompletedTask;
        }
    }
}