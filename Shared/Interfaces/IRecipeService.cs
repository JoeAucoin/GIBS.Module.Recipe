using GIBS.Module.Recipe.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GIBS.Module.Recipe.Services
{
    public interface IRecipeService
    {
        Task<List<Models.Recipe>> GetRecipesAsync(int moduleId);
        Task<Models.Recipe> GetRecipeAsync(int recipeId, int moduleId);
        Task<Models.Recipe> AddRecipeAsync(Models.Recipe recipe);
        Task<Models.Recipe> UpdateRecipeAsync(Models.Recipe recipe);
        Task DeleteRecipeAsync(int recipeId, int moduleId);

        // Ingredients
        Task<List<Ingredient>> GetIngredientsAsync(int moduleId);
        Task<Ingredient> GetIngredientAsync(int ingredientId, int moduleId);
        Task<Ingredient> AddIngredientAsync(Ingredient ingredient);
        Task<Ingredient> UpdateIngredientAsync(Ingredient ingredient);
        Task DeleteIngredientAsync(int ingredientId, int moduleId);

        // Units
        Task<List<Unit>> GetUnitsAsync(int moduleId);
        Task<Unit> GetUnitAsync(int unitId, int moduleId);
        Task<Unit> AddUnitAsync(Unit unit);
        Task<Unit> UpdateUnitAsync(Unit unit);
        Task DeleteUnitAsync(int unitId, int moduleId);

        // RecipeIngredients
        Task<List<RecipeIngredient>> GetRecipeIngredientsAsync(int recipeId, int moduleId);
        Task<RecipeIngredient> GetRecipeIngredientAsync(int recipeIngredientId, int moduleId);
        Task<RecipeIngredient> AddRecipeIngredientAsync(RecipeIngredient recipeIngredient);
        Task<RecipeIngredient> UpdateRecipeIngredientAsync(RecipeIngredient recipeIngredient);
        Task DeleteRecipeIngredientAsync(int recipeIngredientId, int moduleId);

        // Steps
        Task<List<Step>> GetStepsAsync(int recipeId, int moduleId);
        Task<Step> GetStepAsync(int stepId, int moduleId);
        Task<Step> AddStepAsync(Step step);
        Task<Step> UpdateStepAsync(Step step);
        Task DeleteStepAsync(int stepId, int moduleId);

        // Image Resize
        Task<Oqtane.Models.File> ResizeImageAsync(int fileId, int maxWidth, int maxHeight, int moduleId);
    }
}