using Microsoft.Extensions.DependencyInjection;
using XperiCad.EngineeringFramework.Feedback;
using XperiCad.HoleRasterCalculator.Factories;

namespace XperiCad.HoleRasterCalculator.Int.Test.Factories
{
    public class HoleFactoryTests : CommonTestBase
    {
        #region Tests
        [Theory]
        [MemberData(nameof(HF0011_GetTestParameters))]
        public void HF0011_Given_NullParam_When_CreateHoles_Then_ThrowArgumentNullException(
            IList<decimal> xValues,
            IList<decimal> yValues,
            IList<string> tags,
            IList<decimal> diameters,
            IList<decimal> depths)
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var factory = scope.ServiceProvider.GetRequiredService<IHoleFactory>();

                Assert.Throws<ArgumentNullException>(() => factory.CreateHoles(xValues, yValues, tags, diameters, depths));
            }
        }

        [Theory]
        [MemberData(nameof(HF0021_GetTestParameters))]
        public void HF0021_Given_InvalidData_When_CreateHoles_Then_ReturnEmptyListAndGiveFeedback(
            IList<decimal> xValues,
            IList<decimal> yValues,
            IList<string> tags,
            IList<decimal> diameters,
            IList<decimal> depths)
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var factory = scope.ServiceProvider.GetRequiredService<IHoleFactory>();

                var holes = factory.CreateHoles(xValues, yValues, tags, diameters, depths);
                Assert.NotNull(holes);
                Assert.Empty(holes);

                var feedbackCollector = scope.ServiceProvider.GetRequiredService<IFeedbackCollector>();
                Assert.Single(feedbackCollector.FeedbackMessages);
            }
        }

        [Theory]
        [MemberData(nameof(HF0031_GetTestParameters))]
        public void HF0031_Given_InvalidData_When_CreateHole_Then_ReturnNullAndGiveFeedback(
            decimal x,
            decimal y,
            string tag,
            decimal diameter,
            decimal depth)
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var factory = scope.ServiceProvider.GetRequiredService<IHoleFactory>();

                var hole = factory.CreateHole(x, y, tag, diameter, depth);
                Assert.Null(hole);

                var feedbackCollector = scope.ServiceProvider.GetRequiredService<IFeedbackCollector>();
                Assert.Single(feedbackCollector.FeedbackMessages);
            }
        }

        [Theory]
        [MemberData(nameof(HF0041_GetTestParameters))]
        public void HF0041_Given_ValidData_When_CreateHoles_Then_ReturnsWithHoleList(
            IList<decimal> xValues,
            IList<decimal> yValues,
            IList<string> tags,
            IList<decimal> diameters,
            IList<decimal> depths,
            int expectedResultCount)
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var factory = scope.ServiceProvider.GetRequiredService<IHoleFactory>();

                var holes = factory.CreateHoles(xValues, yValues, tags, diameters, depths);
                Assert.NotNull(holes);
                Assert.NotEmpty(holes);
                Assert.Equal(expectedResultCount, holes.Count);

                var feedbackCollector = scope.ServiceProvider.GetRequiredService<IFeedbackCollector>();
                Assert.Empty(feedbackCollector.FeedbackMessages);

                var holeCount = holes.Count;
                var holeList = holes.ToList();
                for (int i = 0; i < holeCount; i++)
                {
                    Assert.Equal(xValues[i], holeList[i].Coordinates.X);
                    Assert.Equal(yValues[i], holeList[i].Coordinates.Y);
                    Assert.Equal(tags[i], holeList[i].Tag);
                    Assert.Equal(diameters[i], holeList[i].Diameter);
                    Assert.Equal(depths[i], holeList[i].Depth);
                }
            }
        }

        [Theory]
        [MemberData(nameof(HF0051_GetTestParameters))]
        public void HF0051_Given_ValidData_When_CreateHole_Then_ReturnsWithHoleObject(
            decimal x,
            decimal y,
            string tag,
            decimal diameter,
            decimal depth)
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var factory = scope.ServiceProvider.GetRequiredService<IHoleFactory>();

                var hole = factory.CreateHole(x, y, tag, diameter, depth);
                Assert.NotNull(hole);

                var feedbackCollector = scope.ServiceProvider.GetRequiredService<IFeedbackCollector>();
                Assert.Empty(feedbackCollector.FeedbackMessages);

                Assert.Equal(x, hole.Coordinates.X);
                Assert.Equal(y, hole.Coordinates.Y);
                Assert.Equal(tag, hole.Tag);
                Assert.Equal(diameter, hole.Diameter);
                Assert.Equal(depth, hole.Depth);
            }
        }
        #endregion

        #region Test data
        public static IEnumerable<object[]> HF0011_GetTestParameters()
        {
            yield return new object[] { null, new List<decimal>(), new List<string>(), new List<decimal>(), new List<decimal>(), };
            yield return new object[] { new List<decimal>(), null, new List<string>(), new List<decimal>(), new List<decimal>(), };
            yield return new object[] { new List<decimal>(), new List<decimal>(), null, new List<decimal>(), new List<decimal>(), };
            yield return new object[] { new List<decimal>(), new List<decimal>(), new List<string>(), null, new List<decimal>(), };
            yield return new object[] { new List<decimal>(), new List<decimal>(), new List<string>(), new List<decimal>(), null, };
        }

        public static IEnumerable<object[]> HF0021_GetTestParameters()
        {
            yield return new object[] { new List<decimal>() { 5m }, new List<decimal>() { 3.14m }, new List<string>() { null }, new List<decimal>() { 9.81m }, new List<decimal>() { 6.4m } };
            yield return new object[] { new List<decimal>() { 5m }, new List<decimal>() { 3.14m }, new List<string>() { "" }, new List<decimal>() { 9.81m }, new List<decimal>() { 6.4m } };
            yield return new object[] { new List<decimal>() { 5m }, new List<decimal>() { 3.14m }, new List<string>() { "   " }, new List<decimal>() { 9.81m }, new List<decimal>() { 6.4m } };

            yield return new object[] { new List<decimal>(), new List<decimal>() { 3.14m }, new List<string>() { "Test1" }, new List<decimal>() { -9.81m }, new List<decimal>() { 6.4m } };
            yield return new object[] { new List<decimal>() { 3.14m }, new List<decimal>(), new List<string>() { "Test1" }, new List<decimal>() { -9.81m }, new List<decimal>() { 6.4m } };
            yield return new object[] { new List<decimal>() { 42m }, new List<decimal>() { 3.14m }, new List<string>(), new List<decimal>() { -9.81m }, new List<decimal>() { 6.4m } };
            yield return new object[] { new List<decimal>() { 42m }, new List<decimal>() { 3.14m }, new List<string>() { "Test1" }, new List<decimal>(), new List<decimal>() { 6.4m } };
            yield return new object[] { new List<decimal>() { 42m }, new List<decimal>() { 3.14m }, new List<string>() { "Test1" }, new List<decimal>() { -9.81m }, new List<decimal>() };

            yield return new object[] { new List<decimal>() { 42m }, new List<decimal>() { 3.14m, 2.7m }, new List<string>() { "Test1" }, new List<decimal>() { -9.81m }, new List<decimal>() { 6.4m } };
            yield return new object[] { new List<decimal>() { 42m, 19.84m }, new List<decimal>() { 3.14m }, new List<string>() { "Test1" }, new List<decimal>() { -9.81m }, new List<decimal>() { 6.4m } };

            yield return new object[] { new List<decimal>() { 42m }, new List<decimal>() { 3.14m }, new List<string>() { "Test1", "Test2" }, new List<decimal>() { -9.81m }, new List<decimal>() { 6.4m } };
            yield return new object[] { new List<decimal>() { 42m, 1m }, new List<decimal>() { 3.14m, 2m }, new List<string>() { "Test1" }, new List<decimal>() { -9.81m }, new List<decimal>() { 6.4m } };

            yield return new object[] { new List<decimal>() { 42m }, new List<decimal>() { 3.14m }, new List<string>() { "Test1" }, new List<decimal>() { -9.81m, 0m }, new List<decimal>() { 6.4m } };
            yield return new object[] { new List<decimal>() { 42m, 1m }, new List<decimal>() { 3.14m, 6m }, new List<string>() { "Test1", "asd" }, new List<decimal>() { -9.81m }, new List<decimal>() { 6.4m } };

            yield return new object[] { new List<decimal>() { 42m }, new List<decimal>() { 3.14m }, new List<string>() { "Test1" }, new List<decimal>() { -9.81m }, new List<decimal>() { 6.4m, -32.5m } };
            yield return new object[] { new List<decimal>() { 42m, 32m }, new List<decimal>() { 3.14m, 7.6m }, new List<string>() { "Test1", "Test2" }, new List<decimal>() { -9.81m, 3.57m }, new List<decimal>() { 6.4m } };
        }

        public static IEnumerable<object[]> HF0031_GetTestParameters()
        {
            yield return new object[] { 3.14m, 1.41m, "Tag1", -2.71m, 9.81m };
            yield return new object[] { 3.14m, 1.41m, "Tag1", 0m, 9.81m };
            yield return new object[] { 3.14m, 1.41m, "Tag1", -2.71m, 0m };
            yield return new object[] { 3.14m, 1.41m, "Tag2", 2.71m, -9.81m };
        }

        public static IEnumerable<object[]> HF0041_GetTestParameters()
        {
            yield return new object[] { new List<decimal>() { 6m }, new List<decimal>() { 3.14m }, new List<string>() { "Test1" }, new List<decimal>() { 9.81m }, new List<decimal>() { 6.4m }, 1 };
            yield return new object[] { new List<decimal>() { 6m, 2m }, new List<decimal>() { 3.14m, 0m }, new List<string>() { "Test1", "Test2" }, new List<decimal>() { 9.81m, 3m }, new List<decimal>() { 6.4m, 2.75m }, 2 };
        }

        public static IEnumerable<object[]> HF0051_GetTestParameters()
        {
            yield return new object[] { 3.14m, 1.41m, "Tag1", 2.71m, 9.81m };
            yield return new object[] { 3.14m, 1.41m, "Tag2", 8.42m, 27.1m };
            yield return new object[] { 0m, 0m, "Tag3", 0.1m, 0.1m };
            yield return new object[] { 4563.14m, 1984m, "Tag4", 1848.49m, 11000m };
        }
        #endregion
    }
}
