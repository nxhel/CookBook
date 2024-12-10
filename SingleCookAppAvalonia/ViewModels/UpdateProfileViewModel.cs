using cookBook;
using ReactiveUI;
using System;
using System.Reactive;

namespace SingleCookAppAvalonia.ViewModels
{
    public class UpdateProfileViewModel : ViewModelBase
    {
        RecipeServices rs = RecipeServices.Instance;

        MainWindowViewModel MainWindow;

        private string _username;
        private string _password;

        public string Username
        {
            get => _username;
            set => this.RaiseAndSetIfChanged(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        private string _email;
        private string _bio;

        public string Email
        {
            get => _email;
            set => this.RaiseAndSetIfChanged(ref _email, value);
        }

        public string Bio
        {
            get => _bio;
            set => this.RaiseAndSetIfChanged(ref _bio, value);
        }

        public ReactiveCommand<Unit, Unit> UpdateProfileCommand { get; }
        public ReactiveCommand<Unit, Unit> CancelCommand { get; }

        public UpdateProfileViewModel(MainWindowViewModel mainWindowViewModel)
        {
            MainWindow = mainWindowViewModel;
            UpdateProfileCommand = ReactiveCommand.Create(UpdateUser);
            CancelCommand = ReactiveCommand.Create(GoBackDashBoard);

            if (rs != null && rs.user != null)
            {
                Password = string.Empty;
                Email = rs.user.Email;
                Bio = rs.user.Bio;
                Username = rs.user.Username;
            }
        }

        private void UpdateUser()
        {
            
            MainClass.UpdateUserProfile(rs, Email, Bio, Password);
            Console.WriteLine("Profile updated");//Other way show message later
        }

        private void GoBackDashBoard()
        {
            MainWindow.NavigateToDashBoard();
        }
    }
}
