using System.Reactive;
using cookBook;
using ReactiveUI;
using System;
using SingleCookAppAvalonia.Models;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia;
using SingleCookAppAvalonia.Views;
using System.Collections.ObjectModel;

namespace SingleCookAppAvalonia.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        public string Username { get; }
        public string Email { get; }
        public string Bio { get; }
        public ObservableCollection<Recipe> Recipes { get; private set; }

        private readonly MainWindowViewModel main;
        private RecipeServices rs = RecipeServices.Instance;

        public ReactiveCommand<Unit, Unit> CreateRecipeCommand { get; }
        public ReactiveCommand<Unit, Unit> LogoutCommand { get; }
        public ReactiveCommand<Unit, Unit> DeleteAccountCommand { get; }
        public ReactiveCommand<Unit, Unit> UpdateProfileCommand { get; }

        public ReactiveCommand<Unit, Unit> NavigateToSearchRecipes { get; }

        public ReactiveCommand<Unit, Unit> MyRecipesCommand { get; }
        public ReactiveCommand<Unit, Unit> MyFavoritesCommand { get; }

        public ReactiveCommand<Recipe, Unit> ViewRecipeCommand { get; }

      
        public static bool IsLogedIn=false;

        private Recipe _selectedRecipe;
        public Recipe SelectedRecipe
        {
            get => _selectedRecipe;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedRecipe, value);
            }
        }


        public DashboardViewModel(MainWindowViewModel mainWindowViewModel, RegisteredUser? user)
        {
            main = mainWindowViewModel;
            user=rs.user;
            if(user!=null){
                IsLogedIn=true;
            }
           
            Username = user?.Username ?? "Unknown";
            Email = user?.Email ?? "Unknown";
            Bio = user?.Bio ?? "Unknown";
            

            CreateRecipeCommand = ReactiveCommand.Create(NavigateToRecipeCreation);
            LogoutCommand = ReactiveCommand.Create(NavigateToLogout);
            DeleteAccountCommand = ReactiveCommand.Create(NavigateToDelete);
            UpdateProfileCommand = ReactiveCommand.Create(NavigeteToUpdate);
            NavigateToSearchRecipes = ReactiveCommand.Create(() => mainWindowViewModel.NavigateToSearchRecipes());
            MyRecipesCommand = ReactiveCommand.Create(NavigateToDisplayMyRecipe);
            MyFavoritesCommand = ReactiveCommand.Create(NavigateToDisplayMyFavorites);
           
           this.WhenAnyValue(x => x.SelectedRecipe)
                .Subscribe((recipe) => NavigateToShowRecipe(recipe));

            Recipes = new ObservableCollection<Recipe>(rs.GetTopThreePopularRecipes());
        }

        private void NavigateToShowRecipe(Recipe SelectedRecipe)
        {
            main.NavigateToRecipeDetails(SelectedRecipe);
        }


        private void NavigateToRecipeCreation()
        {
            main.NavigateToRecipeCreation();
        }

        private void NavigateToDisplayMyFavorites()
        {
           
            main.NavigateToDisplayMyFavorites();
        }


        private void NavigateToLogout()
        {
            rs.user = null;
            IsLogedIn=false;
            main.NavigateToLogin();
        }

        public void NavigateToDelete()
        {
            rs.DeleteUser(rs.user);
            main.NavigateToRegisterPage();
        }

        private void NavigeteToUpdate()
        {
            main.NavigateToUpdatePage();
        }
        private void NavigateToDisplayMyRecipe()
        {
            main.NewDisplayUserRecipe(); 
        }

    }
}
