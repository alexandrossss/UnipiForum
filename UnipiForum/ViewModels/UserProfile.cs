using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
}