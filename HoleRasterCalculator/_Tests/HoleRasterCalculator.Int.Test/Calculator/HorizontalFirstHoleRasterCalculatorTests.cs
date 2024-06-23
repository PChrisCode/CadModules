using Microsoft.Extensions.DependencyInjection;
using XperiCad.EngineeringFramework.Feedback;
using XperiCad.HoleRasterCalculator.Calculator;
using XperiCad.HoleRasterCalculator.Models;

namespace XperiCad.HoleRasterCalculator.Int.Test.Calculator
{
    public class HorizontalFirstHoleRasterCalculatorTests : CommonTestBase
    {
        #region Tests
        [Fact]
        public void HFHRC0011_Given_NullParam_When_CalculatorIsCalled_Then_ThrowArgumentNullException()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var calculator = scope.ServiceProvider.GetRequiredService<IHoleRasterCalculator>();
                Assert.NotNull(calculator);

                Assert.Throws<ArgumentNullException>(() => calculator.CalculateHoleRasters(null));
            }
        }

        [Theory]
        [MemberData(nameof(HFHRC0021_GetTestParameters))]
        public void HFHRC0021_Given_InvalidHoles_When_CalculatorIsCalled_Then_ReturnEmptyListAndGiveFeedback(
            ICollection<IHole> holes)
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var calculator = scope.ServiceProvider.GetRequiredService<IHoleRasterCalculator>();
                Assert.NotNull(calculator);

                var holeRasters = calculator.CalculateHoleRasters(holes);
                Assert.NotNull(holeRasters);
                Assert.Empty(holeRasters);

                var feedbackCollector = scope.ServiceProvider.GetRequiredService<IFeedbackCollector>();
                Assert.Single(feedbackCollector.FeedbackMessages);
            }
        }

        [Theory]
        [MemberData(nameof(HFHRC0031_GetTestParameters))]
        public void HFHRC0031_Given_ValidHoles_When_CalculatorIsCalled_Then_ReturnsWithCalculatedHoleRasters(
            ICollection<IHole> holes,
            IEnumerable<IHoleRaster> expectedHoleRasters)
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                var calculator = scope.ServiceProvider.GetRequiredService<IHoleRasterCalculator>();
                Assert.NotNull(calculator);

                var holeRasters = calculator.CalculateHoleRasters(holes);
                Assert.NotNull(holeRasters);
                Assert.NotEmpty(holeRasters);

                var feedbackCollector = scope.ServiceProvider.GetRequiredService<IFeedbackCollector>();
                Assert.Empty(feedbackCollector.FeedbackMessages);

                var sortedResults = holeRasters.OrderBy(hr => hr.StartingHole.Tag).ToList();
                var sortedExpectedResults = expectedHoleRasters.OrderBy(hr => hr.StartingHole.Tag).ToList();
                Assert.Equal(sortedExpectedResults.Count, sortedResults.Count);

                for (var i = 0; i < sortedResults.Count; i++)
                {
                    Assert.Equal(sortedExpectedResults[i].StartingHole.Tag, sortedResults[i].StartingHole.Tag);
                    Assert.Equal(sortedExpectedResults[i].StartingHole.Diameter, sortedResults[i].StartingHole.Diameter);
                    Assert.Equal(sortedExpectedResults[i].StartingHole.Depth, sortedResults[i].StartingHole.Depth);
                    Assert.Equal(sortedExpectedResults[i].StartingHole.Coordinates.X, sortedResults[i].StartingHole.Coordinates.X);
                    Assert.Equal(sortedExpectedResults[i].StartingHole.Coordinates.Y, sortedResults[i].StartingHole.Coordinates.Y);

                    Assert.Equal(sortedExpectedResults[i].AmountInXDirection, sortedResults[i].AmountInXDirection);
                    Assert.Equal(sortedExpectedResults[i].DistanceBetweenHolesInXDirection, sortedResults[i].DistanceBetweenHolesInXDirection);
                    Assert.Equal(sortedExpectedResults[i].AmountInYDirection, sortedResults[i].AmountInYDirection);
                    Assert.Equal(sortedExpectedResults[i].DistanceBetweenHolesInYDirection, sortedResults[i].DistanceBetweenHolesInYDirection);
                }
            }
        }
        #endregion

        #region Test data
        public static IEnumerable<object[]> HFHRC0021_GetTestParameters()
        {
            yield return new object[] { new List<IHole> { new Hole("Tag1", new HolePoint(3.14m, 1.41m), -2.71m, 9.81m) } };
            yield return new object[] { new List<IHole> { new Hole("Tag1", new HolePoint(-3.14m, 1.41m), 0m, 9.81m) } };
            yield return new object[] { new List<IHole> { new Hole("Tag1", new HolePoint(-3.14m, -1.41m), -2.71m, 0m) } };
            yield return new object[] { new List<IHole> { new Hole("Tag2", new HolePoint(3.14m, 1.41m), 2.71m, -9.81m) } };
            yield return new object[] { new List<IHole> { new Hole("Tag1", new HolePoint(3.14m, 1.41m), -2.71m, 9.81m), new Hole("Tag2", new HolePoint(-3.14m, 1.41m), 2.71m, 9.81m) } };
        }

        public static IEnumerable<object[]> HFHRC0031_GetTestParameters()
        {
            yield return new object[]
            {
                new List<IHole>
                {
                    new Hole("A1", new HolePoint(9m, 27.5m), 8m, 19m),
                    new Hole("A2", new HolePoint(9m, 155.5m), 8m, 19m),
                    new Hole("A3", new HolePoint(9m, 283.5m), 8m, 19m),
                    new Hole("A4", new HolePoint(9m, 411.5m), 8m, 19m),
                },
                new List<IHoleRaster>
                {
                    new HoleRaster(new Hole("A1", new HolePoint(9m, 27.5m), 8m, 19m), 1, 0, 4, 128)
                }
            };
            yield return new object[] {
                new List<IHole>
                {
                    new Hole("A1", new HolePoint(9m, 30.5m), 8m, 14m),
                    new Hole("A2", new HolePoint(9m, 158.5m), 8m, 14m),
                    new Hole("A3", new HolePoint(9m, 286.5m), 8m, 14m),
                    new Hole("A4", new HolePoint(9m, 414.5m), 8m, 14m),
                    new Hole("B1", new HolePoint(86m, 388m), 5m, 14m),
                    new Hole("B2", new HolePoint(118m, 388m), 5m, 14m),
                    new Hole("B3", new HolePoint(541.25m, 388m), 5m, 14m),
                    new Hole("B4", new HolePoint(573.25m, 388m), 5m, 14m),
                    new Hole("B5", new HolePoint(996.5m, 388m), 5m, 14m),
                    new Hole("B6", new HolePoint(1028.5m, 388m), 5m, 14m),
                    new Hole("B7", new HolePoint(1451.75m, 388m), 5m, 14m),
                    new Hole("B8", new HolePoint(1483.75m, 388m), 5m, 14m),
                    new Hole("B9", new HolePoint(1907m, 388m), 5m, 14m),
                    new Hole("B10", new HolePoint(1939m, 388m), 5m, 14m),
                },
                new List<IHoleRaster>
                {
                    new HoleRaster(new Hole("A1", new HolePoint(9m, 30.5m), 8m, 14m), 1, 0, 4, 128),
                    new HoleRaster(new Hole("B1", new HolePoint(86m, 388m), 5m, 14m), 2, 32, 1, 0),
                    new HoleRaster(new Hole("B3", new HolePoint(541.25m, 388m), 5m, 14m), 2, 32, 1, 0),
                    new HoleRaster(new Hole("B5", new HolePoint(996.5m, 388m), 5m, 14m), 2, 32, 1, 0),
                    new HoleRaster(new Hole("B7", new HolePoint(1451.75m, 388m), 5m, 14m), 2, 32, 1, 0),
                    new HoleRaster(new Hole("B9", new HolePoint(1907m, 388m), 5m, 14m), 2, 32, 1, 0),
                    //TODO: add 56*2 C-type holes?
                }
            };
        }
        #endregion
    }
}
