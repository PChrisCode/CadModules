namespace XperiCad.HoleRasterCalculator.Models
{
    internal class HoleRaster : IHoleRaster
    {
        #region Properties
        public IHole StartingHole { get; }

        public long AmountInXDirection { get; set; }
        public decimal DistanceBetweenHolesInXDirection { get; set; }

        public long AmountInYDirection { get; set; }
        public decimal DistanceBetweenHolesInYDirection { get; set; }
        #endregion

        #region ctor
        public HoleRaster(IHole startingHole,
                          long amountInXDirection,
                          decimal distanceBetweenHolesInXDirection,
                          long amountInYDirection,
                          decimal distanceBetweenHolesInYDirection)
        {
            StartingHole = startingHole;
            AmountInXDirection = amountInXDirection;
            DistanceBetweenHolesInXDirection = distanceBetweenHolesInXDirection;
            AmountInYDirection = amountInYDirection;
            DistanceBetweenHolesInYDirection = distanceBetweenHolesInYDirection;
        }
        #endregion
    }
}
