using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Ldap.Models.DTOs
{
    public class UserRequestDTO
    {
        [Required]
        public static string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public static string Password { get; set; }
    }
}
