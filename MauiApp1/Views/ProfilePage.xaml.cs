using MauiApp1.Models;
using MauiApp1.Services;
using MauiApp1.ViewModels;
using Microsoft.Maui;
using Microsoft.Maui.Controls;



namespace MauiApp1.Views
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = new ProfileViewModel();
        }
     
      
    }
}