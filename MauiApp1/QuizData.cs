using System.Collections.Generic;
using MauiApp1.Models; 

namespace MauiApp1.Services 
{
    public static class QuizData
    {
      
        public static readonly List<QuizQuestion> Questions = new()
        {
            new QuizQuestion
            {
                Text = "Hogyan töltöd legszívesebben a szabadidődet?",
                Options = new[] { "Olvasás", "Sport", "Sorozatnézés", "Barátokkal" }
            },
            new QuizQuestion
            {
                Text = "Melyik évszakot szereted legjobban?",
                Options = new[] { "Tavasz", "Nyár", "Ősz", "Tél" }
            },
            new QuizQuestion
            {
                Text = "Milyen típusú zenét hallgatsz legtöbbet?",
                Options = new[] { "Pop", "Rock", "Rap", "Klasszikus" }
            },
            new QuizQuestion
            {
                Text = "Milyen filmet nézel meg legszívesebben este?",
                Options = new[] { "Vígjáték / Romantikus", "Akció / Sci-Fi", "Horror / Thriller", "Dokumentumfilm" }
            },

            
            new QuizQuestion
            {
                Text = "Ha csak egyfajta konyhát ehetnél életed végéig, mi lenne az?",
                Options = new[] { "Olasz (Pizza, Pasta)", "Magyaros", "Ázsiai (Sushi, Thai)", "Amerikai (Burger, BBQ)" }
            },

            
            new QuizQuestion
            {
                Text = "Melyik napszakban vagy a legaktívabb?",
                Options = new[] { "Kora reggelx", "Napközben", "Este", "Éjszaka" }
            },

            
            new QuizQuestion
            {
                Text = "Milyen háziállatot választanál?",
                Options = new[] { "Kutya", "Macska", "Valami egzotikus (pl. kígyó)", "Nem szeretnék állatot" }
            }
        };
    }
}