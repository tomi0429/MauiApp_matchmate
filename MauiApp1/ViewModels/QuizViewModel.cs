using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiApp1.Models;
using MauiApp1.Services;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;

namespace MauiApp1.ViewModels
{
    internal partial class QuizViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<QuizQuestion> questions;

        [ObservableProperty]
        Dictionary<int, int> selectedAnswers = new(); 

        public QuizViewModel()
        {
            LoadQuestions();
        }

        void LoadQuestions()
        {
            Questions = new ObservableCollection<QuizQuestion>(QuizData.Questions);
        }

        [RelayCommand]
        public async Task SaveAnswersAsync()
        {
            if (SelectedAnswers.Count < Questions.Count)
            {
                await Shell.Current.DisplayAlert("Figyelem", "Kérlek válaszolj minden kérdésre!", "OK");
                return;
            }

           
            var users = await DatabaseService.GetUsersAsync();
            var currentUser = users.LastOrDefault();

            if (currentUser == null)
            {
                await Shell.Current.DisplayAlert("Hiba", "Nincs létrehozott profil!", "OK");
                return;
            }

            currentUser.AnswersJson = JsonConvert.SerializeObject(SelectedAnswers);
            await DatabaseService.UpdateUserAsync(currentUser);

            await Shell.Current.DisplayAlert("Siker", "Válaszok mentve!", "OK");
            await Shell.Current.GoToAsync("//MatchPage");
        }
        [RelayCommand]
        public async Task EditProfileAsync()
        {
            
            await Shell.Current.GoToAsync($"//ProfilePage?action=edit");
        }
        public void SelectAnswer(int questionIndex, int answerIndex)
        {
            SelectedAnswers[questionIndex] = answerIndex;
        }
        

    }
}
