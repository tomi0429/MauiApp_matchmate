using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MauiApp1.Models
{
   public  class UserProfile
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bio { get; set; }
        public string PhotoPath { get; set; }
        public string AnswersJson { get; set; }
    }
}
