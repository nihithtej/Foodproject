using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcFood.Models
{


    public class FoodNutrients
    {
        public Food_Nutrient foodvm { get; set; }

        public Nutrient nutrientvm { get; set; }



    }

}
