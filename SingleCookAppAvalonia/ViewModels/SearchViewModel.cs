using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive;
using cookBook;
using System.Collections.Generic;

namespace SingleCookAppAvalonia.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        MainWindowViewModel MainWindow;
        private RecipeServices rs = RecipeServices.Instance;

        private string _keyword;
        public string Keyword
        {
            get => _keyword;
            set => this.RaiseAndSetIfChanged(ref _keyword, value);
        }

        private string _selectedSearchOption;
        public string SelectedSearchOption
        {
            get => _selectedSearchOption;
            set => this.RaiseAndSetIfChanged(ref _selectedSearchOption, value);
        }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get => _selectedIndex;
            set => this.RaiseAndSetIfChanged(ref _selectedIndex, value);
        }

        private ObservableCollection<Recipe> _searchResults;
        public ObservableCollection<Recipe> SearchResults
        {
            get => _searchResults;
            set => this.RaiseAndSetIfChanged(ref _searchResults, value);
        }

        private string _searchFounds;
        public string SearchFounds
        {
            get => _searchFounds;
            set => this.RaiseAndSetIfChanged(ref _searchFounds, value);
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

        private bool _isUserVisible;
        public bool IsUserVisible
        {
            get => _isUserVisible;
            set => this.RaiseAndSetIfChanged(ref _isUserVisible, value);
        }



        public ReactiveCommand<Unit, Unit> SearchCommand { get; }
       

        private RecipeContext _context = RecipeContext.GetInstance();

        public SearchViewModel(MainWindowViewModel mainWindowViewModel)
        {
            MainWindow=mainWindowViewModel;
            
            SearchResults = new ObservableCollection<Recipe>();
            SearchCommand = ReactiveCommand.Create(Search);

           

            this.WhenAnyValue(x => x.SelectedRecipe)
                .Subscribe((recipe) => NavigateToShowRecipe(recipe));
            

            // Bind SelectedIndex to SelectedSearchOption
            this.WhenAnyValue(x => x.SelectedIndex)
                .Subscribe(index =>
                {
                    switch (index)
                    {
                        case 0:
                            SelectedSearchOption = "Search by Recipe Name";
                            break;
                        case 1:
                            SelectedSearchOption = "Search by User";
                            break;
                        case 2:
                            SelectedSearchOption = "Search by Portion";
                            break;
                            
                        case 3:
                            SelectedSearchOption = "Search by Preparation Time";
                            break;
                        case 4:
                            SelectedSearchOption = "Search by Ingredient";
                            break;
                        case 5:
                            SelectedSearchOption = "Search by Tag";
                            break;
                        case 6:
                            SelectedSearchOption = "Search by Rate";
                            break;
                        default:
                            break;
                    }
                });

        }

        private void NavigateToShowRecipe(Recipe SelectedRecipe)
        {
            MainWindow.NavigateToRecipeDetails(SelectedRecipe);
        }
        


        private void Search()
        {
           
            IsUserVisible = rs.user != null;
           
            ISearcher2 searcher = CreateSearcher();
            List<Recipe> results ;

            if (searcher != null)
            {
                if (Keyword !=null || Keyword!=" " || Keyword!="" ){
                    results = searcher.Search2();
                    if(results.Count==0){
                        results = rs.GetAllRecipes();
                    }
                }else{
                    results = rs.GetAllRecipes();
                }
                SearchResults.Clear();
                foreach (var result in results)
                {
                    SearchResults.Add(result);
                }
                SearchFounds= $"Search completed. Found {SearchResults.Count} results";
            }
            
            else
            {
                SearchFounds="No Result for your Search Query";
                SearchResults=new ObservableCollection<Recipe>();
            }
        }

        private ISearcher2 CreateSearcher()
        {
            ISearcher2? searcher;

            switch (SelectedSearchOption)
            {
                case "Search by Recipe Name":
                    searcher = new WordSeacher2(Keyword, _context);
                    break;
                case "Search by User":
                    searcher = new UserSeacher2(Keyword, _context);
                    break;
                case "Search by Ingredient":
                    searcher = new IngredientSearcher2(_context,Keyword);
                    break;
                case "Search by Tag":
                    searcher = new TagsSeacher2(_context,Keyword);
                    break;
                case "Search by Portion":
                    if (int.TryParse(Keyword, out int portion))
                    {
                        searcher = new PortionSeacher2(portion, _context);
                    }
                    else
                    {
                        searcher = null;
                    }
                    break;
                case "Search by Preparation Time":
                    if (ParseTimeRange(Keyword, out int minTime, out int maxTime))
                    {
                        searcher = new TimeSeacher2(minTime, maxTime, _context);
                    }
                    else
                    {
                        searcher = null;
                    }
                    break;
                case "Search by Rate":
                    if (int.TryParse(Keyword, out int minRating))
                    {
                        searcher = new RatingSeacher(minRating, _context);
                    }
                    else
                    {
                        searcher = null;
                    }
                    break;
                default:
                    searcher = null;
                    break;
            }

            return searcher;
        }

        private bool ParseTimeRange(string input, out int minTime, out int maxTime)
        {
            minTime = 0;
            maxTime = 0;
            var parts = input.Split('-');
            if (parts.Length == 2 &&
                int.TryParse(parts[0], out minTime) &&
                int.TryParse(parts[1], out maxTime))
            {
                return true;
            }
            return false;
        }

       
    }
}
