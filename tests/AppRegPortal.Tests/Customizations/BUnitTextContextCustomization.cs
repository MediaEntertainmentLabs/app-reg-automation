using AutoFixture;
using AutoFixture.Kernel;

using Bunit;

using Microsoft.AspNetCore.Components;

using System;

namespace AppRegPortal.Tests.Customizations
{
    public class BUnitTestContextCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<TestContext>(composer =>
            {
                return composer.FromFactory(() =>
                {
                    var ctx = new TestContext();
                    ctx.ComponentFactories.Add(new AutoFixtureComponentFactory(fixture));
                    return ctx;
                }).OmitAutoProperties();
            });
        }
    }
    //var specimen = new SpecimenContext(fixture).Resolve(type);

    public class AutoFixtureComponentFactory : IComponentFactory
    {
        private readonly IFixture _fixture;
        private readonly SpecimenContext _specimenContext;

        public AutoFixtureComponentFactory(IFixture fixture)
        {
            this._fixture = fixture ?? throw new ArgumentNullException(nameof(fixture));
            this._specimenContext = new SpecimenContext(this._fixture);
        }

        public bool CanCreate(Type componentType)
        {
            return typeof(IComponent).IsAssignableFrom(componentType);
        }

        public IComponent Create(Type componentType)
        {
            this._fixture.OmitAutoProperties = true;
            var component = this._specimenContext.Resolve(componentType) as IComponent;
            this._fixture.OmitAutoProperties = false;
            return component;
        }
    }
}
