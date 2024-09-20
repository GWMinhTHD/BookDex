using WebStore.Server.Models;

namespace WebStore.Server.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user);
    }
}
