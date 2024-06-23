using XperiCad.HoleRasterCalculator.Models;

namespace XperiCad.HoleRasterCalculator.Calculator
{
    /// <summary>
    /// This component groups the given holes into hole rasters.
    /// </summary>
    public interface IHoleRasterCalculator
    {
        /// <summary>
        /// This method groups the given holes into hole rasters.
        /// </summary>
        /// <param name="holes">The hole collection from which the hole rasters are created.</param>
        /// <returns>A collection of hole rasters.</returns>
        IEnumerable<IHoleRaster> CalculateHoleRasters(ICollection<IHole> holes);
    }
}
