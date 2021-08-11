using AppRegPortal.Pages;
using AppRegPortal.Utilities;

using AutoFixture.Xunit2;

using NSubstitute;

using TestUtilities;

using Xunit;

namespace AppRegPortal.Tests.Pages
{
    public class UserRegListTests
    {
        [Theory, AutoDataNSubstitute]
        public void When_New_Button_Clicked_ShouldNavigateToNewRequestPage([Frozen] INavigator navigator, UserRegList sut)
        {
            sut.OnNewRequestClick();
            navigator.Received().NavigateToNewAppRegRequestDialog();
        }
    }
}