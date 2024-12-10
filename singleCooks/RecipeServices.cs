using cookBook;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace cookBook;

    /// <summary>
    /// Class <c>RecipeServices</c> Provides various services for managing recipes and users.
    /// </summary>
    public class RecipeServices{
        
        /// <summary>
        /// Gets or sets the context for managing database operations.
        /// </summary>
         public RecipeContext Context {get;set;}

         /// <summary>
        /// Gets or sets the currently logged-in user.
        /// </summary>
         public RegisteredUser? user {get;set;}

         /// <summary>
        /// Provides an interface for searching capabilities.
        /// </summary>
         public ISearcher2 search {get;set;}

        private static RecipeServices? _instance = null;
        
        /// <summary>
        /// Prevents a default instance of the RecipeServices class from being created.
        /// Useful for dependency injections.
        /// </summary>
         private RecipeServices (){
            Context = RecipeContext.GetInstance();
            user = null;
         }

        /// <summary>
        /// Gets the singleton instance of the RecipeServices class.
        /// </summary>
         public static RecipeServices Instance{
            get{
                if (_instance == null){
                    _instance = new RecipeServices();
                }
                return _instance;
            }
        }
         
        /// <summary>
        /// Creates a new recipe and saves it to the database.
        /// </summary>
        /// <param name="username">The username of the recipe creator.</param>
        /// <param name="name">The name of the recipe.</param>
        /// <param name="description">The description of the recipe.</param>
        /// <param name="ingredients">The list of ingredients.</param>
        /// <param name="prepTime">The preparation time in minutes.</param>
        /// <param name="cookingTime">The cooking time in minutes.</param>
        /// <param name="servings">The number of servings.</param>
        /// <param name="instructions">The list of instructions.</param>
        /// <param name="tags">The list of tags.</param>
        /// <returns>The created recipe.</returns>
         public Recipe CreateRecipe(string username, string name, string description, List<Ingredient> ingredients, int prepTime, int cookingTime, int servings, List<Instruction> instructions,List<Tag> tags){
            var recipe = new Recipe(username, name,description,ingredients,prepTime, cookingTime,servings, instructions,tags);
            Context.Add(recipe);
            Context.SaveChanges();
            return recipe;
         }

        /// <summary>
        /// Retrieves the list of ingredients for a given recipe.
        /// </summary>
        /// <param name="recipe">The recipe to get ingredients for.</param>
        /// <returns>A list of ingredients.</returns>
         public List<Ingredient> GetRecipeIngredients(Recipe recipe)
         {
            var ingredients = new List<Ingredient>();
            foreach(Ingredient ingredient in recipe.Ingredients)
            {
                ingredients.Add(ingredient);
            }
            return ingredients;
         }

        /// <summary>
        /// Adds a given recipe to the database.
        /// </summary>
        /// <param name="r">The recipe to add.</param>
        /// <returns>The added recipe.</returns>
          public Recipe AddRecipe(Recipe r){
            Context.Recipes.Add(r);
            Context.SaveChanges();
            return r;
         }

        /// <summary>
        /// Adds a new user to the database.
        /// </summary>
        /// <param name="u">The user to add.</param>
        /// <returns>The added user.</returns>
         public User AddUser(RegisteredUser u){
            Context.Users.Add(u);
            Context.SaveChanges();
            return u;
         }


        /// <summary>
        /// Retrieves all recipes from the database, including related entities.
        /// </summary>
        /// <returns>A list of recipes.</returns>
        public List<Recipe> GetAllRecipes(){
            //in the slides -> List<Recipe> recipes = _db.Recipes.ToList<Recipe>();
            List<Recipe> recipes = Context.Recipes
                                        .Include(r => r.Ingredients)
                                            .ThenInclude(i => i.Nutrients)
                                        .Include(r => r.RecipeTags)
                                        .Include(r => r.Instructions)
                                        .Include(r => r.Ratings)
                                        .ToList();
             return recipes;
        }

        /// <summary>
        /// Modifies the details of an existing recipe identified by its ID.
        /// </summary>
        /// <param name="recipeId">The ID of the recipe to modify.</param>
        /// <param name="newName">The new name of the recipe.</param>
        /// <param name="newDescription">The new description of the recipe.</param>
        /// <param name="newPrepTime">The new preparation time in minutes.</param>
        /// <param name="newCookingTime">The new cooking time in minutes.</param>
        /// <param name="newServings">The new number of servings.</param>
         public void ModifyRecipe(int recipeId, string newName, string newDescription, int newPrepTime, int newCookingTime, int newServings){
            Recipe recipeToUpdate =  Context.Recipes.FirstOrDefault(r => r.RecipeId == recipeId) ?? throw new ArgumentException("Recipe not found.");
            recipeToUpdate.RecipeName = newName;
            recipeToUpdate.Description = newDescription;
            // recipeToUpdate.Ingredients = newIngredients;
            recipeToUpdate.PreparationTime = newPrepTime;
            recipeToUpdate.CookingTime = newCookingTime;
            recipeToUpdate.Servings = newServings;
            // recipeToUpdate.Instructions = newInstructions;
            // recipeToUpdate.RecipeTags = newTags;
            Context.SaveChanges();
         }

        /// <summary>
        /// Deletes a recipe identified by its ID from the database.
        /// </summary>
        /// <param name="recipeId">The ID of the recipe to delete.</param>
         public void DeleteRecipe(int recipeId)
         {
            //nutrients to delete
            //list ingredient to delete
            var recipeToDelete = Context.Recipes.FirstOrDefault(r => r.RecipeId == recipeId) ?? throw new ArgumentException("Recipe not found");

            Context.Recipes.Remove(recipeToDelete);
            Context.SaveChanges();
         }


        /// <summary>
        /// Adds a recipe to the logged-in user's list of favorite recipes if it is not already present.
        /// </summary>
        /// <param name="recipe">The recipe to add to favorites.</param>
        public void AddRecipeToFavourites(Recipe recipe)
        {
            if (user == null || recipe == null)
            {
                return; 
            }

            if(user.favrecipes == null)
            {
                user.favrecipes = new List<UserFavouriteRecipe>();
            }
            if (!user.favrecipes.Any(fr => fr.RecipeLikedId == recipe.RecipeId))
            {
                user.favrecipes.Add(new UserFavouriteRecipe{UserId = user.RegisteredUserId, RecipeLikedId = recipe.RecipeId});
                Context.SaveChanges();
            }
        }

        /// <summary>
        /// Removes a recipe from the logged-in user's list of favorite recipes.
        /// </summary>
        /// <param name="recipe">The recipe to remove from favorites.</param>
        public void RemoveRecipeFromFavourites(Recipe recipe)
        {
            if (user == null || recipe == null || user.favrecipes == null)
            {
                return; 
            }
            var favRecipe = user.favrecipes.FirstOrDefault(fr => fr.RecipeLikedId == recipe.RecipeId);
            if(favRecipe != null)
            {
                user.favrecipes.Remove(favRecipe);
                Context.SaveChanges();
            }
        }

    /// <summary>
    /// Retrieves the list of the logged-in user's favorite recipes.
    /// </summary>
    /// <returns>A list of favorite recipes.</returns>   
    public List<Recipe> GetFavouriteRecipes()
    {
        if(user == null || user.favrecipes == null)
        {
            return new List<Recipe>();
        }
        var favoriteRecipeIds = user.favrecipes.Select(fr => fr.RecipeLikedId).ToList();
        return Context.Recipes.Where(r => favoriteRecipeIds.Contains(r.RecipeId)).ToList();
    }

    /// <summary>
    /// Retrieves all recipes created by the logged-in user.
    /// </summary>
    /// <returns>A list of the user's recipes.</returns>
    public List<Recipe> GetUserRecipe()
    {
        List<Recipe> userRecipes = Context.Recipes
        .Where(recipe => recipe.UserId == user.Username)
        .Include(recipe => recipe.Ingredients)
            .ThenInclude(ingredient => ingredient.Nutrients)
        .Include(recipe => recipe.RecipeTags)
        .Include(recipe => recipe.Instructions)
        .Include(recipe => recipe.Ratings)
        .ToList();

    return userRecipes;
    }
    
    /// <summary>
    /// Deletes a user and all recipes associated with that user from the database.
    /// </summary>
    /// <param name="user">The user to delete.</param>
    public void DeleteUser(RegisteredUser user)
    {
        
        // get all recipes associated with the user
        var userRecipes = Context.Recipes.Where(r => r.UserId == user.Username).ToList();

        
        if (userRecipes.Any())
        {
            Context.Recipes.RemoveRange(userRecipes);
            Context.SaveChanges();
            //cant user remove
        }
        
        Context.Users.Remove(user);
        Context.SaveChanges();
        this.user=null;
        
    }

    /// <summary>
    /// Retrieves the top three recipes based on their overall rating.
    /// </summary>
    /// <returns>A list of the top three popular recipes.</returns>
    public List<Recipe> GetTopThreePopularRecipes()
    {
        return Context.Recipes
            .Include(r => r.Ingredients)
                .ThenInclude(ingredient => ingredient.Nutrients)
            .Include(r => r.RecipeTags)
            .Include(r => r.Instructions)
            .OrderByDescending(r => r.OverallRating)
            .Take(3)
            .ToList();
    }



}

