using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using cookBook;
namespace singleCooksTest;


[TestClass]
public class RegisteredUserManagerTests{

    [TestMethod]
    public void CreatingUser_Sucess()
    {
        // Arrange
        // Act
        RegisteredUser user;
        user= new RegisteredUser("nihel","123","email@","this project is fun");
        // Arange
        Assert.AreEqual("nihel",user.Username);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TestingSetName_User()
    {
        //Arrange
        RegisteredUser user;
        user= new RegisteredUser("nihel","123","email@","this project is fun");
        //Act
        user.Username="";
        //no Assert shoudl throwx exeptions
        
    }
    [TestMethod]
    public void TestifUserRegistered()
    {
        // Arrange
        var mockSet = new Mock<DbSet<RegisteredUser>>(); 
        var mockContext = new Mock<RecipeContext>();
        mockContext.Setup(m => m.Users).Returns(mockSet.Object);
        var userManager = new RegisteredUserManager(mockContext.Object);

        var username = "user1";
        var email = "user1@gmail.com";
        var password = "123";
        var bio = "hi";

        //Act
        userManager.RegisterUser(username, email, password, bio);

        // Assert
        mockContext.Verify(m => m.Add(It.IsAny<RegisteredUser>()), Times.Once);
        mockContext.Verify(m => m.SaveChanges(), Times.Once);
    }

    [TestMethod]
    public void TestIfDeletedUser()
    {
        // Arrange
        var data = new List<RegisteredUser>
        {
            new RegisteredUser { RegisteredUserId = 1, Username = "emma", Email = "email", Bio = "bio" }
        }.AsQueryable();

        var mockSet = new Mock<DbSet<RegisteredUser>>();
        mockSet.As<IQueryable<RegisteredUser>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<RegisteredUser>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<RegisteredUser>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<RegisteredUser>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        var mockContext = new Mock<RecipeContext>();
        mockContext.Setup(c => c.Users).Returns(mockSet.Object);

        var userManager = new RegisteredUserManager(mockContext.Object);

        // Act
        userManager.DeleteUser("emma");

        // Assert
        mockSet.Verify(m => m.Remove(It.IsAny<RegisteredUser>()), Times.Once);
        mockContext.Verify(m => m.SaveChanges(), Times.Once);
    }











    [TestMethod]
    //[ExpectedException(typeof(ArgumentException))]
    public void TestWithRegisteredUserName()
    {
        // Arrange
        var mockSet = new Mock<DbSet<RegisteredUser>>();
        var mockContext = new Mock<RecipeContext>();
        mockContext.Setup(m => m.Users).Returns(mockSet.Object);
        var userManager = new RegisteredUserManager(mockContext.Object);

        var data = new List<RegisteredUser>
        {
            new RegisteredUser { Username = "emma", Email = "email", Bio = "mybio" }
        }.AsQueryable();

        var username = "existing";
        var email = "user@example.com";
        var password = "password";
        var bio = "Existing bio";
        
        // Act & Assert
        userManager.RegisterUser(username, email, password, bio);

        mockContext.Verify(m => m.Add(It.IsAny<RegisteredUser>()), Times.Once);
        mockContext.Verify(m => m.SaveChanges(), Times.Once);
        
    }






















    
    


}