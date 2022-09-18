using Frontend.Services.Base;

namespace Frontend.Services.Auth
{
    public interface IAuthService
    {
        Task<bool> AuthAsync(LoginDTO loginUserDTO);
        public Task Logout();
    }
}
