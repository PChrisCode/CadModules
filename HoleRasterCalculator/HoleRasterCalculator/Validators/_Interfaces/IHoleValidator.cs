using XperiCad.HoleRasterCalculator.Models;

namespace XperiCad.HoleRasterCalculator.Validators
{
    internal interface IHoleValidator
    {
        bool IsHoleValid(IHole hole);
        bool IsHoleCollectionValid(IEnumerable<IHole> holes);
    }
}
