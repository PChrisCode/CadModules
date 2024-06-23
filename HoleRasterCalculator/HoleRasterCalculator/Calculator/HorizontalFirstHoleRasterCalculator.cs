using XperiCad.HoleRasterCalculator.Models;
using XperiCad.HoleRasterCalculator.Validators;

namespace XperiCad.HoleRasterCalculator.Calculator
{
    internal class HorizontalFirstHoleRasterCalculator : IHoleRasterCalculator
    {
        #region Fields
        private readonly IHoleValidator _holeValidator;
        private readonly IHoleRasterAssembler _holeRasterAssembler;
        #endregion

        #region ctor
        public HorizontalFirstHoleRasterCalculator(IHoleValidator holeValidator, IHoleRasterAssembler holeRasterAssembler)
        {
            _holeValidator = holeValidator ?? throw new ArgumentNullException(nameof(holeValidator));
            _holeRasterAssembler = holeRasterAssembler ?? throw new ArgumentNullException(nameof(holeRasterAssembler));
        }
        #endregion

        public IEnumerable<IHoleRaster> CalculateHoleRasters(ICollection<IHole> holes)
        {
            if (holes is null)
            {
                throw new ArgumentNullException(nameof(holes));
            }

            if (!_holeValidator.IsHoleCollectionValid(holes))
            {
                return new List<IHoleRaster>();
            }

            var horizontalHoleRasters = _holeRasterAssembler.Create1DHoleRasters(holes);
            var result = _holeRasterAssembler.Create2DHoleRasters(horizontalHoleRasters);

            return result;
        }
    }
}
