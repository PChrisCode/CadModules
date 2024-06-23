using Microsoft.Extensions.DependencyInjection;
using XperiCad.DependencyInjectionFramework.Core;
using XperiCad.HoleRasterCalculator.Calculator;
using XperiCad.HoleRasterCalculator.Factories;
using XperiCad.HoleRasterCalculator.Validators;

namespace XperiCad.HoleRasterCalculator.Core
{
    public class HoleRasterCalculatorModule : IModule
    {
        #region IModule members
        public void LoadModule(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IHoleRasterCalculator, HorizontalFirstHoleRasterCalculator>();
            serviceCollection.AddTransient<IHoleRasterAssembler, HorizontalFirstHoleRasterAssembler>();

            serviceCollection.AddTransient<IHoleFactory, HoleFactory>();
            serviceCollection.AddTransient<IHoleRasterFactory, HoleRasterFactory>();

            serviceCollection.AddTransient<IHoleValidator, HoleValidator>();
        }
        #endregion
    }
}
