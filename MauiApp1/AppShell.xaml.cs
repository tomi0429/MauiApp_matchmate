using MauiApp1.Services;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace MauiApp1;

public partial class AppShell : Shell
{
    public static AppShell Instance { get; private set; }

    public AppShell()
    {
        InitializeComponent();
        Instance = this;
        Routing.RegisterRoute(nameof(Views.QuizPage), typeof(Views.QuizPage));
        Routing.RegisterRoute(nameof(Views.MatchPage), typeof(Views.MatchPage));
        Routing.RegisterRoute(nameof(Views.UserDetailsPage), typeof(Views.UserDetailsPage));
    }

  
}
