﻿using System.ComponentModel.DataAnnotations;

namespace PayrollManagementSystem.Models.ViewModel
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}