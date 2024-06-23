using System.Reflection;
using XperiCad.EngineeringFramework.Feedback;

namespace XperiCad.HoleRasterCalculator.Resources.Feedback.Validators
{
    internal static class HoleFactoryFeedback
    {
        private static readonly Assembly _currentAssembly = Assembly.GetAssembly(typeof(HoleFactoryFeedback)) ?? throw new InvalidOperationException($"Could not load current assembly in {nameof(HoleFactoryFeedback)}.");
        private const string LANGUAGE_RESOURCES_PATH = "XperiCad.HoleRasterCalculator.Resources.Feedback.Validators.i18n.en.FeedbackMessages.json";

        internal static IFeedbackResource ERROR_XValuesNotProvided = new JsonFeedbackResource(Severity.Error, "ERROR_XValuesNotProvided", LANGUAGE_RESOURCES_PATH, _currentAssembly);
        internal static IFeedbackResource ERROR_YValuesNotProvided = new JsonFeedbackResource(Severity.Error, "ERROR_YValuesNotProvided", LANGUAGE_RESOURCES_PATH, _currentAssembly);
        internal static IFeedbackResource ERROR_TagValuesNotProvided = new JsonFeedbackResource(Severity.Error, "ERROR_TagValuesNotProvided", LANGUAGE_RESOURCES_PATH, _currentAssembly);
        internal static IFeedbackResource ERROR_DiameterValuesNotProvided = new JsonFeedbackResource(Severity.Error, "ERROR_DiameterValuesNotProvided", LANGUAGE_RESOURCES_PATH, _currentAssembly);
        internal static IFeedbackResource ERROR_DepthValuesNotProvided = new JsonFeedbackResource(Severity.Error, "ERROR_DepthValuesNotProvided", LANGUAGE_RESOURCES_PATH, _currentAssembly);
        internal static IFeedbackResource ERROR_XAndYValueCountsAreNotEqual = new JsonFeedbackResource(Severity.Error, "ERROR_XAndYValueCountsAreNotEqual", LANGUAGE_RESOURCES_PATH, _currentAssembly);
        internal static IFeedbackResource ERROR_CoordinateAndTagCountsAreNotEqual = new JsonFeedbackResource(Severity.Error, "ERROR_CoordinateAndTagCountsAreNotEqual", LANGUAGE_RESOURCES_PATH, _currentAssembly);
        internal static IFeedbackResource ERROR_CoordinateAndDiameterCountsAreNotEqual = new JsonFeedbackResource(Severity.Error, "ERROR_CoordinateAndDiameterCountsAreNotEqual", LANGUAGE_RESOURCES_PATH, _currentAssembly);
        internal static IFeedbackResource ERROR_CoordinateAndDepthCountsAreNotEqual = new JsonFeedbackResource(Severity.Error, "ERROR_CoordinateAndDepthCountsAreNotEqual", LANGUAGE_RESOURCES_PATH, _currentAssembly);
    }
}
