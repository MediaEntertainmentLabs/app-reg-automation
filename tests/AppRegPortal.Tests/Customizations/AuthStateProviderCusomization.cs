using AutoFixture;

using Microsoft.AspNetCore.Components.Authorization;

using System.Security.Claims;
using System.Threading.Tasks;

namespace AppRegPortal.Tests.Customizations
{
    public class AuthStateProviderCusomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<AuthenticationStateProvider>(composer =>
            {

                return composer.FromFactory(() =>
                {
                    ClaimsPrincipal user = this.CreateClaimsPrincipal();
                    var testProvider = new TestAuthenticationStateProvider(user);
                    return testProvider;
                });
            });
        }

        public virtual ClaimsPrincipal CreateClaimsPrincipal()
        {
            return null;
        }
    }

    public class TestAuthenticationStateProvider : AuthenticationStateProvider
    {
        public TestAuthenticationStateProvider(ClaimsPrincipal user)
        {
            this.User = user;
        }

        public ClaimsPrincipal User { get; set; }
        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var authState = new AuthenticationState(this.User);
            return Task.FromResult(authState);
        }
    }
}
