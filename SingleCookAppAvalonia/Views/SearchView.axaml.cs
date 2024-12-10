using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using SingleCookAppAvalonia.ViewModels;
using cookBook; // Ensure this namespace is correct

namespace SingleCookAppAvalonia.Views
{
    public partial class SearchView : UserControl
    {
        public SearchView()
        {
            InitializeComponent();

            // // Create an instance of RecipeContext here or pass it as a parameter
            // RecipeContext context = RecipeContext.GetInstance();
            // DataContext = new SearchViewModel(context);
        }
    }
}
