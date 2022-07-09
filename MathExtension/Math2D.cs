using System;

namespace MathExtension
{
    public static class Math2D
    {
        public static double DefaultCalculationThreshold { get; } = 1E-23;
        public static double MinCalculationThreshold { get; } = 0;
        public static double MaxCalculationThreshold { get; } = 1E-12;

        private static double calculationThreshold = DefaultCalculationThreshold;
        public static double CalculationThreshold
        {
            get => calculationThreshold;
            set
            {
                if (value > MaxCalculationThreshold)
                    calculationThreshold = MaxCalculationThreshold;
                else if (value < MinCalculationThreshold)
                    calculationThreshold = MinCalculationThreshold;
                else
                    calculationThreshold = value;
            }
        }

        /// <summary>
        /// Returns coefficients A, B and C of a line that defined by points.
        /// </summary>
        /// <param name="x1">Coordinate X of the first point.</param>
        /// <param name="y1">Coordinate Y of the first point.</param>
        /// <param name="x2">Coordinate X of the second point.</param>
        /// <param name="y2">Coordinate Y of the second point.</param>
        /// <returns>An array { A, B, C }.</returns>
        public static double[] LineABCCoefficients(double x1, double y1, double x2, double y2)
        {
            double a = y2 - y1;
            double b = x1 - x2;
            double c = x2 * y1 - x1 * y2;
            
            return new double[] { a, b, c };
        }

        /// <summary>
        /// Returns coefficients A, B and C of a line that defined by points.
        /// </summary>
        /// <param name="firstPoint">First point.</param>
        /// <param name="secondPoint">Second point.</param>
        /// <returns>An array { A, B, C }.</returns>
        public static double[] LineABCCoefficients(Vector2D firstPoint, Vector2D secondPoint)
            => LineABCCoefficients(firstPoint.X, firstPoint.Y, secondPoint.X, secondPoint.Y);

        private static double[] VectorABCoefficients(double x, double y)
        {
            double a = y;
            double b = -x;

            return new double[] { a, b };
        }

        private static double[] VectorABCoefficients(Vector2D vector)
            => VectorABCoefficients(vector.X, vector.Y);

        /// <summary>
        /// Returns an angle of a vector in degrees relative to X axis.
        /// </summary>
        /// <param name="x">Coordinate X of the vector.</param>
        /// <param name="y">Coordinate Y of the vector.</param>
        /// <returns>A number from 0 to 360.</returns>
        public static double VectorAngle(double x, double y)
        {
            double angle = Math.Atan(y / x) * 180 / Math.PI;

            if (x == 0)
            {
                if (y > 0)
                    return 90;
                else if (y < 0)
                    return 270;
                else
                    return 0;
            }
            else if (x > 0 && y >= 0)
                return angle;
            else if (x < 0)
                return angle + 180;
            else
                return angle + 360;
        }

        /// <summary>
        /// Returns an angle of a vector in degrees relative to X axis.
        /// </summary>
        /// <param name="vector">Vector.</param>
        /// <returns>A number from 0 to 360.</returns>
        public static double VectorAngle(Vector2D vector)
            => VectorAngle(vector.X, vector.Y);

        /// <summary>
        /// Returns a location of a point relative to a line.
        /// </summary>
        /// <param name="pointX">Coordinate X of the single point.</param>
        /// <param name="pointY">Coordinate Y of the single point.</param>
        /// <param name="lineX1">Coordinate X of the line first point.</param>
        /// <param name="lineY1">Coordinate Y of the line first point.</param>
        /// <param name="lineX2">Coordinate X of the line second point.</param>
        /// <param name="lineY2">Coordinate Y of the line second point.</param>
        /// <returns>0 — <i>if the point is on the line,</i><br/>
        /// 1 — <i>if the point is on an upper half-plane from the line,</i><br/>
        /// -1 — <i>if the point is on an lower half-plane from the line.</i><br/></returns>
        public static int RelativeToLinePointLocation(double pointX, double pointY, double lineX1, double lineY1, double lineX2, double lineY2)
        {
            ConvertOneLineABCCoefficients(LineABCCoefficients(lineX1, lineY1, lineX2, lineY2), out double a, out double b, out double c);
            return -Math.Sign(a * pointX + b * pointY + c);
        }

