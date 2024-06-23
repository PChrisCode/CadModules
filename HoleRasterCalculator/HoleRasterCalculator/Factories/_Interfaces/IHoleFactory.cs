using XperiCad.HoleRasterCalculator.Models;

namespace XperiCad.HoleRasterCalculator.Factories
{
    /// <summary>
    /// This component creates hole objects used by the hole raster calculator.
    /// </summary>
    public interface IHoleFactory
    {
        /// <summary>
        /// This method creates hole objects used by the hole raster calculator.
        /// </summary>
        /// <param name="xValues">The x values of the holes.</param>
        /// <param name="yValues">The y values of the holes.</param>
        /// <param name="tags">The tags of the holes.</param>
        /// <param name="diameters">The diameters of the holes.</param>
        /// <param name="depths">The depths of the holes.</param>
        /// <returns>A hole collection.</returns>
        ICollection<IHole> CreateHoles(
            IList<decimal> xValues,
            IList<decimal> yValues,
            IList<string> tags,
            IList<decimal> diameters,
            IList<decimal> depths);

        /// <summary>
        /// This method creates a hole object used by the hole raster calculator.
        /// </summary>
        /// <param name="x">The x value of the hole.</param>
        /// <param name="y">The y value of the hole.</param>
        /// <param name="tag">The tag of the hole.</param>
        /// <param name="diameter">The diameter of the hole.</param>
        /// <param name="depth">The depth of the hole.</param>
        /// <returns>A hole object.</returns>
        IHole CreateHole(
            decimal x,
            decimal y,
            string tag,
            decimal diameter,
            decimal depth);
    }
}
