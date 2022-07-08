using NUnit.Framework;
using System;
using System.Collections.Generic;
using static Math2D.Tests.InputTestData;

namespace Math2D.Tests
{
    [TestFixture]
    public class Math2DTests
    {
        private static double calculationThreshold = 1E-12;
        private static double angleCalculationThreshold = 1E-8;

        private static IEnumerable<TestCaseData> LineSegmentsAndABCCoefficientsSource()
        {
            if (LineSegments.Length != Result_ABCCoefficients.Length)
                throw new ArgumentException($"{nameof(LineSegments)} and {nameof(Result_ABCCoefficients)} lenghts are different.");

            for (int i = 0; i < LineSegments.Length; i++)
                yield return new TestCaseData(LineSegments[i], Result_ABCCoefficients[i]);
        }

        [Test, TestCaseSource(nameof(LineSegmentsAndABCCoefficientsSource))]
        public void LineABCCoefficients_Values_Values(LineSegment segment, LineABCCoefficients coefficients)
        {
            double[] result = Math2D.LineABCCoefficients(segment.X1, segment.Y1, segment.X2, segment.Y2);
            Assert.True(result.Length == 3 && result[0] == coefficients.A
                && result[1] == coefficients.B && result[2] == coefficients.C,
                $"{result[0]}, {result[1]}, {result[2]} are not {coefficients.A}, {coefficients.B}, {coefficients.C}");
        }

        private static IEnumerable<TestCaseData> RelativeToLinePointLocationsSource()
        {
            for (int i = 0; i < SinglePoints.Length; i++)
                for (int j = 0; j < LineSegments.Length; j++)
                    yield return new TestCaseData(SinglePoints[i], LineSegments[j], Result_RelativeToLinePointLocations[i][j]);
        }

        [Test, TestCaseSource(nameof(RelativeToLinePointLocationsSource))]
        public void RelativeToLinePointLocation_Values_Values(SinglePoint point, LineSegment line, int location)
        {
            int result = Math2D.RelativeToLinePointLocation(point.X, point.Y, line.X1, line.Y1, line.X2, line.Y2);
            Assert.True(result == location, ErrorText());

            string ErrorText()
            {
                if (Math.Abs(result) > 1)
                    return $"Result absolute value is more than 1: {result}.";
                else if (Math.Abs(location) > 1)
                    return $"Source absolute value is more than 1: {location}.";
                else if (result != location)
                    return $"{result} is not {location}.";
                else
                    return $"Not a error.";
            }
        }

        private static IEnumerable<TestCaseData> PointsAndLinesSource()
        {
            for (int i = 0; i < SinglePoints.Length; i++)
                for (int j = 0; j < LineSegments.Length; j++)
                    yield return new TestCaseData(SinglePoints[i], LineSegments[j], Result_BetweenPointAndLineDistances[i][j]);
        }

        [Test, TestCaseSource(nameof(PointsAndLinesSource))]
        public void BetweenPointAndLineDistance_Values_Values(SinglePoint point, LineSegment line, double distance)
        {
            double result = Math2D.BetweenPointAndLineDistance(point.X, point.Y, line.X1, line.Y1, line.X2, line.Y2);
            Assert.True(Math.Abs(result - distance) < calculationThreshold, $"{result} is not {distance}.");
        }

        private static IEnumerable<TestCaseData> TwoLinesSource()
        {
            for (int i = 0; i < LineSegments.Length - 1; i++)
                for (int j = i + 1; j < LineSegments.Length; j++)
                    yield return new TestCaseData(LineSegments[i], LineSegments[j], Result_TwoLinesIntersectionPoints[i][j - i - 1], i, j);
        }

        [Test, TestCaseSource(nameof(TwoLinesSource))]
        public void TwoLinesIntersectionPoint_Values_Values(LineSegment firstLine, LineSegment secondLine, SinglePoint? point,
            int firstLineIndex, int secondLineIndex)
        {
            double[]? result = Math2D.TwoLinesIntersectionPoint(firstLine.X1, firstLine.Y1, firstLine.X2, firstLine.Y2,
                secondLine.X1, secondLine.Y1, secondLine.X2, secondLine.Y2);
            Assert.True((result == null && point == null)
                || (result != null && point != null
                    && Math.Abs(result[0] - ((SinglePoint)point).X) < calculationThreshold
                    && Math.Abs(result[1] - ((SinglePoint)point).Y) < calculationThreshold),
                ErrorText());

            string ErrorText()
            {
                if (result == null && point != null)
                    return $"Null is not {{ {((SinglePoint)point).X}, {((SinglePoint)point).Y} }}."
                        + Environment.NewLine + $"First line index: {firstLineIndex}, second line index: {secondLineIndex}.";
                else if (result != null && point == null)
                    return $"{{ {result[0]}, {result[1]} }} is not null."
                        + Environment.NewLine + $"First line index: {firstLineIndex}, second line index: {secondLineIndex}.";
                else if (result != null && point != null)
                    return $"{{ {result[0]}, {result[1]} }} is not {{ {((SinglePoint)point).X}, {((SinglePoint)point).Y} }}."
                        + Environment.NewLine + $"First line index: {firstLineIndex};"
                        + Environment.NewLine + $"Second line index: {secondLineIndex}.";
                else
                    return "Not an error.";
            }
        }

