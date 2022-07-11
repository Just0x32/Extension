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
        private static double halfPrecisionCalculationThreshold = 1E-5;
        private static readonly string unexpectedError = "Unexpected error.";

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
            => result.Length == 3
                && result[0] == validValues.A
                && result[1] == validValues.B
                && result[2] == validValues.C;

        private string LineABCCoefficientsErrorMessage(double[] result, LineABCCoefficients validValues)
        {
            if (result.Length != 3)
                return $"Result have not 3 coefficients: {result.Length}.";
            else if (result[0] != validValues.A || result[1] != validValues.B || result[2] != validValues.C)
                return $"{result[0]}, {result[1]}, {result[2]} are not {validValues.A}, {validValues.B}, {validValues.C}.";
            else
                return "Unexpected error.";
        }
        #endregion

        #region [   ByLengthAndAngleVector  ]
        private static IEnumerable<TestCaseData> ByLengthAndAngleVectorsSource()
        {
            CheckSameLengthArrays(VectorLengthsAndAngles, nameof(VectorLengthsAndAngles), Result_ByAngleVector, nameof(Result_ByAngleVector));

            for (int i = 0; i < VectorLengthsAndAngles.Length; i++)
                yield return new TestCaseData(VectorLengthsAndAngles[i], Result_ByAngleVector[i]);
        }

        [Test, TestCaseSource(nameof(ByLengthAndAngleVectorsSource))]
        public void ByLengthAndAngleVectorCoordinates_Values_Values(LengthAndAngle lengthAndAngle, Vector? validValue)
        {
            double[]? result = ByLengthAndAngleVectorCoordinates(lengthAndAngle.Length, lengthAndAngle.Angle);
            Assert.True(IsResultValid(), ResultIsNotValidValueErrorMessage(result, validValue));

            bool IsResultValid()
            {
                return (result == null && validValue == null)
                        || (result != null && validValue != null
                            && Math.Abs(result[0] - ((Vector)validValue).X) < calculationThreshold
                            && Math.Abs(result[1] - ((Vector)validValue).Y) < calculationThreshold);
            }
        }

        [Test, TestCaseSource(nameof(ByLengthAndAngleVectorsSource))]
        public void ByLengthAndAngleVector_Values_Values(LengthAndAngle lengthAndAngle, Vector? validValue)
        {
            Vector2D? result = ByLengthAndAngleVector(lengthAndAngle.Length, lengthAndAngle.Angle);
            Assert.True(IsResultValid(), ResultIsNotValidValueErrorMessage(result, validValue));

            bool IsResultValid()
            {
                return (result == null && validValue == null)
                        || (result != null && validValue != null
                            && Math.Abs(((Vector2D)result).X - ((Vector)validValue).X) < calculationThreshold
                            && Math.Abs(((Vector2D)result).Y - ((Vector)validValue).Y) < calculationThreshold);
            }
        }
        #endregion

        #region [   VectorLength    ]
        private static IEnumerable<TestCaseData> VectorLengthsSource()
        {
            CheckSameLengthArrays(Result_ByAngleVector, nameof(Result_ByAngleVector), VectorLengthsAndAngles, nameof(VectorLengthsAndAngles));

            for (int i = 0; i < Result_ByAngleVector.Length; i++)
                if (Result_ByAngleVector[i] != null)
                    yield return new TestCaseData((Vector)Result_ByAngleVector[i], VectorLengthsAndAngles[i].Length);
        }

        [Test, TestCaseSource(nameof(VectorLengthsSource))]
        public void VectorLength_Doubles_Values(Vector vector, double validValue)
        {
            double result = VectorLength(vector.X, vector.Y);
            Assert.True(IsCalculatedResultValid(result, validValue, calculationThreshold), ResultIsNotValidValueErrorMessage(result, validValue));
        }

        [Test, TestCaseSource(nameof(VectorLengthsSource))]
        public void VectorLength_Vectors_Values(Vector vector, double validValue)
        {
            double result = VectorLength(new Vector2D(vector.X, vector.Y));
            Assert.True(IsCalculatedResultValid(result, validValue, calculationThreshold), ResultIsNotValidValueErrorMessage(result, validValue));
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
            Assert.True(IsCalculatedResultValid(result, validvalue, halfPrecisionCalculationThreshold),
                ResultIsNotValidValueErrorMessage(result, validvalue, vector));
        }

        [Test, TestCaseSource(nameof(VectorAnglesSource))]
        public void VectorAngle_Vectors_Values(Vector vector, double validvalue)
        {
            double result = VectorAngle(new Vector2D(vector.X, vector.Y));
            Assert.True(IsCalculatedResultValid(result, validvalue, halfPrecisionCalculationThreshold),
                ResultIsNotValidValueErrorMessage(result, validvalue, vector));
        }
        #endregion

        #region [   RelativeToLinePointLocation     ]
        private static IEnumerable<TestCaseData> RelativeToLinePointLocationsSource()
        {
            CheckSameLengthArrays(SinglePoints, nameof(SinglePoints), Result_RelativeToLinePointLocations,
                nameof(Result_RelativeToLinePointLocations));
            CheckArray(LineSegments, nameof(LineSegments));

            for (int i = 0; i < SinglePoints.Length; i++)
            {
                CheckSameLengthArrays(LineSegments, nameof(LineSegments),
                        Result_RelativeToLinePointLocations[i], nameof(Result_RelativeToLinePointLocations));

                for (int j = 0; j < LineSegments.Length; j++)
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
                return "Unexpected error.";
        }
        #endregion

        #region [   BetweenPointAndLineDistance     ]
        private static IEnumerable<TestCaseData> PointsAndLinesSource()
        {
            CheckSameLengthArrays(SinglePoints, nameof(SinglePoints), Result_BetweenPointAndLineDistances,
                nameof(Result_BetweenPointAndLineDistances));
            CheckArray(LineSegments, nameof(LineSegments));

            for (int i = 0; i < SinglePoints.Length; i++)
            {
                CheckSameLengthArrays(LineSegments, nameof(LineSegments),
                        Result_BetweenPointAndLineDistances[i], nameof(Result_BetweenPointAndLineDistances) + $"[{i}]");

                for (int j = 0; j < LineSegments.Length; j++)
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
            {
                CheckArray(Result_TwoLinesIntersectionPoints[i], nameof(Result_TwoLinesIntersectionPoints) + $"[{i}]", LineSegments.Length - i - 1);

                for (int j = i + 1; j < LineSegments.Length; j++)
                    yield return new TestCaseData(LineSegments[i], LineSegments[j], Result_TwoLinesIntersectionPoints[i][j - i - 1]);
            }
        }

        [Test, TestCaseSource(nameof(TwoLinesSource))]
        public void TwoLinesIntersectionPoint_Doubles_Values(LineSegment firstLine, LineSegment secondLine, Vector? validValue)
        {
            double[]? result = TwoLinesIntersectionPoint(firstLine.X1, firstLine.Y1, firstLine.X2, firstLine.Y2,
                secondLine.X1, secondLine.Y1, secondLine.X2, secondLine.Y2);
            Assert.True(IsResultValid(), ResultIsNotValidValueErrorMessage(result, validValue));

            bool IsResultValid()
                => (result == null && validValue == null)
                        || (result != null && validValue != null
                            && Math.Abs(result[0] - ((Vector)validValue).X) < calculationThreshold
                            && Math.Abs(result[1] - ((Vector)validValue).Y) < calculationThreshold);
        }

        [Test, TestCaseSource(nameof(TwoLinesSource))]
        public void TwoLinesIntersectionPoint_Vectors_Values(LineSegment firstLine, LineSegment secondLine, Vector? validValue)
        {
            Vector2D? result = TwoLinesIntersectionPoint(new Vector2D(firstLine.X1, firstLine.Y1), new Vector2D(firstLine.X2, firstLine.Y2),
                new Vector2D(secondLine.X1, secondLine.Y1), new Vector2D(secondLine.X2, secondLine.Y2));
            Assert.True(IsResultValid(), ResultIsNotValidValueErrorMessage(result, validValue));

            bool IsResultValid()
                => (result == null && validValue == null)
                        || (result != null && validValue != null
                            && Math.Abs(((Vector2D)result).X - ((Vector)validValue).X) < calculationThreshold
                            && Math.Abs(((Vector2D)result).Y - ((Vector)validValue).Y) < calculationThreshold);
        }
        #endregion

        #region [   AreTwoLinesParallel     ]
        private static IEnumerable<TestCaseData> AreTwoLinesParallelSource()
        {
            CheckArray(LineSegments, nameof(LineSegments));
            CheckArray(Result_AreTwoLinesParallel, nameof(Result_AreTwoLinesParallel), LineSegments.Length - 1);

            for (int i = 0; i < LineSegments.Length - 1; i++)
            {
                CheckArray(Result_AreTwoLinesParallel[i], nameof(Result_AreTwoLinesParallel) + $"[{i}]", LineSegments.Length - i - 1);

                for (int j = i + 1; j < LineSegments.Length; j++)
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
            {
                CheckArray(Result_AreTwoLinesMatching[i], nameof(Result_AreTwoLinesMatching) + $"[{i}]", LineSegments.Length - i - 1);

                for (int j = i + 1; j < LineSegments.Length; j++)
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
            {
                CheckArray(Result_AreTwoLinesParallel[i], nameof(Result_AreTwoLinesParallel) + $"[{i}]", Vectors.Length - i - 1);

                for (int j = i + 1; j < Vectors.Length; j++)
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
            {
                CheckArray(Result_AreTwoLinesPerpendicular[i], nameof(Result_AreTwoLinesPerpendicular) + $"[{i}]", LineSegments.Length - i - 1);

                for (int j = i + 1; j < LineSegments.Length; j++)
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
            {
                CheckArray(Result_AreTwoLinesPerpendicular[i], nameof(Result_AreTwoLinesPerpendicular) + $"[{i}]", Vectors.Length - i - 1);

                for (int j = i + 1; j < Vectors.Length; j++)
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
            {
                CheckArray(Result_BetweenTwoLinesAngles[i], nameof(Result_BetweenTwoLinesAngles) + $"[{i}]", LineSegments.Length - i - 1);

                for (int j = i + 1; j < LineSegments.Length; j++)
                    yield return new TestCaseData(LineSegments[i], LineSegments[j], Result_BetweenTwoLinesAngles[i][j - i - 1]);
            }
        }

        [Test, TestCaseSource(nameof(BetweenTwoLinesAnglesSource))]
        public void BetweenTwoLinesAngle_Doubles_Values(LineSegment firstLine, LineSegment secondLine, double validValue)
        {
            double result = BetweenTwoLinesAngle(firstLine.X1, firstLine.Y1, firstLine.X2, firstLine.Y2,
                secondLine.X1, secondLine.Y1, secondLine.X2, secondLine.Y2);
            Assert.True(IsCalculatedResultValid(result, validValue, halfPrecisionCalculationThreshold), ResultIsNotValidValueErrorMessage(result, validValue));
        }

        [Test, TestCaseSource(nameof(BetweenTwoLinesAnglesSource))]
        public void BetweenTwoLinesAngle_Vectors_Values(LineSegment firstLine, LineSegment secondLine, double validValue)
        {
            double result = BetweenTwoLinesAngle(new Vector2D(firstLine.X1, firstLine.Y1), new Vector2D(firstLine.X2, firstLine.Y2),
                new Vector2D(secondLine.X1, secondLine.Y1), new Vector2D(secondLine.X2, secondLine.Y2));
            Assert.True(IsCalculatedResultValid(result, validValue, halfPrecisionCalculationThreshold), ResultIsNotValidValueErrorMessage(result, validValue));
        }
        #endregion

        #region [   BetweenTwoVectorsAngle  ]
        private static IEnumerable<TestCaseData> BetweenTwoVectorsAnglesSource()
        {
            CheckSameLengthArrays(Vectors, nameof(Vectors), Result_BetweenTwoReversedVectorsAngles, nameof(Result_BetweenTwoReversedVectorsAngles));
            CheckArray(ReversedVectors, nameof(ReversedVectors));

            for (int i = 0; i < Vectors.Length; i++)
            {
                CheckSameLengthArrays(ReversedVectors, nameof(ReversedVectors),
                        Result_BetweenTwoReversedVectorsAngles[i], nameof(Result_BetweenTwoReversedVectorsAngles) + $"[{i}]");

                for (int j = 0; j < ReversedVectors.Length; j++)
                    yield return new TestCaseData(Vectors[i], ReversedVectors[j], Result_BetweenTwoReversedVectorsAngles[i][j]);
            }
        }

        [Test, TestCaseSource(nameof(BetweenTwoVectorsAnglesSource))]
        public void BetweenTwoVectorsAngle_Doubles_Values(Vector firstVector, Vector secondVector, double validValue)
        {
            double result = BetweenTwoVectorsAngle(firstVector.X, firstVector.Y, secondVector.X, secondVector.Y);
            Assert.True(IsCalculatedResultValid(result, validValue, halfPrecisionCalculationThreshold),
                ResultIsNotValidValueErrorMessage(result, validValue, firstVector, secondVector));
        }

        [Test, TestCaseSource(nameof(BetweenTwoVectorsAnglesSource))]
        public void BetweenTwoVectorsAngle_Vectors_Values(Vector firstVector, Vector secondVector, double validValue)
        {
            double result = BetweenTwoVectorsAngle(new Vector2D(firstVector.X, firstVector.Y), new Vector2D(secondVector.X, secondVector.Y));
            Assert.True(IsCalculatedResultValid(result, validValue, halfPrecisionCalculationThreshold),
                ResultIsNotValidValueErrorMessage(result, validValue, firstVector, secondVector));
        }
        #endregion

        #region [   Addition operation  ]
        private static IEnumerable<TestCaseData> AdditionOperationsSource()
        {
            CheckArray(Result_ByAngleVector, nameof(Result_ByAngleVector));
            CheckArray(Result_AdditionResultVectors, nameof(Result_AdditionResultVectors));

            for (int i = 0; i < Result_ByAngleVector.Length - 1; i++)
                if (Result_ByAngleVector[i] != null)
                {
                    CheckArray(Result_AdditionResultVectors[i], nameof(Result_AdditionResultVectors) + $"[{i}]");

                    for (int j = i + 1; j < Result_ByAngleVector.Length; j++)
                        if (Result_ByAngleVector[j] != null)
                            yield return new TestCaseData((Vector)Result_ByAngleVector[i], (Vector)Result_ByAngleVector[j],
                                    Result_AdditionResultVectors[i][j - i - 1]);
                }
        }

        [Test, TestCaseSource(nameof(AdditionOperationsSource))]
        public void AdditionOperator_Values_Values(Vector firstVector, Vector secondVector, Vector validValue)
        {
            Vector2D firstVector2D = new Vector2D(firstVector.X, firstVector.Y);
            Vector2D secondVector2D = new Vector2D(secondVector.X, secondVector.Y);
            Vector2D firstResult = firstVector2D + secondVector2D;
            Vector2D secondResult = secondVector2D + firstVector2D;
            Assert.True(IsValidAdditionOperation(firstResult, secondResult, validValue),
                AdditionOperationErrorMessage(firstResult, secondResult, validValue, firstVector, secondVector));
        }

        [Test, TestCaseSource(nameof(AdditionOperationsSource))]
        public void AddStatic_Values_Values(Vector firstVector, Vector secondVector, Vector validValue)
        {
            Vector2D firstVector2D = new Vector2D(firstVector.X, firstVector.Y);
            Vector2D secondVector2D = new Vector2D(secondVector.X, secondVector.Y);
            Vector2D firstResult = Vector2D.Add(firstVector2D, secondVector2D);
            Vector2D secondResult = Vector2D.Add(secondVector2D, firstVector2D);
            Assert.True(IsValidAdditionOperation(firstResult, secondResult, validValue),
                AdditionOperationErrorMessage(firstResult, secondResult, validValue, firstVector, secondVector));
        }

        private bool IsValidAdditionOperation(Vector2D firstResult, Vector2D secondResult, Vector validValue)
            => IsCalculatedResultValid(firstResult.X, secondResult.X, calculationThreshold)
                && IsCalculatedResultValid(firstResult.X, validValue.X, calculationThreshold)
                && IsCalculatedResultValid(firstResult.Y, secondResult.Y, calculationThreshold)
                && IsCalculatedResultValid(firstResult.Y, validValue.Y, calculationThreshold);

        private string AdditionOperationErrorMessage(Vector2D firstResult, Vector2D secondResult, Vector validValue,
            Vector firstVector, Vector secondVector)
        {
            if (firstResult.X != secondResult.X || firstResult.Y != secondResult.Y)
                return "The position of the arguments matters." +
                    Environment.NewLine + $"First vector: {firstVector.X}, {firstVector.Y};" +
                    Environment.NewLine + $"Second vector: {secondVector.X}, {secondVector.Y};" +
                    Environment.NewLine + $"First result vector: {firstResult.X}, {firstResult.Y};" +
                    Environment.NewLine + $"Second result vector: {secondResult.X}, {secondResult.Y}.";
            else if (firstResult.X != validValue.X || firstResult.Y != validValue.Y)
                return $"{firstResult.X}, {firstResult.Y} is not {validValue.X}, {validValue.Y}." +
                    Environment.NewLine + $"First vector: {firstVector.X}, {firstVector.Y};" +
                    Environment.NewLine + $"Second vector: {secondVector.X}, {secondVector.Y}.";
            else
                return "Unexpected error.";
        }
        #endregion

        #region [   Subtraction operation   ]
        private static IEnumerable<TestCaseData> SubtractionOperationsSource()
        {
            CheckArray(Result_ByAngleVector, nameof(Result_ByAngleVector));
            CheckArray(Result_SubtractionResultVectors, nameof(Result_SubtractionResultVectors));

            for (int i = 0; i < Result_ByAngleVector.Length - 1; i++)
                if (Result_ByAngleVector[i] != null)
                {
                    CheckArray(Result_SubtractionResultVectors[i], nameof(Result_SubtractionResultVectors) + $"[{i}]");

                    for (int j = i + 1; j < Result_ByAngleVector.Length; j++)
                        if (Result_ByAngleVector[j] != null)
                            yield return new TestCaseData((Vector)Result_ByAngleVector[i], (Vector)Result_ByAngleVector[j],
                                    Result_SubtractionResultVectors[i][j - i - 1]);
                }
        }

        [Test, TestCaseSource(nameof(SubtractionOperationsSource))]
        public void SubtractionOperator_Values_Values(Vector firstVector, Vector secondVector, Vector validValue)
        {
            Vector2D firstVector2D = new Vector2D(firstVector.X, firstVector.Y);
            Vector2D secondVector2D = new Vector2D(secondVector.X, secondVector.Y);
            Vector2D result = firstVector2D - secondVector2D;
            Assert.True(IsResultValid(result, validValue, calculationThreshold),
                ResultIsNotValidValueErrorMessage(result, validValue, firstVector, secondVector));
        }

        [Test, TestCaseSource(nameof(SubtractionOperationsSource))]
        public void SubstructStatic_Values_Values(Vector firstVector, Vector secondVector, Vector validValue)
        {
            Vector2D firstVector2D = new Vector2D(firstVector.X, firstVector.Y);
            Vector2D secondVector2D = new Vector2D(secondVector.X, secondVector.Y);
            Vector2D result = Vector2D.Substruct(firstVector2D, secondVector2D);
            Assert.True(IsResultValid(result, validValue, calculationThreshold),
                ResultIsNotValidValueErrorMessage(result, validValue, firstVector, secondVector));
        }
        #endregion

        #region [   Multiplication operation    ]
        private static IEnumerable<TestCaseData> MultiplicationOperationsSource()
        {
            CheckArray(Result_ByAngleVector, nameof(Result_ByAngleVector));
            CheckArray(DoubleNumbers, nameof(DoubleNumbers));
            CheckArray(IntegerNumbers, nameof(IntegerNumbers));
            CheckArray(Result_MultiplicationWithDoubleResultVectors, nameof(Result_MultiplicationWithDoubleResultVectors));
            CheckArray(Result_MultiplicationWithIntegerResultVectors, nameof(Result_MultiplicationWithIntegerResultVectors));

            for (int i = 0; i < Result_ByAngleVector.Length; i++)
                if (Result_ByAngleVector[i] != null)
                {
                    CheckArray(Result_MultiplicationWithDoubleResultVectors[i], nameof(Result_MultiplicationWithDoubleResultVectors) + $"[{i}]");
                    CheckArray(Result_MultiplicationWithIntegerResultVectors[i], nameof(Result_MultiplicationWithIntegerResultVectors) + $"[{i}]");

                    for (int j = 0; j < DoubleNumbers.Length; j++)
                        yield return new TestCaseData((Vector)Result_ByAngleVector[i], DoubleNumbers[j],
                                Result_MultiplicationWithDoubleResultVectors[i][j]);

                    for (int j = 0; j < IntegerNumbers.Length; j++)
                        yield return new TestCaseData((Vector)Result_ByAngleVector[i], IntegerNumbers[j],
                                Result_MultiplicationWithIntegerResultVectors[i][j]);
                }
        }

        [Test, TestCaseSource(nameof(MultiplicationOperationsSource))]
        public void MultiplicationOperator_Values_Values(Vector vector, object number, Vector validValue)
        {
            Vector2D vector2D = new Vector2D(vector.X, vector.Y);
            dynamic typedNumber = Convert.ChangeType(number, number.GetType());
            Vector2D firstResult = vector2D * typedNumber;
            Vector2D secondResult = typedNumber * vector2D;
            Assert.True(IsValidMultiplicationOperation(firstResult, secondResult, validValue),
                MultiplicationOperationErrorMessage(firstResult, secondResult, validValue, vector, typedNumber));
        }

        [Test, TestCaseSource(nameof(MultiplicationOperationsSource))]
        public void MultiplyStatic_Values_Values(Vector vector, object number, Vector validValue)
        {
            Vector2D vector2D = new Vector2D(vector.X, vector.Y);
            dynamic typedNumber = Convert.ChangeType(number, number.GetType());
            Vector2D firstResult = Vector2D.Multiply(vector2D, typedNumber);
            Vector2D secondResult = Vector2D.Multiply(typedNumber, vector2D);
            Assert.True(IsValidMultiplicationOperation(firstResult, secondResult, validValue),
                MultiplicationOperationErrorMessage(firstResult, secondResult, validValue, vector, typedNumber));
        }

        private bool IsValidMultiplicationOperation(Vector2D firstResult, Vector2D secondResult, Vector validValue)
            => IsCalculatedResultValid(firstResult.X, secondResult.X, halfPrecisionCalculationThreshold)
                && IsCalculatedResultValid(firstResult.X, validValue.X, halfPrecisionCalculationThreshold)
                && IsCalculatedResultValid(firstResult.Y, secondResult.Y, halfPrecisionCalculationThreshold)
                && IsCalculatedResultValid(firstResult.Y, validValue.Y, halfPrecisionCalculationThreshold);

        private string MultiplicationOperationErrorMessage(Vector2D firstResult, Vector2D secondResult, Vector validValue,
            Vector vector, double number)
        {
            if (firstResult.X != secondResult.X || firstResult.Y != secondResult.Y)
                return "The position of the arguments matters." +
                    Environment.NewLine + $"Vector: {vector.X}, {vector.Y};" +
                    Environment.NewLine + $"Number: {number};" +
                    Environment.NewLine + $"First result vector: {firstResult.X}, {firstResult.Y};" +
                    Environment.NewLine + $"Second result vector: {secondResult.X}, {secondResult.Y}.";
            else if (firstResult.X != validValue.X || firstResult.Y != validValue.Y)
                return $"{firstResult.X}, {firstResult.Y} is not {validValue.X}, {validValue.Y}." +
                    Environment.NewLine + $"Vector: {vector.X}, {vector.Y};" +
                    Environment.NewLine + $"Number: {number}.";
            else
                return "Unexpected error.";
        }
        #endregion

        #region [   Division operation      ]
        private static IEnumerable<TestCaseData> DivisionOperationsSource()
        {
            CheckArray(Result_ByAngleVector, nameof(Result_ByAngleVector));
            CheckArray(DoubleNumbers, nameof(DoubleNumbers));
            CheckArray(IntegerNumbers, nameof(IntegerNumbers));
            CheckArray(Result_DivisionWithDoubleResultVectors, nameof(Result_DivisionWithDoubleResultVectors));
            CheckArray(Result_DivisionWithIntegerResultVectors, nameof(Result_DivisionWithIntegerResultVectors));

            for (int i = 0; i < Result_ByAngleVector.Length; i++)
                if (Result_ByAngleVector[i] != null)
                {
                    CheckArray(Result_DivisionWithDoubleResultVectors[i], nameof(Result_DivisionWithDoubleResultVectors) + $"[{i}]");
                    CheckArray(Result_DivisionWithIntegerResultVectors[i], nameof(Result_DivisionWithIntegerResultVectors) + $"[{i}]");

                    for (int j = 0; j < DoubleNumbers.Length; j++)
                        yield return new TestCaseData((Vector)Result_ByAngleVector[i], DoubleNumbers[j],
                                Result_DivisionWithDoubleResultVectors[i][j]);
                    

                    for (int j = 0; j < IntegerNumbers.Length; j++)
                        yield return new TestCaseData((Vector)Result_ByAngleVector[i], IntegerNumbers[j],
                                Result_DivisionWithIntegerResultVectors[i][j]);
                }
        }

        [Test, TestCaseSource(nameof(DivisionOperationsSource))]
        public void DivisionOperator_Values_Values(Vector vector, object number, Vector? validValue)
        {
            Vector2D vector2D = new Vector2D(vector.X, vector.Y);
            dynamic typedNumber = Convert.ChangeType(number, number.GetType());
            Vector2D? result = vector2D / typedNumber;
            Assert.True(IsResultValid(result, validValue, halfPrecisionCalculationThreshold),
                ResultIsNotValidValueErrorMessage(result, validValue, vector, typedNumber));
        }

        [Test, TestCaseSource(nameof(DivisionOperationsSource))]
        public void DevideStatic_Values_Values(Vector vector, object number, Vector? validValue)
        {
            Vector2D vector2D = new Vector2D(vector.X, vector.Y);
            dynamic typedNumber = Convert.ChangeType(number, number.GetType());
            Vector2D? result = Vector2D.Devide(vector2D, typedNumber);
            Assert.True(IsResultValid(result, validValue, halfPrecisionCalculationThreshold),
                ResultIsNotValidValueErrorMessage(result, validValue, vector, typedNumber));
        }
        #endregion

        private static IEnumerable<TestCaseData> EqualVectorsSource()
        {
            CheckArray(Result_ByAngleVector, nameof(Result_ByAngleVector));

            for (int i = 0; i < Result_ByAngleVector.Length; i++)
                if (Result_ByAngleVector[i] != null)
                {
                    double x = ((Vector)Result_ByAngleVector[i]).X;
                    double y = ((Vector)Result_ByAngleVector[i]).Y;
                    Vector2D firstVector = new Vector2D(x, y);
                    Vector2D secondVector = new Vector2D(x, y);
                    yield return new TestCaseData(firstVector, secondVector);
                }
        }

        private static IEnumerable<TestCaseData> NotEqualVectorsSource()
        {
            CheckArray(Result_ByAngleVector, nameof(Result_ByAngleVector));

            for (int i = 0; i < Result_ByAngleVector.Length; i++)
                if (Result_ByAngleVector[i] != null)
                {
                    double x = ((Vector)Result_ByAngleVector[i]).X;
                    double y = ((Vector)Result_ByAngleVector[i]).Y;
                    Vector2D firstVector = new Vector2D(x, y);
                    Vector2D secondVector = new Vector2D(x, y + 1);
                    yield return new TestCaseData(firstVector, secondVector);
                }
        }

        #region [   Equivalence operation   ]
        [Test, TestCaseSource(nameof(EqualVectorsSource))]
        public void EquivalenceOperator_Values_True(Vector2D firstVector, Vector2D secondVector)
        {
            bool validValue = true;
            bool firstResult = firstVector == secondVector;
            bool secondResult = secondVector == firstVector;
            Assert.True(firstResult == secondResult && firstResult == validValue,
                ResultIsNotValidValueErrorMessage(firstResult, validValue, firstVector, secondVector));
        }

        [Test, TestCaseSource(nameof(NotEqualVectorsSource))]
        public void EquivalenceOperator_Values_False(Vector2D firstVector, Vector2D secondVector)
        {
            bool validValue = false;
            bool firstResult = firstVector == secondVector;
            bool secondResult = secondVector == firstVector;
            Assert.True(firstResult == secondResult && firstResult == validValue,
                ResultIsNotValidValueErrorMessage(firstResult, validValue, firstVector, secondVector));
        }

        [Test, TestCaseSource(nameof(NotEqualVectorsSource))]
        public void NotEquivalenceOperator_Values_True(Vector2D firstVector, Vector2D secondVector)
        {
            bool validValue = true;
            bool firstResult = firstVector != secondVector;
            bool secondResult = secondVector != firstVector;
            Assert.True(firstResult == secondResult && firstResult == validValue,
                ResultIsNotValidValueErrorMessage(firstResult, validValue, firstVector, secondVector));
        }

        [Test, TestCaseSource(nameof(EqualVectorsSource))]
        public void NotEquivalenceOperator_Values_False(Vector2D firstVector, Vector2D secondVector)
        {
            bool validValue = false;
            bool firstResult = firstVector != secondVector;
            bool secondResult = secondVector != firstVector;
            Assert.True(firstResult == secondResult && firstResult == validValue,
                ResultIsNotValidValueErrorMessage(firstResult, validValue, firstVector, secondVector));
        }

        [Test, TestCaseSource(nameof(EqualVectorsSource))]
        public void EqualsStatic_Values_True(Vector2D firstVector, Vector2D secondVector)
        {
            bool validValue = true;
            bool firstResult = Vector2D.Equals(firstVector, secondVector);
            bool secondResult = Vector2D.Equals(secondVector, firstVector);
            Assert.True(firstResult == secondResult && firstResult == validValue,
                ResultIsNotValidValueErrorMessage(firstResult, validValue, firstVector, secondVector));
        }

        [Test, TestCaseSource(nameof(NotEqualVectorsSource))]
        public void EqualsStatic_Values_False(Vector2D firstVector, Vector2D secondVector)
        {
            bool validValue = false;
            bool firstResult = Vector2D.Equals(firstVector, secondVector);
            bool secondResult = Vector2D.Equals(secondVector, firstVector);
            Assert.True(firstResult == secondResult && firstResult == validValue,
                ResultIsNotValidValueErrorMessage(firstResult, validValue, firstVector, secondVector));
        }
        #endregion

        #region [   Equals      ]
        [Test, TestCaseSource(nameof(EqualVectorsSource))]
        public void Equals_Values_True(Vector2D firstVector, Vector2D secondVector)
        {
            bool validValue = true;
            bool firstResult = firstVector.Equals(secondVector);
            bool secondResult = secondVector.Equals(firstVector);
            Assert.True(firstResult == secondResult && firstResult == validValue,
                ResultIsNotValidValueErrorMessage(firstResult, validValue, firstVector, secondVector));
        }

        [Test, TestCaseSource(nameof(NotEqualVectorsSource))]
        public void Equals_Values_False(Vector2D firstVector, Vector2D secondVector)
        {
            bool validValue = false;
            bool firstResult = firstVector.Equals(secondVector);
            bool secondResult = secondVector.Equals(firstVector);
            Assert.True(firstResult == secondResult && firstResult == validValue,
                ResultIsNotValidValueErrorMessage(firstResult, validValue, firstVector, secondVector));
        }
        #endregion

        #region [   GetHashCode     ]
        [Test, TestCaseSource(nameof(EqualVectorsSource))]
        public void GetHashCode_Values_True(Vector2D firstVector, Vector2D secondVector)
        {
            bool validValue = true;
            bool result = firstVector.GetHashCode() == secondVector.GetHashCode();
            Assert.True(result == validValue, ResultIsNotValidValueErrorMessage(result, validValue, firstVector, secondVector));
        }

        [Test, TestCaseSource(nameof(NotEqualVectorsSource))]
        public void GetHashCode_Values_False(Vector2D firstVector, Vector2D secondVector)
        {
            bool validValue = false;
            bool result = firstVector.GetHashCode() == secondVector.GetHashCode();
            Assert.True(result == validValue, ResultIsNotValidValueErrorMessage(result, validValue, firstVector, secondVector));
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

        private bool IsResultValid(Vector2D result, Vector validValue, double calculationThreshold)
            => IsCalculatedResultValid(result.X, validValue.X, calculationThreshold)
                && IsCalculatedResultValid(result.Y, validValue.Y, calculationThreshold);

        private bool IsResultValid(Vector2D? result, Vector? validValue, double calculationThreshold)
            => (result == null && validValue == null)
                || (result != null && validValue != null
                    && IsCalculatedResultValid(((Vector2D)result).X, ((Vector)validValue).X, calculationThreshold)
                    && IsCalculatedResultValid(((Vector2D)result).Y, ((Vector)validValue).Y, calculationThreshold));

        private string ResultIsNotValidValueErrorMessage(double result, double validValue) => $"{result} is not {validValue}.";

        private string ResultIsNotValidValueErrorMessage(bool result, bool validValue) => $"{result} is not {validValue}.";

        private string ResultIsNotValidValueErrorMessage(double result, double validvalue, Vector vector)
            => $"{result} is not {validvalue}." +
            Environment.NewLine + $"Vector: {vector.X}; {vector.Y}.";

        private string ResultIsNotValidValueErrorMessage(double[]? result, Vector? validValue)
        {
            if (result == null && validValue != null)
                return $"Null is not ({((Vector)validValue).X}; {((Vector)validValue).Y}).";
            else if (result != null && validValue == null)
                return $"({result[0]}; {result[1]}) is not null.";
            else if (result != null && validValue != null)
                return $"({result[0]}; {result[1]}) is not ({((Vector)validValue).X}; {((Vector)validValue).Y}).";
            else
                return unexpectedError;
        }

        private string ResultIsNotValidValueErrorMessage(Vector2D? result, Vector? validValue)
        {
            if (result == null && validValue != null)
                return $"Null is not ({((Vector)validValue).X}; {((Vector)validValue).Y}).";
            else if (result != null && validValue == null)
                return $"({((Vector2D)result).X}; {((Vector2D)result).Y}) is not null.";
            else if (result != null && validValue != null)
                return $"({((Vector2D)result).X}; {((Vector2D)result).Y}) is not ({((Vector)validValue).X}; {((Vector)validValue).Y}).";
            else
                return unexpectedError;
        }

        private string ResultIsNotValidValueErrorMessage(bool result, bool validValue, Vector2D firstVector, Vector2D secondVector)
            => $"{result} is not {validValue}." +
                    Environment.NewLine + $"First vector: {firstVector.X}; {firstVector.Y}." +
                    Environment.NewLine + $"Second vector: {secondVector.X}; {secondVector.Y}.";

        private string ResultIsNotValidValueErrorMessage(double result, double validValue, Vector firstVector, Vector secondVector)
            => $"{result} is not {validValue}." +
                    Environment.NewLine + $"First vector: {firstVector.X}; {firstVector.Y}." +
                    Environment.NewLine + $"Second vector: {secondVector.X}; {secondVector.Y}.";

        private string ResultIsNotValidValueErrorMessage(Vector2D result, Vector validValue, Vector firstVector, Vector secondVector)
            => $"({result.X}; {result.Y}) is not ({validValue.X}; {validValue.Y})." +
                    Environment.NewLine + $"First vector: {firstVector.X}; {firstVector.Y}." +
                    Environment.NewLine + $"Second vector: {secondVector.X}; {secondVector.Y}.";

        private string ResultIsNotValidValueErrorMessage(Vector2D result, Vector validValue, Vector vector, double number)
            => $"({result.X}; {result.Y}) is not ({validValue.X}; {validValue.Y})." +
                    Environment.NewLine + $"Vector: {vector.X}; {vector.Y}." +
                    Environment.NewLine + $"Number: {number}.";

        private string ResultIsNotValidValueErrorMessage(Vector2D? result, Vector? validValue, Vector vector, double number)
        {
            if (result == null && validValue != null)
                return $"Null is not ({((Vector)validValue).X}; {((Vector)validValue).Y})." +
                    Environment.NewLine + $"Vector: {vector.X}; {vector.Y}." +
                    Environment.NewLine + $"Number: {number}.";
            else if (result != null && validValue == null)
                return $"({((Vector2D)result).X}, {((Vector2D)result).Y}) is not null." +
                    Environment.NewLine + $"Vector: {vector.X}; {vector.Y}." +
                    Environment.NewLine + $"Number: {number}.";
            else if (result != null && validValue != null)
                return $"({((Vector2D)result).X}, {((Vector2D)result).Y}) is not ({((Vector)validValue).X}, {((Vector)validValue).Y})." +
                    Environment.NewLine + $"Vector: {vector.X}; {vector.Y}." +
                    Environment.NewLine + $"Number: {number}.";
            else
                return unexpectedError;
        }
    }
}