using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UnipiForum.ViewModels
{

    public class AnswersRadioButton
    {

        public int Answer_ID { get; set; }
        public int? Question_ID { get; set; }
        public bool? Is_Correct { get; set; }
        public string Answer_Text { get; set; }

    }
    public class Question
    {
        public int Question_ID { get; set; }
        public int? Test_ID { get; set; }
        public int? Question_Difficulty { get; set; }
        public string Question_Text { get; set; }

    }
    public enum TestTypes
    {
        [Display(Name = "Sign Up")]
        Sign_Up,
        [Display(Name = "Δομές Επανάληψης")]
        domes_epanalipsis,
        [Display(Name = "Δομές Επιλογής")]
        domes_epilogis,
        [Display(Name = "Πίνακες")]
        pinakes,
        [Display(Name = "Συναρτήσεις")]
        sinartiseis
    }

    public class DoATest
    {
        public int Test_ID { get; set; }
        public string Test_Text { get; set; }
        public IList<Question> Questions { get; set; }
        public IList<AnswersRadioButton> Answers { get; set; }

    }
}