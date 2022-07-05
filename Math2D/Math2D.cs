using System;
namespace Math2D
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
        /// Returns a distance between a point and a line.
        /// </summary>
        /// <param name="pointX">Coordinate X of the single point.</param>
        /// <param name="pointY">Coordinate Y of the single point.</param>
        /// <param name="lineX1">Coordinate X of the line first point.</param>
        /// <param name="lineY1">Coordinate Y of the line first point.</param>
        /// <param name="lineX2">Coordinate X of the line second point.</param>
        /// <param name="lineY2">Coordinate Y of the line second point.</param>
        /// <returns>A number.</returns>
        public static double BetweenPointAndLineDistance(double pointX, double pointY, double lineX1, double lineY1, double lineX2, double lineY2)
        {
            ConvertOneLineABCCoefficients(LineABCCoefficients(lineX1, lineY1, lineX2, lineY2), out double a, out double b, out double c);
            return Math.Abs(a * pointX + b * pointY + c) / Math.Sqrt(a * a + b * b);
        }

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
        /// <returns>An array { X, Y } <i>or</i><br/>Null — <i>if parallel or matching lines.</i></returns>
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
        /// Returns an angle in degrees between two lines.
        /// </summary>
        /// <param name="firstLineX1"></param>
        /// <param name="firstLineY1"></param>
        /// <param name="firstLineX2"></param>
        /// <param name="firstLineY2"></param>
        /// <param name="secondLineX1"></param>
        /// <param name="secondLineY1"></param>
        /// <param name="secondLineX2"></param>
        /// <param name="secondLineY2"></param>
        /// <returns>A number.</returns>
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
                double angleTangent = Math.Abs((a2 * b1 - a1 * b2) / denominator);
                return Math.Atan(angleTangent) * 180 / Math.PI;
            }
        }

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
    }
}