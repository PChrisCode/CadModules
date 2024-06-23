namespace XperiCad.HoleRasterCalculator.Models
{
    /// <summary>
    /// This class represents the coordinates of a hole.
    /// </summary>
    public interface IHolePoint
    {
        /// <summary>
        /// The X component of the coordinates.
        /// </summary>
        decimal X { get; }

        /// <summary>
        /// The Y component of the coordinates.
        /// </summary>
        decimal Y { get; }
    }
}
