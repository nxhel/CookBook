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
    public class RecipeCreationViewModel : ViewModelBase
    {
        MainWindowViewModel MainWindow;
        private RecipeServices rs = RecipeServices.Instance;
        private string _username => rs.user?.Username ?? "Guest";
        public string RecipeName {get; set;}
        public string Description {get; set;}
        public ObservableCollection<IngredientViewModel> Ingredients {get; set;} = new ObservableCollection<IngredientViewModel>();
        public int PreparationTime {get; set;}
        public int CookingTime {get; set;}
        public int Servings {get; set;}
        public ObservableCollection<Instruction> Instructions {get; set;} = new ObservableCollection<Instruction>();
        public ObservableCollection<Tag> RecipeTags {get; set;} = new ObservableCollection<Tag>();

        //reactive commands
        public ReactiveCommand<Unit, Unit> CreateRecipeCommand {get;}
        public ReactiveCommand<Unit, Unit> AddIngredientCommand { get; }
        public ReactiveCommand<Unit, Unit> AddInstructionCommand { get; }
        public ReactiveCommand<Unit, Unit> AddTagCommand { get; }

        public string NewIngredientName { get; set; }

         private string _recipeStatus;
        public string RecipeStatus
        {
            get => _recipeStatus;
            set => this.RaiseAndSetIfChanged(ref _recipeStatus, value);
        }
        public string NewInstructionStep { get; set; }
        public string NewTag { get; set; }

        public RecipeCreationViewModel()
        {
            CreateRecipeCommand = ReactiveCommand.Create(CreateRecipe);
            AddIngredientCommand = ReactiveCommand.Create(AddIngredient);
            AddInstructionCommand = ReactiveCommand.Create(AddInstruction);
            AddTagCommand = ReactiveCommand.Create(AddTag);
        }

        public void CreateRecipe()
        {
            var ingredientsList = Ingredients
            .Select(ingredientVM => new Ingredient(ingredientVM.Name, ingredientVM.Nutrients.ToList()))
            .ToList();

            rs.CreateRecipe(
                username: _username,
                name: RecipeName,
                description: Description,
                ingredients: ingredientsList,
                prepTime: PreparationTime,
                cookingTime: CookingTime,
                servings: Servings,
                instructions: Instructions.ToList(),
                tags : RecipeTags.ToList()
            );

            RecipeStatus="Recipe Created Sucessfully";
        
        }

        private void AddIngredient()
        {
            if (!string.IsNullOrWhiteSpace(NewIngredientName))
            {
                Ingredients.Add(new IngredientViewModel(NewIngredientName));
                NewIngredientName = string.Empty; // Clear input field after adding
            }
        }

        private void AddInstruction()
        {
            if (!string.IsNullOrWhiteSpace(NewInstructionStep))
            {
                Instructions.Add(new Instruction(NewInstructionStep));
                NewInstructionStep = string.Empty; // Clear input field after adding
            }
        }

        private void AddTag()
        {
            if (!string.IsNullOrWhiteSpace(NewTag))
            {
                RecipeTags.Add(new Tag(NewTag));
                NewTag = string.Empty; // Clear input field after adding
            }
        }


        public class IngredientViewModel : ViewModelBase
        {
            public string Name { get; set; }
            public ObservableCollection<Nutrient> Nutrients { get; set; } = new ObservableCollection<Nutrient>();

            // Temporary properties for nutrient input
            public string NewNutrientName { get; set; }
            public double NewNutrientAmount { get; set; }

            public ReactiveCommand<Unit, Unit> AddNutrientCommand { get; }

            public IngredientViewModel(string name)
            {
                Name = name;
                AddNutrientCommand = ReactiveCommand.Create(AddNutrient);
            }

            // Add nutrient to the collection
            private void AddNutrient()
            {
                if (!string.IsNullOrWhiteSpace(NewNutrientName) && NewNutrientAmount > 0)
                {
                    Nutrients.Add(new Nutrient(NewNutrientName, NewNutrientAmount));
                    NewNutrientName = string.Empty;
                    NewNutrientAmount = 0;
                }
            }
        }
    }
}