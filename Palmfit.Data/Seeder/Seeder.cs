////using Microsoft.AspNetCore.Builder;
////using Microsoft.EntityFrameworkCore;
////using Microsoft.Extensions.DependencyInjection;
////using Palmfit.Data.AppDbContext;
////using Palmfit.Data.Entities;
////using Palmfit.Data.EntityEnums;

////namespace Palmfit.Data.Seeder
////{
////    public class Seeder
////    {
////        public static async Task SeedData(IApplicationBuilder app)
////        {
////            //Get db context
////            var context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<PalmfitDbContext>();

////            if ((await context.Database.GetPendingMigrationsAsync()).Any())
////            {
////                await context.Database.MigrateAsync();
////            }

////            var foodClasses = new List<FoodClass>();

//if (!context.FoodClasses.Any())
//{
//    // Seed data for FoodClasses
//    foodClasses = new List<FoodClass>
//                {
//                    new FoodClass
//                    {
//                        Id = Guid.NewGuid().ToString(),
//                        Name = "Beverages",
//                        Description = "Refreshing liquid options, including teas, juices, and drinks.",
//                        Details = " A diverse range of beverages, from energizing coffee to hydrating fruit juices.",
//                        IsDeleted = false,
//                        CreatedAt = DateTime.UtcNow,
//                    },
//                    new FoodClass
//                    {
//                        Id = Guid.NewGuid().ToString(),
//                        Name = "Breakfast Cereals",
//                        Description = "Quick and nutritious morning choices, often paired with milk or yogurt.",
//                        Details = "Cereals offer a variety of textures and flavors, packed with vitamins and minerals.",
//                        IsDeleted = false,
//                        CreatedAt = DateTime.UtcNow,
//                    },
//                    new FoodClass
//                    {
//                        Id = Guid.NewGuid().ToString(),
//                        Name = "Fish",
//                        Description = "Nutrient-rich seafood, from mild to flavorful options.",
//                        Details = "Fish provides lean protein, omega-3s, and versatility in cooking styles.",
//                        IsDeleted = false,
//                        CreatedAt = DateTime.UtcNow,
//                    },
//                    new FoodClass
//                    {
//                        Id = Guid.NewGuid().ToString(),
//                        Name = "Fruits",
//                        Description = "Natural, sweet delights in various forms and flavors.",
//                        Details = "Fruits offer vitamins, fiber, and antioxidants, perfect for snacks and desserts.",
//                        IsDeleted = false,
//                        CreatedAt = DateTime.UtcNow,
//                    },
//                    new FoodClass
//                    {
//                        Id = Guid.NewGuid().ToString(),
//                        Name = "Grains and Pasta",
//                        Description = "Staple foods like rice, grains, and pasta, suitable for many dishes.",
//                        Details = "Grains provide energy and are versatile foundations for diverse meals.",
//                        IsDeleted = false,
//                        CreatedAt = DateTime.UtcNow,
//                    },
//                    new FoodClass
//                    {
//                       Id = Guid.NewGuid().ToString(),
//                        Name = "Meats",
//                        Description = "Protein sources from different animals, ideal for hearty meals.",
//                        Details = "Meats offer protein variety, cooked to tender perfection in various recipes.",
//                        IsDeleted = false,
//                        CreatedAt = DateTime.UtcNow,
//                    },
//                    new FoodClass
//                    {
//                        Id = Guid.NewGuid().ToString(),
//                        Name = "Nuts and Seeds",
//                        Description = " Nutrient-dense snacks, rich in healthy fats and proteins.",
//                        Details = " Nuts and seeds offer satisfying crunch and valuable nutrients.",
//                        IsDeleted = false,
//                        CreatedAt = DateTime.UtcNow,
//                    },
//                    new FoodClass
//                    {
//                        Id = Guid.NewGuid().ToString(),
//                        Name = "Snacks",
//                        Description = "Quick bites for satisfying cravings, in sweet and savory forms.",
//                        Details = " Snacks range from crispy chips to guilt-free popcorn, perfect for anytime munching.",
//                        IsDeleted = false,
//                        CreatedAt = DateTime.UtcNow,
//                    },
//                    new FoodClass
//                    {
//                        Id = Guid.NewGuid().ToString(),
//                        Name = "Soups and Sauces",
//                        Description = "Flavor boosters like soups and sauces, enhancing meals.",
//                        Details = " Soups provide comfort, while sauces add depth and flavor to dishes.",
//                        IsDeleted = false,
//                        CreatedAt = DateTime.UtcNow,
//                    },

