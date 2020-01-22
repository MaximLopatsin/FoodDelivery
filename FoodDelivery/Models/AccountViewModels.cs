﻿using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "LoginName")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
