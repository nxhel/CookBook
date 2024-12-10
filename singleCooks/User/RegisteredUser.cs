using System;
using System.Security.Cryptography;
namespace cookBook;

public class RegisteredUser : User{

    public int RegisteredUserId {get;set;}
    private string? _userName;
    public string? Username
    {
        get => _userName;
        set{
                if(string.IsNullOrEmpty(value)){
                    throw new ArgumentException("Username cannot be empty");
                }
                _userName = value;
            }
        }
    
    private string _email;
    public string Email
    {
        get => _email;
        set{
            if(string.IsNullOrEmpty(value)){
                    throw new ArgumentException("Email cannot be empty");
                }
                _email = value;
            }
        }
    

    private string _bio;
    public string Bio
    {
        get => _bio;
        set{
            if(string.IsNullOrEmpty(value)){
                    throw new ArgumentException("Bio cannot be empty");
                }
                _bio = value;
            }
    }
    

    private byte[] _passwordSalt;
    public byte[] PasswordSalt
    {
        get => _passwordSalt;
        set => _passwordSalt = value;
    }

    private byte[] _passwordHash;
    public byte[] PasswordHash
    {
        get => _passwordHash;
        set => _passwordHash = value;
    }

    //public Favourite FavouriteRecipes = new Favourite();

    public List<Recipe> userRecipes {get;set;}
    public List<UserFavouriteRecipe> favrecipes {get;set;}


    public RegisteredUser(string username,string password,string email,string bio)
    {
        _userName = username;
        _email = email;
        _bio = bio;
        SetPassword(password);
    }

    public RegisteredUser(){}

    private void SetPassword(string password)
    {
        _passwordSalt = GenerateSalt(8);
        _passwordHash = HashPassword(password,_passwordSalt,1000,32);
    }

    public override void ViewMenus()
    {
     //This will be implemented later
    }

    //This method returns true if user types the same password
    public bool VerifyPassword(string passwordToVerify)
    {
        if (passwordToVerify != null)
        {
            byte[] passwordToVerifyHash = HashPassword(passwordToVerify, _passwordSalt,1000,32);
            return passwordToVerifyHash.SequenceEqual(_passwordHash);
        }
        else
        {
            throw new ArgumentException("Please enter the password to verify");
        }

    }

    //This method helps the user to reset a password
    public void ResetPassword(string newPassword)
    {
        if (string.IsNullOrEmpty(newPassword))
        {
            throw new ArgumentException("Please don't leave the new password empty if you want to reset one!");
        }

        SetPassword(newPassword);
    }

    //THis method uses rngCsy to generate salt
    private byte[] GenerateSalt(int size)
    {
        byte[] salt = new byte[size];
        using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
        {
            rngCsp.GetBytes(salt);
        }
        return salt;
    }

    //This method uses generated salt to hash the password
    private byte[] HashPassword(string password, byte[] salt, int numIterations, int hashSize)
    {
        using (Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt, numIterations))
        {
            return key.GetBytes(hashSize);
        }
    }
}
