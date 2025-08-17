using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Oqtane.Models;

namespace GIBS.Module.Recipe.Models
{
    [Table("GIBSRecipe_Category")]
    public class Category : IAuditable
    {
        [Key]
        public int CategoryId { get; set; }
        public int ModuleId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
