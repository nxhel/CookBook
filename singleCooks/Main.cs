using System.Data.SqlTypes;
using System.DirectoryServices;
namespace cookBook{

    public class MainClass{
       // private static RegisteredUserManager userManager = new RegisteredUserManager(new List<RegisteredUser>());
        private static RegisteredUserManager userManager = new RegisteredUserManager(RecipeContext.GetInstance());  
        public static void Main (string[] args){
            RecipeServices rs = RecipeServices.Instance;
            List <Recipe> x= rs.GetAllRecipes();
            Console.WriteLine(x);
            bool logedIn = false;
            
            userManager.RegisterUser("emma2","123","emai","bio");
            registerNewUser(RecipeServices.Instance, "nini","12333","e2mai","bio2");

            //call login(), register(), 
            while(!logedIn){
                Console.WriteLine("Welcome to our CookBook! Would you like to (1. Login (2.Signup (3.Login As Guest (4.Exit? Please enter one of the options(words) for the operation.");
                string userOption = Console.ReadLine().Trim().ToLower();
                switch(userOption)
                {
                    case "login":
                        //logedIn = UserLogin(rs);
                        
                    break;

                    case "signup":
                        //registerNewUser(rs);
                        //logedIn = UserLogin(rs);
                    break;

                    // case "loginasguest":
                    //     GuestUser gu = new GuestUser();
                    //     gu.ViewMenus(sd);
                    //     Console.WriteLine("Please login or signup for more function, thank you :)");
                    // break;

                    case "exit":
                        ExitApplication();
                        logedIn=false;
                        break;

                    default:
                        Console.WriteLine("Invalide Option. Please try again by typing down the words.");
                        break;

                }
            }

            
                bool exitProgram = false;
                while(!exitProgram)
                {
                    Console.WriteLine("for recipe related actions press R OR User related actions press U, to quit press Q");
                    string action = Console.ReadLine().ToUpper();

                    switch(action)
                    {
                        case "R":
                            
                            break;
                        case "U":
                            //user actions here
                            //displayUserOperation(rs);
                            break;
                        case "Q":
                            exitProgram = true;
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
            
            
        }
        
        //used with the console
        private static void UpdateUserProfile(RecipeServices rs)
        {
            if(rs.user != null)
            {
                
                //finds the user based on username since unique
                Console.WriteLine("Enter your new username");
                string newUserName = Console.ReadLine();

                //sets new email
                Console.WriteLine("Enter your new email:");
                string newEmail = Console.ReadLine();

                Console.WriteLine("Enter your new bio:");
                string newBio = Console.ReadLine();

                //just in case we want to update 
                Console.WriteLine("Enter your new password:");
                string newPassword = Console.ReadLine();

                //logic for username why use the word new if you want to find the based on it
                userManager.UpdateUser(newUserName,newEmail,newPassword,newBio);

                //s.user.UpdateProfile(newBio,newEmail,newPassword);
                
                Console.WriteLine("Profile Uploaded!");
            }
            else
            {
                Console.WriteLine("No User is currently logged in...");
            }
        }
   
        /// <summary>
        /// Deletes the currently logged-in user after confirmation.
        /// </summary>
        /// <param name="rs">The RecipeServices instance containing the logged-in user.</param>
        private static void DeleteUser(RecipeServices rs)
        {
            if (rs.user != null)
            {
                Console.WriteLine("Are you sure you want to delete this account  {yes/no}");
                string confirmed = Console.ReadLine().Trim().ToLower();

                if( confirmed == "yes")
                {
                    userManager.DeleteUser(rs.user.Username);
                    rs.user = null;

                    Console.WriteLine("User account deleted successfully.");
                }
            }
            else
            {
                Console.WriteLine("No User is currently logged in...");
            }
        }

        /// <summary>
        /// Attempts to log in a user with the provided username and password.
            /// </summary>
        /// <param name="rs">The <see cref="RecipeServices"/> instance to update the logged-in user.</param>
        /// <param name="userName">The username of the user attempting to log in.</param>
        /// <param name="passWord">The password of the user attempting to log in.</param>
        /// <returns>Returns true if the login is successful; otherwise, false.</returns>
        public static bool UserLogin(RecipeServices rs, string userName, string passWord)
        {
            const int maxLoginAttempts = 3;
            int loginAttempts = 0;
            bool userVerifiedState = false;

            while(loginAttempts < maxLoginAttempts && !userVerifiedState)
            {
                // Console.WriteLine("Enter your username:");
                //string userName = Console.ReadLine();
               
                RegisteredUser user = FindUserByUsername(userName);
                
                if (user != null)
                {
                    // Console.WriteLine("Enter your password(note: 3 chances only):");
                    // string passWord = Console.ReadLine();

                    if(VerifyPassword(user,passWord))
                    {
                        userVerifiedState = true;
                        rs.user = user;
                        // Console.WriteLine("Login successful!");
                        return userVerifiedState;
                        //helper methods to display user actions after user state is logged in
                    }
                    else
                    {
                        // Console.WriteLine("Incorrect password");
                        return userVerifiedState;
                    }
                }
                 return userVerifiedState;
            }
            return userVerifiedState;
        }

             
           
       
        /// <summary>
        /// Finds a user by their username.
        /// </summary>
        /// <param name="userName">The username of the user to find.</param>
        /// <returns>Returns the RegisteredUser if found; otherwise, null.</returns>
        private static RegisteredUser FindUserByUsername(string userName)
        {
            return userManager.GetUsers().FirstOrDefault(user => user.Username.Equals(userName,StringComparison.OrdinalIgnoreCase));
        }


        /// <summary>
        /// Verifies the password of the given user.
        /// </summary>
        /// <param name="user">The RegisteredUser whose password is to be verified.</param>
        /// <param name="passWord">The password to verify.</param>
        /// <returns>Returns true if the password is correct; otherwise, false.</returns>
        public static bool VerifyPassword(RegisteredUser user, string passWord)
        {
            return user.VerifyPassword(passWord);
        }

        /// <summary>
        /// Finds a user by their email address.
        /// </summary>
        /// <param name="email">The email address of the user to find.</param>
        /// <returns>Returns the RegisteredUserif found; otherwise, null.</returns>
        private static RegisteredUser FindUserByEmail(string email)
        {
            return userManager.GetUsers().FirstOrDefault(user => user.Email.Equals(email,StringComparison.OrdinalIgnoreCase));
        }



        

        /// <summary>
        /// Registers a new user with the provided details.
        /// </summary>
        /// <param name="rs">The RecipeServices instance to update with the new user.</param>
        /// <param name="username">The username of the new user.</param>
        /// <param name="password">The password of the new user.</param>
        /// <param name="email">The email address of the new user.</param>
        /// <param name="bio">The bio of the new user.</param>
        public static void registerNewUser(RecipeServices rs,string username,string password,string email, string bio)
        {
        
            try{
                RegisteredUser u = FindUserByUsername(username);
                if( u != null)
                {
                    Console.WriteLine("USER 1");
                }
                else
                {
                    Console.WriteLine("USER 1");
                    userManager.RegisterUser(username,password,email,bio);
                    rs.user = new RegisteredUser(username, password, email, bio);
                }
            }catch(InvalidOperationException e)
            {
                throw new InvalidOperationException("Failed to register your account...");
            }

        }


        /// <summary>
        /// Offers the user the option to register a new account if they cannot log in.
        /// </summary>
        /// <param name="rs">The RecipeServices instance to update with the new registration.</param>
        private static void OfferUserRegeisteration(RecipeServices rs)
        {
            Console.WriteLine("You can no longer login again. Would you like to register for a new account? (y/n)");
            string choice = Console.ReadLine().Trim().ToLower();
            if(choice == "y")
            {
                //registerNewUser(rs);
            }
            else
            {
                //LoginAsGuest
                Console.WriteLine("Please login or sign up for more functions,thank you :)");
            }
        }
        
        /// <summary>
        /// Exits the application.
        /// </summary>
        private static void ExitApplication()
        {
            Console.WriteLine("You are exiting CookBook. Bye!");
            System.Environment.Exit(0);
        }


        /// <summary>
        /// Updates the profile of the currently logged-in user.
        /// </summary>
        /// <param name="rs">The RecipeServices instance containing the logged-in user.</param>
        /// <param name="newEmail">The new email address of the user.</param>
        /// <param name="newBio">The new bio of the user.</param>
        /// <param name="newPassword">The new password of the user.</param>
        public static void UpdateUserProfile(RecipeServices rs,  string newEmail, string newBio, string newPassword)
        {
            if (rs.user != null)
                {
                    // Update user information
                    //rs.user.Username = newUsername;
                    rs.user.Email = newEmail;
                    rs.user.Bio = newBio;

                    // Update password if provided
                    if (!string.IsNullOrEmpty(newPassword))
                    {
                        rs.user.ResetPassword(newPassword);
                    }

                    rs.Context.SaveChanges();

                    Console.WriteLine("Profile Updated!");
                }
                
            else
                {
                    Console.WriteLine("No User is currently logged in...");
                }
        }


        }
}

