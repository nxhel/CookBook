using System.Reactive;
using cookBook;
using ReactiveUI;
using System;
using SingleCookAppAvalonia.Models;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia;
using SingleCookAppAvalonia.Views;

namespace SingleCookAppAvalonia.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly MainWindowViewModel main;
        RecipeServices rs =RecipeServices.Instance;
        private static RegisteredUserManager userManager = new RegisteredUserManager(RecipeContext.GetInstance());  
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

        private string _loginStatus;
        public string LoginStatus
        {
            get => _loginStatus;
            set => this.RaiseAndSetIfChanged(ref _loginStatus, value);
        }

       
        public ReactiveCommand<Unit, Unit> Login { get; }

    
        public LoginViewModel(MainWindowViewModel mainWindowViewModel)
        {
           
            main=mainWindowViewModel;
            Login = ReactiveCommand.Create(LoginUser);
          
        }

        private void LoginUser()
        {
           bool User = MainClass.UserLogin(rs, Username, Password);
           if(User){
                var user = rs.user;
                GoDashboard(user);
            }else{
                LoginStatus = ":( Incorrect username or password.";
            }
        }

   

        private void GoDashboard(RegisteredUser user)
        {
            
            rs.user=user;
            main.NavigateToDashBoard();
        }

        public registeredUser? registeredUser { get; private set; }
        public guestUser? guest { get; private set; }

    }
}

