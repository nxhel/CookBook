using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;

namespace cookBook;

    /// <summary>
    /// Interface for searching recipes.
    /// </summary>
    public interface ISearcher2{
        /// <summary>
        /// Searches for recipes based on specific criteria.
        /// </summary>
        /// <returns>A list of recipes that match the search criteria.</returns>
        public List<Recipe> Search2();

    }

    /// <summary>
    /// Searches for recipes by a specific user.
    /// </summary>
    public class UserSeacher2: ISearcher2{
        /// <summary>
        /// Gets or sets the recipe context.
        /// </summary>
        public RecipeContext rc {get;set;}

        /// <summary>
        /// Gets or sets the username for searching recipes.
        /// </summary>
        string UserName {get;set;}

        /// <summary>
        /// Initializes a new instance of the UserSeacher2 class.
        /// </summary>
        /// <param name="user">The username to search recipes for.</param>
        /// <param name="context">The recipe context.</param>
        /// <exception cref="ArgumentNullException">Thrown when the username is null or empty.</exception>
        public UserSeacher2(string user, RecipeContext context){
            if(user==null || user == "" || user == " "){
                throw new ArgumentNullException();
            }
            UserName=user;
            rc = context;
        }

        /// <summary>
        /// Searches for recipes by the specified user.
        /// </summary>
        /// <returns>A list of recipes created by the user.</returns>
        public List<Recipe> Search2(){
            List<Recipe> matchingRecipes = rc.Recipes
                .Where(r => r.UserId == UserName)
                .Include(r => r.Ingredients)
                    .ThenInclude(ingredient => ingredient.Nutrients)
                .Include(r => r.RecipeTags)
                .Include(r => r.Instructions)
                .ToList();
                return matchingRecipes;
            }
    }


    /// <summary>
    /// Searches for recipes containing a specific keyword.
    /// </summary>
    public class WordSeacher2: ISearcher2
    {
        private string _keyword;
        /// <summary>
        /// Gets or sets the keyword for searching recipes.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown when the keyword is null or empty.</exception>
        public string Keyword
            {
                get => _keyword;
                set
                {
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        throw new ArgumentException("Keyword cannot be null or empty.");
                    }
                    _keyword = value;
                }
            }

        /// <summary>
        /// Gets or sets the recipe context.
        /// </summary>
        public RecipeContext rc {get;set;}

        /// <summary>
        /// Initializes a new instance of the WordSeacher2 class.
        /// </summary>
        /// <param name="keyword">The keyword to search for.</param>
        /// <param name="context">The recipe context.</param>
        public WordSeacher2(string keyword, RecipeContext context)
        {
            Keyword = keyword;
            rc = context;
        }

        /// <summary>
        /// Searches for recipes containing the specified keyword.
        /// </summary>
        /// <returns>A list of recipes containing the keyword.</returns>
        public List<Recipe> Search2()
        {
            List<Recipe> matchingRecipes = rc.Recipes
            .Where(r => (r.RecipeName.Contains(Keyword) || r.Description.Contains(Keyword)))
            .Include(r => r.Ingredients)
                .ThenInclude(ingredient => ingredient.Nutrients)
            .Include(r => r.RecipeTags)
            .Include(r => r.Instructions)
            .ToList();

        return matchingRecipes;

        }
    }
        

    /// <summary>
    /// Searches for recipes within a specified time range.
    /// </summary>
    public class TimeSeacher2: ISearcher2{
        //PROBLEM OCCURED 
        //for some reason this is return an empty list

        /// <summary>
        /// Gets or sets the minimum preparation and cooking time in minutes.
        /// </summary>
        public int minTimeMinutes {get; set;}

        /// <summary>
        /// Gets or sets the maximum preparation and cooking time in minutes.
        /// </summary>
        public int maxTimeMinutes {get; set;}

        /// <summary>
        /// Gets or sets the recipe context.
        /// </summary>
        public RecipeContext rc {get;set;}

        /// <summary>
        /// Initializes a new instance of the TimeSeacher2 class.
        /// </summary>
        /// <param name="minTimeMinutes">The minimum time in minutes.</param>
        /// <param name="maxTimeMinutes">The maximum time in minutes.</param>
        /// <param name="context">The recipe context.</param>
        public TimeSeacher2(int minTimeMinutes, int maxTimeMinutes, RecipeContext context)
        {
            this.minTimeMinutes = minTimeMinutes;
            this.maxTimeMinutes = maxTimeMinutes;
            rc = context;
        }

        /// <summary>
        /// Searches for recipes within the specified time range.
        /// </summary>
        /// <returns>A list of recipes within the specified time range.</returns>
        public List<Recipe> Search2(){
            //need to implement the code
            List<Recipe> matchingRecipes = 
                        (from r in rc.Recipes
                        where (r.PreparationTime +r.CookingTime) >= minTimeMinutes && (r.PreparationTime+r.CookingTime) <= maxTimeMinutes
                        select r).ToList();

            return matchingRecipes;
        }
}
        /// <summary>
        /// Searches for recipes with a minimum number of servings.
        /// </summary>
        public class PortionSeacher2: ISearcher2{
            /// <summary>
            /// Gets or sets the recipe context.
            /// </summary>
            public RecipeContext rc {get;set;}

            /// <summary>
            /// Gets or sets the minimum number of servings.
            /// </summary>
            int Portion {get;set;}

            /// <summary>
            /// Initializes a new instance of the PortionSeacher2 class.
            /// </summary>
            /// <param name="portion">The minimum number of servings.</param>
            /// <param name="context">The recipe context.</param>
            public PortionSeacher2(int portion, RecipeContext context){
                Portion=portion;
                rc = context;
            }

            /// <summary>
            /// Searches for recipes with the specified minimum number of servings.
            /// </summary>
            /// <returns>A list of recipes with at least the specified number of servings.</returns>
            public List<Recipe> Search2(){

                List<Recipe> matchingRecipes = 
                            (from r in rc.Recipes
                            where r.Servings >= Portion
                            select r).ToList();

                return matchingRecipes;
            
            }
        }

        /// <summary>
        /// Searches for recipes containing a specific ingredient.
        /// </summary>
        public class IngredientSearcher2 : ISearcher2{
            /// <summary>
            /// Gets or sets the recipe context.
            /// </summary>
            public RecipeContext rc {get;set;}

            /// <summary>
            /// Gets or sets the ingredient to search for.
            /// </summary>
            public string Ing {get;set;}
    
            
            /// <summary>
            /// Initializes a new instance of the <see cref="IngredientSearcher2"/> class.
            /// </summary>
            /// <param name="context">The recipe context.</param>
            /// <param name="ingredient">The ingredient to search for.</param>
            public IngredientSearcher2(RecipeContext context, string ingredient)
            {
                rc = context;
                Ing = ingredient;
            }

            /// <summary>
            /// Searches for recipes containing the specified ingredient.
            /// </summary>
            /// <returns>A list of recipes containing the ingredient.</returns>
            public List<Recipe> Search2()
            {
                List<Recipe> matchingRecipes = 
                (from r in rc.Recipes
                where (r.Ingredients.Any(i => i.IngredientName.Contains(Ing)))
                select r).ToList();

                return matchingRecipes;
            }
        }


    /// <summary>
    /// Searches for recipes with a specific tag.
    /// </summary>
     public class TagsSeacher2: ISearcher2{
        /// <summary>
        /// Gets or sets the recipe context.
        /// </summary>
         public RecipeContext rc {get;set;}

        /// <summary>
        /// Gets or sets the tag to search for.
        /// </summary>
         public string Tag{get;set;}


        /// <summary>
        /// Initializes a new instance of the TagsSeacher2 class.
        /// </summary>
        /// <param name="context">The recipe context.</param>
        /// <param name="tag">The tag to search for.</param>
         public TagsSeacher2(RecipeContext context, string tag)
         {
             rc = context;
             Tag = tag;
         }
            /// <summary>
            /// Searches for recipes with the specified tag.
            /// </summary>
            /// <returns>A list of recipes with the specified tag.</returns>
             public List<Recipe> Search2()
             {
                 List<Recipe> matchingRecipes = (
                    from recipe in rc.Recipes
                    where (recipe.RecipeTags.Any(i => i.TagName.Contains(Tag)))
                    select recipe).ToList();

                return matchingRecipes;
            }
        }

     

    /// <summary>
    /// Searches for recipes with a minimum rating.
    /// </summary>
     public class RatingSeacher: ISearcher2{

        /// <summary>
        /// Gets or sets the minimum rating.
        /// </summary>
         double MinRating {get;set;}

        /// <summary>
        /// Gets or sets the recipe context.
        /// </summary>
         public RecipeContext rc {get;set;}


        /// <summary>
        /// Initializes a new instance of the RatingSeacher class.
        /// </summary>
        /// <param name="minRating">The minimum rating.</param>
        /// <param name="context">The recipe context.</param>
         public RatingSeacher(double minRating, RecipeContext context){
             MinRating=minRating;
             rc = context;
         }

         /// <summary>
        /// Searches for recipes with at least the specified rating.
        /// </summary>
        /// <returns>A list of recipes with at least the specified rating.</returns>
         public List<Recipe> Search2(){
             List<Recipe> matchingRecipes = (
                from recipe in rc.Recipes
                where recipe.OverallRating >= MinRating
                select recipe
             ).ToList();
        
             return matchingRecipes;
         }
        }

// //need to ask the girl about this method!!!
// public class FavoritesSeacher2: ISearcher2{
//     public List<Recipe> Search(List<Recipe> allRecipes){
//         return null;
//         //need to implement the code
//     }
// }