        private static IEnumerable<TestCaseData> AreTwoLinesParallelSource()
        {
            for (int i = 0; i < LineSegments.Length - 1; i++)
                for (int j = i + 1; j < LineSegments.Length; j++)
                    yield return new TestCaseData(LineSegments[i], LineSegments[j], Result_AreTwoLinesParallel[i][j - i - 1], i, j);
        }

        [Test, TestCaseSource(nameof(AreTwoLinesParallelSource))]
        public void AreTwoLinesParallel_Values_Values(LineSegment firstLine, LineSegment secondLine, bool areMatching,
            int firstLineIndex, int secondLineIndex)
        {
            bool result = Math2D.AreTwoLinesParallel(firstLine.X1, firstLine.Y1, firstLine.X2, firstLine.Y2,
                secondLine.X1, secondLine.Y1, secondLine.X2, secondLine.Y2);
            Assert.True(result == areMatching, TwoLineComparisonErrorText(result, areMatching, firstLineIndex, secondLineIndex));
        }

        private static IEnumerable<TestCaseData> AreTwoLinesMatchingSource()
        {
            for (int i = 0; i < LineSegments.Length - 1; i++)
                for (int j = i + 1; j < LineSegments.Length; j++)
                    yield return new TestCaseData(LineSegments[i], LineSegments[j], Result_AreTwoLinesMatching[i][j - i - 1], i, j);
        }

        [Test, TestCaseSource(nameof(AreTwoLinesMatchingSource))]
        public void AreTwoLinesMatching_Values_Values(LineSegment firstLine, LineSegment secondLine, bool areMatching,
            int firstLineIndex, int secondLineIndex)
        {
            bool result = Math2D.AreTwoLinesMatching(firstLine.X1, firstLine.Y1, firstLine.X2, firstLine.Y2,
                secondLine.X1, secondLine.Y1, secondLine.X2, secondLine.Y2);
            Assert.True(result == areMatching, TwoLineComparisonErrorText(result, areMatching, firstLineIndex, secondLineIndex));
        }

        private static IEnumerable<TestCaseData> AreTwoLinesPerpendicularSource()
        {
            for (int i = 0; i < LineSegments.Length - 1; i++)
                for (int j = i + 1; j < LineSegments.Length; j++)
                    yield return new TestCaseData(LineSegments[i], LineSegments[j], Result_AreTwoLinesPerpendicular[i][j - i - 1], i, j);
        }

        [Test, TestCaseSource(nameof(AreTwoLinesPerpendicularSource))]
        public void AreTwoLinesPerpendicular_Values_Values(LineSegment firstLine, LineSegment secondLine, bool areMatching,
            int firstLineIndex, int secondLineIndex)
        {
            bool result = Math2D.AreTwoLinesPerpendicular(firstLine.X1, firstLine.Y1, firstLine.X2, firstLine.Y2,
                secondLine.X1, secondLine.Y1, secondLine.X2, secondLine.Y2);
            Assert.True(result == areMatching, TwoLineComparisonErrorText(result, areMatching, firstLineIndex, secondLineIndex));
        }

        private static IEnumerable<TestCaseData> BetweenTwoLinesAnglesSource()
        {
            for (int i = 0; i < LineSegments.Length - 1; i++)
                for (int j = i + 1; j < LineSegments.Length; j++)
                    yield return new TestCaseData(LineSegments[i], LineSegments[j], Result_BetweenTwoLinesAngles[i][j - i - 1], i, j);
        }

        [Test, TestCaseSource(nameof(BetweenTwoLinesAnglesSource))]
        public void BetweenTwoLinesAngle_Values_Values(LineSegment firstLine, LineSegment secondLine, double angle,
            int firstLineIndex, int secondLineIndex)
        {
            double result = Math2D.BetweenTwoLinesAngle(firstLine.X1, firstLine.Y1, firstLine.X2, firstLine.Y2,
                secondLine.X1, secondLine.Y1, secondLine.X2, secondLine.Y2);
            Assert.True(Math.Abs(result - angle) < angleCalculationThreshold,
                $"{result} is not {angle}."
                + Environment.NewLine + $"First line index: {firstLineIndex};"
                + Environment.NewLine + $"Second line index: {secondLineIndex}.");
        }

        private string TwoLineComparisonErrorText(bool result, bool expected, int firstLineIndex, int secondLineIndex)
        {
            return $"{result} is not {expected}."
                + Environment.NewLine + $"First line index: {firstLineIndex};"
                + Environment.NewLine + $"Second line index: {secondLineIndex}.";
        }
    }
}