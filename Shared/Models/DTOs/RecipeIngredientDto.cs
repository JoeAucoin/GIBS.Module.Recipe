using System;

namespace GIBS.Module.Recipe.Models.DTOs
{
    public class RecipeIngredientDto
    {
        public int RecipeIngredientId { get; set; }
        public int ModuleId { get; set; }
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public decimal Quantity { get; set; }
        public int UnitId { get; set; }
        public string Notes { get; set; }
        public string IngredientName { get; set; }
        public string UnitName { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}