        /// <summary>
        /// Returns a location of a point relative to a line.
        /// </summary>
        /// <param name="singlePoint">Single point.</param>
        /// <param name="lineFirstPoint">First point of the line.</param>
        /// <param name="lineSecondPoint">Second point of the line.</param>
        /// <returns>0 — <i>if the point is on the line,</i><br/>
        /// 1 — <i>if the point is on an upper half-plane from the line,</i><br/>
        /// -1 — <i>if the point is on an lower half-plane from the line.</i><br/></returns>
        public static int RelativeToLinePointLocation(Vector2D singlePoint, Vector2D lineFirstPoint, Vector2D lineSecondPoint)
            => RelativeToLinePointLocation(singlePoint.X, singlePoint.Y, lineFirstPoint.X, lineFirstPoint.Y, lineSecondPoint.X, lineSecondPoint.Y);

        /// <summary>
        /// Returns a distance between a point and a line.
        /// </summary>
        /// <param name="pointX">Coordinate X of the single point.</param>
        /// <param name="pointY">Coordinate Y of the single point.</param>
        /// <param name="lineX1">Coordinate X of the line first point.</param>
        /// <param name="lineY1">Coordinate Y of the line first point.</param>
        /// <param name="lineX2">Coordinate X of the line second point.</param>
        /// <param name="lineY2">Coordinate Y of the line second point.</param>
        /// <returns>A positive number.</returns>
        public static double BetweenPointAndLineDistance(double pointX, double pointY, double lineX1, double lineY1, double lineX2, double lineY2)
        {
            ConvertOneLineABCCoefficients(LineABCCoefficients(lineX1, lineY1, lineX2, lineY2), out double a, out double b, out double c);
            return Math.Abs(a * pointX + b * pointY + c) / Math.Sqrt(a * a + b * b);
        }

        /// <summary>
        /// Returns a distance between a point and a line.
        /// </summary>
        /// <param name="singlePoint">Single point.</param>
        /// <param name="lineFirstPoint">First point of the line.</param>
        /// <param name="lineSecondPoint">Second point of the line.</param>
        /// <returns>A positive number.</returns>
        public static double BetweenPointAndLineDistance(Vector2D singlePoint, Vector2D lineFirstPoint, Vector2D lineSecondPoint)
            => BetweenPointAndLineDistance(singlePoint.X, singlePoint.Y, lineFirstPoint.X, lineFirstPoint.Y, lineSecondPoint.X, lineSecondPoint.Y);

        /// <summary>
        /// Returns a point of two lines intersection.
        /// </summary>
        /// <param name="firstLineX1">Coordinate X of the first line first point.</param>
        /// <param name="firstLineY1">Coordinate Y of the first line first point.</param>
        /// <param name="firstLineX2">Coordinate X of the first line second point.</param>
        /// <param name="firstLineY2">Coordinate Y of the first line second point.</param>
        /// <param name="secondLineX1">Coordinate X of the second line first point.</param>
        /// <param name="secondLineY1">Coordinate Y of the second line first point.</param>
        /// <param name="secondLineX2">Coordinate X of the second line second point.</param>
        /// <param name="secondLineY2">Coordinate Y of the second line second point.</param>
        /// <returns>An array { X, Y } of a numbers <i>or</i><br/>Null — <i>if parallel or matching lines.</i></returns>
        public static double[]? TwoLinesIntersectionPoint(double firstLineX1, double firstLineY1, double firstLineX2, double firstLineY2,
            double secondLineX1, double secondLineY1, double secondLineX2, double secondLineY2)
        {
            ConvertTwoLinesABCCoefficients(
                LineABCCoefficients(firstLineX1, firstLineY1, firstLineX2, firstLineY2), out double a1, out double b1, out double c1,
                LineABCCoefficients(secondLineX1, secondLineY1, secondLineX2, secondLineY2), out double a2, out double b2, out double c2);

            double denominator = a1 * b2 - a2 * b1;

            if (denominator == 0)
                return null;
            else
            {
                double x = (b1 * c2 - b2 * c1) / denominator;
                double y = (a2 * c1 - a1 * c2) / denominator;
                return new double[] { x, y };
            }
        }

