namespace XperiCad.HoleRasterCalculator.Models
{
    internal class Hole : IHole
    {
        #region Properties
        public string Tag { get; }
        public IHolePoint Coordinates { get; }
        public decimal Diameter { get; }
        public decimal Depth { get; }
        #endregion

        #region ctor
        public Hole(string tag, IHolePoint coordinates, decimal diameter, decimal depth)
        {
            Tag = tag;
            Coordinates = coordinates ?? throw new ArgumentNullException(nameof(coordinates));
            Diameter = diameter;
            Depth = depth;
        }
        #endregion
    }
}
