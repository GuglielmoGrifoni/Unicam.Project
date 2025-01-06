using Unicam.Project.Application.Models.Requests;

namespace Unicam.Project.Application.Abstractions.Services
{
    public interface ITokenService
    {
        string CreateToken(CreateTokenRequest request);
    }
}
