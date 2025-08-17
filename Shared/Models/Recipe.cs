using Oqtane.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GIBS.Module.Recipe.Models
{
    [Table("GIBSRecipe")]
    public class Recipe : IAuditable
    {
        [Key]
        public int RecipeId { get; set; }
        public int ModuleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public int Servings { get; set; }
        public string PrepTime { get; set; }
        public string CookTime { get; set; }
        public string ImageURL { get; set; }
        public string Slug { get; set; }
        public bool IsFeatured { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        // Add this navigation property
        public virtual ICollection<Step> Steps { get; set; }
    }
}
