using System.Collections.Generic;
using System.Threading.Tasks;
using GIBS.Module.Recipe.Models;

namespace GIBS.Module.Recipe.Repository
{
    public interface IRecipeRepository
    {
        IEnumerable<Models.Recipe> GetRecipes(int moduleId);
        Models.Recipe GetRecipe(int recipeId, bool tracking = true);
        Models.Recipe AddRecipe(Models.Recipe recipe);
        Models.Recipe UpdateRecipe(Models.Recipe recipe);
        void DeleteRecipe(int recipeId);
        Task<Oqtane.Models.File> ResizeImageAsync(int fileId, int maxWidth, int maxHeight);

        IEnumerable<Ingredient> GetIngredients(int moduleId);
        Ingredient GetIngredient(int ingredientId);
        Ingredient AddIngredient(Ingredient ingredient);
        Ingredient UpdateIngredient(Ingredient ingredient);
        void DeleteIngredient(int ingredientId);

        IEnumerable<Unit> GetUnits(int moduleId);
        IEnumerable<RecipeIngredient> GetRecipeIngredients(int recipeId);
        RecipeIngredient GetRecipeIngredient(int recipeIngredientId);
        RecipeIngredient AddRecipeIngredient(RecipeIngredient recipeIngredient);
        RecipeIngredient UpdateRecipeIngredient(RecipeIngredient recipeIngredient);
        void DeleteRecipeIngredient(int recipeIngredientId);

        // Add these new methods for Unit CRUD
        Unit GetUnit(int unitId);
        Unit AddUnit(Unit unit);
        Unit UpdateUnit(Unit unit);
        void DeleteUnit(int unitId);

        // Step Methods
        IEnumerable<Step> GetSteps(int recipeId);
        Step GetStep(int stepId);
        Step AddStep(Step step);
        Step UpdateStep(Step step);
        void DeleteStep(int stepId);

        IEnumerable<Category> GetCategories(int moduleId);
        Category GetCategory(int categoryId);
        Category AddCategory(Category category);
        Category UpdateCategory(Category category);
        void DeleteCategory(int categoryId);

        // RecipeCategory Methods
        IEnumerable<RecipeCategory> GetRecipeCategories(int recipeId);
        RecipeCategory GetRecipeCategory(int recipeCategoryId);
        RecipeCategory AddRecipeCategory(RecipeCategory recipeCategory);
        RecipeCategory UpdateRecipeCategory(RecipeCategory recipeCategory);
        void DeleteRecipeCategory(int recipeCategoryId);

        // Tag CRUD
        IEnumerable<Tag> GetTags(int moduleId);
        Tag GetTag(int tagId);
        Tag AddTag(Tag tag);
        Tag UpdateTag(Tag tag);
        void DeleteTag(int tagId);

        // RecipeTag CRUD
        IEnumerable<RecipeTag> GetRecipeTags(int recipeId);
        RecipeTag GetRecipeTag(int recipeTagId);
        RecipeTag AddRecipeTag(RecipeTag recipeTag);
        RecipeTag UpdateRecipeTag(RecipeTag recipeTag);
        void DeleteRecipeTag(int recipeTagId);
    }
}