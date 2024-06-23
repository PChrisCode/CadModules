namespace XperiCad.HoleRasterCalculator.Models
{
    /// <summary>
    /// This class represents a hole object.
    /// </summary>
    public interface IHole
    {
        /// <summary>
        /// The tag of the hole.
        /// </summary>
        string Tag { get; }

        /// <summary>
        /// The coordinates of the hole.
        /// </summary>
        IHolePoint Coordinates { get; }

        /// <summary>
        /// The diameter of the hole.
        /// </summary>
        decimal Diameter { get; }

        /// <summary>
        /// The depth of the hole.
        /// </summary>
        decimal Depth { get; }
    }
}
