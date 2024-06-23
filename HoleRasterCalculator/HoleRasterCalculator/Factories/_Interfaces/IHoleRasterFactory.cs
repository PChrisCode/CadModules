using XperiCad.HoleRasterCalculator.Models;

namespace XperiCad.HoleRasterCalculator.Factories
{
    internal interface IHoleRasterFactory
    {
        IHoleRaster CreateHoleRaster(IHole startingHole,
                          long amountInXDirection,
                          decimal distanceBetweenHolesInXDirection,
                          long amountInYDirection,
                          decimal distanceBetweenHolesInYDirection);
    }
}