//                };
//    context.FoodClasses.AddRange(foodClasses);
//    await context.SaveChangesAsync();
//}

//if (!context.Foods.Any())
//{
//    // Seed data for Foods
//    var foods = new List<Food>
//            {
//                // Foods for Beverages
//                new Food
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    Name = "Coffee Instant With Whitener Reduced Calorie",
//                    Description = " Indulge in the rich taste of instant coffee with reduced calorie whitener.",
//                    Details = "A delightful blend of instant coffee and a creamy, reduced-calorie whitener.",
//                    Origin = "Coffee beans sourced from the finest plantations.",
//                    Image = "coffee.jpg",
//                    Calorie = 509,
//                    Carbs = 59.94M,
//                    Proteins = 1.96M,
//                    Fats = 29.1M,
//                    Unit = UnitType.Cup,
//                    FoodClassId = foodClasses[0].Id,
//                    IsDeleted = false,
//                    CreatedAt = DateTime.UtcNow,
//                },
//                new Food
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    Name = "Kraft Coffee Instant French Vanilla Cafe",
//                    Description = " Experience the elegance of French Vanilla Cafe in a convenient instant form.",
//                    Details = "A fragrant blend of instant coffee with the classic taste of French vanilla.",
//                    Origin = " Exquisite vanilla beans sourced from Madagascar.",
//                    Image = "french_vanilla_cafe.jpg",
//                    Calorie = 481,
//                    Carbs = 74.6M,
//                    Proteins = 2.5M,
//                    Fats = 19.2M,
//                    Unit = UnitType.Cup,
//                    FoodClassId = foodClasses[0].Id,
//                    IsDeleted = false,
//                    CreatedAt = DateTime.UtcNow,
//                },
//                // Additional foods for Beverages
//                new Food
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    Name = "Apple Juice",
//                    Description = "Savor the natural sweetness of pure apple juice.",
//                    Details = "Savor the natural sweetness of pure apple juice.",
//                    Origin = "Apples grown in orchards nestled in pristine countryside.",
//                    Image = "apple_juice.jpg",
//                    Calorie = 110,
//                    Carbs = 28.97M,
//                    Proteins = 0.25M,
//                    Fats = 0.28M,
//                    Unit = UnitType.Cup,
//                    FoodClassId = foodClasses[0].Id,
//                    IsDeleted = false,
//                    CreatedAt = DateTime.UtcNow,
//                },
//                new Food
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    Name = "Orange Juice",
//                    Description = "Experience the tangy refreshment of freshly squeezed orange juice.",
//                    Details = " Zesty orange juice made from ripe oranges bursting with flavor.",
//                    Origin = "Oranges cultivated in sunny groves near the Mediterranean.",
//                    Image = "orange_juice.jpg",
//                    Calorie = 112,
//                    Carbs = 25.79M,
//                    Proteins = 1.74M,
//                    Fats = 0.21M,
//                    Unit = UnitType.Cup,
//                    FoodClassId = foodClasses[0].Id,
//                    IsDeleted = false,
//                    CreatedAt = DateTime.UtcNow,
//                },
//                new Food
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    Name = "Green Tea",
//                    Description = "Enjoy the soothing and healthy qualities of green tea.",
//                    Details = "Nurtured by the tender care of tea artisans, this green tea offers a delicate and revitalizing taste.",
//                    Origin = " Green tea leaves handpicked from lush, organic tea gardens.",
//                    Image = "green_tea.jpg",
//                    Calorie = 2,
//                    Carbs = 0.47M,
//                    Proteins = 0.02M,
//                    Fats = 0.05M,
//                    Unit = UnitType.Cup,
//                    FoodClassId = foodClasses[0].Id,
//                    IsDeleted = false,
//                    CreatedAt = DateTime.UtcNow,
//                },
//                new Food
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    Name = "Corn Flakes",
//                    Description = "Delight in the crispy goodness of classic corn flakes.",
//                    Details = "A bowl of golden corn flakes, lightly toasted to perfection.",
//                    Origin = "Sustainably sourced corn from fertile fields.",
//                    Image = "corn_flakes.jpg",
//                    Calorie = 126,
//                    Carbs = 28.97M,
//                    Proteins = 1.9M,
//                    Fats = 0.2M,
//                    Unit = UnitType.Ounce,
//                    FoodClassId = foodClasses[1].Id,
//                    IsDeleted = false,
//                    CreatedAt = DateTime.UtcNow,
//                },
//                new Food
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    Name = "Oatmeal",
//                    Description = "Sustainably sourced corn from fertile fields.",
//                    Details = "Creamy oats slow-cooked to create a nourishing breakfast option.",
//                    Origin = "Premium oats harvested from heartland farms.",
//                    Image = "oatmeal.jpg",
//                    Calorie = 71,
//                    Carbs = 12.12M,
//                    Proteins = 2.41M,
//                    Fats = 1.42M,
//                    Unit = UnitType.Ounce,
//                    FoodClass = foodClasses[1],
//                    IsDeleted = false,
//                    CreatedAt = DateTime.UtcNow,
//                },
//                new Food
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    Name = "Granola",
//                    Description = "Indulge in the crunchiness of wholesome granola.",
//                    Details = " A mix of rolled oats, nuts, and dried fruits baked to a golden finish.",
//                    Origin = "Carefully chosen ingredients from nature's bounty.",
//                    Image = "granola.jpg",
//                    Calorie = 499,
//                    Carbs = 64.11M,
//                    Proteins = 9.44M,
//                    Fats = 22.75M,
//                    Unit = UnitType.Ounce,
//                    FoodClassId = foodClasses[1].Id,
//                    IsDeleted = false,
//                    CreatedAt = DateTime.UtcNow,
//                },
//                new Food
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    Name = "Tuna Salad",
//                    Description = "Revel in the protein-packed goodness of tuna salad.",
//                    Details = "Flaky tuna chunks mixed with fresh vegetables and zesty dressing.",
//                    Origin = "Responsibly caught tuna from pristine ocean waters.",
//                    Image = "tuna_salad.jpg",
//                    Calorie = 210,
//                    Carbs = 4.1M,
//                    Proteins = 20.3M,
//                    Fats = 13.4M,
//                    Unit = UnitType.Ounce,
//                    FoodClassId = foodClasses[2].Id,
//                    IsDeleted = false,
//                    CreatedAt = DateTime.UtcNow,
//                },
//                new Food
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    Name = "Salmon Sushi",
//                    Description = "Experience the delectable delight of salmon sushi.",
//                    Details = "Slices of premium salmon atop seasoned rice, a sushi lover's favorite.",
//                    Origin = "High-quality salmon sourced from sustainable fisheries.",
//                    Image = "salmon_sushi.jpg",
//                    Calorie = 304,
//                    Carbs = 38.2M,
//                    Proteins = 13.9M,
//                    Fats = 10.9M,
//                    Unit = UnitType.Piece,
//                    FoodClassId = foodClasses[2].Id,
//                    IsDeleted = false,
//                    CreatedAt = DateTime.UtcNow,
//                },
//                new Food
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    Name = "Shrimp Scampi",
//                    Description = "Enjoy the succulent taste of shrimp scampi.",
//                    Details = "Plump shrimp sautéed in garlic butter, a culinary masterpiece.",
//                    Origin = "Juicy shrimp harvested from ocean depths.",
//                    Image = "shrimp_scampi.jpg",
//                    Calorie = 367,
//                    Carbs = 5.4M,
//                    Proteins = 21.8M,
//                    Fats = 28.7M,
//                    Unit = UnitType.Ounce,
//                    FoodClassId = foodClasses[2].Id,
//                    IsDeleted = false,
//                    CreatedAt = DateTime.UtcNow,
//                },
//                new Food
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    Name = "Pineapple",
//                    Description = "Delight in the tropical sweetness of succulent pineapple.",
//                    Details = "Chunks of juicy pineapple, bursting with natural flavor.",
//                    Origin = "Pineapples grown in sun-kissed island plantations.",
//                    Image = "pineapple.jpg",
//                    Calorie = 50,
//                    Carbs = 13.12M,
//                    Proteins = 0.54M,
//                    Fats = 0.12M,
//                    Unit = UnitType.Cup,
//                    FoodClassId = foodClasses[3].Id,
//                    IsDeleted = false,
//                    CreatedAt = DateTime.UtcNow,
//                },
//                new Food
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    Name = "Mango",
//                    Description = " Experience the lusciousness of ripe and juicy mango.",
//                    Details = "A perfectly ripened mango, a tropical paradise in every bite.",
//                    Origin = "Mangoes handpicked from lush orchards.",
//                    Image = "mango.jpg",
//                    Calorie = 150,
//                    Carbs = 38.6M,
//                    Proteins = 1.6M,
//                    Fats = 0.6M,
//                    Unit = UnitType.Piece,
//                    FoodClassId = foodClasses[3].Id,
//                    IsDeleted = false,
//                    CreatedAt = DateTime.UtcNow,
//                },
//                new Food
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    Name = "Kiwi",
//                    Description = "Savor the tangy and vibrant flavor of kiwi.",
//                    Details = "A zesty kiwi, packed with vitamins and a refreshing taste.",
//                    Origin = " Kiwi fruits nurtured in lush green orchards.",
//                    Image = "kiwi.jpg",
//                    Calorie = 61,
//                    Carbs = 14.9M,
//                    Proteins = 1.1M,
//                    Fats = 0.5M,
//                    Unit = UnitType.Piece,
//                    FoodClass = foodClasses[3],
//                    IsDeleted = false,
//                    CreatedAt = DateTime.UtcNow,
//                },
//                new Food
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    Name = "Brown Rice",
//                    Description = "Enjoy the earthy goodness of nutritious brown rice.",
//                    Details = " Fluffy grains of brown rice, a wholesome addition to your meals.",
//                    Origin = "Brown rice cultivated through sustainable farming practices.",
//                    Image = "brown_rice.jpg",
//                    Calorie = 215,
//                    Carbs = 45.8M,
//                    Proteins = 5.0M,
//                    Fats = 1.8M,
//                    Unit = UnitType.Cup,
//                    FoodClassId = foodClasses[4].Id,
//                    IsDeleted = false,
//                    CreatedAt = DateTime.UtcNow,
//                },
//                new Food
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    Name = "Whole Wheat Bread",
//                    Description = "Embrace the heartiness of whole wheat bread.",
//                    Details = "Slices of whole wheat bread, perfect for a satisfying meal.",
//                    Origin = "Made from premium whole wheat grains.",
//                    Image = "whole_wheat_bread.jpg",
//                    Calorie = 128,
//                    Carbs = 25.2M,
//                    Proteins = 5.6M,
//                    Fats = 1.7M,
//                    Unit = UnitType.Piece,
//                    FoodClassId = foodClasses[4].Id,
//                    IsDeleted = false,
//                    CreatedAt = DateTime.UtcNow,
//                },
//                new Food
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    Name = "Quinoa",
//                    Description = "Discover the ancient grain's nutritional power with quinoa.",
//                    Details = "Nutty quinoa grains, a protein-rich alternative to traditional grains.",
//                    Origin = "Premium quinoa cultivated in high-altitude regions.",
//                    Image = "quinoa.jpg",
//                    Calorie = 222,
//                    Carbs = 39.4M,
//                    Proteins = 4.1M,
//                    Fats = 3.5M,
//                    Unit = UnitType.Cup,
//                    FoodClassId = foodClasses[4].Id,
//                    IsDeleted = false,
//                    CreatedAt = DateTime.UtcNow,
//                },
//                new Food
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    Name = "Popcorn (Air-Popped)",
//                    Description = "Enjoy guilt-free snacking with air-popped popcorn.",
//                    Details = "Light and airy popcorn, a wholesome treat for movie nights.",
//                    Origin = "Non-GMO popcorn kernels popped to perfection.",
//                    Image = "air_popped_popcorn.jpg",
//                    Calorie = 31,
//                    Carbs = 6.2M,
//                    Proteins = 1.0M,
//                    Fats = 0.4M,
//                    Unit = UnitType.Cup,
//                    FoodClassId = foodClasses[5].Id,
//                    IsDeleted = false,
//                    CreatedAt = DateTime.UtcNow,
//                },
//                new Food
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    Name = "Trail Mix",
//                    Description = "Nourish your body with a mix of energy-packed ingredients.",
//                    Details = " A blend of nuts, seeds, and dried fruits for on-the-go snacking.",
//                    Origin = "Carefully selected ingredients for a balanced snack.",
//                    Image = "trail_mix.jpg",
//                    Calorie = 693,
//                    Carbs = 60.5M,
//                    Proteins = 14.1M,
//                    Fats = 45.0M,
//                    Unit = UnitType.Cup,
//                    FoodClassId = foodClasses[5].Id,
//                    IsDeleted = false,
//                    CreatedAt = DateTime.UtcNow,
//                },
//                new Food
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    Name = "Potato Chips",
//                    Description = "Indulge in the crispy and savory delight of potato chips.",
//                    Details = "Thinly sliced and kettle-cooked potato chips, a satisfying treat.",
//                    Origin = " Locally grown potatoes transformed into crispy chips.",
//                    Image = "potato_chips.jpg",
//                    Calorie = 152,
//                    Carbs = 15.4M,
//                    Proteins = 2.0M,
//                    Fats = 9.8M,
//                    Unit = UnitType.Ounce,
//                    FoodClassId = foodClasses[5].Id,
//                    IsDeleted = false,
//                    CreatedAt = DateTime.UtcNow,
//                },
//                new Food
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    Name = "Apple Cider",
//                    Description = "Sip on the wholesome and comforting flavors of apple cider.",
//                    Details = "A blend of freshly pressed apples, capturing the essence of autumn.",
//                    Origin = "Apples harvested from orchards during the crisp apple-picking season.",
//                    Image = "apple_cider.jpg",
//                    Calorie = 117,
//                    Carbs = 28.9M,
//                    Proteins = 0.4M,
//                    Fats = 0.2M,
//                    Unit = UnitType.Cup,
//                    FoodClassId = foodClasses[0].Id,
//                    IsDeleted = false,
//                    CreatedAt = DateTime.UtcNow,
//                },

