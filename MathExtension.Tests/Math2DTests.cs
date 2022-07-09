using NUnit.Framework;
using System;
using System.Collections.Generic;
using static MathExtension.Math2D;
using static MathExtension.Tests.InputTestData;

namespace MathExtension.Tests
{
    [TestFixture]
    public class Math2DTests
    {
        private static double calculationThreshold = 1E-12;
        private static double angleCalculationThreshold = 1E-5;

        #region [   LineABCCoefficients     ]
        private static IEnumerable<TestCaseData> LineSegmentsAndABCCoefficientsSource()
        {
            CheckSameLengthArrays(LineSegments, nameof(LineSegments), Result_ABCCoefficients, nameof(Result_ABCCoefficients));

            for (int i = 0; i < LineSegments.Length; i++)
                yield return new TestCaseData(LineSegments[i], Result_ABCCoefficients[i]);
        }

        [Test, TestCaseSource(nameof(LineSegmentsAndABCCoefficientsSource))]
        public void LineABCCoefficients_Doubles_Values(LineSegment segment, LineABCCoefficients validValues)
        {
            double[] result = LineABCCoefficients(segment.X1, segment.Y1, segment.X2, segment.Y2);
            Assert.True(AreValidLineABCCoefficients(result, validValues), LineABCCoefficientsErrorMessage(result, validValues));
        }

        [Test, TestCaseSource(nameof(LineSegmentsAndABCCoefficientsSource))]
        public void LineABCCoefficients_Vectors_Values(LineSegment segment, LineABCCoefficients validValues)
        {
            double[] result = LineABCCoefficients(new Vector2D(segment.X1, segment.Y1), new Vector2D(segment.X2, segment.Y2));
            Assert.True(AreValidLineABCCoefficients(result, validValues), LineABCCoefficientsErrorMessage(result, validValues));
        }

        private bool AreValidLineABCCoefficients(double[] result, LineABCCoefficients validValues)
        {
            return result.Length == 3
                && result[0] == validValues.A
                && result[1] == validValues.B
                && result[2] == validValues.C;
        }

        private string LineABCCoefficientsErrorMessage(double[] result, LineABCCoefficients validValues)
        {
            if (result.Length != 3)
                return $"Result have not 3 coefficients: {result.Length}.";
            else if (result[0] != validValues.A || result[1] != validValues.B || result[2] != validValues.C)
                return $"{result[0]}, {result[1]}, {result[2]} are not {validValues.A}, {validValues.B}, {validValues.C}.";
            else
                return $"Not an error.";
        }
        #endregion

        #region [   VectorAngle     ]
        private static IEnumerable<TestCaseData> VectorAnglesSource()
        {
            CheckSameLengthArrays(Vectors, nameof(Vectors), Result_VectorAngles, nameof(Result_VectorAngles));
            CheckSameLengthArrays(ReversedVectors, nameof(ReversedVectors), Result_ReversedVectorAngles, nameof(Result_ReversedVectorAngles));

            for (int i = 0; i < Vectors.Length; i++)
                yield return new TestCaseData(Vectors[i], Result_VectorAngles[i]);

            for (int i = 0; i < ReversedVectors.Length; i++)
                yield return new TestCaseData(ReversedVectors[i], Result_ReversedVectorAngles[i]);
        }

        [Test, TestCaseSource(nameof(VectorAnglesSource))]
        public void VectorAngle_Doubles_Values(Vector vector, double validvalue)
        {
            double result = VectorAngle(vector.X, vector.Y);
            Assert.True(IsCalculatedResultValid(result, validvalue, angleCalculationThreshold), VectorAngleErrorMessage(result, validvalue, vector));
        }

        [Test, TestCaseSource(nameof(VectorAnglesSource))]
        public void VectorAngle_Vectors_Values(Vector vector, double validvalue)
        {
            double result = VectorAngle(new Vector2D(vector.X, vector.Y));
            Assert.True(IsCalculatedResultValid(result, validvalue, angleCalculationThreshold), VectorAngleErrorMessage(result, validvalue, vector));
        }

        private string VectorAngleErrorMessage(double result, double validvalue, Vector vector)
            => $"{result} is not {validvalue} (vector {vector.X}, {vector.Y}).";
        #endregion

