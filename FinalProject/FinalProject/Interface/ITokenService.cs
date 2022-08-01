using FinalProject.Entities;

namespace FinalProject.Interface
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
