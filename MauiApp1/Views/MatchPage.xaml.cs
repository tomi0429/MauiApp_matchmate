using MauiApp1.ViewModels;

namespace MauiApp1.Views
{
    public partial class MatchPage : ContentPage
    {
        public MatchPage()
        {
            InitializeComponent();
        }

        
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            
            if (BindingContext is MatchViewModel vm)
            {
                await vm.LoadMatchesAsync();
            }
        }
    }
}