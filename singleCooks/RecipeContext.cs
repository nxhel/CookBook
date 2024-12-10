using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace cookBook{
    /// <summary>
    /// Class <c>RecipeContext</c> Represents the database context for the cookbook application.
    /// </summary>
    public class RecipeContext : DbContext{
        /// <summary>
        /// Gets or sets the recipes in the database.
        /// </summary>
        public virtual DbSet<Recipe> Recipes {get;set;}

        /// <summary>
        /// Gets or sets the recipe ingredients in the database.
        /// </summary>
        public virtual DbSet<RecipeIngredient> RecipeIngredients { get; set; }

        /// <summary>
        /// Gets or sets the ingredients in the database.
        /// </summary>
        public virtual DbSet<Ingredient> Ingredients { get; set; } 

        /// <summary>
        /// Gets or sets the registered users in the database.
        /// </summary>
        public virtual DbSet<RegisteredUser> Users {get;set;}

        /// <summary>
        /// Gets or sets the tags in the database.
        /// </summary>
        public virtual DbSet<Tag> Tags {get;set;}

        /// <summary>
        /// Gets or sets the instructions in the database.
        /// </summary>
        public virtual DbSet<Instruction> Instructions {get;set;}

        /// <summary>
        /// Gets or sets the user favorite recipes in the database.
        /// </summary>
        public virtual DbSet<UserFavouriteRecipe> UserFavouriteRecipes { get; set; }
        private static RecipeContext? _instance = null;

        /// <summary>
        /// Initializes a new instance of the RecipeContext class.
        /// </summary>
        private RecipeContext()
        {
        }


        /// <summary>
        /// Gets the singleton instance of the RecipeContext class.
        /// </summary>
        /// <returns>The singleton instance of the RecipeContext class.</returns>
        public static RecipeContext GetInstance(){
            if (_instance == null){
                _instance = new RecipeContext();
                }
                return _instance;
            }
    
        /// <summary>
        /// Configures the database connection.
        /// </summary>
        /// <param name="optionsBuilder">The options builder used to configure the database connection.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            string oracleUser = Environment.GetEnvironmentVariable("ORA_USER");
            string oraclePassword = Environment.GetEnvironmentVariable("ORA_PASSWD");

            string connectionString = $@"User Id={oracleUser};
                                        Password={oraclePassword};
                                        Data Source=198.168.52.211:1521/pdbora19c.dawsoncollege.qc.ca;";

            optionsBuilder.UseOracle(connectionString);
        }
    }
}
