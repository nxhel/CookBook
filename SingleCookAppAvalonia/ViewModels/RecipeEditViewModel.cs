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
    public class RecipeEditViewModel : ViewModelBase
    {
        private RecipeServices rs = RecipeServices.Instance;
        public int RecipeId {get;set;}
        private string _username => rs.user?.Username ?? "Guest";
        private string _recipeName;
        public string RecipeName {
            get => _recipeName;
            set => this.RaiseAndSetIfChanged(ref _recipeName, value);
            }
        public string Description {get; set;}
        public ObservableCollection<IngredientViewModel> Ingredients {get; set;} = new ObservableCollection<IngredientViewModel>();
        public int PreparationTime {get; set;}
        public int CookingTime {get; set;}
        public int Servings {get; set;}
        public ObservableCollection<Instruction> Instructions {get; set;} = new ObservableCollection<Instruction>();
        public ObservableCollection<Tag> Tags {get; set;} = new ObservableCollection<Tag>();

        //reactive commands
        public ReactiveCommand<Unit, Unit> UpdateRecipeCommand {get;}
        public ReactiveCommand<Unit, Unit> AddIngredientCommand { get; }
        public ReactiveCommand<Unit, Unit> AddInstructionCommand { get; }
        public ReactiveCommand<Unit, Unit> AddTagCommand { get; }

        public string NewIngredientName { get; set; }
        public string NewInstructionStep { get; set; }
        public string NewTag { get; set; }

        public RecipeEditViewModel(Recipe recipe)
        {
            RecipeId = recipe.RecipeId;
            RecipeName = recipe.RecipeName;
            Description = recipe.Description;
            PreparationTime = recipe.PreparationTime;
            CookingTime = recipe.CookingTime;
            Servings = recipe.Servings;
            // Ingredients = new ObservableCollection<IngredientViewModel>(
            //     recipe.Ingredients.Select(ingredient => new IngredientViewModel(ingredient.IngredientName))
            // );
            // Instructions = new ObservableCollection<Instruction>(recipe.Instructions);
            // Tags = new ObservableCollection<cookBook.Tag>(recipe.RecipeTags);

            UpdateRecipeCommand = ReactiveCommand.Create(UpdateRecipe);
            //AddIngredientCommand = ReactiveCommand.Create(AddIngredient);
            // AddInstructionCommand = ReactiveCommand.Create(AddInstruction);
            // AddTagCommand = ReactiveCommand.Create(AddTag);
        }

        public void UpdateRecipe()
        {
            var ingredientsList = Ingredients
                .Select(ingredientVM => new Ingredient(ingredientVM.Name, ingredientVM.Nutrients.ToList()))
                .ToList();

            rs.ModifyRecipe(
                RecipeId,
                RecipeName,
                Description,
                // ingredientsList,
                PreparationTime,
                CookingTime,
                Servings
                // Instructions.ToList(),
                // Tags.ToList()
            );
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
                Tags.Add(new cookBook.Tag(NewTag));
                NewTag = string.Empty; // Clear input field after adding
            }
        }

        public class IngredientViewModel: ViewModelBase
        {
            public string Name { get; set; }
            public ObservableCollection<Nutrient> Nutrients { get; set; } = new ObservableCollection<Nutrient>();
            public string NewNutrientName { get; set; }
            public double NewNutrientAmount { get; set; }
            public ReactiveCommand<Unit, Unit> UpdateNutrientCommand { get; }

            public IngredientViewModel(string name)
            {
                Name = name;
                UpdateNutrientCommand = ReactiveCommand.Create(UpdateNutrient);
            }
            private void UpdateNutrient()
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