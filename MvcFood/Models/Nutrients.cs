using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MvcFood.Models
{


    public class Nutrients
    {
       public string nutrientName { get; set; }
        
        public float value { get; set; }

        public string unitName { get; set; }

    }

}
