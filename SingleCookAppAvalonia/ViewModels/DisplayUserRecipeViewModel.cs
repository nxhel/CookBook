using System.Reactive;
using System.Reactive.Linq;
using System.Linq;
using cookBook;
using ReactiveUI;
using SingleCookAppAvalonia.Models;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia;
using SingleCookAppAvalonia.Views;
using System.Collections.ObjectModel;

namespace SingleCookAppAvalonia.ViewModels
{
    public class DisplayUserRecipeViewModel : ViewModelBase
    {

        private readonly RecipeServices rs = RecipeServices.Instance;
        private readonly MainWindowViewModel MainWindow;

        public ObservableCollection<Recipe> UserRecipes { get; } = new ObservableCollection<Recipe>();

        public ReactiveCommand<Unit, Unit> GoBack { get; }
        public ReactiveCommand<Recipe, Unit> EditRecipeCommand { get; }
        public ReactiveCommand<Recipe, Unit> ViewRecipeCommand { get; }
        public ReactiveCommand<Recipe, Unit> DeleteRecipeCommand { get; }
        private Recipe _selectedRecipe;
        public Recipe SelectedRecipe
        {
            get => _selectedRecipe;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedRecipe, value);
            }
        }

        public DisplayUserRecipeViewModel(MainWindowViewModel mainWindowViewModel)
        {
            MainWindow=mainWindowViewModel;
            if(rs.user!=null){
                LoadUserRecipes();
            }
            EditRecipeCommand =  ReactiveCommand.Create<Recipe>(recipe => NavigateToEditRecipe(recipe));
            ViewRecipeCommand =  ReactiveCommand.Create<Recipe>(recipe => NavigateToViewRecipe(recipe));
            DeleteRecipeCommand = ReactiveCommand.Create<Recipe>(recipe => DeleteRecipe(recipe));
            GoBack = ReactiveCommand.Create(NavigateDashBoard);
        }

        private void NavigateDashBoard(){
            MainWindow.NavigateToDashBoard();
        }

     
        private void NavigateToEditRecipe(Recipe recipe){
            MainWindow.NavigateToRecipeEdit(recipe);
        }
        private void NavigateToViewRecipe(Recipe recipe){
            MainWindow.NavigateToRecipeDetails(recipe);
        }

        public void DeleteRecipe(Recipe recipe)
        {
            if(recipe != null)
            {
                rs.DeleteRecipe(recipe.RecipeId);
                LoadUserRecipes();
            }
        }

        private void LoadUserRecipes()
        {
            if (rs.user != null)
            {
                var userRecipes = rs.GetUserRecipe();

              
                UserRecipes.Clear();

                if (userRecipes.Any())
                {
                    // If there are recipes, add them to the collection
                    foreach (var recipe in userRecipes)
                    {
                        UserRecipes.Add(recipe);
                    }
                }
                else
                {
                
                    UserRecipes.Add(new Recipe
                    {
                        RecipeName = "No recipes found",
                        Description = "No recipes matching your criteria were found.",
                        
                    });
                }
            }
        }

    }
}
