using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Oqtane.Models;

namespace GIBS.Module.Recipe.Models
{
    [Table("GIBSRecipe_Step")]
    public class Step : IAuditable
    {
        [Key]
        public int StepId { get; set; }
        public int ModuleId { get; set; }
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public string Instructions { get; set; }
        public string ImageURL { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        // Navigation property
        public virtual Recipe Recipe { get; set; }
    }
}