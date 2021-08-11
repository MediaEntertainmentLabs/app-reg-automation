using AppRegPortal.Utilities;

using AutoFixture.Xunit2;

using Bunit;

using Microsoft.AspNetCore.Components;

using System;

using TestUtilities;

using Xunit;

namespace AppRegPortal.Tests.Customizations
{
    public class BUnitCustomizationTest
    {
        public class AddDataAttribute : AutoDataNSubstituteAttribute
        {
            public AddDataAttribute() : base(typeof(BUnitTestContextCustomization))
            { }
        }

        public class TestComponent : ComponentBase
        {
            public INavigator Navigator { get; private set; }
            public string SomeString { get; set; }

            public TestComponent(INavigator navigator)
            {
                this.Navigator = navigator ?? throw new ArgumentNullException(nameof(navigator));
            }
        }

        [Theory, AddData]
        public void FrozenAttributeShouldWork([Frozen] INavigator navigator, TestContext context)
        {
            IRenderedComponent<TestComponent> cut = context.RenderComponent<TestComponent>();
            Assert.Same(navigator, cut.Instance.Navigator);
        }

        [Theory, AddData]
        public void ShouldNotAssignMembers(TestContext context)
        {
            IRenderedComponent<TestComponent> cut = context.RenderComponent<TestComponent>();
            Assert.Null(cut.Instance.SomeString);
        }
    }
}
