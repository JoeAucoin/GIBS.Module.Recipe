using GIBS.Module.Recipe.Models;
using GIBS.Module.Recipe.Models.DTOs;
using GIBS.Module.Recipe.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oqtane.Controllers;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Oqtane.Shared;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Linq;

namespace GIBS.Module.Recipe.Controllers
{
    [Route(ControllerRoutes.ApiRoute)]
    public class RecipeController : ModuleControllerBase
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService, ILogManager logger, IHttpContextAccessor accessor) : base(logger, accessor)
        {
            _recipeService = recipeService;
        }

        // GET: api/<controller>?moduleid=x
        [HttpGet]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public async Task<IEnumerable<RecipeDto>> Get(string moduleid)
        {
            if (int.TryParse(moduleid, out int ModuleId) && IsAuthorizedEntityId(EntityNames.Module, ModuleId))
            {
                var recipes = await _recipeService.GetRecipesAsync(ModuleId);
                return recipes.Select(MapRecipeToDto).ToList();
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Recipe Get Attempt {ModuleId}", moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        //// GET api/<controller>/5/1
        //[HttpGet("{id}/{moduleid}")]
        //[Authorize(Policy = PolicyNames.ViewModule)]
        //public async Task<Models.Recipe> Get(int id, int moduleid)
        //{
        //    if (IsAuthorizedEntityId(EntityNames.Module, moduleid))
        //    {
        //        return await _recipeService.GetRecipeAsync(id, moduleid);
        //    }
        //    else
        //    {
        //        _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Recipe Get Attempt {RecipeId} {ModuleId}", id, moduleid);
        //        HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
        //        return null;
        //    }
        //}

        [HttpGet("{id}/{moduleid}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public async Task<ActionResult<RecipeDto>> Get(int id, int moduleid)
        {
            if (IsAuthorizedEntityId(EntityNames.Module, moduleid))
            {
                var recipe = await _recipeService.GetRecipeAsync(id, moduleid);
                if (recipe == null) return NotFound();
                return Ok(MapRecipeToDto(recipe));
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Recipe Get Attempt {RecipeId} {ModuleId}", id, moduleid);
                return Forbid();
            }
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<Models.Recipe> Post([FromBody] Models.Recipe recipe)
        {
            if (ModelState.IsValid && IsAuthorizedEntityId(EntityNames.Module, recipe.ModuleId))
            {
                return await _recipeService.AddRecipeAsync(recipe);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Recipe Post Attempt {Recipe}", recipe);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<ActionResult<RecipeDto>> Put(int id, [FromBody] Models.Recipe recipe)
        {
            if (ModelState.IsValid && recipe.RecipeId == id && IsAuthorizedEntityId(EntityNames.Module, recipe.ModuleId))
            {
                var updatedRecipe = await _recipeService.UpdateRecipeAsync(recipe);
                return Ok(MapRecipeToDto(updatedRecipe));
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Recipe Put Attempt {Recipe}", recipe);
                return Forbid();
            }
        }

        // DELETE api/<controller>/5/1
        [HttpDelete("{id}/{moduleid}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task Delete(int id, int moduleid)
        {
            if (IsAuthorizedEntityId(EntityNames.Module, moduleid))
            {
                await _recipeService.DeleteRecipeAsync(id, moduleid);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Recipe Delete Attempt {RecipeId} {ModuleId}", id, moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
        }

        // GET: api/<controller>/ingredient?moduleid=x
        [HttpGet("ingredient")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public async Task<IEnumerable<Ingredient>> GetIngredients(string moduleid)
        {
            if (int.TryParse(moduleid, out int ModuleId) && IsAuthorizedEntityId(EntityNames.Module, ModuleId))
            {
                return await _recipeService.GetIngredientsAsync(ModuleId);
            }
            return null;
        }

        // POST: api/<controller>/ingredient
        [HttpPost("ingredient")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<Ingredient> PostIngredient([FromBody] Ingredient ingredient)
        {
            if (ModelState.IsValid && IsAuthorizedEntityId(EntityNames.Module, ingredient.ModuleId))
            {
                return await _recipeService.AddIngredientAsync(ingredient);
            }
            return null;
        }

        // PUT: api/<controller>/ingredient/1
        [HttpPut("ingredient/{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<Ingredient> PutIngredient(int id, [FromBody] Ingredient ingredient)
        {
            if (ModelState.IsValid && ingredient.IngredientId == id && IsAuthorizedEntityId(EntityNames.Module, ingredient.ModuleId))
            {
                return await _recipeService.UpdateIngredientAsync(ingredient);
            }
            return null;
        }

        // DELETE: api/<controller>/ingredient/1
        [HttpDelete("ingredient/{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task DeleteIngredient(int id)
        {
            await _recipeService.DeleteIngredientAsync(id, AuthEntityId(EntityNames.Module));
        }

        // GET: api/<controller>/unit?moduleid=x
        [HttpGet("unit")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public async Task<IEnumerable<Unit>> GetUnits(string moduleid)
        {
            if (int.TryParse(moduleid, out int ModuleId) && IsAuthorizedEntityId(EntityNames.Module, ModuleId))
            {
                return await _recipeService.GetUnitsAsync(ModuleId);
            }
            return null;
        }

        // POST: api/<controller>/unit
        [HttpPost("unit")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<Unit> PostUnit([FromBody] Unit unit)
        {
            if (ModelState.IsValid && IsAuthorizedEntityId(EntityNames.Module, unit.ModuleId))
            {
                return await _recipeService.AddUnitAsync(unit);
            }
            return null;
        }

        // PUT: api/<controller>/unit/1
        [HttpPut("unit/{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<Unit> PutUnit(int id, [FromBody] Unit unit)
        {
            if (ModelState.IsValid && unit.UnitId == id && IsAuthorizedEntityId(EntityNames.Module, unit.ModuleId))
            {
                return await _recipeService.UpdateUnitAsync(unit);
            }
            return null;
        }

        // DELETE: api/<controller>/unit/1
        [HttpDelete("unit/{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task DeleteUnit(int id)
        {
            await _recipeService.DeleteUnitAsync(id, AuthEntityId(EntityNames.Module));
        }

        // GET: api/<controller>/recipeingredient/getbyrecipe/1
        [HttpGet("recipeingredient/getbyrecipe/{recipeId}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public async Task<IEnumerable<RecipeIngredient>> GetRecipeIngredients(int recipeId)
        {
            return await _recipeService.GetRecipeIngredientsAsync(recipeId, AuthEntityId(EntityNames.Module));
        }

        // GET: api/<controller>/recipeingredient/1
        [HttpGet("recipeingredient/{id}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public async Task<RecipeIngredient> GetRecipeIngredient(int id)
        {
            return await _recipeService.GetRecipeIngredientAsync(id, AuthEntityId(EntityNames.Module));
        }

        // POST: api/<controller>/recipeingredient
        [HttpPost("recipeingredient")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<RecipeIngredient> PostRecipeIngredient([FromBody] RecipeIngredient recipeIngredient)
        {
            if (ModelState.IsValid && IsAuthorizedEntityId(EntityNames.Module, recipeIngredient.ModuleId))
            {
                return await _recipeService.AddRecipeIngredientAsync(recipeIngredient);
            }
            return null;
        }

        // PUT: api/<controller>/recipeingredient/1
        [HttpPut("recipeingredient/{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<RecipeIngredient> PutRecipeIngredient(int id, [FromBody] RecipeIngredient recipeIngredient)
        {
            if (ModelState.IsValid && recipeIngredient.RecipeIngredientId == id && IsAuthorizedEntityId(EntityNames.Module, recipeIngredient.ModuleId))
            {
                return await _recipeService.UpdateRecipeIngredientAsync(recipeIngredient);
            }
            return null;
        }

        // DELETE: api/<controller>/recipeingredient/1
        [HttpDelete("recipeingredient/{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task DeleteRecipeIngredient(int id)
        {
            var recipeIngredient = await _recipeService.GetRecipeIngredientAsync(id, AuthEntityId(EntityNames.Module));
            if (recipeIngredient != null)
            {
                await _recipeService.DeleteRecipeIngredientAsync(id, recipeIngredient.ModuleId);
            }
        }

        // GET: api/<controller>/step/getbyrecipe/1
        [HttpGet("step/getbyrecipe/{recipeId}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public async Task<IEnumerable<StepDto>> GetSteps(int recipeId)
        {
            var steps = await _recipeService.GetStepsAsync(recipeId, AuthEntityId(EntityNames.Module));
            return steps.Select(MapStepToDto).ToList();
        }

        // GET: api/<controller>/step/1
        [HttpGet("step/{id}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public async Task<Step> GetStep(int id)
        {
            return await _recipeService.GetStepAsync(id, AuthEntityId(EntityNames.Module));
        }

        // POST: api/<controller>/step
        [HttpPost("step")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<Step> PostStep([FromBody] Step step)
        {
            if (ModelState.IsValid && IsAuthorizedEntityId(EntityNames.Module, step.ModuleId))
            {
                return await _recipeService.AddStepAsync(step);
            }
            return null;
        }

        // PUT: api/<controller>/step/1
        [HttpPut("step/{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<Step> PutStep(int id, [FromBody] Step step)
        {
            if (ModelState.IsValid && step.StepId == id && IsAuthorizedEntityId(EntityNames.Module, step.ModuleId))
            {
                return await _recipeService.UpdateStepAsync(step);
            }
            return null;
        }

        // DELETE: api/<controller>/step/1
        [HttpDelete("step/{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task DeleteStep(int id)
        {
            var step = await _recipeService.GetStepAsync(id, AuthEntityId(EntityNames.Module));
            if (step != null)
            {
                await _recipeService.DeleteStepAsync(id, step.ModuleId);
            }
        }


        [HttpPost("resize-image")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<Oqtane.Models.File> ResizeImage([FromBody] dynamic request)
        {
            int fileId = (int)request.FileId;
            int width = (int)request.Width;
            int height = (int)request.Height;
            int moduleId = (int)request.ModuleId;

            if (IsAuthorizedEntityId(EntityNames.Module, moduleId))
            {
                return await _recipeService.ResizeImageAsync(fileId, width, height, moduleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Image Resize Attempt {FileId}", fileId);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }



        // Mapping methods


        private RecipeDto MapRecipeToDto(GIBS.Module.Recipe.Models.Recipe recipe)
        {
            if (recipe == null) return null;
            return new RecipeDto
            {
                RecipeId = recipe.RecipeId,
                ModuleId = recipe.ModuleId,
                Name = recipe.Name,
                Description = recipe.Description,
                Instructions = recipe.Instructions,
                Servings = recipe.Servings,
                PrepTime = recipe.PrepTime,
                CookTime = recipe.CookTime,
                ImageURL = recipe.ImageURL,
                IsFeatured = recipe.IsFeatured,
                IsActive = recipe.IsActive,
                CreatedBy = recipe.CreatedBy,
                CreatedOn = recipe.CreatedOn,
                ModifiedBy = recipe.ModifiedBy,
                ModifiedOn = recipe.ModifiedOn,
                Steps = recipe.Steps?.Select(s => new StepDto
                {
                    StepId = s.StepId,
                    ModuleId = s.ModuleId,
                    RecipeId = s.RecipeId,
                    Name = s.Name,
                    Instructions = s.Instructions,
                    ImageURL = s.ImageURL,
                    CreatedBy = s.CreatedBy,
                    CreatedOn = s.CreatedOn
                }).ToList(),
                Ingredients = recipe.RecipeIngredients?.Select(ri => new RecipeIngredientDto
                {
                    RecipeIngredientId = ri.RecipeIngredientId,
                    ModuleId = ri.ModuleId,
                    RecipeId = ri.RecipeId,
                    IngredientId = ri.IngredientId,
                    Quantity = ri.Quantity,
                    UnitId = ri.UnitId,
                    Notes = ri.Notes,
                    IngredientName = ri.Ingredient?.Name,
                    UnitName = ri.Unit?.Name,
                    CreatedBy = ri.CreatedBy,
                    CreatedOn = ri.CreatedOn,
                    ModifiedBy = ri.ModifiedBy,
                    ModifiedOn = ri.ModifiedOn
                }).ToList()
            };
        }

        private RecipeDto MapDtoToRecipe(RecipeDto dto)
        {
            if (dto == null) return null;
            return new RecipeDto
            {
                RecipeId = dto.RecipeId,
                ModuleId = dto.ModuleId,
                Name = dto.Name,
                Description = dto.Description,
                Instructions = dto.Instructions,
                Servings = dto.Servings,
                PrepTime = dto.PrepTime,
                CookTime = dto.CookTime,
                ImageURL = dto.ImageURL,
                IsFeatured = dto.IsFeatured,
                IsActive = dto.IsActive,
                CreatedBy = dto.CreatedBy,
                CreatedOn = dto.CreatedOn,
                ModifiedBy = dto.ModifiedBy,
                ModifiedOn = dto.ModifiedOn
                // Steps and Ingredients are handled separately
            };
        }

        private RecipeIngredientDto MapRecipeIngredientToDto(RecipeIngredient ri)
        {
            if (ri == null) return null;
            return new RecipeIngredientDto
            {
                RecipeIngredientId = ri.RecipeIngredientId,
                ModuleId = ri.ModuleId,
                RecipeId = ri.RecipeId,
                IngredientId = ri.IngredientId,
                Quantity = ri.Quantity,
                UnitId = ri.UnitId,
                Notes = ri.Notes,
                IngredientName = ri.Ingredient?.Name,
                UnitName = ri.Unit?.Name,
                CreatedBy = ri.CreatedBy,
                CreatedOn = ri.CreatedOn,
                ModifiedBy = ri.ModifiedBy,
                ModifiedOn = ri.ModifiedOn
            };
        }

        private RecipeIngredient MapDtoToRecipeIngredient(RecipeIngredientDto dto)
        {
            if (dto == null) return null;
            return new RecipeIngredient
            {
                RecipeIngredientId = dto.RecipeIngredientId,
                ModuleId = dto.ModuleId,
                RecipeId = dto.RecipeId,
                IngredientId = dto.IngredientId,
                Quantity = dto.Quantity,
                UnitId = dto.UnitId,
                Notes = dto.Notes,
                CreatedBy = dto.CreatedBy,
                CreatedOn = dto.CreatedOn,
                ModifiedBy = dto.ModifiedBy,
                ModifiedOn = dto.ModifiedOn
            };
        }

        private StepDto MapStepToDto(Step step)
        {
            if (step == null) return null;
            return new StepDto
            {
                StepId = step.StepId,
                ModuleId = step.ModuleId,
                RecipeId = step.RecipeId,
                Name = step.Name,
                Instructions = step.Instructions,
                ImageURL = step.ImageURL,
                CreatedBy = step.CreatedBy,
                CreatedOn = step.CreatedOn
            };
        }

        private Step MapDtoToStep(StepDto dto)
        {
            if (dto == null) return null;
            return new Step
            {
                StepId = dto.StepId,
                ModuleId = dto.ModuleId,
                RecipeId = dto.RecipeId,
                Name = dto.Name,
                Instructions = dto.Instructions,
                ImageURL = dto.ImageURL,
                CreatedBy = dto.CreatedBy,
                CreatedOn = dto.CreatedOn
            };
        }

        [HttpGet("category")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public async Task<IEnumerable<Category>> GetCategories(string moduleid)
        {
            if (int.TryParse(moduleid, out int ModuleId) && IsAuthorizedEntityId(EntityNames.Module, ModuleId))
            {
                return await _recipeService.GetCategoriesAsync(ModuleId);
            }
            return null;
        }

        [HttpPost("category")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<Category> PostCategory([FromBody] Category category)
        {
            if (ModelState.IsValid && IsAuthorizedEntityId(EntityNames.Module, category.ModuleId))
            {
                return await _recipeService.AddCategoryAsync(category);
            }
            return null;
        }

        [HttpPut("category/{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<Category> PutCategory(int id, [FromBody] Category category)
        {
            if (ModelState.IsValid && category.CategoryId == id && IsAuthorizedEntityId(EntityNames.Module, category.ModuleId))
            {
                return await _recipeService.UpdateCategoryAsync(category);
            }
            return null;
        }

        [HttpDelete("category/{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task DeleteCategory(int id)
        {
            await _recipeService.DeleteCategoryAsync(id, AuthEntityId(EntityNames.Module));
        }

        [HttpGet("recipecategory/getbyrecipe/{recipeId}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public async Task<IEnumerable<RecipeCategory>> GetRecipeCategories(int recipeId)
        {
            return await _recipeService.GetRecipeCategoriesAsync(recipeId, AuthEntityId(EntityNames.Module));
        }

        [HttpGet("recipecategory/{id}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public async Task<RecipeCategory> GetRecipeCategory(int id)
        {
            return await _recipeService.GetRecipeCategoryAsync(id, AuthEntityId(EntityNames.Module));
        }

        [HttpPost("recipecategory")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<RecipeCategory> PostRecipeCategory([FromBody] RecipeCategory recipeCategory)
        {
            if (ModelState.IsValid && IsAuthorizedEntityId(EntityNames.Module, recipeCategory.ModuleId))
            {
                return await _recipeService.AddRecipeCategoryAsync(recipeCategory);
            }
            return null;
        }

        [HttpPut("recipecategory/{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task<RecipeCategory> PutRecipeCategory(int id, [FromBody] RecipeCategory recipeCategory)
        {
            if (ModelState.IsValid && recipeCategory.RecipeCategoryId == id && IsAuthorizedEntityId(EntityNames.Module, recipeCategory.ModuleId))
            {
                return await _recipeService.UpdateRecipeCategoryAsync(recipeCategory);
            }
            return null;
        }

        [HttpDelete("recipecategory/{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public async Task DeleteRecipeCategory(int id)
        {
            await _recipeService.DeleteRecipeCategoryAsync(id, AuthEntityId(EntityNames.Module));
        }

    }
}