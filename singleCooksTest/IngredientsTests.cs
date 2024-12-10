using System.Runtime.CompilerServices;
using cookBook;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace singleCooksTest;

//author: Nihel Madani-Fouatih

[TestClass]
public class IngredientsTests{


    [TestMethod]
    public void CreatingIngredient()
    {
        // Arrange
        Nutrient nutri1 = new Nutrient("Fiber", 1.2);
        Nutrient nutri2 = new Nutrient("Calcium", 0.4);
        Nutrient nutri3 = new Nutrient("IRON", 0.2);
        List<Nutrient> nutris = new List<Nutrient> { nutri1, nutri2, nutri3 };

        // Act
        Ingredient ingredient = new Ingredient("apple", nutris);

        // Arange
        Assert.AreEqual("apple",ingredient.IngredientName);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void IncorrectIngredientName_Constructor()
    {
        // Arrange
        Nutrient nutri1 = new Nutrient("Fiber", 1.2);
        Nutrient nutri2 = new Nutrient("Calcium", 0.4);
        Nutrient nutri3 = new Nutrient("IRON", 0.2);
        List<Nutrient> nutris = new List<Nutrient> { nutri1, nutri2, nutri3 };

        // Act
        Ingredient ingredient = new Ingredient("", nutris);

        // No need for an assertion here; 
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void IncorrectQuantity_Constructor()
    {
        // Arrange
        Nutrient nutri1 = new Nutrient("Fiber", 1.2);
        Nutrient nutri2 = new Nutrient("Calcium", 0.4);
        Nutrient nutri3 = new Nutrient("IRON", 0.2);
        List<Nutrient> nutris = new List<Nutrient> { nutri1, nutri2, nutri3 };

        // Act
        Ingredient ingredient = new Ingredient("", nutris);

        // No need for an assertion here; 
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NullNutrientList_Constructor()
    {
        // Arrange
        List<Nutrient> nutris = new List<Nutrient> ();

        // Act
        Ingredient ingredient = new Ingredient("", nutris);

        // No need for an assertion here; 
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void IncorrectIngredientName_Set()
    {
        // Arrange
        Nutrient nutri1 = new Nutrient("Fiber", 1.2);
        Nutrient nutri2 = new Nutrient("Calcium", 0.4);
        Nutrient nutri3 = new Nutrient("IRON", 0.2);
        List<Nutrient> nutris = new List<Nutrient> { nutri1, nutri2, nutri3 };

        // Act
        Ingredient ingredient = new Ingredient("apple", nutris);
        ingredient.IngredientName="";

        // No need for an assertion here; 
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void IncorrectQuantity_Set()
    {
        // Arrange
        Nutrient nutri1 = new Nutrient("Fiber", 1.2);
        Nutrient nutri2 = new Nutrient("Calcium", 0.4);
        Nutrient nutri3 = new Nutrient("IRON", 0.2);
        List<Nutrient> nutris = new List<Nutrient> { nutri1, nutri2, nutri3 };

        // Act
        Ingredient ingredient = new Ingredient("apple", nutris);
        //ingredient.Quantity=-100;

        // No need for an assertion here; 
    }

}
