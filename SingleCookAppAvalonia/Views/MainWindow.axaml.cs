using System;
using Avalonia.Controls;
using ReactiveUI;
using SingleCookAppAvalonia.ViewModels;

namespace SingleCookAppAvalonia.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
    }
}