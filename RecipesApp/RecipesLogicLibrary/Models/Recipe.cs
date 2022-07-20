using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipesLogicLibrary.Models
{
    public class Recipe
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Tittle { get; set; }
        [Required]
        public int TimeToComplete { get; set; }
        public int? Recipe_Quantity_Grams { get; set; }

        [Required]
        public string ExecutionMethod { get; set; }


    }
}
