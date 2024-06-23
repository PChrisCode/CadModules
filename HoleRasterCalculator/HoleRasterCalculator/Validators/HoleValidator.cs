using XperiCad.EngineeringFramework.Feedback;
using XperiCad.HoleRasterCalculator.Models;
using XperiCad.HoleRasterCalculator.Resources.Feedback.Validators;

namespace XperiCad.HoleRasterCalculator.Validators
{
    internal class HoleValidator : IHoleValidator
    {
        #region Fields
        private readonly IFeedbackCollector _feedbackCollector;
        #endregion

        #region ctor
        public HoleValidator(IFeedbackCollector feedbackCollector)
        {
            _feedbackCollector = feedbackCollector ?? throw new ArgumentNullException(nameof(feedbackCollector));
        }
        #endregion

        #region IHoleValidator members
        public bool IsHoleCollectionValid(IEnumerable<IHole> holes)
        {
            if (holes is null)
            {
                _feedbackCollector.AddFeedback(HoleValidatorFeedback.ERROR_HoleCollectionIsNull);
                return false;
            }

            foreach (var hole in holes)
            {
                if (!IsHoleValid(hole))
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsHoleValid(IHole hole)
        {
            if (hole is null)
            {
                _feedbackCollector.AddFeedback(HoleValidatorFeedback.ERROR_HoleIsNull);
                return false;
            }

            if (string.IsNullOrWhiteSpace(hole.Tag))
            {
                _feedbackCollector.AddFeedback(HoleValidatorFeedback.ERROR_HoleTagNotFound);
                return false;
            }

            if (hole.Coordinates is null)
            {
                _feedbackCollector.AddFeedback(HoleValidatorFeedback.ERROR_HoleCoordinatesNotFound);
                return false;
            }

            if (hole.Diameter <= 0)
            {
                _feedbackCollector.AddFeedback(HoleValidatorFeedback.ERROR_HoleDiameterIsNotPositive, hole.Diameter.ToString());
                return false;
            }

            if (hole.Depth <= 0)
            {
                _feedbackCollector.AddFeedback(HoleValidatorFeedback.ERROR_HoleDepthIsNotPositive, hole.Depth.ToString());
                return false;
            }

            return true;
        }
        #endregion
    }
}
