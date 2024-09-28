﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace StoreManagement.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public string? Name { get; set; }
        [ValidateNever]
        public List<Library> Library { get; set; }

    }
}