        #region [   RelativeToLinePointLocation     ]
        private static IEnumerable<TestCaseData> RelativeToLinePointLocationsSource()
        {
            CheckSameLengthArrays(SinglePoints, nameof(SinglePoints), Result_RelativeToLinePointLocations,
                nameof(Result_RelativeToLinePointLocations));
            CheckArray(LineSegments, nameof(LineSegments));

            for (int i = 0; i < SinglePoints.Length; i++)
                for (int j = 0; j < LineSegments.Length; j++)
                {
                    CheckSameLengthArrays(LineSegments, nameof(LineSegments),
                        Result_RelativeToLinePointLocations[i], nameof(Result_RelativeToLinePointLocations));
                    yield return new TestCaseData(SinglePoints[i], LineSegments[j], Result_RelativeToLinePointLocations[i][j]);
                }
        }

        [Test, TestCaseSource(nameof(RelativeToLinePointLocationsSource))]
        public void RelativeToLinePointLocation_Doubles_Values(Vector point, LineSegment line, int validValue)
        {
            int result = RelativeToLinePointLocation(point.X, point.Y, line.X1, line.Y1, line.X2, line.Y2);
            Assert.True(result == validValue, RelativeToLinePointLocationErrorMessage(result, validValue));
        }

        [Test, TestCaseSource(nameof(RelativeToLinePointLocationsSource))]
        public void RelativeToLinePointLocation_Vectors_Values(Vector point, LineSegment line, int validValue)
        {
            int result = RelativeToLinePointLocation(new Vector2D(point.X, point.Y), new Vector2D(line.X1, line.Y1), new Vector2D(line.X2, line.Y2));
            Assert.True(result == validValue, RelativeToLinePointLocationErrorMessage(result, validValue));
        }

        private string RelativeToLinePointLocationErrorMessage(int result, int validValue)
        {
            if (Math.Abs(result) > 1)
                return $"Result absolute value is more than 1: {result}.";
            else if (Math.Abs(validValue) > 1)
                return $"Source absolute value is more than 1: {validValue}.";
            else if (result != validValue)
                return $"{result} is not {validValue}.";
            else
                return $"Not an error.";
        }
        #endregion

        #region [   BetweenPointAndLineDistance     ]
        private static IEnumerable<TestCaseData> PointsAndLinesSource()
        {
            CheckSameLengthArrays(SinglePoints, nameof(SinglePoints), Result_BetweenPointAndLineDistances,
                nameof(Result_BetweenPointAndLineDistances));
            CheckArray(LineSegments, nameof(LineSegments));

            for (int i = 0; i < SinglePoints.Length; i++)
                for (int j = 0; j < LineSegments.Length; j++)
                {
                    CheckSameLengthArrays(LineSegments, nameof(LineSegments),
                        Result_BetweenPointAndLineDistances[i], nameof(Result_BetweenPointAndLineDistances));
                    yield return new TestCaseData(SinglePoints[i], LineSegments[j], Result_BetweenPointAndLineDistances[i][j]);
                }
        }

        [Test, TestCaseSource(nameof(PointsAndLinesSource))]
        public void BetweenPointAndLineDistance_Doubles_Values(Vector point, LineSegment line, double validValue)
        {
            double result = BetweenPointAndLineDistance(point.X, point.Y, line.X1, line.Y1, line.X2, line.Y2);
            Assert.True(IsCalculatedResultValid(result, validValue, calculationThreshold), ResultIsNotValidValueErrorMessage(result, validValue));
        }

        [Test, TestCaseSource(nameof(PointsAndLinesSource))]
        public void BetweenPointAndLineDistance_Vectors_Values(Vector point, LineSegment line, double validValue)
        {
            double result = BetweenPointAndLineDistance(new Vector2D(point.X, point.Y),
                new Vector2D(line.X1, line.Y1), new Vector2D(line.X2, line.Y2));
            Assert.True(IsCalculatedResultValid(result, validValue, calculationThreshold), ResultIsNotValidValueErrorMessage(result, validValue));
        }
        #endregion

        #region [   TwoLinesIntersectionPoint   ]
        private static IEnumerable<TestCaseData> TwoLinesSource()
        {
            CheckArray(LineSegments, nameof(LineSegments));
            CheckArray(Result_TwoLinesIntersectionPoints, nameof(Result_TwoLinesIntersectionPoints), LineSegments.Length - 1);

            for (int i = 0; i < LineSegments.Length - 1; i++)
                for (int j = i + 1; j < LineSegments.Length; j++)
                {
                    CheckArray(Result_TwoLinesIntersectionPoints[i], nameof(Result_TwoLinesIntersectionPoints) + $"[{i}]", LineSegments.Length - i - 1);
                    yield return new TestCaseData(LineSegments[i], LineSegments[j], Result_TwoLinesIntersectionPoints[i][j - i - 1]);
                }
        }

