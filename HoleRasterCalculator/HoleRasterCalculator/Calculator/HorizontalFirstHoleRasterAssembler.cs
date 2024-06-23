using XperiCad.HoleRasterCalculator.Factories;
using XperiCad.HoleRasterCalculator.Models;

namespace XperiCad.HoleRasterCalculator.Calculator
{
    internal class HorizontalFirstHoleRasterAssembler : IHoleRasterAssembler
    {
        #region Fields
        private readonly IHoleRasterFactory _holeRasterFactory;
        #endregion

        #region ctor
        public HorizontalFirstHoleRasterAssembler(IHoleRasterFactory holeRasterFactory)
        {
            _holeRasterFactory = holeRasterFactory ?? throw new ArgumentNullException(nameof(holeRasterFactory));
        }
        #endregion

        #region IHoleRasterAssembler members
        public ICollection<IHoleRaster> Create1DHoleRasters(ICollection<IHole> holes)
        {
            if (holes is null)
            {
                throw new ArgumentNullException(nameof(holes));
            }

            holes = holes.OrderByDescending(h => h.Coordinates.Y)
                         .ThenByDescending(h => h.Coordinates.X)
                         .ToList();

            var result = new List<IHoleRaster>();
            var currentRaster = default(IHoleRaster);
            var previousCoordinates = default(IHolePoint);

            while (holes.Any())
            {
                var hole = holes.Last();

                if (currentRaster is null)
                {
                    //1. if no raster selected, start a raster with the next hole as starting point
                    //delete the starting point from holes
                    currentRaster = _holeRasterFactory.CreateHoleRaster(hole, 1, Constants.HoleRasters.DEFAULT_X_DISTANCE, 1, Constants.HoleRasters.DEFAULT_Y_DISTANCE);
                }
                else if (currentRaster.StartingHole.Coordinates.Y == hole.Coordinates.Y &&
                    (currentRaster.DistanceBetweenHolesInXDirection == Constants.HoleRasters.DEFAULT_X_DISTANCE
                    || currentRaster.DistanceBetweenHolesInXDirection.Equals(hole.Coordinates.X - previousCoordinates.X)))//TODO: test this
                {
                    //2. if raster is selected and next hole matches distance (or the first after starting point), add to raster
                    //remove used hole
                    if (currentRaster.DistanceBetweenHolesInXDirection == Constants.HoleRasters.DEFAULT_X_DISTANCE)
                    {
                        currentRaster.DistanceBetweenHolesInXDirection = hole.Coordinates.X - previousCoordinates.X;
                    }

                    currentRaster.AmountInXDirection++;
                }
                else
                {
                    //3. if raster is selected and next hole does not match, then unselect raster (it is finished, add to result list)
                    //continue with step 1
                    result.Add(currentRaster);
                    currentRaster = null;
                    continue;
                }

                previousCoordinates = hole.Coordinates;
                holes.Remove(hole);
            }

            if (currentRaster is not null)
            {
                result.Add(currentRaster);
            }

            return result;
        }

        public IEnumerable<IHoleRaster> Create2DHoleRasters(ICollection<IHoleRaster> holeRasters)
        {
            if (holeRasters is null)
            {
                throw new ArgumentNullException(nameof(holeRasters));
            }

            holeRasters = holeRasters.OrderByDescending(hr => hr.StartingHole.Coordinates.X)
                                     .ThenByDescending(hr => hr.StartingHole.Coordinates.Y)
                                     .ToList();

            var result = new List<IHoleRaster>();
            var currentRaster = default(IHoleRaster);
            var previousCoordinates = default(IHolePoint);

            while (holeRasters.Any())
            {
                var holeRaster = holeRasters.Last();

                if (currentRaster is null)
                {
                    //1. if no raster selected, select next raster
                    currentRaster = holeRaster;
                }
                else if (IsRasterMatchAnother(currentRaster, holeRaster, previousCoordinates))//TODO: test this
                {
                    //2. if raster is selected and next raster matches distance and count (or the first after starting raster and matches count), add to raster
                    //remove used raster
                    if (currentRaster.DistanceBetweenHolesInYDirection == Constants.HoleRasters.DEFAULT_Y_DISTANCE)
                    {
                        currentRaster.DistanceBetweenHolesInYDirection = holeRaster.StartingHole.Coordinates.Y - previousCoordinates.Y;
                    }

                    currentRaster.AmountInYDirection++;
                }
                else
                {
                    //3. if raster is selected and next raster does not match, then unselect raster (it is finished, add to result list)
                    //continue with step 1
                    result.Add(currentRaster);
                    currentRaster = null;
                    continue;
                }

                previousCoordinates = holeRaster.StartingHole.Coordinates;
                holeRasters.Remove(holeRaster);
            }

            if (currentRaster is not null)
            {
                result.Add(currentRaster);
            }

            return result;
        }
        #endregion

        #region Private methods
        private bool IsRasterMatchAnother(IHoleRaster currentRaster, IHoleRaster otherRaster, IHolePoint previousCoordinates)
        {
            if (currentRaster.StartingHole.Depth != otherRaster.StartingHole.Depth
                || currentRaster.StartingHole.Diameter != otherRaster.StartingHole.Diameter
                || currentRaster.AmountInXDirection != otherRaster.AmountInXDirection
                || currentRaster.DistanceBetweenHolesInXDirection != otherRaster.DistanceBetweenHolesInXDirection
                || currentRaster.StartingHole.Coordinates.X != otherRaster.StartingHole.Coordinates.X)
            {
                return false;
            }

            return currentRaster.DistanceBetweenHolesInYDirection == Constants.HoleRasters.DEFAULT_Y_DISTANCE
                    || currentRaster.DistanceBetweenHolesInYDirection.Equals(otherRaster.StartingHole.Coordinates.Y - previousCoordinates.Y);
        }
        #endregion
    }
}
