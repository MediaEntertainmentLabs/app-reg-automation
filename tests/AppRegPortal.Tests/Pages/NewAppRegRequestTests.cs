using AppRegPortal.Pages;
using AppRegPortal.Services;
using AppRegPortal.Utilities;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Logging;

using NSubstitute;

using System;
using System.Threading.Tasks;

using TestUtilities;

using Xunit;

namespace AppRegPortal.Tests.Pages
{
    public class NewAppRegRequestTests
    {
        private IAppRegistrationService subAppRegistrationService;
        private INavigator subNavigator;
        private AuthenticationStateProvider subAuthenticationStateProvider;
        private ILogger<NewAppRegRequest> subLogger;

        public NewAppRegRequestTests()
        {
            this.subAppRegistrationService = Substitute.For<IAppRegistrationService>();
            this.subNavigator = Substitute.For<INavigator>();
            this.subAuthenticationStateProvider = Substitute.For<AuthenticationStateProvider>();
            this.subLogger = Substitute.For<ILogger<NewAppRegRequest>>();
        }

        private NewAppRegRequest CreateNewAppRegRequest()
        {
            return new NewAppRegRequest(
                this.subAppRegistrationService,
                this.subNavigator,
                this.subAuthenticationStateProvider,
                this.subLogger);
        }

        [Theory, AutoDataNSubstitute]
        public async Task Foo(NewAppRegRequest page)
        {

        }

        [Fact]
        public async Task OnValidSubmit_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var newAppRegRequest = this.CreateNewAppRegRequest();

            // Act
            await newAppRegRequest.OnValidSubmit();

            // Assert
            Assert.True(false);
        }

        [Fact]
        public async Task OnReset_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var newAppRegRequest = this.CreateNewAppRegRequest();
            MouseEventArgs args = null;

            // Act
            await newAppRegRequest.OnReset(
                args);

            // Assert
            Assert.True(false);
        }

        [Fact]
        public void OnCancel_StateUnderTest_ExpectedBehavior()
        {
            // Arrange
            var newAppRegRequest = this.CreateNewAppRegRequest();
            MouseEventArgs args = null;

            // Act
            newAppRegRequest.OnCancel(
                args);

            // Assert
            Assert.True(false);
        }
    }
}
