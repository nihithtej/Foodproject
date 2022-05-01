using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcFood.Models
{
    
    
        public class FoodTable
        {
            [Key]
            public int fdcId { get; set; }
            public string description { get; set; }
            public List<Food_NutrientTable> food_nutrients { get; set; }
        }

        public class NutrientTable
        {
            [Key]
            public int nutrientId { get; set; }
            public string nutrientName { get; set; }
            //public int nutrientNumber { get; set; }
            public List<Food_NutrientTable> food_nutrients { get; set; }

        }

        public class Food_NutrientTable
        {
            [Key]
            public int FNId { get; set; }

            public float value { get; set; }

            public string unitName { get; set; }

            public FoodTable food { get; set; }

            public NutrientTable nutrient { get; set; }
        
        }
    
}
