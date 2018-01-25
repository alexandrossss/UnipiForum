using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UnipiForum.Models;

namespace UnipiForum.Areas.Admin.ViewModels
{
    public class TestsIndex
    {
        public IEnumerable<test> Tests { get; set; }
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

    public enum Difficulty
    {
        [Display(Name = "Δομές Επανάληψης")]
        domes_epanalipsis,
        [Display(Name = "Δομές Επιλογής")]
        domes_epilogis,
        [Display(Name = "Πίνακες")]
        pinakes,
        [Display(Name = "Συναρτήσεις")]
        sinartiseis
    }

    public class TestsNew
    {
        [Required]
        public string TestText{ get; set; }
        
        public TestTypes TestType { get; set; }

        public Difficulty Difficulty{ get; set; }

        [Required]
        public List<question> Questions { get; set; }

        [Required]
        public List<answer> Answers { get; set; }

    }
}