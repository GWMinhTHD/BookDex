using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Server.Models.DTOs
{
    public class CartDTO
    {
        public int Id { get; set; }
        public string BookName { get; set; }
        public double BookPrice { get; set; }
        public string BookCover { get; set; }
        public List<string> BookAuthors { get; set; } = new List<string>();
    }
}
