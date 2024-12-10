using System;
using System.Reactive;
using System.Reactive.Linq;
using cookBook;
using ReactiveUI;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia;
using SingleCookAppAvalonia.Views;
using SingleCookAppAvalonia.Models;

namespace SingleCookAppAvalonia.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        RecipeServices rs =RecipeServices.Instance;

        MainWindowViewModel MainWindow;

        private string _registertatus;
        public string RegisterStatus
        {
            get => _registertatus;
            set => this.RaiseAndSetIfChanged(ref _registertatus, value);
        }


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


        public ReactiveCommand<Unit, Unit> Register { get; }

        public ReactiveCommand<Unit, Unit> Cancel { get; }

        public RegisterViewModel(MainWindowViewModel mainWindowViewModel)
        {
            MainWindow = mainWindowViewModel;
            Register = ReactiveCommand.Create(RegisterNewUser);
            Cancel = ReactiveCommand.Create(GoMainWindow);
        }

        private void RegisterNewUser(){

            MainClass.registerNewUser(rs, Username, Password, Email, Bio);
            RegisterStatus="Welcome to SingleCooks";
            MainWindow.NavigateToLogin();
            
        }

         private void GoMainWindow()
        {
            MainWindow.NavToMainPage();
        }

        


        


    }
}