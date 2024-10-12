using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace StoreManagement.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public byte[]? Cover { get; set; }
        public string Description { get; set; }
        public byte[]? FileLocation { get; set; }
        public float Price { get; set; }
        public bool IsFeatured { get; set; }
        [ValidateNever]
        public List<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
        [ValidateNever]
        public List<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
        [ValidateNever]
        public List<Library> Library { get; set; } = new List<Library>();

    }
}
