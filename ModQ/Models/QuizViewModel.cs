using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ModQ.Models
{
    public class QuizViewModel
    {
        [Display(Name="Faculty - ")]
        public string Faculty { get; set; }

        [Display(Name="Module - ")]
        public string Module { get; set; }

        [Display(Name="Q: ")]
        public string Question { get; set; }

        [Display(Name="1) ")]
        public string FirstOption { get; set;}

        [Display(Name="2) ")]
        public string SecondOption { get; set; }

        [Display(Name="3) ")]
        public string ThirdOption { get; set; }

        [Display(Name="4) ")]
        public string FourthOption { get; set; }

        public int HiddenIndex { get; set; }
    }
}