        [Test, TestCaseSource(nameof(TwoLinesSource))]
        public void TwoLinesIntersectionPoint_Doubles_Values(LineSegment firstLine, LineSegment secondLine, Vector? point)
        {
            double[]? result = TwoLinesIntersectionPoint(firstLine.X1, firstLine.Y1, firstLine.X2, firstLine.Y2,
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

        [Test, TestCaseSource(nameof(TwoLinesSource))]
        public void TwoLinesIntersectionPoint_Vectors_Values(LineSegment firstLine, LineSegment secondLine, Vector? point)
        {
            Vector2D? result = TwoLinesIntersectionPoint(new Vector2D(firstLine.X1, firstLine.Y1), new Vector2D(firstLine.X2, firstLine.Y2),
                new Vector2D(secondLine.X1, secondLine.Y1), new Vector2D(secondLine.X2, secondLine.Y2));
            Assert.True(IsResultValid(), ErrorText());

            bool IsResultValid()
            {
                return (result == null && point == null)
                        || (result != null && point != null
                            && Math.Abs(((Vector2D)result).X - ((Vector)point).X) < calculationThreshold
                            && Math.Abs(((Vector2D)result).Y - ((Vector)point).Y) < calculationThreshold);
            }

            string ErrorText()
            {
                if (result == null && point != null)
                    return $"Null is not {{ {((Vector)point).X}, {((Vector)point).Y} }}.";
                else if (result != null && point == null)
                    return $"{{ {((Vector2D)result).X}, {((Vector2D)result).Y} }} is not null.";
                else if (result != null && point != null)
                    return $"{{ {((Vector2D)result).X}, {((Vector2D)result).Y} }} is not {{ {((Vector)point).X}, {((Vector)point).Y} }}.";
                else
                    return "Not an error.";
            }
        }
        #endregion

        #region [   AreTwoLinesParallel     ]
        private static IEnumerable<TestCaseData> AreTwoLinesParallelSource()
        {
            CheckArray(LineSegments, nameof(LineSegments));
            CheckArray(Result_AreTwoLinesParallel, nameof(Result_AreTwoLinesParallel), LineSegments.Length - 1);

            for (int i = 0; i < LineSegments.Length - 1; i++)
                for (int j = i + 1; j < LineSegments.Length; j++)
                {
                    CheckArray(Result_AreTwoLinesParallel[i], nameof(Result_AreTwoLinesParallel) + $"[{i}]", LineSegments.Length - i - 1);
                    yield return new TestCaseData(LineSegments[i], LineSegments[j], Result_AreTwoLinesParallel[i][j - i - 1]);
                }
        }

        [Test, TestCaseSource(nameof(AreTwoLinesParallelSource))]
        public void AreTwoLinesParallel_Doubles_Values(LineSegment firstLine, LineSegment secondLine, bool validValue)
        {
            bool result = AreTwoLinesParallel(firstLine.X1, firstLine.Y1, firstLine.X2, firstLine.Y2,
                secondLine.X1, secondLine.Y1, secondLine.X2, secondLine.Y2);
            Assert.True(result == validValue, ResultIsNotValidValueErrorMessage(result, validValue));
        }

        [Test, TestCaseSource(nameof(AreTwoLinesParallelSource))]
        public void AreTwoLinesParallel_Vectors_Values(LineSegment firstLine, LineSegment secondLine, bool validValue)
        {
            bool result = AreTwoLinesParallel(new Vector2D(firstLine.X1, firstLine.Y1), new Vector2D(firstLine.X2, firstLine.Y2),
                new Vector2D(secondLine.X1, secondLine.Y1), new Vector2D(secondLine.X2, secondLine.Y2));
            Assert.True(result == validValue, ResultIsNotValidValueErrorMessage(result, validValue));
        }
        #endregion

        #region [   AreTwoLinesMatching     ]
        private static IEnumerable<TestCaseData> AreTwoLinesMatchingSource()
        {
            CheckArray(LineSegments, nameof(LineSegments));
            CheckArray(Result_AreTwoLinesMatching, nameof(Result_AreTwoLinesMatching), LineSegments.Length - 1);

            for (int i = 0; i < LineSegments.Length - 1; i++)
                for (int j = i + 1; j < LineSegments.Length; j++)
                {
                    CheckArray(Result_AreTwoLinesMatching[i], nameof(Result_AreTwoLinesMatching) + $"[{i}]", LineSegments.Length - i - 1);
                    yield return new TestCaseData(LineSegments[i], LineSegments[j], Result_AreTwoLinesMatching[i][j - i - 1]);
                }
        }

        [Test, TestCaseSource(nameof(AreTwoLinesMatchingSource))]
        public void AreTwoLinesMatching_Doubles_Values(LineSegment firstLine, LineSegment secondLine, bool validValue)
        {
            bool result = AreTwoLinesMatching(firstLine.X1, firstLine.Y1, firstLine.X2, firstLine.Y2,
                secondLine.X1, secondLine.Y1, secondLine.X2, secondLine.Y2);
            Assert.True(result == validValue, ResultIsNotValidValueErrorMessage(result, validValue));
        }

        [Test, TestCaseSource(nameof(AreTwoLinesMatchingSource))]
        public void AreTwoLinesMatching_Vectors_Values(LineSegment firstLine, LineSegment secondLine, bool validValue)
        {
            bool result = AreTwoLinesMatching(new Vector2D(firstLine.X1, firstLine.Y1), new Vector2D(firstLine.X2, firstLine.Y2),
                new Vector2D(secondLine.X1, secondLine.Y1), new Vector2D(secondLine.X2, secondLine.Y2));
            Assert.True(result == validValue, ResultIsNotValidValueErrorMessage(result, validValue));
        }
        #endregion

        #region [   AreTwoVectorsCollinear  ]
        private static IEnumerable<TestCaseData> AreTwoVectorsCollinearSource()
        {
            CheckArray(Vectors, nameof(Vectors));
            CheckArray(Result_AreTwoLinesParallel, nameof(Result_AreTwoLinesParallel), Vectors.Length - 1);

            for (int i = 0; i < Vectors.Length - 1; i++)
                for (int j = i + 1; j < Vectors.Length; j++)
                {
                    CheckArray(Result_AreTwoLinesParallel[i], nameof(Result_AreTwoLinesParallel) + $"[{i}]", Vectors.Length - i - 1);
                    yield return new TestCaseData(Vectors[i], Vectors[j], Result_AreTwoLinesParallel[i][j - i - 1]);
                }
        }

        [Test, TestCaseSource(nameof(AreTwoVectorsCollinearSource))]
        public void AreTwoVectorsCollinear_Doubles_Values(Vector firstVector, Vector secondVector, bool validValue)
        {
            bool result = AreTwoVectorsCollinear(firstVector.X, firstVector.Y, secondVector.X, secondVector.Y);
            Assert.True(result == validValue, ResultIsNotValidValueErrorMessage(result, validValue));
        }

        [Test, TestCaseSource(nameof(AreTwoVectorsCollinearSource))]
        public void AreTwoVectorsCollinear_Vectors_Values(Vector firstVector, Vector secondVector, bool validValue)
        {
            bool result = AreTwoVectorsCollinear(new Vector2D(firstVector.X, firstVector.Y), new Vector2D(secondVector.X, secondVector.Y));
            Assert.True(result == validValue, ResultIsNotValidValueErrorMessage(result, validValue));
        }
        #endregion

        #region [   AreTwoLinesPerpendicular    ]
        private static IEnumerable<TestCaseData> AreTwoLinesPerpendicularSource()
        {
            CheckArray(LineSegments, nameof(LineSegments));
            CheckArray(Result_AreTwoLinesPerpendicular, nameof(Result_AreTwoLinesPerpendicular), LineSegments.Length - 1);

            for (int i = 0; i < LineSegments.Length - 1; i++)
                for (int j = i + 1; j < LineSegments.Length; j++)
                {
                    CheckArray(Result_AreTwoLinesPerpendicular[i], nameof(Result_AreTwoLinesPerpendicular) + $"[{i}]", LineSegments.Length - i - 1);
                    yield return new TestCaseData(LineSegments[i], LineSegments[j], Result_AreTwoLinesPerpendicular[i][j - i - 1]);
                }
        }

        [Test, TestCaseSource(nameof(AreTwoLinesPerpendicularSource))]
        public void AreTwoLinesPerpendicular_Doubles_Values(LineSegment firstLine, LineSegment secondLine, bool validValue)
        {
            bool result = AreTwoLinesPerpendicular(firstLine.X1, firstLine.Y1, firstLine.X2, firstLine.Y2,
                secondLine.X1, secondLine.Y1, secondLine.X2, secondLine.Y2);
            Assert.True(result == validValue, ResultIsNotValidValueErrorMessage(result, validValue));
        }

        [Test, TestCaseSource(nameof(AreTwoLinesPerpendicularSource))]
        public void AreTwoLinesPerpendicular_Vectors_Values(LineSegment firstLine, LineSegment secondLine, bool validValue)
        {
            bool result = AreTwoLinesPerpendicular(new Vector2D(firstLine.X1, firstLine.Y1), new Vector2D(firstLine.X2, firstLine.Y2),
                new Vector2D(secondLine.X1, secondLine.Y1), new Vector2D(secondLine.X2, secondLine.Y2));
            Assert.True(result == validValue, ResultIsNotValidValueErrorMessage(result, validValue));
        }
        #endregion

        #region [   AreTwoVectorsPerpendicular  ]
        private static IEnumerable<TestCaseData> AreTwoVectorsPerpendicularSource()
        {
            CheckArray(Vectors, nameof(Vectors));
            CheckArray(Result_AreTwoLinesPerpendicular, nameof(Result_AreTwoLinesPerpendicular), Vectors.Length - 1);

            for (int i = 0; i < Vectors.Length - 1; i++)
                for (int j = i + 1; j < Vectors.Length; j++)
                {
                    CheckArray(Result_AreTwoLinesPerpendicular[i], nameof(Result_AreTwoLinesPerpendicular) + $"[{i}]", Vectors.Length - i - 1);
                    yield return new TestCaseData(Vectors[i], Vectors[j], Result_AreTwoLinesPerpendicular[i][j - i - 1]);
                }
        }

        [Test, TestCaseSource(nameof(AreTwoVectorsPerpendicularSource))]
        public void AreTwoVectorsPerpendicular_Doubles_Values(Vector firstVector, Vector secondVector, bool validValue)
        {
            bool result = AreTwoVectorsPerpendicular(firstVector.X, firstVector.Y,
                secondVector.X, secondVector.Y);
            Assert.True(result == validValue, ResultIsNotValidValueErrorMessage(result, validValue));
        }

        [Test, TestCaseSource(nameof(AreTwoVectorsPerpendicularSource))]
        public void AreTwoVectorsPerpendicular_Vectors_Values(Vector firstVector, Vector secondVector, bool validValue)
        {
            bool result = AreTwoVectorsPerpendicular(new Vector2D(firstVector.X, firstVector.Y),
                new Vector2D(secondVector.X, secondVector.Y));
            Assert.True(result == validValue, ResultIsNotValidValueErrorMessage(result, validValue));
        }
        #endregion

        #region [   BetweenTwoLinesAngle    ]
        private static IEnumerable<TestCaseData> BetweenTwoLinesAnglesSource()
        {
            CheckArray(LineSegments, nameof(LineSegments));
            CheckArray(Result_BetweenTwoLinesAngles, nameof(Result_BetweenTwoLinesAngles), LineSegments.Length - 1);

            for (int i = 0; i < LineSegments.Length - 1; i++)
                for (int j = i + 1; j < LineSegments.Length; j++)
                {
                    CheckArray(Result_BetweenTwoLinesAngles[i], nameof(Result_BetweenTwoLinesAngles) + $"[{i}]", LineSegments.Length - i - 1);
                    yield return new TestCaseData(LineSegments[i], LineSegments[j], Result_BetweenTwoLinesAngles[i][j - i - 1]);
                }
        }

        [Test, TestCaseSource(nameof(BetweenTwoLinesAnglesSource))]
        public void BetweenTwoLinesAngle_Doubles_Values(LineSegment firstLine, LineSegment secondLine, double validValue)
        {
            double result = BetweenTwoLinesAngle(firstLine.X1, firstLine.Y1, firstLine.X2, firstLine.Y2,
                secondLine.X1, secondLine.Y1, secondLine.X2, secondLine.Y2);
            Assert.True(IsCalculatedResultValid(result, validValue, angleCalculationThreshold), ResultIsNotValidValueErrorMessage(result, validValue));
        }

        [Test, TestCaseSource(nameof(BetweenTwoLinesAnglesSource))]
        public void BetweenTwoLinesAngle_Vectors_Values(LineSegment firstLine, LineSegment secondLine, double validValue)
        {
            double result = BetweenTwoLinesAngle(new Vector2D(firstLine.X1, firstLine.Y1), new Vector2D(firstLine.X2, firstLine.Y2),
                new Vector2D(secondLine.X1, secondLine.Y1), new Vector2D(secondLine.X2, secondLine.Y2));
            Assert.True(IsCalculatedResultValid(result, validValue, angleCalculationThreshold), ResultIsNotValidValueErrorMessage(result, validValue));
        }
        #endregion

        #region [   BetweenTwoVectorsAngle  ]
        private static IEnumerable<TestCaseData> BetweenTwoVectorsAnglesSource()
        {
            CheckSameLengthArrays(Vectors, nameof(Vectors), Result_BetweenTwoReversedVectorsAngles, nameof(Result_BetweenTwoReversedVectorsAngles));
            CheckArray(ReversedVectors, nameof(ReversedVectors));

            for (int i = 0; i < Vectors.Length; i++)
                for (int j = 0; j < ReversedVectors.Length; j++)
                {
                    CheckSameLengthArrays(ReversedVectors, nameof(ReversedVectors),
                        Result_BetweenTwoReversedVectorsAngles[i], nameof(Result_BetweenTwoReversedVectorsAngles) + $"[{i}]");
                    yield return new TestCaseData(Vectors[i], ReversedVectors[j], Result_BetweenTwoReversedVectorsAngles[i][j]);
                }
        }

        [Test, TestCaseSource(nameof(BetweenTwoVectorsAnglesSource))]
        public void BetweenTwoVectorsAngle_Doubles_Values(Vector firstVector, Vector secondVector, double angle)
        {
            double result = BetweenTwoVectorsAngle(firstVector.X, firstVector.Y, secondVector.X, secondVector.Y);
            Assert.True(IsCalculatedResultValid(result, angle, angleCalculationThreshold), ErrorMessage());

            string ErrorMessage()
            {
                return $"{result} is not {angle}." +
                    Environment.NewLine + $"First vector: {firstVector.X}, {firstVector.Y};" +
                    Environment.NewLine + $"Second vector: {secondVector.X}, {secondVector.Y}.";
            }
        }

        [Test, TestCaseSource(nameof(BetweenTwoVectorsAnglesSource))]
        public void BetweenTwoVectorsAngle_Vectors_Values(Vector firstVector, Vector secondVector, double angle)
        {
            double result = BetweenTwoVectorsAngle(new Vector2D(firstVector.X, firstVector.Y), new Vector2D(secondVector.X, secondVector.Y));
            Assert.True(IsCalculatedResultValid(result, angle, angleCalculationThreshold), ErrorMessage());

            string ErrorMessage()
            {
                return $"{result} is not {angle}." +
                    Environment.NewLine + $"First vector: {firstVector.X}, {firstVector.Y};" +
                    Environment.NewLine + $"Second vector: {secondVector.X}, {secondVector.Y}.";
            }
        }
        #endregion

        private static void CheckArray(dynamic array, string name, int? length = null)
        {
            if (array is null)
                throw new ArgumentNullException($"{name} is null.");

            if (!(array is Array))
                throw new ArgumentException($"{name} is not an array.");

            if (length != null && array.Length != length)
                throw new ArgumentException($"{name} length is not {length}: {array.Length}");
        }

        private static void CheckSameLengthArrays(dynamic firstArray, string firstArrayName, dynamic secondArray, string secondArrayName)
        {
            CheckArray(firstArray, firstArrayName);
            CheckArray(secondArray, secondArrayName);

            if (firstArray.Length != secondArray.Length)
                throw new ArgumentException(
                    $"{firstArrayName} and {secondArrayName} lenghts are different: " +
                    $"{firstArray.Length}; {secondArray.Length}.");
        }

        private bool IsCalculatedResultValid(double result, double validvalue, double calculationThreshold)
            => Math.Abs(result - validvalue) < calculationThreshold;

        private string ResultIsNotValidValueErrorMessage(double result, double validValue) => $"{result} is not {validValue}.";

        private string ResultIsNotValidValueErrorMessage(bool result, bool validValue) => $"{result} is not {validValue}.";
    }
}