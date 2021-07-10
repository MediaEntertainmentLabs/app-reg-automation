using AppRegPortal.Tests.Customizations;

using TestUtilities;

namespace AppRegPortal.Tests.Pages
{
    public class BUnitCustomizationTest
    {
        public class AddDataAttribute : AutoDataNSubstituteAttribute
        {
            public AddDataAttribute() : base(typeof(BUnitTestContextCustomization))
            { }
        }
    }
}
