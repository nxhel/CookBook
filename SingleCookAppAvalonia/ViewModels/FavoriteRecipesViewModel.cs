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
using System;

namespace SingleCookAppAvalonia.ViewModels
{
    public class FavoriteRecipesViewModel : ViewModelBase
    {

        MainWindowViewModel MainWindow;
        private RecipeServices rs = RecipeServices.Instance;

        private ObservableCollection<Recipe> _favoriteRecipes;
        public ObservableCollection<Recipe> FavoriteRecipes
        {
            get => _favoriteRecipes;
            set => this.RaiseAndSetIfChanged(ref _favoriteRecipes, value);
        }

        private Recipe _selectedRecipe;
        public Recipe SelectedRecipe
        {
            get => _selectedRecipe;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedRecipe, value);
            }
        }

        public ReactiveCommand<Recipe, Unit> RemoveFromFavoritesCommand { get; }
       
        public FavoriteRecipesViewModel(MainWindowViewModel mainWindowViewModel)
        {
            MainWindow=mainWindowViewModel;
            FavoriteRecipes = new ObservableCollection<Recipe>();
            LoadFavoriteRecipes();
            RemoveFromFavoritesCommand = ReactiveCommand.Create<Recipe>(recipe =>RemoveFromFavorites(recipe));
            this.WhenAnyValue(x => x.SelectedRecipe)
                .Subscribe(NavigateToShowRecipe);

        }

       

        private void LoadFavoriteRecipes()
        {
            var favoriteRecipesList = rs.GetFavouriteRecipes();
            FavoriteRecipes = new ObservableCollection<Recipe>(favoriteRecipesList);
        }


        private void RemoveFromFavorites(Recipe recipe)
        {
            rs.RemoveRecipeFromFavourites(recipe);
            FavoriteRecipes.Remove(recipe);
        }

        private void NavigateToShowRecipe(Recipe recipe)
        {
            MainWindow.NavigateToRecipeDetails(recipe);
        }
    }
}
