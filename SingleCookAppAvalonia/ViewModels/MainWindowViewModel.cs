﻿using System;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using singleCooks;
using SingleCookAppAvalonia.Views;
using cookBook;


namespace SingleCookAppAvalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        public RecipeServices rs =RecipeServices.Instance;
        public RecipeContext Context =RecipeContext.GetInstance();

        public MainPageViewModel mainPage;
        private object _content;
        public object Content
        {
            get => _content;
            set => this.RaiseAndSetIfChanged(ref _content, value);
        }

        public MainWindowViewModel()
        {
            NavToMainPage();
        }

        public void NavToMainPage()
        {
            if (mainPage== null){
                mainPage = new MainPageViewModel(this);
                Content = mainPage;
            }else{
                if(DashboardViewModel.IsLogedIn){
                    Content = mainPage.NewDashBoard();
                }else{
                    Content = mainPage;
                }
               
            }
        }

        public void NavigateToRegisterPage()
        {
            Content = mainPage.RegisterPage;
        
        }

        public void NavigateToLogin()
        {
            Content = mainPage.LoginPage;
        }
        public void NavigateToDashBoard(){
            Content =mainPage.NewDashBoard();
            
        }

        public void NavigateToSearchRecipes(){
            
             Content = mainPage.SearchRecipesPage;
        }

        public void NavigateToDisplayMyFavorites()
        {
            Content = new FavoriteRecipesViewModel(this);
        }

        public void NavigateToRecipeCreation()
        {
            Content = new RecipeCreationViewModel();
        }

        public void NavigateToRecipeEdit(Recipe recipe)
        {
           
            Content = new RecipeEditViewModel(recipe);
        }

        public void NavigateToRecipeDetails(Recipe recipe)
        {
           
            Content = new RecipeDetailsViewModel(this,recipe);
        }


        public void NavigateToLogout()
        {
            NavigateToRegisterPage();
        }

        public void NavigateToDelete()
        {
            rs.user = null;
            NavigateToRegisterPage();

        }


        public void NavigateDisplayUserRecipe()
        {

            Content = mainPage.UserRecipePage;

        }

        public void NewDisplayUserRecipe()
        {
            
            Content = mainPage.NewDisplayRecipe();

        }
        
       

        public void NavigateToUpdatePage()
        {
          
            Content = mainPage.UpdateProfilePage;
        }
    }
}

