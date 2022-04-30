namespace MvcFood.Models
{

    public class Foodresults
    {

        public Food[] foods { get; set; }
    }


    public class Food
    {
        public int fdcId { get; set; }
        public string description { get; set; }
        public Foodnutrient[] foodNutrients { get; set; }

    }

    public class Foodnutrient
    {
        public int nutrientId { get; set; }
        public string nutrientName { get; set; }
        public string nutrientNumber { get; set; }
        public string unitName { get; set; }
        public float value { get; set; }

    }
}