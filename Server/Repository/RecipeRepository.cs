using GIBS.Module.Recipe.Models;
using Microsoft.EntityFrameworkCore;
using Oqtane.Models;
using Oqtane.Modules;
using Oqtane.Repository;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GIBS.Module.Recipe.Repository
{
    public class RecipeRepository : IRecipeRepository, IService
    {
        private readonly RecipeContext _db;
        private readonly IFileRepository _files;
        private readonly IFolderRepository _folders;

        public RecipeRepository(RecipeContext context, IFileRepository files, IFolderRepository folders)
        {
            _db = context;
            _files = files;
            _folders = folders;
        }

        public IEnumerable<Models.Recipe> GetRecipes(int moduleId)
        {
            return _db.Recipe.Where(item => item.ModuleId == moduleId);
        }

        public Models.Recipe GetRecipe(int recipeId, bool tracking = true)
        {
            if (tracking)
            {
                return _db.Recipe.Find(recipeId);
            }
            else
            {
                return _db.Recipe.AsNoTracking().FirstOrDefault(item => item.RecipeId == recipeId);
            }
        }

        public Models.Recipe AddRecipe(Models.Recipe recipe)
        {
            _db.Recipe.Add(recipe);
            _db.SaveChanges();
            return recipe;
        }

        public Models.Recipe UpdateRecipe(Models.Recipe recipe)
        {
            _db.Entry(recipe).State = EntityState.Modified;
            _db.SaveChanges();
            return recipe;
        }

        public void DeleteRecipe(int recipeId)
        {
            Models.Recipe recipe = _db.Recipe.Find(recipeId);
            _db.Recipe.Remove(recipe);
            _db.SaveChanges();
        }

        public IEnumerable<Ingredient> GetIngredients(int moduleId)
        {
            return _db.Ingredient.Where(i => i.ModuleId == moduleId).OrderBy(i => i.Name);
        }

        public Ingredient GetIngredient(int ingredientId)
        {
            return _db.Ingredient.Find(ingredientId);
        }

        public Ingredient AddIngredient(Ingredient ingredient)
        {
            _db.Ingredient.Add(ingredient);
            _db.SaveChanges();
            return ingredient;
        }

        public Ingredient UpdateIngredient(Ingredient ingredient)
        {
            _db.Entry(ingredient).State = EntityState.Modified;
            _db.SaveChanges();
            return ingredient;
        }

        public void DeleteIngredient(int ingredientId)
        {
            Ingredient ingredient = _db.Ingredient.Find(ingredientId);
            _db.Ingredient.Remove(ingredient);
            _db.SaveChanges();
        }

        public IEnumerable<Unit> GetUnits(int moduleId)
        {
            return _db.Unit.Where(u => u.ModuleId == moduleId).OrderBy(u => u.Name);
        }

        public IEnumerable<RecipeIngredient> GetRecipeIngredients(int recipeId)
        {
            return _db.RecipeIngredient
                .Include(ri => ri.Ingredient)
                .Include(ri => ri.Unit)
                .Where(ri => ri.RecipeId == recipeId);
        }

        public RecipeIngredient GetRecipeIngredient(int recipeIngredientId)
        {
            return _db.RecipeIngredient.Find(recipeIngredientId);
        }

        public RecipeIngredient AddRecipeIngredient(RecipeIngredient recipeIngredient)
        {
            _db.RecipeIngredient.Add(recipeIngredient);
            _db.SaveChanges();
            return recipeIngredient;
        }

        public RecipeIngredient UpdateRecipeIngredient(RecipeIngredient recipeIngredient)
        {
            _db.Entry(recipeIngredient).State = EntityState.Modified;
            _db.SaveChanges();
            return recipeIngredient;
        }

        public void DeleteRecipeIngredient(int recipeIngredientId)
        {
            RecipeIngredient recipeIngredient = _db.RecipeIngredient.Find(recipeIngredientId);
            _db.RecipeIngredient.Remove(recipeIngredient);
            _db.SaveChanges();
        }

        public async Task<Oqtane.Models.File> ResizeImageAsync(int fileId, int maxWidth, int maxHeight)
        {
            var file = _files.GetFile(fileId);
            if (file == null) return null;

            var folder = _folders.GetFolder(file.FolderId);
            var filePath = _files.GetFilePath(file);

            using (var image = await Image.LoadAsync(filePath))
            {
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(maxWidth, maxHeight),
                    Mode = ResizeMode.Max
                }));

                var newFileName = $"{Path.GetFileNameWithoutExtension(file.Name)}_{maxWidth}x{maxHeight}{Path.GetExtension(file.Name)}";
                var newFilePath = Path.Combine(Path.GetDirectoryName(filePath), newFileName);
                await image.SaveAsync(newFilePath);

                var newFile = new Oqtane.Models.File
                {
                    FolderId = file.FolderId,
                    Name = newFileName,
                    Extension = file.Extension,
                    Size = (int)new FileInfo(newFilePath).Length,
                    ImageHeight = image.Height,
                    ImageWidth = image.Width
                };
                newFile = _files.AddFile(newFile);
                return newFile;
            }
        }

        // Add these new implementations for Unit CRUD
        public Unit GetUnit(int unitId)
        {
            return _db.Unit.Find(unitId);
        }

        public Unit AddUnit(Unit unit)
        {
            _db.Unit.Add(unit);
            _db.SaveChanges();
            return unit;
        }

        public Unit UpdateUnit(Unit unit)
        {
            _db.Entry(unit).State = EntityState.Modified;
            _db.SaveChanges();
            return unit;
        }

        public void DeleteUnit(int unitId)
        {
            Unit unit = _db.Unit.Find(unitId);
            _db.Unit.Remove(unit);
            _db.SaveChanges();
        }

        // Step Methods
        public IEnumerable<Step> GetSteps(int recipeId)
        {
            return _db.Step.Where(s => s.RecipeId == recipeId);
        }

        public Step GetStep(int stepId)
        {
            return _db.Step.Find(stepId);
        }

        public Step AddStep(Step step)
        {
            _db.Step.Add(step);
            _db.SaveChanges();
            return step;
        }

        public Step UpdateStep(Step step)
        {
            _db.Entry(step).State = EntityState.Modified;
            _db.SaveChanges();
            return step;
        }

        public void DeleteStep(int stepId)
        {
            Step step = _db.Step.Find(stepId);
            _db.Step.Remove(step);
            _db.SaveChanges();
        }

        public IEnumerable<Category> GetCategories(int moduleId)
        {
            return _db.Category.Where(c => c.ModuleId == moduleId).OrderBy(c => c.Name);
        }

        public Category GetCategory(int categoryId)
        {
            return _db.Category.Find(categoryId);
        }

        public Category AddCategory(Category category)
        {
            _db.Category.Add(category);
            _db.SaveChanges();
            return category;
        }

        //public Category UpdateCategory(Category category)
        //{
        //    _db.Entry(category).State = EntityState.Modified;
        //    _db.SaveChanges();
        //    return category;
        //}

        public Category UpdateCategory(Category category)
        {
            var existing = _db.Category.Find(category.CategoryId);
            if (existing != null)
            {
                // Only update fields that are allowed to change
                existing.Name = category.Name;
                existing.Slug = category.Slug;
                existing.ModuleId = category.ModuleId;
                // Do NOT overwrite CreatedBy/CreatedOn
                existing.ModifiedBy = category.ModifiedBy;
                existing.ModifiedOn = category.ModifiedOn;
                // Save changes
                _db.SaveChanges();
                return existing;
            }
            else
            {
                // Attach if not tracked
                _db.Category.Attach(category);
                _db.Entry(category).State = EntityState.Modified;
                _db.SaveChanges();
                return category;
            }
        }

        public void DeleteCategory(int categoryId)
        {
            Category category = _db.Category.Find(categoryId);
            _db.Category.Remove(category);
            _db.SaveChanges();
        }

        public IEnumerable<RecipeCategory> GetRecipeCategories(int recipeId)
        {
            return _db.RecipeCategory
                .Include(rc => rc.Category)
                .Include(rc => rc.Recipe)
                .Where(rc => rc.RecipeId == recipeId);
        }

        public RecipeCategory GetRecipeCategory(int recipeCategoryId)
        {
            return _db.RecipeCategory.Find(recipeCategoryId);
        }

        public RecipeCategory AddRecipeCategory(RecipeCategory recipeCategory)
        {
            _db.RecipeCategory.Add(recipeCategory);
            _db.SaveChanges();
            return recipeCategory;
        }

        public RecipeCategory UpdateRecipeCategory(RecipeCategory recipeCategory)
        {
            _db.Entry(recipeCategory).State = EntityState.Modified;
            _db.SaveChanges();
            return recipeCategory;
        }

        public void DeleteRecipeCategory(int recipeCategoryId)
        {
            RecipeCategory recipeCategory = _db.RecipeCategory.Find(recipeCategoryId);
            _db.RecipeCategory.Remove(recipeCategory);
            _db.SaveChanges();
        }
    }
}