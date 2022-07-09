using NUnit.Framework;
using System;
using System.Collections.Generic;
using static MathExtension.Tests.InputTestData;

namespace MathExtension.Tests
{
    [TestFixture]
    public class Math2DTests
    {
        private static double calculationThreshold = 1E-12;
        private static double angleCalculationThreshold = 1E-5;

        private static IEnumerable<TestCaseData> LineSegmentsAndABCCoefficientsSource()
        {
            if (LineSegments.Length != Result_ABCCoefficients.Length)
                throw new ArgumentException(
                    $"{nameof(LineSegments)} and {nameof(Result_ABCCoefficients)} lenghts are different: " +
                    $"{LineSegments.Length}; {Result_ABCCoefficients.Length}.");

            for (int i = 0; i < LineSegments.Length; i++)
                yield return new TestCaseData(LineSegments[i], Result_ABCCoefficients[i]);
        }

        [Test, TestCaseSource(nameof(LineSegmentsAndABCCoefficientsSource))]
        public void LineABCCoefficients_Values_Values(LineSegment segment, LineABCCoefficients coefficients)
        {
            double[] result = Math2D.LineABCCoefficients(segment.X1, segment.Y1, segment.X2, segment.Y2);
            Assert.True(IsValidResult(), ErrorText());

            bool IsValidResult()
            {
                return result.Length == 3
                    && result[0] == coefficients.A
                    && result[1] == coefficients.B
                    && result[2] == coefficients.C;
            }

            string ErrorText()
            {
                if (result.Length != 3)
                    return $"Result have not 3 coefficients: {result.Length}.";
                else if (result[0] != coefficients.A || result[1] != coefficients.B || result[2] != coefficients.C)
                    return $"{result[0]}, {result[1]}, {result[2]} are not {coefficients.A}, {coefficients.B}, {coefficients.C}.";
                else
                    return $"Not an error.";
            }
        }

        private static IEnumerable<TestCaseData> VectorAnglesSource()
        {
            if (Vectors.Length != Result_VectorAngles.Length)
                throw new ArgumentException(
                    $"{nameof(Vectors)} and {nameof(Result_VectorAngles)} lenghts are different: " +
                    $"{Vectors.Length}; {Result_VectorAngles.Length}.");

            if (ReversedVectors.Length != Result_ReversedVectorAngles.Length)
                throw new ArgumentException(
                    $"{nameof(ReversedVectors)} and {nameof(Result_ReversedVectorAngles)} lenghts are different: " +
                    $"{ReversedVectors.Length}; {Result_ReversedVectorAngles.Length}.");

            for (int i = 0; i < Vectors.Length; i++)
                yield return new TestCaseData(Vectors[i], Result_VectorAngles[i]);

            for (int i = 0; i < ReversedVectors.Length; i++)
                yield return new TestCaseData(ReversedVectors[i], Result_ReversedVectorAngles[i]);
        }

        [Test, TestCaseSource(nameof(VectorAnglesSource))]
        public void VectorAngle_Values_Values(Vector vector, double angle)
        {
            double result = Math2D.VectorAngle(vector.X, vector.Y);
            Assert.True(Math.Abs(result - angle) < angleCalculationThreshold, $"{result} is not {angle} (vector {vector.X}, {vector.Y}).");
        }

        private static IEnumerable<TestCaseData> RelativeToLinePointLocationsSource()
        {
            for (int i = 0; i < SinglePoints.Length; i++)
                for (int j = 0; j < LineSegments.Length; j++)
                    yield return new TestCaseData(SinglePoints[i], LineSegments[j], Result_RelativeToLinePointLocations[i][j]);
        }

        [Test, TestCaseSource(nameof(RelativeToLinePointLocationsSource))]
        public void RelativeToLinePointLocation_Values_Values(Vector point, LineSegment line, int location)
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
        public void BetweenPointAndLineDistance_Values_Values(Vector point, LineSegment line, double distance)
        {
            double result = Math2D.BetweenPointAndLineDistance(point.X, point.Y, line.X1, line.Y1, line.X2, line.Y2);
            Assert.True(Math.Abs(result - distance) < calculationThreshold, $"{result} is not {distance}.");
        }

