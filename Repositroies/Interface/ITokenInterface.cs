using Microsoft.AspNetCore.Identity;

namespace myproject.Repositroies.Interface
{
    public interface ITokenInterface
    {

        public  string createJwtToken(IdentityUser user, List<string> roles);
    }
}
