using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UnipiForum.Models;
using UnipiForum.ViewModels;

namespace UnipiForum.Areas.Admin.ViewModels
{
    public class GroupsIndex
    {
        public IEnumerable<group> Groups { get; set; }
        public Difficulty DifficultyValue { get; set; }
        public enum Difficulty
        {
            [Display(Name = "First")]
            First,
            [Display(Name = "Second`")]
            Second,
            [Display(Name = "Thrird`")]
            Thrird,
            [Display(Name = "Forth")]
            Forth
        }
    }

    public class GTG
    {
        public group Group { get; set; }
        public ulh Ulh { get; set; }
        public List<ChatMessageViewModel> ChatMessages { get; set; }
    }


}