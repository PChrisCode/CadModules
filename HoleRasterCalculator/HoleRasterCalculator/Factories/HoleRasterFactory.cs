using XperiCad.HoleRasterCalculator.Models;

namespace XperiCad.HoleRasterCalculator.Factories
{
    internal class HoleRasterFactory : IHoleRasterFactory
    {
        public IHoleRaster CreateHoleRaster(IHole startingHole, long amountInXDirection, decimal distanceBetweenHolesInXDirection, long amountInYDirection, decimal distanceBetweenHolesInYDirection)
        {
            if (startingHole is null)
            {
                throw new ArgumentNullException(nameof(startingHole));
            }

            return new HoleRaster(
                startingHole,
                amountInXDirection,
                distanceBetweenHolesInXDirection,
                amountInYDirection,
                distanceBetweenHolesInYDirection);
        }
    }
}
