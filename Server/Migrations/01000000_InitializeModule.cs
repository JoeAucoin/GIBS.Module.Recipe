using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Oqtane.Databases.Interfaces;
using Oqtane.Migrations;
using GIBS.Module.Recipe.Migrations.EntityBuilders;
using GIBS.Module.Recipe.Repository;

namespace GIBS.Module.Recipe.Migrations
{
    [DbContext(typeof(RecipeContext))]
    [Migration("GIBS.Module.Recipe.01.00.00.00")]
    public class InitializeModule : MultiDatabaseMigration
    {
        public InitializeModule(IDatabase database) : base(database)
        {
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Independent Tables
            var recipeEntityBuilder = new RecipeEntityBuilder(migrationBuilder, ActiveDatabase);
            recipeEntityBuilder.Create();

            var ingredientEntityBuilder = new IngredientEntityBuilder(migrationBuilder, ActiveDatabase);
            ingredientEntityBuilder.Create();

            var unitEntityBuilder = new UnitEntityBuilder(migrationBuilder, ActiveDatabase);
            unitEntityBuilder.Create();

            var categoryEntityBuilder = new CategoryEntityBuilder(migrationBuilder, ActiveDatabase);
            categoryEntityBuilder.Create();

            var tagEntityBuilder = new TagEntityBuilder(migrationBuilder, ActiveDatabase);
            tagEntityBuilder.Create();

            // Dependent Tables
            var recipeIngredientEntityBuilder = new RecipeIngredientEntityBuilder(migrationBuilder, ActiveDatabase);
            recipeIngredientEntityBuilder.Create();

            var recipeCategoryEntityBuilder = new RecipeCategoryEntityBuilder(migrationBuilder, ActiveDatabase);
            recipeCategoryEntityBuilder.Create();

            var recipeTagEntityBuilder = new RecipeTagEntityBuilder(migrationBuilder, ActiveDatabase);
            recipeTagEntityBuilder.Create();

            var userRecipeEntityBuilder = new UserRecipeEntityBuilder(migrationBuilder, ActiveDatabase);
            userRecipeEntityBuilder.Create();

            var ratingsReviewEntityBuilder = new RatingsReviewEntityBuilder(migrationBuilder, ActiveDatabase);
            ratingsReviewEntityBuilder.Create();

            // Steps Entity
            var stepsEntityBuilder = new StepsEntityBuilder(migrationBuilder, ActiveDatabase);
            stepsEntityBuilder.Create();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Dependent Tables
            var ratingsReviewEntityBuilder = new RatingsReviewEntityBuilder(migrationBuilder, ActiveDatabase);
            ratingsReviewEntityBuilder.Drop();

            var userRecipeEntityBuilder = new UserRecipeEntityBuilder(migrationBuilder, ActiveDatabase);
            userRecipeEntityBuilder.Drop();

            var recipeTagEntityBuilder = new RecipeTagEntityBuilder(migrationBuilder, ActiveDatabase);
            recipeTagEntityBuilder.Drop();

            var recipeCategoryEntityBuilder = new RecipeCategoryEntityBuilder(migrationBuilder, ActiveDatabase);
            recipeCategoryEntityBuilder.Drop();

            var recipeIngredientEntityBuilder = new RecipeIngredientEntityBuilder(migrationBuilder, ActiveDatabase);
            recipeIngredientEntityBuilder.Drop();
            
            var stepsEntityBuilder = new StepsEntityBuilder(migrationBuilder, ActiveDatabase);
            stepsEntityBuilder.Drop();

            // Independent Tables
            var tagEntityBuilder = new TagEntityBuilder(migrationBuilder, ActiveDatabase);
            tagEntityBuilder.Drop();

            var categoryEntityBuilder = new CategoryEntityBuilder(migrationBuilder, ActiveDatabase);
            categoryEntityBuilder.Drop();

            var unitEntityBuilder = new UnitEntityBuilder(migrationBuilder, ActiveDatabase);
            unitEntityBuilder.Drop();

            var ingredientEntityBuilder = new IngredientEntityBuilder(migrationBuilder, ActiveDatabase);
            ingredientEntityBuilder.Drop();

            var recipeEntityBuilder = new RecipeEntityBuilder(migrationBuilder, ActiveDatabase);
            recipeEntityBuilder.Drop();

            
        }
    }
}