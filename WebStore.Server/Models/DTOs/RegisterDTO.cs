﻿using System.ComponentModel.DataAnnotations;

namespace WebStore.Server.Models.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { set; get; }
        [Required]
        public string Password { set; get; }
    }
}
