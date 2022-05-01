using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcFood.Models
{


    public class FoodNutrients
    {
        public Food_NutrientTable foodvm { get; set; }

        public NutrientTable nutrientvm { get; set; }



    }

    public class Viewmodel
    {
        public  IEnumerable<FoodNutrients> foodnutrients {get; set;}
        public string str{ get; set; }
        public string num { get; set; }
    }

}
