using StoreManagement.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace StoreManagement.ViewModels.BookViewModel
{
    public class CreateUpdateVM
    {
        public Book Book { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> MyAuthors { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> MyCategories { get; set; }
        [ValidateNever]
        public int[] CatIDs { get; set; }
        [ValidateNever]
        public int[] AuIDs { get; set; }
    }
}
