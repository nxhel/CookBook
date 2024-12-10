using System.Numerics;

namespace cookBook;

/// <summary>
/// Class <c>RegisteredUserManager</c>Manages registered users within the cookbook application.
/// </summary>
public class RegisteredUserManager

{
    private readonly RecipeContext _context;


    /// <summary>
    /// Initializes a new instance of the RegisteredUserManager class.
    /// </summary>
    /// <param name="context">The database context to be used by the manager.</param>
    public RegisteredUserManager(RecipeContext context)
    {
        _context = context;
    }

    
    /// <summary>
    /// Registers a new user in the database.
    /// </summary>
    /// <param name="username">The username of the new user.</param>
    /// <param name="email">The email address of the new user.</param>
    /// <param name="password">The password of the new user.</param>
    /// <param name="bio">The bio of the new user.</param>
    /// <exception cref="ArgumentException">Thrown if the username already exists.</exception>
    public void RegisterUser(string username, string email, string password,string bio)
    {

        // Check if the username already exists in the database
        var existingUser = _context.Users.FirstOrDefault(u => u.Username == username);
        if (existingUser != null)
        {
            throw new ArgumentException("Username already exists.");
        }

        // Create a new user
        var newUser = new RegisteredUser(username, email, password, bio);
        Console.WriteLine("USER 2");

        // Add the new user to the database
        _context.Add(newUser);
        _context.SaveChanges();

    }


    /// <summary>
    /// Updates an existing user's details in the database.
    /// </summary>
    /// <param name="username">The username of the user to update.</param>
    /// <param name="email">The new email address of the user.</param>
    /// <param name="password">The new password of the user.</param>
    /// <param name="bio">The new bio of the user.</param>
    /// <exception cref="ArgumentException">Thrown if the user is not found.</exception>
    public void UpdateUser(string username, string email, string password, string bio)
    {
        //ADD DATA VALIDATION FOR THE FIELDS PASSED
        // Find the user in the database
        var user = _context.Users.FirstOrDefault(u => u.Username == username);


        if (user != null)
        {
            // Update user email
            user.Email = email;
            user.Bio= bio;
            // do something for password?

            // Save changes to the database
            _context.SaveChanges();

        }

        else
        {
            throw new ArgumentException("User not found.");

        }

    }


    /// <summary>
    /// Deletes a user from the database.
    /// </summary>
    /// <param name="username">The username of the user to delete.</param>
    /// <exception cref="ArgumentException">Thrown if the user is not found.</exception>
    public void DeleteUser(string username)
    {
        // Find the user in the database
        var user = _context.Users.FirstOrDefault(u => u.Username == username);

        if (user != null)
        {

            // Remove the user from the database
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        else
        {

            throw new ArgumentException("User not found.");

        }

    }


    /// <summary>
    /// Gets a user by their username.
    /// </summary>
    /// <param name="username">The username of the user to retrieve.</param>
    /// <returns>The user with the specified username, or null if not found.</returns>
    public RegisteredUser GetUserByUsername(string username)
    {

        // Find the user in the database by username
        return _context.Users.FirstOrDefault(u => u.Username == username);

    }


    /// <summary>
    /// Gets a user by their ID.
    /// </summary>
    /// <param name="userId">The ID of the user to retrieve.</param>
    /// <returns>The user with the specified ID, or null if not found.</returns>
    public RegisteredUser GetUserById(int userId)
    {

        // Find the user in the database by user ID
        return _context.Users.FirstOrDefault(u => u.RegisteredUserId == userId);

    }


    /// <summary>
    /// Retrieves all users from the database.
    /// </summary>
    /// <returns>A list of all registered users.</returns>
    public List<RegisteredUser> GetUsers()

    {
        // Retrieve all users from the database
        return _context.Users.ToList();

    }



}