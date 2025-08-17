using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using GIBS.Module.Recipe.Services;
using Oqtane.Controllers;
using System.Net;
using System.Threading.Tasks;
using GIBS.Module.Recipe.Models;

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
        public async Task<IEnumerable<Models.Recipe>> Get(string moduleid)
        {
            if (int.TryParse(moduleid, out int ModuleId) && IsAuthorizedEntityId(EntityNames.Module, ModuleId))
            {
                return await _recipeService.GetRecipesAsync(ModuleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Recipe Get Attempt {ModuleId}", moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // GET api/<controller>/5/1
        [HttpGet("{id}/{moduleid}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public async Task<Models.Recipe> Get(int id, int moduleid)
        {
            if (IsAuthorizedEntityId(EntityNames.Module, moduleid))
            {
                return await _recipeService.GetRecipeAsync(id, moduleid);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Recipe Get Attempt {RecipeId} {ModuleId}", id, moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
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
        public async Task<Models.Recipe> Put(int id, [FromBody] Models.Recipe recipe)
        {
            if (ModelState.IsValid && recipe.RecipeId == id && IsAuthorizedEntityId(EntityNames.Module, recipe.ModuleId))
            {
                return await _recipeService.UpdateRecipeAsync(recipe);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Recipe Put Attempt {Recipe}", recipe);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
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
        public async Task<IEnumerable<Step>> GetSteps(int recipeId)
        {
            return await _recipeService.GetStepsAsync(recipeId, AuthEntityId(EntityNames.Module));
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
    }
}