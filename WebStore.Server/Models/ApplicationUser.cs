using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WebStore.Server.Models
{
    public class ApplicationUser: IdentityUser
    {
        [ValidateNever]
        public List<Library> Library { get; set; }
    }
}