        private static IEnumerable<TestCaseData> TwoLinesSource()
        {
            for (int i = 0; i < LineSegments.Length - 1; i++)
                for (int j = i + 1; j < LineSegments.Length; j++)
                    yield return new TestCaseData(LineSegments[i], LineSegments[j], Result_TwoLinesIntersectionPoints[i][j - i - 1]);
        }

        [Test, TestCaseSource(nameof(TwoLinesSource))]
        public void TwoLinesIntersectionPoint_Values_Values(LineSegment firstLine, LineSegment secondLine, Vector? point)
        {
            double[]? result = Math2D.TwoLinesIntersectionPoint(firstLine.X1, firstLine.Y1, firstLine.X2, firstLine.Y2,
                secondLine.X1, secondLine.Y1, secondLine.X2, secondLine.Y2);
            Assert.True(IsResultValid(), ErrorText());

            bool IsResultValid()
            {
                return (result == null && point == null)
                        || (result != null && point != null
                            && Math.Abs(result[0] - ((Vector)point).X) < calculationThreshold
                            && Math.Abs(result[1] - ((Vector)point).Y) < calculationThreshold);
            }

            string ErrorText()
            {
                if (result == null && point != null)
                    return $"Null is not {{ {((Vector)point).X}, {((Vector)point).Y} }}.";
                else if (result != null && point == null)
                    return $"{{ {result[0]}, {result[1]} }} is not null.";
                else if (result != null && point != null)
                    return $"{{ {result[0]}, {result[1]} }} is not {{ {((Vector)point).X}, {((Vector)point).Y} }}.";
                else
                    return "Not an error.";
            }
        }

        private static IEnumerable<TestCaseData> AreTwoLinesParallelSource()
        {
            for (int i = 0; i < LineSegments.Length - 1; i++)
                for (int j = i + 1; j < LineSegments.Length; j++)
                    yield return new TestCaseData(LineSegments[i], LineSegments[j], Result_AreTwoLinesParallel[i][j - i - 1]);
        }

        [Test, TestCaseSource(nameof(AreTwoLinesParallelSource))]
        public void AreTwoLinesParallel_Values_Values(LineSegment firstLine, LineSegment secondLine, bool areMatching)
        {
            bool result = Math2D.AreTwoLinesParallel(firstLine.X1, firstLine.Y1, firstLine.X2, firstLine.Y2,
                secondLine.X1, secondLine.Y1, secondLine.X2, secondLine.Y2);
            Assert.True(result == areMatching, $"{result} is not {areMatching}.");
        }

        private static IEnumerable<TestCaseData> AreTwoLinesMatchingSource()
        {
            for (int i = 0; i < LineSegments.Length - 1; i++)
                for (int j = i + 1; j < LineSegments.Length; j++)
                    yield return new TestCaseData(LineSegments[i], LineSegments[j], Result_AreTwoLinesMatching[i][j - i - 1]);
        }

        [Test, TestCaseSource(nameof(AreTwoLinesMatchingSource))]
        public void AreTwoLinesMatching_Values_Values(LineSegment firstLine, LineSegment secondLine, bool areMatching)
        {
            bool result = Math2D.AreTwoLinesMatching(firstLine.X1, firstLine.Y1, firstLine.X2, firstLine.Y2,
                secondLine.X1, secondLine.Y1, secondLine.X2, secondLine.Y2);
            Assert.True(result == areMatching, $"{result} is not {areMatching}.");
        }

        private static IEnumerable<TestCaseData> AreTwoVectorsCollinearSource()
        {
            for (int i = 0; i < Vectors.Length - 1; i++)
                for (int j = i + 1; j < Vectors.Length; j++)
                    yield return new TestCaseData(Vectors[i], Vectors[j], Result_AreTwoLinesParallel[i][j - i - 1]);
        }

        [Test, TestCaseSource(nameof(AreTwoVectorsCollinearSource))]
        public void AreTwoVectorsCollinear_Values_Values(Vector firstVector, Vector secondVector, bool areMatching)
        {
            bool result = Math2D.AreTwoVectorsCollinear(firstVector.X, firstVector.Y, secondVector.X, secondVector.Y);
            Assert.True(result == areMatching, $"{result} is not {areMatching}.");
        }