        /// <summary>
        /// Returns a point of two lines intersection.
        /// </summary>
        /// <param name="firstLineFirstPoint">First point of the first line.</param>
        /// <param name="firstLineSecondPoint">Second point of the first line.</param>
        /// <param name="secondLineFirstPoint">First point of the second line.</param>
        /// <param name="secondLineSecondPoint">Second point of the second line.</param>
        /// <returns>A vector <i>or</i><br/>Null — <i>if parallel or matching lines.</i></returns>
        public static Vector2D? TwoLinesIntersectionPoint(Vector2D firstLineFirstPoint, Vector2D firstLineSecondPoint,
            Vector2D secondLineFirstPoint, Vector2D secondLineSecondPoint)
        {
            double[]? result = TwoLinesIntersectionPoint(firstLineFirstPoint.X, firstLineFirstPoint.Y, firstLineSecondPoint.X, firstLineSecondPoint.Y,
                secondLineFirstPoint.X, secondLineFirstPoint.Y, secondLineSecondPoint.X, secondLineSecondPoint.Y);

            if (result is null)
                return null;
            else
                return new Vector2D(result[0], result[1]);
        }

        /// <summary>
        /// Returns result of two lines parallelism.
        /// </summary>
        /// <param name="firstLineX1">Coordinate X of the first line first point.</param>
        /// <param name="firstLineY1">Coordinate Y of the first line first point.</param>
        /// <param name="firstLineX2">Coordinate X of the first line second point.</param>
        /// <param name="firstLineY2">Coordinate Y of the first line second point.</param>
        /// <param name="secondLineX1">Coordinate X of the second line first point.</param>
        /// <param name="secondLineY1">Coordinate Y of the second line first point.</param>
        /// <param name="secondLineX2">Coordinate X of the second line second point.</param>
        /// <param name="secondLineY2">Coordinate Y of the second line second point.</param>
        /// <returns>True — <i>if the lines are parallel,</i><br/>False — <i>if the lines are not parallel.</i></returns>
        public static bool AreTwoLinesParallel(double firstLineX1, double firstLineY1, double firstLineX2, double firstLineY2,
            double secondLineX1, double secondLineY1, double secondLineX2, double secondLineY2)
        {
            ConvertTwoLinesABCCoefficients(
                LineABCCoefficients(firstLineX1, firstLineY1, firstLineX2, firstLineY2), out double a1, out double b1, out double c1,
                LineABCCoefficients(secondLineX1, secondLineY1, secondLineX2, secondLineY2), out double a2, out double b2, out double c2);

            if (Math.Abs(a1 * b2 - a2 * b1) < CalculationThreshold)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Returns result of two lines parallelism.
        /// </summary>
        /// <param name="firstLineFirstPoint">First point of the first line.</param>
        /// <param name="firstLineSecondPoint">Second point of the first line.</param>
        /// <param name="secondLineFirstPoint">First point of the second line.</param>
        /// <param name="secondLineSecondPoint">Second point of the second line.</param>
        /// <returns>True — <i>if the lines are parallel,</i><br/>False — <i>if the lines are not parallel.</i></returns>
        public static bool AreTwoLinesParallel(Vector2D firstLineFirstPoint, Vector2D firstLineSecondPoint,
            Vector2D secondLineFirstPoint, Vector2D secondLineSecondPoint)
            => AreTwoLinesParallel(firstLineFirstPoint.X, firstLineFirstPoint.Y, firstLineSecondPoint.X, firstLineSecondPoint.Y,
                secondLineFirstPoint.X, secondLineFirstPoint.Y, secondLineSecondPoint.X, secondLineSecondPoint.Y);

        /// <summary>
        /// Returns result of two lines matching.
        /// </summary>
        /// <param name="firstLineX1">Coordinate X of the first line first point.</param>
        /// <param name="firstLineY1">Coordinate Y of the first line first point.</param>
        /// <param name="firstLineX2">Coordinate X of the first line second point.</param>
        /// <param name="firstLineY2">Coordinate Y of the first line second point.</param>
        /// <param name="secondLineX1">Coordinate X of the second line first point.</param>
        /// <param name="secondLineY1">Coordinate Y of the second line first point.</param>
        /// <param name="secondLineX2">Coordinate X of the second line second point.</param>
        /// <param name="secondLineY2">Coordinate Y of the second line second point.</param>
        /// <returns>True — <i>if the lines are matching,</i><br/>False — <i>if the lines are not matching.</i></returns>
        public static bool AreTwoLinesMatching(double firstLineX1, double firstLineY1, double firstLineX2, double firstLineY2,
            double secondLineX1, double secondLineY1, double secondLineX2, double secondLineY2)
        {
            ConvertTwoLinesABCCoefficients(
                LineABCCoefficients(firstLineX1, firstLineY1, firstLineX2, firstLineY2), out double a1, out double b1, out double c1,
                LineABCCoefficients(secondLineX1, secondLineY1, secondLineX2, secondLineY2), out double a2, out double b2, out double c2);

            if (Math.Abs(a1 * b2 - a2 * b1) < CalculationThreshold
                && Math.Abs(a1 * c2 - a2 * c1) < CalculationThreshold
                && Math.Abs(b1 * c2 - b2 * c1) < CalculationThreshold)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Returns result of two lines matching.
        /// </summary>
        /// <param name="firstLineFirstPoint">First point of the first line.</param>
        /// <param name="firstLineSecondPoint">Second point of the first line.</param>
        /// <param name="secondLineFirstPoint">First point of the second line.</param>
        /// <param name="secondLineSecondPoint">Second point of the second line.</param>
        /// <returns>True — <i>if the lines are matching,</i><br/>False — <i>if the lines are not matching.</i></returns>
        public static bool AreTwoLinesMatching(Vector2D firstLineFirstPoint, Vector2D firstLineSecondPoint,
            Vector2D secondLineFirstPoint, Vector2D secondLineSecondPoint)
            => AreTwoLinesMatching(firstLineFirstPoint.X, firstLineFirstPoint.Y, firstLineSecondPoint.X, firstLineSecondPoint.Y,
                secondLineFirstPoint.X, secondLineFirstPoint.Y, secondLineSecondPoint.X, secondLineSecondPoint.Y);

        /// <summary>
        /// Returns result of two vectors collinearity.
        /// </summary>
        /// <param name="firstVectorX">Coordinate X of the first vector.</param>
        /// <param name="firstVectorY">Coordinate Y of the first vector.</param>
        /// <param name="secondVectorX">Coordinate X of the second vector.</param>
        /// <param name="secondVectorY">Coordinate Y of the second vector.</param>
        /// <returns>True — <i>if the vectors are collinear,</i><br/>False — <i>if the vectors are not collinear.</i></returns>
        public static bool AreTwoVectorsCollinear(double firstVectorX, double firstVectorY, double secondVectorX, double secondVectorY)
        {
            ConvertTwoVectorsABCoefficients(
                VectorABCoefficients(firstVectorX, firstVectorY), out double a1, out double b1,
                VectorABCoefficients(secondVectorX, secondVectorY), out double a2, out double b2);

            if (Math.Abs(a1 * b2 - a2 * b1) < CalculationThreshold)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Returns result of two vectors collinearity.
        /// </summary>
        /// <param name="firstVector">First vector.</param>
        /// <param name="secondVector">Second vector.</param>
        /// <returns>True — <i>if the vectors are collinear,</i><br/>False — <i>if the vectors are not collinear.</i></returns>
        public static bool AreTwoVectorsCollinear(Vector2D firstVector, Vector2D secondVector)
            => AreTwoVectorsCollinear(firstVector.X, firstVector.Y, secondVector.X, secondVector.Y);

        /// <summary>
        /// Returns result of two lines perpendicularity.
        /// </summary>
        /// <param name="firstLineX1">Coordinate X of the first line first point.</param>
        /// <param name="firstLineY1">Coordinate Y of the first line first point.</param>
        /// <param name="firstLineX2">Coordinate X of the first line second point.</param>
        /// <param name="firstLineY2">Coordinate Y of the first line second point.</param>
        /// <param name="secondLineX1">Coordinate X of the second line first point.</param>
        /// <param name="secondLineY1">Coordinate Y of the second line first point.</param>
        /// <param name="secondLineX2">Coordinate X of the second line second point.</param>
        /// <param name="secondLineY2">Coordinate Y of the second line second point.</param>
        /// <returns>True — <i>if the lines are perpendicular,</i><br/>False — <i>if the lines are not perpendicular.</i></returns>
        public static bool AreTwoLinesPerpendicular(double firstLineX1, double firstLineY1, double firstLineX2, double firstLineY2,
            double secondLineX1, double secondLineY1, double secondLineX2, double secondLineY2)
        {
            ConvertTwoLinesABCCoefficients(
                LineABCCoefficients(firstLineX1, firstLineY1, firstLineX2, firstLineY2), out double a1, out double b1, out double c1,
                LineABCCoefficients(secondLineX1, secondLineY1, secondLineX2, secondLineY2), out double a2, out double b2, out double c2);

            if (Math.Abs(a1 * a2 + b1 * b2) < CalculationThreshold)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Returns result of two lines perpendicularity.
        /// </summary>
        /// <param name="firstLineFirstPoint">First point of the first line.</param>
        /// <param name="firstLineSecondPoint">Second point of the first line.</param>
        /// <param name="secondLineFirstPoint">First point of the second line.</param>
        /// <param name="secondLineSecondPoint">Second point of the second line.</param>
        /// <returns>True — <i>if the lines are perpendicular,</i><br/>False — <i>if the lines are not perpendicular.</i></returns>
        public static bool AreTwoLinesPerpendicular(Vector2D firstLineFirstPoint, Vector2D firstLineSecondPoint,
            Vector2D secondLineFirstPoint, Vector2D secondLineSecondPoint)
            => AreTwoLinesPerpendicular(firstLineFirstPoint.X, firstLineFirstPoint.Y, firstLineSecondPoint.X, firstLineSecondPoint.Y,
                secondLineFirstPoint.X, secondLineFirstPoint.Y, secondLineSecondPoint.X, secondLineSecondPoint.Y);

        /// <summary>
        /// Returns result of two vectors perpendicularity.
        /// </summary>
        /// <param name="firstVectorX">Coordinate X of the first vector.</param>
        /// <param name="firstVectorY">Coordinate Y of the first vector.</param>
        /// <param name="secondVectorX">Coordinate X of the second vector.</param>
        /// <param name="secondVectorY">Coordinate Y of the second vector.</param>
        /// <returns>True — <i>if the vectors are perpendicular,</i><br/>False — <i>if the vectors are not perpendicular.</i></returns>
        public static bool AreTwoVectorsPerpendicular(double firstVectorX, double firstVectorY, double secondVectorX, double secondVectorY)
        {
            ConvertTwoVectorsABCoefficients(
                VectorABCoefficients(firstVectorX, firstVectorY), out double a1, out double b1,
                VectorABCoefficients(secondVectorX, secondVectorY), out double a2, out double b2);

            if (Math.Abs(a1 * a2 + b1 * b2) < CalculationThreshold)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Returns result of two vectors perpendicularity.
        /// </summary>
        /// <param name="firstVector">First vector.</param>
        /// <param name="secondVector">Second vector.</param>
        /// <returns>True — <i>if the vectors are perpendicular,</i><br/>False — <i>if the vectors are not perpendicular.</i></returns>
        public static bool AreTwoVectorsPerpendicular(Vector2D firstVector, Vector2D secondVector)
            => AreTwoVectorsPerpendicular(firstVector.X, firstVector.Y, secondVector.X, secondVector.Y);

        /// <summary>
        /// Returns an angle between two lines in degrees.
        /// </summary>
        /// <param name="firstLineX1"></param>
        /// <param name="firstLineY1"></param>
        /// <param name="firstLineX2"></param>
        /// <param name="firstLineY2"></param>
        /// <param name="secondLineX1"></param>
        /// <param name="secondLineY1"></param>
        /// <param name="secondLineX2"></param>
        /// <param name="secondLineY2"></param>
        /// <returns>A number from 0 to 90.</returns>
        public static double BetweenTwoLinesAngle(double firstLineX1, double firstLineY1, double firstLineX2, double firstLineY2,
            double secondLineX1, double secondLineY1, double secondLineX2, double secondLineY2)
        {
            ConvertTwoLinesABCCoefficients(
                LineABCCoefficients(firstLineX1, firstLineY1, firstLineX2, firstLineY2), out double a1, out double b1, out double c1,
                LineABCCoefficients(secondLineX1, secondLineY1, secondLineX2, secondLineY2), out double a2, out double b2, out double c2);

            double denominator = a1 * a2 + b1 * b2;

            if (Math.Abs(denominator) < CalculationThreshold)
                return 90;
            else
            {
                double angleTangent = Math.Abs((a1 * b2 - a2 * b1) / denominator);
                return Math.Atan(angleTangent) * 180 / Math.PI;
            }
        }

        /// <summary>
        /// Returns an angle between two lines in degrees.
        /// </summary>
        /// <param name="firstLineFirstPoint">First point of the first line.</param>
        /// <param name="firstLineSecondPoint">Second point of the first line.</param>
        /// <param name="secondLineFirstPoint">First point of the second line.</param>
        /// <param name="secondLineSecondPoint">Second point of the second line.</param>
        /// <returns>A number from 0 to 90.</returns>
        public static double BetweenTwoLinesAngle(Vector2D firstLineFirstPoint, Vector2D firstLineSecondPoint,
            Vector2D secondLineFirstPoint, Vector2D secondLineSecondPoint)
            => BetweenTwoLinesAngle(firstLineFirstPoint.X, firstLineFirstPoint.Y, firstLineSecondPoint.X, firstLineSecondPoint.Y,
                secondLineFirstPoint.X, secondLineFirstPoint.Y, secondLineSecondPoint.X, secondLineSecondPoint.Y);

        /// <summary>
        /// Returns an angle between two vectors in degrees.
        /// </summary>
        /// <param name="firstVectorX">Coordinate X of the first vector.</param>
        /// <param name="firstVectorY">Coordinate Y of the first vector.</param>
        /// <param name="secondVectorX">Coordinate X of the second vector.</param>
        /// <param name="secondVectorY">Coordinate Y of the second vector.</param>
        /// <returns>A number from 0 to 180.</returns>
        public static double BetweenTwoVectorsAngle(double firstVectorX, double firstVectorY, double secondVectorX, double secondVectorY)
        {
            ConvertTwoVectorsABCoefficients(
                VectorABCoefficients(firstVectorX, firstVectorY), out double a1, out double b1,
                VectorABCoefficients(secondVectorX, secondVectorY), out double a2, out double b2);

            double numerator = firstVectorX * secondVectorX + firstVectorY * secondVectorY;
            double firstVectorLength = Math.Sqrt(firstVectorX * firstVectorX + firstVectorY * firstVectorY);
            double secondVectorLength = Math.Sqrt(secondVectorX * secondVectorX + secondVectorY * secondVectorY);
            double denominator = firstVectorLength * secondVectorLength;

            double cosine = numerator / denominator;

            if (cosine > 1)
                cosine = 1;
            else if (cosine < -1)
                cosine = -1;

            return Math.Acos(cosine) * 180 / Math.PI;
        }

        /// <summary>
        /// Returns an angle between two vectors in degrees.
        /// </summary>
        /// <param name="firstVector">First vector.</param>
        /// <param name="secondVector">Second vector.</param>
        /// <returns>A number from 0 to 180.</returns>
        public static double BetweenTwoVectorsAngle(Vector2D firstVector, Vector2D secondVector)
            => BetweenTwoVectorsAngle(firstVector.X, firstVector.Y, secondVector.X, secondVector.Y);

        private static void ConvertOneLineABCCoefficients(double[] abcCoefficients, out double a, out double b, out double c)
        {
            if (abcCoefficients == null)
                throw new ArgumentNullException(nameof(abcCoefficients));

            if (abcCoefficients.Length != 3)
                throw new ArgumentOutOfRangeException(nameof(abcCoefficients));

            a = abcCoefficients[0];
            b = abcCoefficients[1];
            c = abcCoefficients[2];
        }

        private static void ConvertTwoLinesABCCoefficients(double[] firstLineABCCoefficients, out double a1, out double b1, out double c1,
            double[] secondLineABCCoefficients, out double a2, out double b2, out double c2)
        {
            if (firstLineABCCoefficients == null)
                throw new ArgumentNullException(nameof(firstLineABCCoefficients));

            if (secondLineABCCoefficients == null)
                throw new ArgumentNullException(nameof(secondLineABCCoefficients));

            if (firstLineABCCoefficients.Length != 3)
                throw new ArgumentOutOfRangeException(nameof(firstLineABCCoefficients));

            if (secondLineABCCoefficients.Length != 3)
                throw new ArgumentOutOfRangeException(nameof(secondLineABCCoefficients));

            a1 = firstLineABCCoefficients[0];
            b1 = firstLineABCCoefficients[1];
            c1 = firstLineABCCoefficients[2];
            a2 = secondLineABCCoefficients[0];
            b2 = secondLineABCCoefficients[1];
            c2 = secondLineABCCoefficients[2];
        }

        private static void ConvertTwoVectorsABCoefficients(double[] firstVectorABCoefficients, out double a1, out double b1,
            double[] secondVectorABCoefficients, out double a2, out double b2)
        {
            if (firstVectorABCoefficients == null)
                throw new ArgumentNullException(nameof(firstVectorABCoefficients));

            if (secondVectorABCoefficients == null)
                throw new ArgumentNullException(nameof(secondVectorABCoefficients));

            if (firstVectorABCoefficients.Length != 2)
                throw new ArgumentOutOfRangeException(nameof(firstVectorABCoefficients));

            if (secondVectorABCoefficients.Length != 2)
                throw new ArgumentOutOfRangeException(nameof(secondVectorABCoefficients));

            a1 = firstVectorABCoefficients[0];
            b1 = firstVectorABCoefficients[1];
            a2 = secondVectorABCoefficients[0];
            b2 = secondVectorABCoefficients[1];
        }

        public struct Vector2D
        {
            public double X { get; }
            public double Y { get; }

            public Vector2D(double x, double y)
            {
                X = x;
                Y = y;
            }
        }
    }
}