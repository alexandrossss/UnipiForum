using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UnipiForum.Models;

namespace UnipiForum.Areas.Admin.ViewModels
{
    public class GroupsIndex
    {
        public IEnumerable<group> Groups { get; set; }
    }

    public class GTG
    {
        public group Group { get; set; }
    }


}