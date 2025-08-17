using Microsoft.EntityFrameworkCore;
using GIBS.Module.Recipe.Models;
using Oqtane.Modules;
using Oqtane.Repository;
using Oqtane.Repository.Databases.Interfaces;

namespace GIBS.Module.Recipe.Repository
{
    public class RecipeContext : DBContextBase, ITransientService, IMultiDatabase
    {
        public virtual DbSet<Models.Recipe> Recipe { get; set; }
        public virtual DbSet<Ingredient> Ingredient { get; set; }
        public virtual DbSet<Unit> Unit { get; set; }
        public virtual DbSet<RecipeIngredient> RecipeIngredient { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<RecipeCategory> RecipeCategory { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
        public virtual DbSet<RecipeTag> RecipeTag { get; set; }
        public virtual DbSet<UserRecipe> UserRecipe { get; set; }
        public virtual DbSet<RatingsReview> RatingsReview { get; set; }
        public virtual DbSet<Step> Step { get; set; }


        public RecipeContext(IDBContextDependencies DBContextDependencies) : base(DBContextDependencies)
        {
            // ContextBase handles multi-tenant database connections
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Models.Recipe>().ToTable(ActiveDatabase.RewriteName("GIBSRecipe"));
            builder.Entity<Ingredient>().ToTable(ActiveDatabase.RewriteName("GIBSRecipe_Ingredient"));
            builder.Entity<Unit>().ToTable(ActiveDatabase.RewriteName("GIBSRecipe_Unit"));
            builder.Entity<RecipeIngredient>().ToTable(ActiveDatabase.RewriteName("GIBSRecipeIngredient"));
            builder.Entity<Category>().ToTable(ActiveDatabase.RewriteName("GIBSRecipe_Category"));
            builder.Entity<RecipeCategory>().ToTable(ActiveDatabase.RewriteName("GIBSRecipeCategory"));
            builder.Entity<Tag>().ToTable(ActiveDatabase.RewriteName("GIBSRecipe_Tag"));
            builder.Entity<RecipeTag>().ToTable(ActiveDatabase.RewriteName("GIBSRecipeTag"));
            builder.Entity<UserRecipe>().ToTable(ActiveDatabase.RewriteName("GIBSRecipe_UserRecipe"));
            builder.Entity<RatingsReview>().ToTable(ActiveDatabase.RewriteName("GIBSRecipe_RatingsReview"));
            builder.Entity<Step>().ToTable(ActiveDatabase.RewriteName("GIBSRecipe_Step"));
        }
    }
}