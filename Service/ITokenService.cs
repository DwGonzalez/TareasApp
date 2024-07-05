using TareasApp.Entities;

namespace TareasApp.Service
{
    public interface ITokenService
    {
        string CreateToken(Usuario usuario);
    }
}
