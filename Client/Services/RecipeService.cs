using Oqtane.Services;
using Oqtane.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using GIBS.Module.Recipe.Models;

namespace GIBS.Module.Recipe.Services
{
    public class RecipeService : ServiceBase, IRecipeService
    {
        private readonly HttpClient _httpClient;
        public RecipeService(HttpClient http, SiteState siteState) : base(http, siteState)
        {
            _httpClient = http ?? throw new ArgumentNullException(nameof(http), "HttpClient is not initialized.");
        }

        private string Apiurl => CreateApiUrl("Recipe");

        public async Task<List<Models.Recipe>> GetRecipesAsync(int ModuleId)
        {
            List<Models.Recipe> Recipes = await GetJsonAsync<List<Models.Recipe>>(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={ModuleId}", EntityNames.Module, ModuleId), Enumerable.Empty<Models.Recipe>().ToList());
            return Recipes.OrderBy(item => item.Name).ToList();
        }

        public async Task<Models.Recipe> GetRecipeAsync(int RecipeId, int ModuleId)
        {
            return await GetJsonAsync<Models.Recipe>(CreateAuthorizationPolicyUrl($"{Apiurl}/{RecipeId}", EntityNames.Module, ModuleId));
        }

        public async Task<Models.Recipe> AddRecipeAsync(Models.Recipe Recipe)
        {
            return await PostJsonAsync<Models.Recipe>(CreateAuthorizationPolicyUrl($"{Apiurl}", EntityNames.Module, Recipe.ModuleId), Recipe);
        }

        public async Task<Models.Recipe> UpdateRecipeAsync(Models.Recipe Recipe)
        {
            return await PutJsonAsync<Models.Recipe>(CreateAuthorizationPolicyUrl($"{Apiurl}/{Recipe.RecipeId}", EntityNames.Module, Recipe.ModuleId), Recipe);
        }

        public async Task DeleteRecipeAsync(int RecipeId, int ModuleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{RecipeId}", EntityNames.Module, ModuleId));
        }

        public async Task<Oqtane.Models.File> ResizeImageAsync(int fileId, int width, int height, int moduleId)
        {
            var request = new { FileId = fileId, Width = width, Height = height, ModuleId = moduleId };
            var url = CreateAuthorizationPolicyUrl($"{Apiurl}/resize-image", EntityNames.Module, moduleId);

            try
            {
                var response = await _httpClient.PostAsJsonAsync(url, request);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadFromJsonAsync<Oqtane.Models.File>();
            }
            catch (Exception ex)
            {
                // Log or handle exceptions as needed
                Console.WriteLine($"Error resizing image: {ex.Message}");
                return null;
            }
        }

        public async Task<List<Ingredient>> GetIngredientsAsync(int moduleId)
        {
            return await GetJsonAsync<List<Ingredient>>(CreateAuthorizationPolicyUrl($"{Apiurl}/ingredient", EntityNames.Module, moduleId));
        }

        public async Task<Ingredient> GetIngredientAsync(int ingredientId, int moduleId)
        {
            return await GetJsonAsync<Ingredient>(CreateAuthorizationPolicyUrl($"{Apiurl}/ingredient/{ingredientId}", EntityNames.Module, moduleId));
        }

        public async Task<Ingredient> AddIngredientAsync(Ingredient ingredient)
        {
            return await PostJsonAsync<Ingredient>(CreateAuthorizationPolicyUrl($"{Apiurl}/ingredient", EntityNames.Module, ingredient.ModuleId), ingredient);
        }

        public async Task<Ingredient> UpdateIngredientAsync(Ingredient ingredient)
        {
            return await PutJsonAsync<Ingredient>(CreateAuthorizationPolicyUrl($"{Apiurl}/ingredient/{ingredient.IngredientId}", EntityNames.Module, ingredient.ModuleId), ingredient);
        }

        public async Task DeleteIngredientAsync(int ingredientId, int moduleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/ingredient/{ingredientId}", EntityNames.Module, moduleId));
        }

        public async Task<List<Unit>> GetUnitsAsync(int moduleId)
        {
            return await GetJsonAsync<List<Unit>>(CreateAuthorizationPolicyUrl($"{Apiurl}/unit", EntityNames.Module, moduleId));
        }

        public async Task<Unit> GetUnitAsync(int unitId, int moduleId)
        {
            return await GetJsonAsync<Unit>(CreateAuthorizationPolicyUrl($"{Apiurl}/unit/{unitId}", EntityNames.Module, moduleId));
        }

        public async Task<List<RecipeIngredient>> GetRecipeIngredientsAsync(int recipeId, int moduleId)
        {
            return await GetJsonAsync<List<RecipeIngredient>>(CreateAuthorizationPolicyUrl($"{Apiurl}/recipeingredient/getbyrecipe/{recipeId}", EntityNames.Module, moduleId));
        }

        public async Task<RecipeIngredient> GetRecipeIngredientAsync(int recipeIngredientId, int moduleId)
        {
            return await GetJsonAsync<RecipeIngredient>(CreateAuthorizationPolicyUrl($"{Apiurl}/recipeingredient/{recipeIngredientId}", EntityNames.Module, moduleId));
        }

        public async Task<RecipeIngredient> AddRecipeIngredientAsync(RecipeIngredient recipeIngredient)
        {
            return await PostJsonAsync<RecipeIngredient>(CreateAuthorizationPolicyUrl($"{Apiurl}/recipeingredient", EntityNames.Module, recipeIngredient.ModuleId), recipeIngredient);
        }

        public async Task<RecipeIngredient> UpdateRecipeIngredientAsync(RecipeIngredient recipeIngredient)
        {
            return await PutJsonAsync<RecipeIngredient>(CreateAuthorizationPolicyUrl($"{Apiurl}/recipeingredient/{recipeIngredient.RecipeIngredientId}", EntityNames.Module, recipeIngredient.ModuleId), recipeIngredient);
        }

        public async Task DeleteRecipeIngredientAsync(int recipeIngredientId, int moduleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/recipeingredient/{recipeIngredientId}", EntityNames.Module, moduleId));
        }

        // Add these new implementations for Unit CRUD
        public async Task<Unit> AddUnitAsync(Unit unit)
        {
            return await PostJsonAsync<Unit>(CreateAuthorizationPolicyUrl($"{Apiurl}/unit", EntityNames.Module, unit.ModuleId), unit);
        }

        public async Task<Unit> UpdateUnitAsync(Unit unit)
        {
            return await PutJsonAsync<Unit>(CreateAuthorizationPolicyUrl($"{Apiurl}/unit/{unit.UnitId}", EntityNames.Module, unit.ModuleId), unit);
        }

        public async Task DeleteUnitAsync(int unitId, int moduleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/unit/{unitId}", EntityNames.Module, moduleId));
        }

        // Steps
        public async Task<List<Step>> GetStepsAsync(int recipeId, int moduleId)
        {
            return await GetJsonAsync<List<Step>>(CreateAuthorizationPolicyUrl($"{CreateApiUrl("Step")}/getbyrecipe/{recipeId}", EntityNames.Module, moduleId));
        }

        public async Task<Step> GetStepAsync(int stepId, int moduleId)
        {
            return await GetJsonAsync<Step>(CreateAuthorizationPolicyUrl($"{CreateApiUrl("Step")}/{stepId}", EntityNames.Module, moduleId));
        }

        public async Task<Step> AddStepAsync(Step step)
        {
            return await PostJsonAsync<Step>(CreateAuthorizationPolicyUrl(CreateApiUrl("Step"), EntityNames.Module, step.ModuleId), step);
        }

        public async Task<Step> UpdateStepAsync(Step step)
        {
            return await PutJsonAsync<Step>(CreateAuthorizationPolicyUrl($"{CreateApiUrl("Step")}/{step.StepId}", EntityNames.Module, step.ModuleId), step);
        }

        public async Task DeleteStepAsync(int stepId, int moduleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{CreateApiUrl("Step")}/{stepId}", EntityNames.Module, moduleId));
        }

        public async Task<List<GIBS.Module.Recipe.Models.Category>> GetCategoriesAsync(int moduleId)
        {
            return await GetJsonAsync<List<GIBS.Module.Recipe.Models.Category>>(CreateAuthorizationPolicyUrl($"{Apiurl}/category", EntityNames.Module, moduleId));
        }

        public async Task<GIBS.Module.Recipe.Models.Category> GetCategoryAsync(int categoryId, int moduleId)
        {
            return await GetJsonAsync<GIBS.Module.Recipe.Models.Category>(CreateAuthorizationPolicyUrl($"{Apiurl}/category/{categoryId}", EntityNames.Module, moduleId));
        }

        public async Task<GIBS.Module.Recipe.Models.Category> AddCategoryAsync(GIBS.Module.Recipe.Models.Category category)
        {
            return await PostJsonAsync<GIBS.Module.Recipe.Models.Category>(CreateAuthorizationPolicyUrl($"{Apiurl}/category", EntityNames.Module, category.ModuleId), category);
        }

        public async Task<GIBS.Module.Recipe.Models.Category> UpdateCategoryAsync(GIBS.Module.Recipe.Models.Category category)
        {
            return await PutJsonAsync<GIBS.Module.Recipe.Models.Category>(CreateAuthorizationPolicyUrl($"{Apiurl}/category/{category.CategoryId}", EntityNames.Module, category.ModuleId), category);
        }

        public async Task DeleteCategoryAsync(int categoryId, int moduleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/category/{categoryId}", EntityNames.Module, moduleId));
        }

        // RecipeCategory Methods
        public async Task<List<RecipeCategory>> GetRecipeCategoriesAsync(int recipeId, int moduleId)
        {
            return await GetJsonAsync<List<RecipeCategory>>(CreateAuthorizationPolicyUrl($"{Apiurl}/recipecategory/getbyrecipe/{recipeId}", EntityNames.Module, moduleId));
        }

        public async Task<RecipeCategory> GetRecipeCategoryAsync(int recipeCategoryId, int moduleId)
        {
            return await GetJsonAsync<RecipeCategory>(CreateAuthorizationPolicyUrl($"{Apiurl}/recipecategory/{recipeCategoryId}", EntityNames.Module, moduleId));
        }

        public async Task<RecipeCategory> AddRecipeCategoryAsync(RecipeCategory recipeCategory)
        {
            return await PostJsonAsync<RecipeCategory>(CreateAuthorizationPolicyUrl($"{Apiurl}/recipecategory", EntityNames.Module, recipeCategory.ModuleId), recipeCategory);
        }

        public async Task<RecipeCategory> UpdateRecipeCategoryAsync(RecipeCategory recipeCategory)
        {
            return await PutJsonAsync<RecipeCategory>(CreateAuthorizationPolicyUrl($"{Apiurl}/recipecategory/{recipeCategory.RecipeCategoryId}", EntityNames.Module, recipeCategory.ModuleId), recipeCategory);
        }

        public async Task DeleteRecipeCategoryAsync(int recipeCategoryId, int moduleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/recipecategory/{recipeCategoryId}", EntityNames.Module, moduleId));
        }

    }
}