using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Models;
using MauiApp1.Services;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls; 

namespace MauiApp1.ViewModels
{
    
    public partial class ProfileViewModel : ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string description;

        [ObservableProperty]
        private string photoPath;

        [ObservableProperty]
        private string buttonText = "Regisztráció"; 

        private int existingUserId = 0; 

        public ICommand TakePhotoCommand { get; }
        public ICommand RegisterCommand { get; }

        public ProfileViewModel()
        {
            TakePhotoCommand = new AsyncRelayCommand(TakePhotoAsync);
            RegisterCommand = new AsyncRelayCommand(SaveOrUpdateProfileAsync);
        }

        
        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("action") && query["action"].ToString() == "edit")
            {
               
                var users = await DatabaseService.GetUsersAsync();
                var currentUser = users.LastOrDefault();

                if (currentUser != null)
                {
                    existingUserId = currentUser.Id; 
                    Name = currentUser.Name;
                    Description = currentUser.Bio;
                    PhotoPath = currentUser.PhotoPath;
                    ButtonText = "Módosítások mentése"; 
                }
            }
            else
            {
                
                existingUserId = 0;
                Name = string.Empty;
                Description = string.Empty;
                PhotoPath = null;
                ButtonText = "Regisztráció";
            }
        }

        private async Task TakePhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                if (photo != null) PhotoPath = photo.FullPath;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Hiba", $"Hiba: {ex.Message}", "OK");
            }
        }

        private async Task SaveOrUpdateProfileAsync()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                await Shell.Current.DisplayAlert("Hiba", "Kérlek add meg a neved!", "OK");
                return;
            }

            
            if (existingUserId > 0)
            {
               
                var userToUpdate = new UserProfile
                {
                    Id = existingUserId, 
                    Name = Name,
                    Bio = Description,
                    PhotoPath = PhotoPath,
                   
                    AnswersJson = (await DatabaseService.GetUsersAsync()).FirstOrDefault(u => u.Id == existingUserId)?.AnswersJson
                };

                await DatabaseService.UpdateUserAsync(userToUpdate);
                await Shell.Current.DisplayAlert("Siker", "Profil frissítve!", "OK");
            }
            else
            {
                var newUser = new UserProfile
                {
                    Name = Name,
                    Bio = Description,
                    PhotoPath = PhotoPath
                };
                await DatabaseService.AddUserAsync(newUser);
                await Shell.Current.DisplayAlert("Siker", "Profil létrehozva!", "OK");
            }

            await Shell.Current.GoToAsync("//QuizPage");
        }
    }
}