using System.Collections.Generic;
using UnipiForum.Models;

namespace UnipiForum.Areas.Admin.ViewModels
{
    public class UsersIndex
    {
        public IEnumerable<User> Users { get; set; }
    }


}