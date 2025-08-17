using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Oqtane.Models;

namespace GIBS.Module.Recipe.Models
{
    [Table("GIBSRecipe_UserRecipe")]
    public class UserRecipe : IAuditable
    {
        [Key]
        public int UserRecipeId { get; set; }
        public int ModuleId { get; set; }
        public int UserId { get; set; }
        public int RecipeId { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}