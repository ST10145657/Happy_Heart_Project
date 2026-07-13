using HappyHearts_Draft.Models.ViewModels;
using HappyHearts_Draft.Models;

namespace HappyHearts_Draft.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResult> LoginAsync(LoginViewModel model);
        Task<AuthResult> RegisterAsync(RegisterViewModel model);
    }
}
