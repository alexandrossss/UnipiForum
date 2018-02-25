using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UnipiForum.Models;

namespace UnipiForum.Areas.Admin.ViewModels
{
    public class RoleCheckbox
    {
        public int Id { get; set; }
        public bool IsChecked { get; set; }
        public string Name { get; set; }

    }
    public class UsersIndex
    {
        public IEnumerable<user> Users { get; set; }
    }

    public class UsersNew
    {
        [Required]
        public string University_ID{ get; set; }
        [Required, MaxLength(128)]
        public string Username { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, MaxLength(256), DataType(DataType.EmailAddress)]
        public string Email { get; set; }



        public IList<RoleCheckbox> Roles { get; set; }
    }
    public class UsersEdit
    {

        
        [Required]
        public string University_ID { get; set; }
        [Required, MaxLength(128)]
        public string Username { get; set; }
        [Required, MaxLength(256), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public int? GroupID { get; set; }
        [MaxLength(256)]
        public string Passed_Text { get; set; }


        public IList<RoleCheckbox> Roles { get; set; }
    }
    public class UsersResetPassword
    {
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class ResultsViewModel
    {
        public string Username { get; set; }
        public IList<result> Results { get; set; }

    }

    public class UsGroup
    {
        public group Group { get; set; }
    }
}