//                new Food
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    Name = "Granola Bars",
//                    Description = "Satisfy your cravings with the chewy goodness of granola bars.",
//                    Details = "A compact and portable snack made from oats, nuts, and dried fruits.",
//                    Origin = "Thoughtfully chosen ingredients that offer a burst of energy.",
//                    Image = "granola_bars.jpg",
//                    Calorie = 193,
//                    Carbs = 38.4M,
//                    Proteins = 2.7M,
//                    Fats = 5.1M,
//                    Unit = UnitType.Piece,
//                    FoodClassId = foodClasses[1].Id,
//                    IsDeleted = false,
//                    CreatedAt = DateTime.UtcNow,
//                },
//                new Food
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    Name = "Raisin Bran Cereal",
//                    Description = "Start your day with the nutritious and flavorful Raisin Bran cereal.",
//                    Details = "A mix of bran flakes and succulent raisins for a balanced breakfast.",
//                    Origin = "Premium raisins and whole grains for a wholesome morning.",
//                    Image = "raisin_bran_cereal.jpg",
//                    Calorie = 190,
//                    Carbs = 46.8M,
//                    Proteins = 4.7M,
//                    Fats = 1.5M,
//                    Unit = UnitType.Cup,
//                    FoodClassId = foodClasses[1].Id,
//                    IsDeleted = false,
//                    CreatedAt = DateTime.UtcNow,
//                },

