using XperiCad.HoleRasterCalculator.Models;

namespace XperiCad.HoleRasterCalculator.Calculator
{
    internal interface IHoleRasterAssembler
    {
        ICollection<IHoleRaster> Create1DHoleRasters(ICollection<IHole> holes);
        IEnumerable<IHoleRaster> Create2DHoleRasters(ICollection<IHoleRaster> holeRasters);
    }
}
