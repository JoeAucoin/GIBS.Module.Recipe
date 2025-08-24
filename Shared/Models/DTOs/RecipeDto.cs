using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GIBS.Module.Recipe.Models.DTOs;

namespace GIBS.Module.Recipe.Models.DTOs
{
    public class RecipeDto
    {
        public int RecipeId { get; set; }
        public int ModuleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public int Servings { get; set; }
        public string PrepTime { get; set; }
        public string CookTime { get; set; }
        public string ImageURL { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsActive { get; set; }
        // Add other simple properties as needed
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public List<StepDto> Steps { get; set; }
        public List<RecipeIngredientDto> Ingredients { get; set; }
    }
}
