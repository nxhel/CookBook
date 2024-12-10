using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace cookBook;

public class RecipeIngredient{

    public int RecipeIngredientId {get;set;}
    public int RecipeId { get; set; }
    public Recipe Recipe { get; set; }
    public int IngredientId { get; set; }
    public Ingredient Ingredient { get; set; }
    public int Quantity { get; set; } // Additional column

    public RecipeIngredient(Recipe r, Ingredient i, int qty){
        Recipe =r;
        RecipeId = r.RecipeId;
        Ingredient=i;
        IngredientId= i.IngredientId;
        Quantity=qty;
    }

    public RecipeIngredient(){}
    // Add more columns as needed
}