using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Server.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        [ValidateNever]
        public ApplicationUser User { get; set; }
        [Required]
        public int? BookID { get; set; }
        [ForeignKey("BookID")]
        [ValidateNever]
        public Book? Book { get; set; }
    }
}
