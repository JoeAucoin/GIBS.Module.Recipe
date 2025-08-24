using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIBS.Module.Recipe.Models.DTOs
{
    public class StepDto
    {
        public int StepId { get; set; }
        public int ModuleId { get; set; } // <-- Add this line
        public int RecipeId { get; set; }
        public string Name { get; set; }
        public string Instructions { get; set; }
        public string ImageURL { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
