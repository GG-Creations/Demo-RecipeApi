using Microsoft.EntityFrameworkCore;
using RecipesLogicLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.DataAccess
{
    public class RecipesContext : DbContext
    {
        public RecipesContext(DbContextOptions<RecipesContext> options) : base(options)
        {

        }

        public DbSet<Recipe> RecipesTable { get; set; }
        
    }
}
