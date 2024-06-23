namespace XperiCad.HoleRasterCalculator.Models
{
    internal class HolePoint : IHolePoint
    {
        #region Properties
        public decimal X { get; }
        public decimal Y { get; }
        #endregion

        #region ctor
        public HolePoint(decimal x, decimal y)
        {
            X = x;
            Y = y;
        }
        #endregion
    }
}
