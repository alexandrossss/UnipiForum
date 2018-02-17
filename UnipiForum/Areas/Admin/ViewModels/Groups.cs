using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UnipiForum.Models;
using UnipiForum.ViewModels;

namespace UnipiForum.Areas.Admin.ViewModels
{
    public class GroupsIndex
    {
        public IEnumerable<group> Groups { get; set; }
    }

    public class GTG
    {
        public group Group { get; set; }
        public List<ChatMessageViewModel> ChatMessages { get; set; }
    }


}