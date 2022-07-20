using DataAccessLibrary.DataAccess;
using Microsoft.EntityFrameworkCore;
using RecipesLogicLibrary.Models;

namespace RecipesApi
{
    public class DataGenerator
    {

        private readonly RecipesContext context;

        public DataGenerator(RecipesContext context)
        {
            this.context = context;
        }

        public  void Initialize()
        {
            
                // Look for any data already in.
                if (context.RecipesTable.Any())
                {
                    return;   // Data was already seeded
                }


                context.RecipesTable.AddRange(
                    new Recipe
                    {
                        Id = 1,
                        Tittle = "Basic Omelette",
                        TimeToComplete = 4,
                        Recipe_Quantity_Grams = null,
                        
                        ExecutionMethod = "Whisk eggs, water, salt and pepper.Spray 8-inch (20 cm) non-stick skillet with cooking spray. Heat over medium heat. Pour in egg mixture. As eggs set around edge of skillet, with spatula, gently push cooked portions toward centre of skillet. Tilt and rotate skillet to allow uncooked egg to flow into empty spaces.When eggs are almost set on surface but still look moist, cover half of omelette with filling. Slip spatula under unfilled side; fold over onto filled half. Cook for a minute, then slide omelette onto plate."
                    },
                      new Recipe
                      {
                          Id = 2,
                          Tittle = "Good Old Fashioned Pancakes",
                          TimeToComplete = 20,
                          
                          ExecutionMethod = "In a large bowl, sift together the flour, baking powder, salt and sugar. Make a well in the center and pour in the milk, egg and melted butter; mix until smooth. Heat a lightly oiled griddle or frying pan over medium-high heat. Pour or scoop the batter onto the griddle, using approximately 1/4 cup for each pancake. Brown on both sides and serve hot."
                    });



                context.SaveChanges();
            }
        }
    }

