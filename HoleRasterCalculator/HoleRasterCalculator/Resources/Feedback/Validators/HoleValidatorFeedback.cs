using System.Reflection;
using XperiCad.EngineeringFramework.Feedback;

namespace XperiCad.HoleRasterCalculator.Resources.Feedback.Validators
{
    internal static class HoleValidatorFeedback
    {
        private static readonly Assembly _currentAssembly = Assembly.GetAssembly(typeof(HoleValidatorFeedback)) ?? throw new InvalidOperationException($"Could not load current assembly in {nameof(HoleValidatorFeedback)}.");
        private const string LANGUAGE_RESOURCES_PATH = "XperiCad.HoleRasterCalculator.Resources.Feedback.Validators.i18n.en.FeedbackMessages.json";

        internal static IFeedbackResource ERROR_HoleIsNull = new JsonFeedbackResource(Severity.Error, "ERROR_HoleIsNull", LANGUAGE_RESOURCES_PATH, _currentAssembly);
        internal static IFeedbackResource ERROR_HoleCollectionIsNull = new JsonFeedbackResource(Severity.Error, "ERROR_HoleCollectionIsNull", LANGUAGE_RESOURCES_PATH, _currentAssembly);
        internal static IFeedbackResource ERROR_HoleTagNotFound = new JsonFeedbackResource(Severity.Error, "ERROR_HoleTagNotFound", LANGUAGE_RESOURCES_PATH, _currentAssembly);
        internal static IFeedbackResource ERROR_HoleCoordinatesNotFound = new JsonFeedbackResource(Severity.Error, "ERROR_HoleCoordinatesNotFound", LANGUAGE_RESOURCES_PATH, _currentAssembly);
        internal static IFeedbackResource ERROR_HoleDiameterIsNotPositive = new JsonFeedbackResource(Severity.Error, "ERROR_HoleDiameterIsNotPositive", LANGUAGE_RESOURCES_PATH, _currentAssembly);
        internal static IFeedbackResource ERROR_HoleDepthIsNotPositive = new JsonFeedbackResource(Severity.Error, "ERROR_HoleDepthIsNotPositive", LANGUAGE_RESOURCES_PATH, _currentAssembly);
    }
}
