using XperiCad.EngineeringFramework.Feedback;
using XperiCad.HoleRasterCalculator.Models;
using XperiCad.HoleRasterCalculator.Resources.Feedback.Validators;
using XperiCad.HoleRasterCalculator.Validators;

namespace XperiCad.HoleRasterCalculator.Factories
{
    internal class HoleFactory : IHoleFactory
    {
        #region Fields
        private readonly IHoleValidator _holeValidator;
        private readonly IFeedbackCollector _feedbackCollector;
        #endregion

        #region ctor
        public HoleFactory(IHoleValidator holeValidator, IFeedbackCollector feedbackCollector)
        {
            _holeValidator = holeValidator ?? throw new ArgumentNullException(nameof(holeValidator));
            _feedbackCollector = feedbackCollector ?? throw new ArgumentNullException(nameof(feedbackCollector));
        }
        #endregion

        #region IHoleFactory members
        public IHole CreateHole(decimal x, decimal y, string tag, decimal diameter, decimal depth)
        {
            var result = new Hole(tag, new HolePoint(x, y), diameter, depth);
            if (!_holeValidator.IsHoleValid(result))
            {
                return default;
            }

            return result;
        }

        public ICollection<IHole> CreateHoles(
            IList<decimal> xValues,
            IList<decimal> yValues,
            IList<string> tags,
            IList<decimal> diameters,
            IList<decimal> depths)
        {
            if (xValues is null)
            {
                throw new ArgumentNullException(nameof(xValues));
            }
            if (yValues is null)
            {
                throw new ArgumentNullException(nameof(yValues));
            }
            if (tags is null)
            {
                throw new ArgumentNullException(nameof(tags));
            }
            if (diameters is null)
            {
                throw new ArgumentNullException(nameof(diameters));
            }
            if (depths is null)
            {
                throw new ArgumentNullException(nameof(depths));
            }

            if (IsAllCollectionsCompatible(xValues, yValues, tags, diameters, depths))
            {
                var holeCount = tags.Count;
                var holes = new List<IHole>(holeCount);

                for (int i = 0; i < holeCount; i++)
                {
                    var hole = CreateHole(xValues[i], yValues[i], tags[i], diameters[i], depths[i]);
                    if (hole != null)
                    {
                        holes.Add(hole);
                    }
                }

                if (_holeValidator.IsHoleCollectionValid(holes))
                {
                    return holes;
                }
            }

            return new List<IHole>();
        }
        #endregion

        #region Private methods
        private bool IsAllCollectionsCompatible(
            IList<decimal> xValues,
            IList<decimal> yValues,
            IList<string> tags,
            IList<decimal> diameters,
            IList<decimal> depths)
        {
            if (!xValues.Any())
            {
                _feedbackCollector.AddFeedback(HoleFactoryFeedback.ERROR_XValuesNotProvided);
                return false;
            }
            if (!yValues.Any())
            {
                _feedbackCollector.AddFeedback(HoleFactoryFeedback.ERROR_YValuesNotProvided);
                return false;
            }
            if (!tags.Any())
            {
                _feedbackCollector.AddFeedback(HoleFactoryFeedback.ERROR_TagValuesNotProvided);
                return false;
            }
            if (!diameters.Any())
            {
                _feedbackCollector.AddFeedback(HoleFactoryFeedback.ERROR_DiameterValuesNotProvided);
                return false;
            }
            if (!depths.Any())
            {
                _feedbackCollector.AddFeedback(HoleFactoryFeedback.ERROR_DepthValuesNotProvided);
                return false;
            }

            if (xValues.Count != yValues.Count)
            {
                _feedbackCollector.AddFeedback(HoleFactoryFeedback.ERROR_XAndYValueCountsAreNotEqual,
                                                   xValues.Count.ToString(),
                                                   yValues.Count.ToString());
                return false;
            }
            if (xValues.Count != tags.Count)
            {
                _feedbackCollector.AddFeedback(HoleFactoryFeedback.ERROR_CoordinateAndTagCountsAreNotEqual,
                                                   xValues.Count.ToString(),
                                                   tags.Count.ToString());
                return false;
            }
            if (xValues.Count != diameters.Count)
            {
                _feedbackCollector.AddFeedback(HoleFactoryFeedback.ERROR_CoordinateAndDiameterCountsAreNotEqual,
                                                   xValues.Count.ToString(),
                                                   diameters.Count.ToString());
                return false;
            }
            if (xValues.Count != depths.Count)
            {
                _feedbackCollector.AddFeedback(HoleFactoryFeedback.ERROR_CoordinateAndDepthCountsAreNotEqual,
                                                   xValues.Count.ToString(),
                                                   depths.Count.ToString());
                return false;
            }

            return true;
        }
        #endregion
    }
}
