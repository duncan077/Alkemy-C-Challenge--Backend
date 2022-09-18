using Frontend.Services.Base;
using Blazored.LocalStorage;
using Frontend.Providers;
using Microsoft.AspNetCore.Components.Authorization;

namespace Frontend.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IClient client;
        private readonly ILocalStorageService localStorageService;
        private readonly AuthenticationStateProvider authenticationState;

        public AuthService(IClient client, ILocalStorageService localStorageService,AuthenticationStateProvider  authenticationState )
        {
            this.client = client;
            this.localStorageService = localStorageService;
            this.authenticationState = authenticationState;
          
        }

        public async Task<bool> AuthAsync(LoginDTO loginUserDTO)
        {
            var response = await client.LoginAsync(loginUserDTO);
            await localStorageService.SetItemAsync("accessToken", response.Token);
            await ((ApiAuthStateProvider)authenticationState).Loggedin();
            return true;
        }
     

        public async Task Logout()
        {
            await((ApiAuthStateProvider)authenticationState).LoggedOut();
        }
    }
}