        private static IEnumerable<TestCaseData> AreTwoLinesPerpendicularSource()
        {
            for (int i = 0; i < LineSegments.Length - 1; i++)
                for (int j = i + 1; j < LineSegments.Length; j++)
                    yield return new TestCaseData(LineSegments[i], LineSegments[j], Result_AreTwoLinesPerpendicular[i][j - i - 1]);
        }

        [Test, TestCaseSource(nameof(AreTwoLinesPerpendicularSource))]
        public void AreTwoLinesPerpendicular_Values_Values(LineSegment firstLine, LineSegment secondLine, bool areMatching)
        {
            bool result = Math2D.AreTwoLinesPerpendicular(firstLine.X1, firstLine.Y1, firstLine.X2, firstLine.Y2,
                secondLine.X1, secondLine.Y1, secondLine.X2, secondLine.Y2);
            Assert.True(result == areMatching, $"{result} is not {areMatching}.");
        }

        private static IEnumerable<TestCaseData> AreTwoVectorsPerpendicularSource()
        {
            for (int i = 0; i < Vectors.Length - 1; i++)
                for (int j = i + 1; j < Vectors.Length; j++)
                    yield return new TestCaseData(Vectors[i], Vectors[j], Result_AreTwoLinesPerpendicular[i][j - i - 1]);
        }

        [Test, TestCaseSource(nameof(AreTwoVectorsPerpendicularSource))]
        public void AreTwoVectorsPerpendicular_Values_Values(Vector firstVector, Vector secondVector, bool areMatching)
        {
            bool result = Math2D.AreTwoVectorsPerpendicular(firstVector.X, firstVector.Y,
                secondVector.X, secondVector.Y);
            Assert.True(result == areMatching, $"{result} is not {areMatching}.");
        }

        private static IEnumerable<TestCaseData> BetweenTwoLinesAnglesSource()
        {
            for (int i = 0; i < LineSegments.Length - 1; i++)
                for (int j = i + 1; j < LineSegments.Length; j++)
                    yield return new TestCaseData(LineSegments[i], LineSegments[j], Result_BetweenTwoLinesAngles[i][j - i - 1]);
        }

        [Test, TestCaseSource(nameof(BetweenTwoLinesAnglesSource))]
        public void BetweenTwoLinesAngle_Values_Values(LineSegment firstLine, LineSegment secondLine, double angle)
        {
            double result = Math2D.BetweenTwoLinesAngle(firstLine.X1, firstLine.Y1, firstLine.X2, firstLine.Y2,
                secondLine.X1, secondLine.Y1, secondLine.X2, secondLine.Y2);
            Assert.True(Math.Abs(result - angle) < angleCalculationThreshold, $"{result} is not {angle}.");
        }

        private static IEnumerable<TestCaseData> BetweenTwoVectorsAnglesSource()
        {
            if (Vectors.Length != ReversedVectors.Length)
                throw new ArgumentException($"{nameof(Vectors)} and {nameof(ReversedVectors)} lenghts are different: " +
                    $"{Vectors.Length}; {ReversedVectors.Length}.");

            if (Vectors.Length != Result_BetweenTwoReversedVectorsAngles.Length)
                throw new ArgumentException($"{nameof(Vectors)} and {nameof(Result_BetweenTwoReversedVectorsAngles)} lenghts are different: " +
                    $"{Vectors.Length}; {Result_BetweenTwoReversedVectorsAngles.Length}.");

            for (int i = 0; i < Vectors.Length; i++)
                for (int j = 0; j < ReversedVectors.Length; j++)
                    yield return new TestCaseData(Vectors[i], ReversedVectors[j], Result_BetweenTwoReversedVectorsAngles[i][j]);
        }

        [Test, TestCaseSource(nameof(BetweenTwoVectorsAnglesSource))]
        public void BetweenTwoVectorsAngle_Values_Values(Vector firstVector, Vector secondVector, double angle)
        {
            double result = Math2D.BetweenTwoVectorsAngle(firstVector.X, firstVector.Y, secondVector.X, secondVector.Y);
            Assert.True(Math.Abs(result - angle) < angleCalculationThreshold,
                $"{result} is not {angle}." +
                Environment.NewLine + $"First vector: {firstVector.X}, {firstVector.Y};" +
                Environment.NewLine + $"Second vector: {secondVector.X}, {secondVector.Y}.");
        }
    }
}