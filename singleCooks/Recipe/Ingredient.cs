using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace cookBook;

public class Ingredient { 

    public int IngredientId{get;set;}
    public List<RecipeIngredient> RecipeIngredients { get; set; }
    
    private string _ingredientName;
    public string IngredientName {
        get => _ingredientName;
        set{
            if(string.IsNullOrEmpty(value)){
                throw new ArgumentException("Ingredient name cannot be empty");
            }
            _ingredientName = value;
        }
    }

    private List<Nutrient> _nutrients;
    public List<Nutrient> Nutrients {
        get => _nutrients;
        set {
            if (value == null || value.Count == 0)
                throw new ArgumentException("Nutrient list cannot be empty.");
            _nutrients = value;
        }}

    public Ingredient(){}

    public Ingredient(string ingredientName, List<Nutrient> nutrients){
        
        if (string.IsNullOrWhiteSpace(ingredientName))
        {
            throw new ArgumentException(nameof(ingredientName));
        }
        
        if (nutrients == null || nutrients.Count == 0)
        {
            throw new ArgumentException(nameof(nutrients));
        }
        _ingredientName = ingredientName;
        _nutrients = nutrients;
        
    }

     public string toString(){
        return _ingredientName;
    }
    

    // private double _quantity;
    // public double Quantity {
    //     get => _quantity;
    //     set{
    //         if (value < 0) {
    //             throw new ArgumentException("Quantity must be a positive number");
    //         }
    //         _quantity = value;
    //     }}
    

}
