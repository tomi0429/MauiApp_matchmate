using Microsoft.Maui.Controls; 
using MauiApp1.ViewModels;
using MauiApp1.Models;


namespace MauiApp1.Views
{
    public partial class QuizPage : ContentPage
    {
        public QuizPage()
        {
            InitializeComponent();
        }
       
        private void OnOptionSelected(object sender, CheckedChangedEventArgs e)
        {
            if (!e.Value) return; // Csak akkor reagáljunk, ha valóban kijelölték

            var radio = sender as RadioButton;
            var question = radio?.BindingContext as QuizQuestion;

            if (BindingContext is QuizViewModel vm && question != null)
            {
                int questionIndex = vm.Questions.IndexOf(question);
                int answerIndex = Array.IndexOf(question.Options, radio.Content.ToString());

                vm.SelectAnswer(questionIndex, answerIndex);
            }
        }
    }
}