﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Server.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Alias { get; set; }
        public string Info { get; set; }
        [ValidateNever]
        public List<BookAuthor> BookAuthors { get; set; }
    }
}
