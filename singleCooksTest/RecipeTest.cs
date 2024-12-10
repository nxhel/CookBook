using System.Runtime.CompilerServices;
using cookBook;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;

namespace singleCooksTest;

//author: Graciella Toulassi & nihel madani

[TestClass]
public class RecipeTest{


    [TestMethod]
    //[ExpectedException(typeof(ArgumentException))]
    public void CreateRecipe_Success()
    {
        //arange
        var mockSet = new Mock<DbSet<Recipe>>();
        var mockContext = new Mock<RecipeContext>();
        var service = RecipeServices.Instance;
        service.Context = mockContext.Object;
        mockContext.Setup(m => m.Recipes).Returns(mockSet.Object);
        var ingredients = new List<Ingredient>();
        var instructions = new List<Instruction>();
        var tags = new List<Tag>();
        service.user = new RegisteredUser { RegisteredUserId = 1, Username = "username"  };
        
        //act
        Recipe newRecipe = service.CreateRecipe("username", "name", "description", ingredients, 0, 0, 0, instructions, tags);
        
        //asert
        mockSet.Verify(m => m.Add(It.IsAny<Recipe>()), Times.Once);
        mockContext.Verify(m => m.SaveChanges(), Times.Once);
    }
        

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateRecipe_Wronginput()
    {
        //Arrange
        var mockSet = new Mock<DbSet<Recipe>>();
        var mockContext = new Mock<RecipeContext>();
        var service = RecipeServices.Instance;
        service.Context = mockContext.Object;
        mockContext.Setup(m => m.Recipes).Returns(mockSet.Object);

        var ingredients = new List<Ingredient>();
        var instructions = new List<Instruction>();
        var tags = new List<Tag>();

        //Act
        service.CreateRecipe("username", "name", "description", ingredients, 0, 0, 0, instructions, tags);
    }

    [TestMethod]
    public void GetRecipeIngredients_Success()
    {
        //Arrange
        var mockSet = new Mock<DbSet<Recipe>>();
        var mockContext = new Mock<RecipeContext>();
        var service = RecipeServices.Instance;
        service.Context = mockContext.Object;
        mockContext.Setup(m => m.Recipes).Returns(mockSet.Object);

        var ingredients = new List<Ingredient> { new Ingredient(), new Ingredient() };
        var recipe = new Recipe { Ingredients = ingredients };
        //Act
        var result = service.GetRecipeIngredients(recipe);

        //Assert
        Assert.AreEqual(2, result.Count);

    }

    [TestMethod]
    public void GetRecipeIngredients_EmptyIngredients()
    {
        //Arrange
        var mockSet = new Mock<DbSet<Recipe>>();
        var mockContext = new Mock<RecipeContext>();
        var service = RecipeServices.Instance;
        service.Context = mockContext.Object;
        mockContext.Setup(m => m.Recipes).Returns(mockSet.Object);

        var ingredients = new List<Ingredient>();
        var recipe = new Recipe { Ingredients = ingredients };
        //Act
        var result = service.GetRecipeIngredients(recipe);

        //Assert
        Assert.AreEqual(0, result.Count);

    }

    [TestMethod]
    public void AddRecipe_Success()
    {
        //Arrange
        var mockSet = new Mock<DbSet<Recipe>>();
        var mockContext = new Mock<RecipeContext>();
        var service = RecipeServices.Instance;
        service.Context = mockContext.Object;
        mockContext.Setup(m => m.Recipes).Returns(mockSet.Object);

        var recipe = new Recipe();

        //Act
        var result = service.AddRecipe(recipe);

        //Assert
        mockSet.Verify(m => m.Add(It.IsAny<Recipe>()), Times.Once);
        mockContext.Verify(m => m.SaveChanges(), Times.Once);
    }


    [TestMethod]
    public void GetAllRecipes_Success()
    {
        //Arrange
        var data = new List<Recipe>
        {
            new Recipe(),
            new Recipe(),
        }.AsQueryable();

        var mockSet = new Mock<DbSet<Recipe>>();
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        var mockContext = new Mock<RecipeContext>();
        var service = RecipeServices.Instance;
        service.Context = mockContext.Object;
        mockContext.Setup(m => m.Recipes).Returns(mockSet.Object);
        
        //Act
        var recipes = service.GetAllRecipes();

        //Assert
        Assert.AreEqual(2, recipes.Count);


    }

    [TestMethod]
    public void ModifyRecipe_Success()
    {
        //Arrange
        var mockSet = new Mock<DbSet<Recipe>>();
        var mockContext = new Mock<RecipeContext>();
        var service = RecipeServices.Instance;
        service.Context = mockContext.Object;
        var recipe = new Recipe
        {
            RecipeId = 1,
            RecipeName = "OldName",
            Description = "OldDescription",
            PreparationTime = 10,
            CookingTime = 20,
            Servings = 2
        };

        var data = new List<Recipe> {recipe}.AsQueryable();
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        mockContext.Setup(m => m.Recipes).Returns(mockSet.Object);

        //Act
        service.ModifyRecipe(1, "NewName", "NewDescription", 15, 25, 4);

        //Assert
        Assert.AreEqual("NewName", recipe.RecipeName);
        Assert.AreEqual("NewDescription", recipe.Description);
        Assert.AreEqual(15, recipe.PreparationTime);
        Assert.AreEqual(25, recipe.CookingTime);
        Assert.AreEqual(4, recipe.Servings);
        mockContext.Verify(m => m.SaveChanges(), Times.Once);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void ModifyRecipe_RecipeNotFound()
    {
        //Arrange
        var mockSet = new Mock<DbSet<Recipe>>();
        var mockContext = new Mock<RecipeContext>();
        var service = RecipeServices.Instance;
        service.Context = mockContext.Object;
        var data = new List<Recipe>().AsQueryable();

        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Recipe>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        mockContext.Setup(m => m.Recipes).Returns(mockSet.Object);

        //Act
        service.ModifyRecipe(1, "NewName", "NewDescription", 15, 25, 4);
    }
}
