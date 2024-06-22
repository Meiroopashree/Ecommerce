// Models/User.cs
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace dotnetapp.Models
{

    public class User
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string UserRole { get; set; }


        [Required]
        public string EmailID {get; set; }

        [Required]
        public string MobileNumber {get; set; }
    }
}