namespace Palmfit.Data.Entities
{
    public class FoodClass : BaseEntity
    {
        public string Name { get; set; }
        public string Discription { get; set; }
        public ICollection<MealPlan> MealPlan { get; set; }

    }
}







































//      public string Name { get; set; }
//      public string Description { get; set; }
//      public string Details { get; set; }
//public ICollection<MealPlan> MealPlans { get; set; }

//public ICollection<Food> Foods { get; set; }
