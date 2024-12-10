using System.ComponentModel.DataAnnotations;

namespace cookBook{

public class FavouriteRecipe{

    public FavouriteRecipe(){}

    [Key]
    public int FavoriteRecipeId {get;set;}
    public List<RegisteredUser>? users {get;set;}

    //public List<Recipe> FavRecipes = new List<Recipe>();



        // public void AddRecipe(Recipe recipe)
        // {

        //     if (!FavRecipes.Any(r => r.RecipeId == recipe.RecipeId))
        //     {
        //         FavRecipes.Add(recipe);
        //     }

        // }

        // public bool RemoveRecipe(int recipeId)
        // {
        //     var recipe = FavRecipes.FirstOrDefault(r => r.RecipeId == recipeId);
        //     if (recipe != null)
        //     {
        //         FavRecipes.Remove(recipe);
        //         return true;
        //     } 
        //     else{
        //         return false;
        //     }


        // }

        //more methods for favourite?
    }
}