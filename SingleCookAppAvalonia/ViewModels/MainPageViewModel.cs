using System;
using System.Reactive;
using cookBook;
using ReactiveUI;

namespace SingleCookAppAvalonia.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel _mainWindowViewModel;
        RecipeServices rs =RecipeServices.Instance;

        public RegisterViewModel RegisterPage { get; private set; }
        public LoginViewModel LoginPage { get; private set; }
        public SearchViewModel SearchRecipesPage { get; private set; }
        public DashboardViewModel DashboardPage { get; private set; }
        public UpdateProfileViewModel UpdateProfilePage {get; set;}
        public DisplayUserRecipeViewModel UserRecipePage { get; private set; }
        public RecipeDetailsViewModel RecipePage { get; private set; }
        public FavoriteRecipesViewModel FavRecipePage { get; private set; }

       

        public ReactiveCommand<Unit, Unit> NavigateToRegisterPage { get; }
        public ReactiveCommand<Unit, Unit> NavigateToLogin { get; }
        public ReactiveCommand<Unit, Unit>  NavigateToSearchRecipes { get; }


                                                                               
        public MainPageViewModel(MainWindowViewModel mainWindowViewModel)
        {
            _mainWindowViewModel = mainWindowViewModel;
            NavigateToRegisterPage = ReactiveCommand.Create(() => _mainWindowViewModel.NavigateToRegisterPage());
            NavigateToSearchRecipes = ReactiveCommand.Create(() => _mainWindowViewModel.NavigateToSearchRecipes());
            NavigateToLogin = ReactiveCommand.Create(() => _mainWindowViewModel.NavigateToLogin());
            PrepareOtherPages();
        }
        private void PrepareOtherPages(){
            RegisterPage = new RegisterViewModel(_mainWindowViewModel);
            LoginPage = new LoginViewModel(_mainWindowViewModel);
            SearchRecipesPage = new SearchViewModel(_mainWindowViewModel);
            DashboardPage = new DashboardViewModel(_mainWindowViewModel, rs.user);
            UpdateProfilePage = new UpdateProfileViewModel(_mainWindowViewModel);
            UserRecipePage= new DisplayUserRecipeViewModel(_mainWindowViewModel);
            RecipePage = new RecipeDetailsViewModel(_mainWindowViewModel);
        }

        public DisplayUserRecipeViewModel NewDisplayRecipe(){
            return new DisplayUserRecipeViewModel(_mainWindowViewModel);
        }
        public DashboardViewModel NewDashBoard(){
            return new DashboardViewModel(_mainWindowViewModel,rs.user);
        }

        
    }
}
