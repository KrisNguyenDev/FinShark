using FinShark.Model;

namespace FinShark.Interfaces
{
    public interface ITokenServices
    {
        string CreateToken(AppUser user);
    }
}
