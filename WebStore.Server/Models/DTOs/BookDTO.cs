using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Server.Models.DTOs
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[]? Cover { get; set; }
        public byte[]? FileLocation { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public bool IsFeatured { get; set; }
        public List<int> CategoryIDs { get; set; } = new List<int>();
        public List<int> AuthorIDs{ get; set; } = new List<int>();
        public List<string> Categories { get; set; } = new List<string>();
        public List<string> Authors { get; set; } = new List<string>();


    }
}
