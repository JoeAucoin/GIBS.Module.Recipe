using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Oqtane.Models;

namespace GIBS.Module.Recipe.Models
{
    [Table("GIBSRecipeTag")]
    public class RecipeTag : IAuditable
    {
        [Key]
        public int RecipeTagId { get; set; }
        public int ModuleId { get; set; }
        public int RecipeId { get; set; }
        public int TagId { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        // Navigation properties
        public virtual Recipe Recipe { get; set; }
        public virtual Tag Tag { get; set; }
    }
}