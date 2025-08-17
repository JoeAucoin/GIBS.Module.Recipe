using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Oqtane.Models;

namespace GIBS.Module.Recipe.Models
{
    [Table("GIBSRecipeCategory")]
    public class RecipeCategory : IAuditable
    {
        [Key]
        public int RecipeCategoryId { get; set; }
        public int ModuleId { get; set; }
        public int RecipeId { get; set; }
        public int CategoryId { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        // Navigation properties
        public virtual Recipe Recipe { get; set; }
        public virtual Category Category { get; set; }
    }
}