//                // Additional foods for Beverages
//                new Food
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    Name = "Cranberry Juice",
//                    Description = "Enjoy the tangy and vibrant burst of cranberry juice.",
//                    Details = "Tart cranberries transformed into a refreshing and nutritious beverage.",
//                    Origin = "Cranberries sourced from bogs kissed by the cool breeze.",
//                    Image = "cranberry_juice.jpg",
//                    Calorie = 46,
//                    Carbs = 12.2M,
//                    Proteins = 0.4M,
//                    Fats = 0.1M,
//                    Unit = UnitType.Cup,
//                    FoodClassId = foodClasses[0].Id,
//                    IsDeleted = false,
//                    CreatedAt = DateTime.UtcNow,
//                },
//                new Food
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    Name = "Grape Juice",
//                    Description = "Savor the natural sweetness of luscious grape juice.",
//                    Details = "Grapes, handpicked at their peak, transformed into a delightful juice.",
//                    Origin = "Vineyards that produce grapes with the perfect balance of sweetness.",
//                    Image = "grape_juice.jpg",
//                    Calorie = 152,
//                    Carbs = 38.4M,
//                    Proteins = 1.0M,
//                    Fats = 0.3M,
//                    Unit = UnitType.Cup,
//                    FoodClassId = foodClasses[0].Id,
//                    IsDeleted = false,
//                    CreatedAt = DateTime.UtcNow,
//                },
//                new Food
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    Name = "Mango Smoothie",
//                    Description = "Experience the tropical delight of a creamy mango smoothie.",
//                    Details = "Blended mangoes and creamy goodness in a glass of pure happiness.",
//                    Origin = "Ripe and juicy mangoes sourced from exotic orchards.",
//                    Image = "mango_smoothie.jpg",
//                    Calorie = 207,
//                    Carbs = 51.9M,
//                    Proteins = 1.4M,
//                    Fats = 0.6M,
//                    Unit = UnitType.Cup,
//                    FoodClassId = foodClasses[0].Id,
//                    IsDeleted = false,
//                    CreatedAt = DateTime.UtcNow,
//                },
//                new Food
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    Name = "Trout Fillet",
//                    Description = "Delight in the exquisite flavors of tender trout fillet.",
//                    Details = "Boneless fillet carefully prepared to preserve the fish's delicate taste.",
//                    Origin = "Freshwater trout from clear, pristine rivers.",
//                    Image = "trout_fillet.jpg",
//                    Calorie = 168,
//                    Carbs = 0.0M,
//                    Proteins = 20.4M,
//                    Fats = 9.0M,
//                    Unit = UnitType.Ounce,
//                    FoodClassId = foodClasses[2].Id,
//                    IsDeleted = false,
//                    CreatedAt = DateTime.UtcNow,
//                },
//                new Food
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    Name = "Catfish Fillet",
//                    Description = "Experience the mild and delicate taste of catfish fillet.",
//                    Details = "Skinless fillet with a light flavor, perfect for various preparations.",
//                    Origin = "Sustainably sourced catfish from responsible fisheries.",
//                    Image = "catfish_fillet.jpg",
//                    Calorie = 105,
//                    Carbs = 0.0M,
//                    Proteins = 21.6M,
//                    Fats = 2.4M,
//                    Unit = UnitType.Ounce,
//                    FoodClassId = foodClasses[2].Id,
//                    IsDeleted = false,
//                    CreatedAt = DateTime.UtcNow,
//                },
//                new Food
//                {
//                    Id = Guid.NewGuid().ToString(),
//                    Name = "Halibut Steak",
//                    Description = "Savor the firm and meaty texture of halibut steak.",
//                    Details = "Thick steak cut from halibut, a versatile fish suitable for grilling or baking.",
//                    Origin = "Halibut caught from cold, pristine ocean waters.",
//                    Image = "halibut_steak.jpg",
//                    Calorie = 119,
//                    Carbs = 0.0M,
//                    Proteins = 24.0M,
//                    Fats = 2.0M,
//                    Unit = UnitType.Ounce,
//                    FoodClassId = foodClasses[2].Id,
//                    IsDeleted = false,
//                    CreatedAt = DateTime.UtcNow,
//                },
//            };
//    context.Foods.AddRange(foods);
//    await context.SaveChangesAsync();
//}
//        }
//    }
//}
