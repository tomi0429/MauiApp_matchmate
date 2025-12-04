using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp1.Models
{
    public class QuizQuestion
    {

        public string Text { get; set; }
        public string[] Options { get; set; }
        public int CorrectIndex { get; set; }
    }
}
