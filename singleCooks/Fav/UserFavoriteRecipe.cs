using System.ComponentModel.DataAnnotations;

namespace cookBook;

public class UserFavouriteRecipe{

    public UserFavouriteRecipe(){}
    [Key]
    public int UserFavoritesId {get;set;}
    public int UserId {get;set;}
    public int RecipeLikedId {get;set;}

    }
