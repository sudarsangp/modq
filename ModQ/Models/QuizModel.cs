using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ModQ.Models
{
    public class QuizModel
    {
        public int ID { get; set; }
      
        public string Faculty { get; set; }
        public string Module { get; set; }

        public string Question { get; set; }
        public string FirstOption { get; set; }
        public string SecondOption { get; set; }
        public string ThirdOption { get; set; }
        public string FourthOption { get; set; }

        public string Answer { get; set; }
    }

    public class QuizDBContext : DbContext
    {
        public DbSet<QuizModel> QuizModels { get; set; } 
    }
}