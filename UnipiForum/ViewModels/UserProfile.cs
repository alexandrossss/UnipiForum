using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.ApplicationInsights.Web;
using UnipiForum.Models;

namespace UnipiForum.ViewModels
{

    public class SighUp
    {
        [Required]
        public string University_ID { get; set; }
        [Required, MaxLength(128)]
        public string Username { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, MaxLength(256), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

    }

    public class MyProfileViewModel
    {
        public user Users { get; set; }


    }

    public class ResultsViewModel
    {
        public IList<result> Results { get; set; }

    }
}