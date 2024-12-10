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
    public class RecipeDetailsViewModel : ViewModelBase
    {

        private MainWindowViewModel _mainWindow;
        private RecipeServices rs = RecipeServices.Instance;
        
        private Recipe _recipe;
        public Recipe Recipe
        {
            get => _recipe;
            set => this.RaiseAndSetIfChanged(ref _recipe, value);
        }

         private double _rating;
        public double Rating
        {
            get => _rating;
            set => this.RaiseAndSetIfChanged(ref _rating, value);
        }

        public bool IsLoggedIn { get; private set;}

        public ObservableCollection<Recipe> FavoriteRecipes { get; set;}
        public ReactiveCommand<Recipe, Unit> AddToFavorites{ get; }

        public ReactiveCommand<Recipe, Unit> AddRating{ get; }

        
        public RecipeDetailsViewModel(MainWindowViewModel MainWindow, Recipe recipe)
        {
            _recipe=recipe;
            _mainWindow=MainWindow;
            IsLoggedIn = rs.user == null ? false : true;
            LoadFavoriteRecipes();
            AddToFavorites =  ReactiveCommand.Create<Recipe>(recipe => AddToFav(recipe));
            AddRating= ReactiveCommand.Create<Recipe>(recipe => AddRecipeRating(recipe));
            
        }

        private void LoadFavoriteRecipes()
        {
            var favoriteRecipesList = rs.GetFavouriteRecipes();
            FavoriteRecipes = new ObservableCollection<Recipe>(favoriteRecipesList);
        }
         public RecipeDetailsViewModel(MainWindowViewModel MainWindow)
        {
           _mainWindow=MainWindow;
            
        }
    
        private void AddToFav(Recipe recipe){
            if(rs.user ==null || DashboardViewModel.IsLogedIn==false){
                 _mainWindow.NavigateToLogin();
            }
            rs.AddRecipeToFavourites(recipe);
            _mainWindow.NavigateToDisplayMyFavorites();
        }

        private void AddRecipeRating(Recipe recipe){
            if(rs.user ==null || DashboardViewModel.IsLogedIn==false){
                 _mainWindow.NavigateToLogin();
            }
            recipe.AddRating(Rating);
            Console.WriteLine(Rating);
            Console.WriteLine(recipe.OverallRating);
            _mainWindow.NavigateToDashBoard();
            

            
        }

        

    }
}