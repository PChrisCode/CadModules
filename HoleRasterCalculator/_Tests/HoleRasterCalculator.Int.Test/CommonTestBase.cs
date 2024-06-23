using Microsoft.Extensions.DependencyInjection;
using XperiCad.EngineeringFramework.Core;
using XperiCad.HoleRasterCalculator.Core;

namespace XperiCad.HoleRasterCalculator.Int.Test
{
    public class CommonTestBase
    {
        #region Properties
        protected IServiceProvider ServiceProvider { get; }
        #endregion

        #region ctor
        public CommonTestBase()
        {
            ServiceProvider = InitialiseModules();
        }
        #endregion

        #region Private members
        protected static IServiceProvider InitialiseModules()
        {
            var serviceCollection = new ServiceCollection();

            new EngineeringFrameworkModule(
                "HoleRasterCalculatorTest",
                @".\Preferences\TestApplicationConfiguration.xml",
                "en", "hu-HU")
                .LoadModule(serviceCollection);

            new HoleRasterCalculatorModule().LoadModule(serviceCollection);

            return serviceCollection.BuildServiceProvider();
        }
        #endregion
    }
}
