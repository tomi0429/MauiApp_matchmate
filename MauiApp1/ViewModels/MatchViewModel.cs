using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Services;
using Microsoft.Maui.ApplicationModel.DataTransfer;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Text;

namespace MauiApp1.ViewModels
{

    public class MatchResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public string DisplayText => $"{Name} - {Score} egyezés"; 
    }

    public partial class MatchViewModel : ObservableObject
    {
        
        [ObservableProperty]
        ObservableCollection<MatchResult> matches;

        [RelayCommand]
        public async Task LoadMatchesAsync()
        {
            var users = await DatabaseService.GetUsersAsync();
            if (users.Count == 0) return; 

            var currentUser = users.LastOrDefault(); 

            
            if (currentUser?.AnswersJson == null) return;

            var currentAnswers = JsonConvert.DeserializeObject<Dictionary<int, int>>(currentUser.AnswersJson);

           
            var results = new List<MatchResult>();

            
            foreach (var other in users.Where(u => u.Id != currentUser.Id && u.AnswersJson != null))
            {
                var otherAnswers = JsonConvert.DeserializeObject<Dictionary<int, int>>(other.AnswersJson);

               
                int same = currentAnswers.Count(kv => otherAnswers.ContainsKey(kv.Key) && otherAnswers[kv.Key] == kv.Value);

                results.Add(new MatchResult { Id = other.Id, Name = other.Name, Score = same });
            }

            
            Matches = new ObservableCollection<MatchResult>(results.OrderByDescending(m => m.Score));
        }

        [RelayCommand]
        public async Task ShareResultAsync()
        {
            
            var users = await DatabaseService.GetUsersAsync();
            var me = users.LastOrDefault();

            if (me == null || string.IsNullOrEmpty(me.AnswersJson))
            {
                await Shell.Current.DisplayAlert("Hiba", "Nincsenek betölthető válaszok.", "OK");
                return;
            }

            
            var myAnswersDict = JsonConvert.DeserializeObject<Dictionary<int, int>>(me.AnswersJson);

           
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{me.Name} válaszai");
            sb.AppendLine("--------------------------------");

            
            var questions = QuizData.Questions;

            foreach (var item in myAnswersDict)
            {
               
                if (item.Key < questions.Count)
                {
                    var questionObj = questions[item.Key];

                    
                    if (item.Value < questionObj.Options.Length)
                    {
                        var answerText = questionObj.Options[item.Value];

                        sb.AppendLine($"{questionObj.Text}");
                        sb.AppendLine($"{answerText}");
                        sb.AppendLine(); 
                    }
                }
            }

           

           
            await Share.RequestAsync(new ShareTextRequest
            {
                Title = "MatchMate Válaszaim",
                Text = sb.ToString()
            });
        }
       

        [RelayCommand]
        public async Task DeleteMyProfileAsync()
        {
           
            bool answer = await Shell.Current.DisplayAlert(
                "Profil törlése",
                "Biztosan törölni szeretnéd a saját profilodat?",
                "Igen, törlés",
                "Mégsem");

            if (!answer) return; 
           
            var users = await DatabaseService.GetUsersAsync();
            var currentUser = users.LastOrDefault(); 

            if (currentUser != null)
            {
                
                await DatabaseService.DeleteUserAsync(currentUser);

                
                await Shell.Current.DisplayAlert("Siker", "A profilod törölve lett.", "OK");

                
                await Shell.Current.GoToAsync("//ProfilePage");
            }
            else
            {
                await Shell.Current.DisplayAlert("Hiba", "Nem található törölhető profil.", "OK");
            }
        }

        [RelayCommand]
        public async Task SelectUserAsync(MatchResult match)
        {
            if (match == null) return;

           
            await Shell.Current.GoToAsync($"{nameof(Views.UserDetailsPage)}?userId={match.Id}");
        }

        [RelayCommand]
        public async Task ResetGameAsync()
        {
            bool answer = await Shell.Current.DisplayAlert(
                "Új játék",
                "Biztosan törölni szeretnéd az összes eddigi adatot és új játékot kezdeni?",
                "Igen, törlés",
                "Mégsem");

            if (answer)
            {
                
                await DatabaseService.DeleteAllUsersAsync();

                
                Matches.Clear();

                
                await Shell.Current.GoToAsync("//ProfilePage");
            }
        }




}
}