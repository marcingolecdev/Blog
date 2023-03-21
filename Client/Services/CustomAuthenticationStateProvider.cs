using Blazored.SessionStorage;
using Blog.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blog.Client.Services
{

    /*
     This is a class that stores information about logged in users.
     The "AuthenticationStateProvider" works with the "AuthorizeView" component which responds
     among other things, for sharing content based on their user roles.

     The following methods are described in more detail in the word docs
    */

    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        /*
            To be able to use the constructor below, you must install the "Blazored.SessionStorage" package         
        */
        private readonly ISessionStorageService _sessionStorage;

        public CustomAuthenticationStateProvider (ISessionStorageService sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }


        private ClaimsPrincipal _anonymous = new ClaimsPrincipal (new ClaimsIdentity ()); 
        public override async Task<AuthenticationState> GetAuthenticationStateAsync ()
        {
            try
            {
                var userSession = await _sessionStorage.ReadEncryptedItemAsync <UserSession> ("UserSession");
                if (userSession == null)
                    return await Task.FromResult(new AuthenticationState(_anonymous));

                var claimsPrincipal = new ClaimsPrincipal (new ClaimsIdentity (new List <Claim> ()
                {
                    new Claim (ClaimTypes.Name, userSession.UserName),
                    new Claim (ClaimTypes.Role, userSession.Role)
                }, "JwtAuth"));

                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }

        /*
         When a user logs out of or logs into the system, all information about him
         are deleted - in case of logging out, or updated in case of logging in.
         The "UserSession" data is retrieved from the "EditForm" from the user who logs in.
        */
        public async Task UpdateAuthenticationState (UserSession? userSession)
        {
            ClaimsPrincipal claimsPrincipal;
            if (userSession != null)
            {
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>()
                {
                    new Claim (ClaimTypes.Name, userSession.UserName),
                    new Claim (ClaimTypes.Role, userSession.Role)
                }));
                userSession.ExpiryTimeStamp = DateTime.Now.AddSeconds(userSession.Expiration);
                await _sessionStorage.SaveItemEncryptedAsync("UserSession", userSession);
            }
            else
            {
                claimsPrincipal = _anonymous;
                await _sessionStorage.RemoveItemAsync("UserSession");
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }



    }
}
