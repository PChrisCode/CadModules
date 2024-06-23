namespace XperiCad.HoleRasterCalculator.Models
{
    /// <summary>
    /// This class represents a hole raster.
    /// </summary>
    public interface IHoleRaster
    {
        /// <summary>
        /// The starting hole of the hole raster.
        /// </summary>
        IHole StartingHole { get; }

        /// <summary>
        /// The amount of holes in X direction.
        /// </summary>
        long AmountInXDirection { get; set; }

        /// <summary>
        /// The distance between holes in X direction.
        /// </summary>
        decimal DistanceBetweenHolesInXDirection { get; set; } //TODO: consider making these readonly


        /// <summary>
        /// The amount of holes in Y direction.
        /// </summary>
        long AmountInYDirection { get; set; }

        /// <summary>
        /// The distance between holes in Y direction.
        /// </summary>
        decimal DistanceBetweenHolesInYDirection { get; set; }
    }
}
