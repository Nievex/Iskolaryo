using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;

namespace Iskolaryo.Auth
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _sessionStorage; // We use the browser's session storage to store the user details
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthStateProvider() { }
        public CustomAuthStateProvider(ProtectedSessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var userSessionStorageResult = await _sessionStorage.GetAsync<UserSession>("UserSession"); // Get the user details from the session storage
            UserSession? userSession = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;

            if (userSession == null)
            {
                return await Task.FromResult(new AuthenticationState(_anonymous)); // If there are no user details, return an anonymous AuthenticationState
            }

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Name, userSession.Username), 
                new Claim(ClaimTypes.Role, userSession.Role)}, 
                "CustomAuth"));

            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }

        // <summary>
        // Updates the user details in the session storage and notifies the AuthenticationStateProvider that the authentication state has changed
        // </summary>
        public async Task UpdateAuthenticationState(UserSession userSession)
        {
            ClaimsPrincipal claimsPrincipal;

            if (userSession != null)
            {
                await _sessionStorage!.SetAsync("UserSession", userSession);
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, userSession.Username),
                    new Claim(ClaimTypes.Role, userSession.Role)},
                    "CustomAuth"));
            }
            else
            {
                await _sessionStorage!.DeleteAsync("UserSession");
                claimsPrincipal = _anonymous;
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
    }
}
