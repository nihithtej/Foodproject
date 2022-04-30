using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcFood.Data;
using MvcFood.Models;
using System;
using System.Linq;

namespace MvcFood.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcFoodContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MvcFoodContext>>()))
            {
                // Look for any movies.
                //if (context.Food.Any())
                //{
                //    return;   // DB has been seeded
               // }

                //Food food1 = new Food();
                //food1.description = "apple";
                
                //context.Food.Add(food1);
                //context.SaveChanges();

                //Nutrient nutrient1 = new Nutrient();
                //nutrient1.nutrientName = "Carbohydrates";
                //nutrient1.nutrientNumber = 1;
                //context.Nutrient.Add(nutrient1);
                //context.SaveChanges();

                //Food_Nutrient fn1 = new Food_Nutrient();
                //fn1.value = 1;
                //fn1.unitName = "mg";
                //fn1.food = food1;
                //fn1.nutrient = nutrient1;
                //context.Food_Nutrient.Add(fn1);
                //context.SaveChanges();

            }
    }
        }
    }
