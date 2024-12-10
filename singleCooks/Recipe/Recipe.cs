using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace cookBook;


public class Recipe{   

    [Key]
    public int RecipeId {get;set;}
    public string UserId {get;set;}

    private string _recipeName;
    public string RecipeName
        {
            get => _recipeName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Recipe name cannot be null or empty.");
                }
                _recipeName = value;
            }
        }

    private string _description;
    public string Description 
        { 
            get  => _description;
            set
            {
                if(string.IsNullOrWhiteSpace (value))
                {
                    throw new ArgumentException("Recipe description cannot be null or empty.");
                }
                _description = value;
            }
        }
    
    private int _preparationTime;
    public int PreparationTime 
        { 
            get => _preparationTime; 
            set
            {
                if(value < 1 || value >= 1000)
                {
                    throw new ArgumentException("Preparation time cannot be less than 1 minute or 1000 minutes and more");
                }
            }
        }

//inverse property annotation
    private int _cookingTime;
    public int CookingTime 
        { 
            get => _cookingTime; 
            set
            {
                if(value < 1 || value >= 1000)
                {
                    throw new ArgumentException("Cooking time cannot be less than 1 minute or 1000 minutes and more");
                }
            }
        }

    

    private int _servings;
    public int Servings 
        {
            get => _servings; 
            set
            {
                if(value < 1 || value > 50)
                {
                    throw new ArgumentException("Servings cannot be less than 1 nor more than 50.");
                }
            }
        }


    //need data validation 

    public List<Ingredient> Ingredients { get; set;}
    public List<Instruction> Instructions { get; set;}

    public List<Tag> RecipeTags { get; set; }
    public List<Rating> Ratings { get; set; } = new List<Rating>();
    public List<RegisteredUser> FavoritedBy {get;set;}



    private double _overallRating;
    public double OverallRating 
            { 
                get => _overallRating;
                set
                {
                    if (value >= 0 && value <= 5)
                    {
                        _overallRating = value;
                    }
                    else
                    {
                        throw new ArgumentException("Rating must be between 0 and 5.");
                    }
                }
            }

    public List<RecipeIngredient> RecipeIngredients { get; set; }
     
    
    public Recipe(){}
    
    public Recipe(string userId, string name, string description, List<Ingredient> ingredients, int prepTime, int cookingTime, int servings, List<Instruction> instructions,List<Tag> recipeTags)
    {
        RecipeIngredients = new List<RecipeIngredient>();
        UserId = userId;
        _recipeName = name;
        _description = description;
        Ingredients = ingredients;
        _preparationTime = prepTime;
        _cookingTime = cookingTime;
        _servings = servings;
        Instructions = instructions;
        OverallRating=0;
        RecipeTags = recipeTags;
    }
    
    public void UpdateRating()
    {
        double count = 0;
        double sum = 0;
        foreach (var rating in Ratings)
        {
            sum += rating.RatingValue;
            count++;
        }
        OverallRating = count == 0 ? 0 : sum / count;
    }


    public void AddRating(double rating){
        while (rating > 5 || rating < 1)
        {
            Console.WriteLine("Enter valid rating");
            rating = Convert.ToDouble(Console.ReadLine());
        }
        Ratings.Add(new Rating(rating));
        UpdateRating();
    }

  
    public string toString(){
        return $"Recipe: {RecipeName}, Description: {Description}, Prep Time: {PreparationTime}, Cooking Time: {CookingTime}";
    }
}
