using DataAccessLibrary.DataAccess;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipesLogicLibrary.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RecipesApi.Controllers
{
    [Route("myapi/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class RecipesController : ControllerBase
    {

        private readonly RecipesContext _context;

        public RecipesController(RecipesContext context)
        {
            _context = context;
        }

        /// <summary>
        ///  Get myapi/Recipes return all recipes
        /// </summary>
        /// <returns>Returns all recipes exist in database</returns>
        /// <response code="404">If nothing exist in the database</response>
        /// <response code="200">Returns all recipes</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> Get()
        {
            if (_context.RecipesTable.Count()<1 )
                return NotFound();

            return await _context.RecipesTable.ToListAsync();
        }


        /// <summary>
        ///  Get myapi/Recipes/id return specific recipe
        /// </summary>
        /// <returns>Returns selected recipe by the id you give</returns>
        /// <param name="id">Id</param>
        /// <response code="400">If the item is null or does not exist</response>
        /// <response code="200">Returns already Existing Recipe</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> Get(int id)
        {
            if (_context.RecipesTable is null)
                return NotFound();
            

            var recipe = await _context.RecipesTable.FindAsync(id);

            if (recipe is null)
                return NotFound();

            return recipe;
        }

        /// <summary>
        ///  POST myapi/Recipes
        ///  We create a new recipe
        /// </summary>
        /// <returns>A newly created Recipe</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST a recipe similar to this bellow if you like
        ///     (you can leave  empty recipe_Quantity_Grams is nullable)
        ///     {
        ///        "id": 7,
        ///        "tittle": "Pina colada",
        ///        "timeToComplete": 7,
        ///        "recipe_Quantity_Grams": null, 
        ///        "executionMethod": "Pulse all the ingredients along with a handful of ice in a blender until smooth. Pour into a tall glass and garnish as you like."
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null or wrong</response>
        [HttpPost]
        public async Task<ActionResult<Recipe>> Post([FromBody] Recipe value)
        {
            if (value is null)
                return NotFound();
            _context.RecipesTable.Add(value);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = value.Id }, value);
        }

        /// <summary>
        ///  Put myapi/Recipes update existing api
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT update a recipe similar to this bellow if you like
        ///     (you can leave  empty recipe_Quantity_Grams is nullable)
        ///     {
        ///        "id": 2,
        ///        "tittle": "Don't like Pancakes",
        ///       "timeToComplete": 0,
        ///       "recipe_Quantity_Grams": 0,
        ///        "executionMethod": "NO thanks :P"
        ///     }
        ///
        /// </remarks>
        /// <response code="200"> if operation is succeed</response>
        /// <response code="400">ids doesn't match</response>
        /// <response code="404"> If the value you give doesn't exist </response>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Recipe NewValue)
        {
            if (id != NewValue.Id)
                return BadRequest();

            var OldValue = await _context.RecipesTable.FindAsync(id);
            if (OldValue is null)
            {
               return NotFound();
            }
            
                OldValue.Recipe_Quantity_Grams = NewValue.Recipe_Quantity_Grams;
                OldValue.TimeToComplete = NewValue.TimeToComplete;
                OldValue.Tittle = NewValue.Tittle;
                OldValue.ExecutionMethod = NewValue.ExecutionMethod;
                await _context.SaveChangesAsync();
                return Ok();
            
        }




        /// <summary>
        ///  Delete myapi/Recipes/id delete specific recipe
        /// </summary>
        /// <param name="id">Id</param>
        /// <response code="404">If the item does not exist</response>
        /// <response code="200">If the delete was successfull</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var Value = await _context.RecipesTable.FindAsync(id);
            if (Value is null)
                return NotFound();

            _context.RecipesTable.Remove(Value);
            await _context.SaveChangesAsync();

            return Ok();

        }
    }
}
