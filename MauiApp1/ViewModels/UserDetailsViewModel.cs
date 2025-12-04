using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Models;
using MauiApp1.Services;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace MauiApp1.ViewModels
{
    public class QnA
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }

   
    [QueryProperty(nameof(UserId), "userId")]
    public partial class UserDetailsViewModel : ObservableObject
    {
        [ObservableProperty]
        private UserProfile selectedUser;

        [ObservableProperty]
        private ObservableCollection<QnA> userAnswers;

        
        private string _userId;
        public string UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                if (int.TryParse(_userId, out int id))
                {
                   
                    LoadUserData(id);
                }
            }
        }

        private async void LoadUserData(int id)
        {
            var users = await DatabaseService.GetUsersAsync();
            
            var foundUser = users.FirstOrDefault(u => u.Id == id);

            if (foundUser != null)
            {
                SelectedUser = foundUser; 

               
                if (!string.IsNullOrEmpty(foundUser.AnswersJson))
                {
                    LoadAnswers(foundUser.AnswersJson);
                }
            }
        }

        private void LoadAnswers(string json)
        {
            try
            {
                var answersDict = JsonConvert.DeserializeObject<Dictionary<int, int>>(json);
                var list = new ObservableCollection<QnA>();

                
                var sourceQuestions = QuizData.Questions;

                foreach (var item in answersDict)
                {
                    
                    if (item.Key < sourceQuestions.Count && item.Value >= 0)
                    {
                        var q = sourceQuestions[item.Key];

                        
                        if (item.Value < q.Options.Length)
                        {
                            list.Add(new QnA
                            {
                                Question = q.Text,           
                                Answer = q.Options[item.Value] 
                            });
                        }
                    }
                }
                UserAnswers = list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hiba: " + ex.Message);
            }
        }

        [RelayCommand]
        async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}