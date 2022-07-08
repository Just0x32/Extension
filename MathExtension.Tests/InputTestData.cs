namespace MathExtension.Tests
{
    public static class InputTestData
    {
        public static LineSegment[] LineSegments { get; } =
        {
            #region [ Passed through the origin lines ]
            // On X-axis line
            new LineSegment(0, 0, 1, 0),
            new LineSegment(-1000000, 0, 1000000, 0),

            // On Y-axis line
            new LineSegment(0, 0, 0, 1),
            new LineSegment(0, -1000000, 0, 1000000),

            // Passed through the origin line, at between 0 and -1 degrees to the axis X
            new LineSegment(0, 0, 1000000, -1),
            new LineSegment(-1000000, 1, 1000000, -1),

            // Passed through the origin line, at -11 degrees to the axis X
            new LineSegment(0, 0, 5, -1),
            new LineSegment(-5000000, 1000000, 5000000, -1000000),

            // Passed through the origin line, at -37 degrees to the axis X
            new LineSegment(0, 0, 4, -3),
            new LineSegment(-4000000, 3000000, 4000000, -3000000),

            // Passed through the origin line, at -45 degrees to the axis X
            new LineSegment(0, 0, -1, 1),
            new LineSegment(1000000, -1000000, -1000000, 1000000),
            #endregion

            #region [ Axis-parallel lines ]
            // X-axis-parallel line, above X-axis
            new LineSegment(0, 1, 1, 1),
            new LineSegment(-1000000, 1, 1000000, 1),

            // X-axis-parallel line, below X-axis
            new LineSegment(0, -1, 1, -1),
            new LineSegment(-1000000, -1, 1000000, -1),

            // Y-axis-parallel line, right-hand from Y-axis
            new LineSegment(1, 0, 1, 1),
            new LineSegment(1, -1000000, 1, 1000000),

            // Y-axis-parallel line, left-hand from Y-axis
            new LineSegment(-1, 0, -1, 1),
            new LineSegment(-1, -1000000, -1, 1000000),
            #endregion

            #region [ Not passed through the origin lines, second quarter ]
            // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
            new LineSegment(-1, 1, 999999, 2),
            new LineSegment(-1000001, 0, 999999, 2),

            // Not passed through the origin line, at 11 degrees to the axis X, second quarter
            new LineSegment(-1, 1, 4, 2),
            new LineSegment(-5000001, -999999, 4999999, 1000001),

            // Not passed through the origin line, at 37 degrees to the axis X, second quarter
            new LineSegment(-1, 1, 3, 4),
            new LineSegment(-4000001, -2999999, 3999999, 3000001),

            // Not passed through the origin line, at 45 degrees to the axis X, second quarter
            new LineSegment(-1, 1, 0, 2),
            new LineSegment(-1000001, -999999, 999999, 1000001),
            #endregion
        };

        public static SinglePoint[] SinglePoints { get; } =
        {
            new SinglePoint(0, 0),

            new SinglePoint(1, 0),
            new SinglePoint(-1, 1),
            new SinglePoint(-1, -1),

            new SinglePoint(100, 0),
            new SinglePoint(100, -1),
            new SinglePoint(100, -2),
            new SinglePoint(100, -3),
        };

        public static LineABCCoefficients[] Result_ABCCoefficients { get; } =
        {
            #region [ Passed through the origin lines ]
            // On X-axis line
            new LineABCCoefficients(0, -1, 0),
            new LineABCCoefficients(0, -2000000, 0),

            // On Y-axis line
            new LineABCCoefficients(1, 0, 0),
            new LineABCCoefficients(2000000, 0, 0),

            // Passed through the origin line, at between 0 and -1 degrees to the axis X
            new LineABCCoefficients(-1, -1000000, 0),
            new LineABCCoefficients(-2, -2000000, 0),

            // Passed through the origin line, at -11 degrees to the axis X
            new LineABCCoefficients(-1, -5, 0),
            new LineABCCoefficients(-2000000, -10000000, 0),

            // Passed through the origin line, at -37 degrees to the axis X
            new LineABCCoefficients(-3, -4, 0),
            new LineABCCoefficients(-6000000, -8000000, 0),

            // Passed through the origin line, at -45 degrees to the axis X
            new LineABCCoefficients(1, 1, 0),
            new LineABCCoefficients(2000000, 2000000, 0),
            #endregion

            #region [ Axis-parallel lines ]
            // X-axis-parallel line, above X-axis
            new LineABCCoefficients(0, -1, 1),
            new LineABCCoefficients(0, -2000000, 2000000),

            // X-axis-parallel line, below X-axis
            new LineABCCoefficients(0, -1, -1),
            new LineABCCoefficients(0, -2000000, -2000000),

            // Y-axis-parallel line, right-hand from Y-axis
            new LineABCCoefficients(1, 0, -1),
            new LineABCCoefficients(2000000, 0, -2000000),

            // Y-axis-parallel line, left-hand from Y-axis
            new LineABCCoefficients(1, 0, 1),
            new LineABCCoefficients(2000000, 0, 2000000),
            #endregion

            #region [ Not passed through the origin lines, second quarter ]
            // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
            new LineABCCoefficients(1, -1000000, 1000001),
            new LineABCCoefficients(2, -2000000, 2000002),

            // Not passed through the origin line, at 11 degrees to the axis X, second quarter
            new LineABCCoefficients(1, -5, 6),
            new LineABCCoefficients(2000000, -10000000, 12000000),

            // Not passed through the origin line, at 37 degrees to the axis X, second quarter
            new LineABCCoefficients(3, -4, 7),
            new LineABCCoefficients(6000000, -8000000, 14000000),

            // Not passed through the origin line, at 45 degrees to the axis X, second quarter
            new LineABCCoefficients(1, -1, 2),
            new LineABCCoefficients(2000000, -2000000, 4000000),
            #endregion
        };

        public static double[] Result_LineAngles { get; private set; } =
        {
            #region [ Passed through the origin lines ]
            // On X-axis line
            0, 0,
            // On Y-axis line
            90, 90,
            // Passed through the origin line, at between 0 and -1 degrees to the axis X
            -5.7298E-5, -5.7298E-5,
            // Passed through the origin line, at -11 degrees to the axis X
            -11.309932474, -11.309932474,
            // Passed through the origin line, at -37 degrees to the axis X
            -36.869897646, -36.869897646,
            // Passed through the origin line, at -45 degrees to the axis X
            -45, -45,
            #endregion

            #region [ Axis-parallel lines ]
            // X-axis-parallel line, above X-axis
            0, 0,
            // X-axis-parallel line, below X-axis
            0, 0,
            // Y-axis-parallel line, right-hand from Y-axis
            90, 90,
            // Y-axis-parallel line, left-hand from Y-axis
            90, 90,
            #endregion

            #region [ Not passed through the origin lines, second quarter ]
            // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
            5.7298E-5, 5.7298E-5,
            // Not passed through the origin line, at 11 degrees to the axis X, second quarter
            11.309932474, 11.309932474,
            // Not passed through the origin line, at 37 degrees to the axis X, second quarter
            36.869897646, 36.869897646,
            // Not passed through the origin line, at 45 degrees to the axis X, second quarter
            45, 45,
            #endregion
        };

        public static int[][] Result_RelativeToLinePointLocations { get; private set; } =
        {
            // Point (0, 0)
            new int[]
            {
                #region [ Passed through the origin lines ]
                // On X-axis line
                0, 0,
                // On Y-axis line
                0, 0,
                // Passed through the origin line, at between 0 and -1 degrees to the axis X
                0, 0,
                // Passed through the origin line, at -11 degrees to the axis X
                0, 0,
                // Passed through the origin line, at -37 degrees to the axis X
                0, 0,
                // Passed through the origin line, at -45 degrees to the axis X
                0, 0,
                #endregion
                
                #region [ Axis-parallel lines ]
                // X-axis-parallel line, above X-axis
                -1, -1,
                // X-axis-parallel line, below X-axis
                1, 1,
                // Y-axis-parallel line, right-hand from Y-axis
                1, 1,
                // Y-axis-parallel line, left-hand from Y-axis
                -1, -1,
                #endregion
                
                #region [ Not passed through the origin lines, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                -1, -1,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                -1, -1,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                -1, -1,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                -1, -1,
                #endregion
            },
            // Point (1, 0)
            new int[]
            {
                #region [ Passed through the origin lines ]
                // On X-axis line
                0, 0,
                // On Y-axis line
                -1, -1,
                // Passed through the origin line, at between 0 and -1 degrees to the axis X
                1, 1,
                // Passed through the origin line, at -11 degrees to the axis X
                1, 1,
                // Passed through the origin line, at -37 degrees to the axis X
                1, 1,
                // Passed through the origin line, at -45 degrees to the axis X
                -1, -1,
                #endregion
                
                #region [ Axis-parallel lines ]
                // X-axis-parallel line, above X-axis
                -1, -1,
                // X-axis-parallel line, below X-axis
                1, 1,
                // Y-axis-parallel line, right-hand from Y-axis
                0, 0,
                // Y-axis-parallel line, left-hand from Y-axis
                -1, -1,
                #endregion
                
                #region [ Not passed through the origin lines, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                -1, -1,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                -1, -1,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                -1, -1,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                -1, -1,
                #endregion
            },
            // Point (-1, 1)
            new int[]
            {
                #region [ Passed through the origin lines ]
                // On X-axis line
                1, 1,
                // On Y-axis line
                1, 1,
                // Passed through the origin line, at between 0 and -1 degrees to the axis X
                1, 1,
                // Passed through the origin line, at -11 degrees to the axis X
                1, 1,
                // Passed through the origin line, at -37 degrees to the axis X
                1, 1,
                // Passed through the origin line, at -45 degrees to the axis X
                0, 0,
                #endregion
                
                #region [ Axis-parallel lines ]
                // X-axis-parallel line, above X-axis
                0, 0,
                // X-axis-parallel line, below X-axis
                1, 1,
                // Y-axis-parallel line, right-hand from Y-axis
                1, 1,
                // Y-axis-parallel line, left-hand from Y-axis
                0, 0,
                #endregion
                
                #region [ Not passed through the origin lines, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                0, 0,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                0, 0,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                0, 0,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                0, 0,
                #endregion
            },
            // Point (-1, -1)
            new int[]
            {
                #region [ Passed through the origin lines ]
                // On X-axis line
                -1, -1,
                // On Y-axis line
                1, 1,
                // Passed through the origin line, at between 0 and -1 degrees to the axis X
                -1, -1,
                // Passed through the origin line, at -11 degrees to the axis X
                -1, -1,
                // Passed through the origin line, at -37 degrees to the axis X
                -1, -1,
                // Passed through the origin line, at -45 degrees to the axis X
                1, 1,
                #endregion
                
                #region [ Axis-parallel lines ]
                // X-axis-parallel line, above X-axis
                -1, -1,
                // X-axis-parallel line, below X-axis
                0, 0,
                // Y-axis-parallel line, right-hand from Y-axis
                1, 1,
                // Y-axis-parallel line, left-hand from Y-axis
                0, 0,
                #endregion
                
                #region [ Not passed through the origin lines, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second
                -1, -1,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                -1, -1,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                -1, -1,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                -1, -1,
                #endregion
            },
            // Point (100, 0)
            new int[]
            {
                #region [ Passed through the origin lines ]
                // On X-axis line
                0, 0,
                // On Y-axis line
                -1, -1,
                // Passed through the origin line, at between 0 and -1 degrees to the axis X
                1, 1,
                // Passed through the origin line, at -11 degrees to the axis X
                1, 1,
                // Passed through the origin line, at -37 degrees to the axis X
                1, 1,
                // Passed through the origin line, at -45 degrees to the axis X
                -1, -1,
                #endregion
                
                #region [ Axis-parallel lines ]
                // X-axis-parallel line, above X-axis
                -1, -1,
                // X-axis-parallel line, below X-axis
                1, 1,
                // Y-axis-parallel line, right-hand from Y-axis
                -1, -1,
                // Y-axis-parallel line, left-hand from Y-axis
                -1, -1,
                #endregion
                
                #region [ Not passed through the origin lines, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second
                -1, -1,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                -1, -1,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                -1, -1,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                -1, -1,
                #endregion
            },
            // Point (100, -1)
            new int[]
            {
                #region [ Passed through the origin lines ]
                // On X-axis line
                -1, -1,
                // On Y-axis line
                -1, -1,
                // Passed through the origin line, at between 0 and -1 degrees to the axis X
                -1, -1,
                // Passed through the origin line, at -11 degrees to the axis X
                1, 1,
                // Passed through the origin line, at -37 degrees to the axis X
                1, 1,
                // Passed through the origin line, at -45 degrees to the axis X
                -1, -1,
                #endregion
                
                #region [ Axis-parallel lines ]
                // X-axis-parallel line, above X-axis
                -1, -1,
                // X-axis-parallel line, below X-axis
                0, 0,
                // Y-axis-parallel line, right-hand from Y-axis
                -1, -1,
                // Y-axis-parallel line, left-hand from Y-axis
                -1, -1,
                #endregion
                
                #region [ Not passed through the origin lines, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second
                -1, -1,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                -1, -1,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                -1, -1,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                -1, -1,
                #endregion
            },
            // Point (100, -2)
            new int[]
            {
                #region [ Passed through the origin lines ]
                // On X-axis line
                -1, -1,
                // On Y-axis line
                -1, -1,
                // Passed through the origin line, at between 0 and -1 degrees to the axis X
                -1, -1,
                // Passed through the origin line, at -11 degrees to the axis X
                1, 1,
                // Passed through the origin line, at -37 degrees to the axis X
                1, 1,
                // Passed through the origin line, at -45 degrees to the axis X
                -1, -1,
                #endregion
                
                #region [ Axis-parallel lines ]
                // X-axis-parallel line, above X-axis
                -1, -1,
                // X-axis-parallel line, below X-axis
                -1, -1,
                // Y-axis-parallel line, right-hand from Y-axis
                -1, -1,
                // Y-axis-parallel line, left-hand from Y-axis
                -1, -1,
                #endregion
                
                #region [ Not passed through the origin lines, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second
                -1, -1,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                -1, -1,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                -1, -1,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                -1, -1,
                #endregion
            },
            // Point (100, -3)
            new int[]
            {
                #region [ Passed through the origin lines ]
                // On X-axis line
                -1, -1,
                // On Y-axis line
                -1, -1,
                // Passed through the origin line, at between 0 and -1 degrees to the axis X
                -1, -1,
                // Passed through the origin line, at -11 degrees to the axis X
                1, 1,
                // Passed through the origin line, at -37 degrees to the axis X
                1, 1,
                // Passed through the origin line, at -45 degrees to the axis X
                -1, -1,
                #endregion
                
                #region [ Axis-parallel lines ]
                // X-axis-parallel line, above X-axis
                -1, -1,
                // X-axis-parallel line, below X-axis
                -1, -1,
                // Y-axis-parallel line, right-hand from Y-axis
                -1, -1,
                // Y-axis-parallel line, left-hand from Y-axis
                -1, -1,
                #endregion
                
                #region [ Not passed through the origin lines, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second
                -1, -1,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                -1, -1,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                -1, -1,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                -1, -1,
                #endregion
            },
        };

        public static double[][] Result_BetweenPointAndLineDistances { get; } =
        {
            // Point (0, 0)
            new double[]
            {
                #region [ Passed through the origin lines ]
                // On X-axis line
                0, 0,
                // On Y-axis line
                0, 0,
                // Passed through the origin line, at between 0 and -1 degrees to the axis X
                0, 0,
                // Passed through the origin line, at -11 degrees to the axis X
                0, 0,
                // Passed through the origin line, at -37 degrees to the axis X
                0, 0,
                // Passed through the origin line, at -45 degrees to the axis X
                0, 0,
                #endregion
                
                #region [ Axis-parallel lines ]
                // X-axis-parallel line, above X-axis
                1, 1,
                // X-axis-parallel line, below X-axis
                1, 1,
                // Y-axis-parallel line, right-hand from Y-axis
                1, 1,
                // Y-axis-parallel line, left-hand from Y-axis
                1, 1,
                #endregion
                
                #region [ Not passed through the origin lines, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                1.0000009999995, 1.0000009999995,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                1.1766968108291043, 1.1766968108291043,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                1.4, 1.4,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                1.4142135623730951, 1.4142135623730951,
                #endregion
            },
            // Point (1, 0)
            new double[]
            {
                #region [ Passed through the origin lines ]
                // On X-axis line
                0, 0,
                // On Y-axis line
                1, 1,
                // Passed through the origin line, at between 0 and -1 degrees to the axis X
                9.999999999995E-7, 9.999999999995E-7,
                // Passed through the origin line, at -11 degrees to the axis X
                0.19611613513818404, 0.19611613513818404,
                // Passed through the origin line, at -37 degrees to the axis X
                0.6, 0.6,
                // Passed through the origin line, at -45 degrees to the axis X
                0.7071067811865476, 0.7071067811865476,
                #endregion
                
                #region [ Axis-parallel lines ]
                // X-axis-parallel line, above X-axis
                1, 1,
                // X-axis-parallel line, below X-axis
                1, 1,
                // Y-axis-parallel line, right-hand from Y-axis
                0, 0,
                // Y-axis-parallel line, left-hand from Y-axis
                2, 2,
                #endregion
                
                #region [ Not passed through the origin lines, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                1.0000019999995, 1.0000019999995,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                1.372812945967288, 1.372812945967288,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                2, 2,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                2.121320343559643, 2.121320343559643,
                #endregion
            },
            // Point (-1, 1)
            new double[]
            {
                #region [ Passed through the origin lines ]
                // On X-axis line
                1, 1,
                // On Y-axis line
                1, 1,
                // Passed through the origin line, at between 0 and -1 degrees to the axis X
                0.9999989999995, 0.9999989999995,
                // Passed through the origin line, at -11 degrees to the axis X
                0.7844645405527361, 0.7844645405527361,
                // Passed through the origin line, at -37 degrees to the axis X
                0.2, 0.2,
                // Passed through the origin line, at -45 degrees to the axis X
                0, 0,
                #endregion
                
                #region [ Axis-parallel lines ]
                // X-axis-parallel line, above X-axis
                0, 0,
                // X-axis-parallel line, below X-axis
                2, 2,
                // Y-axis-parallel line, right-hand from Y-axis
                2, 2,
                // Y-axis-parallel line, left-hand from Y-axis
                0, 0,
                #endregion
                
                #region [ Not passed through the origin lines, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                0, 0,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                0, 0,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                0, 0,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                0, 0,
                #endregion
            },
            // Point (-1, -1)
            new double[]
            {
                #region [ Passed through the origin lines ]
                // On X-axis line
                1, 1,
                // On Y-axis line
                1, 1,
                // Passed through the origin line, at between 0 and -1 degrees to the axis X
                1.0000009999995, 1.0000009999995,
                // Passed through the origin line, at -11 degrees to the axis X
                1.1766968108291043, 1.1766968108291043,
                // Passed through the origin line, at -37 degrees to the axis X
                1.4, 1.4,
                // Passed through the origin line, at -45 degrees to the axis X
                1.4142135623730951, 1.4142135623730951,
                #endregion
                
                #region [ Axis-parallel lines ]
                // X-axis-parallel line, above X-axis
                2, 2,
                // X-axis-parallel line, below X-axis
                0, 0,
                // Y-axis-parallel line, right-hand from Y-axis
                2, 2,
                // Y-axis-parallel line, left-hand from Y-axis
                0, 0,
                #endregion
                
                #region [ Not passed through the origin lines, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second
                1.9999999999990001, 1.9999999999990001,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                1.9611613513818402, 1.9611613513818402,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                1.6, 1.6,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                1.4142135623730951, 1.4142135623730951,
                #endregion
            },
            // Point (100, 0)
            new double[]
            {
                #region [ Passed through the origin lines ]
                // On X-axis line
                0, 0,
                // On Y-axis line
                100, 100,
                // Passed through the origin line, at between 0 and -1 degrees to the axis X
                0.00009999999999995001, 0.00009999999999995001,
                // Passed through the origin line, at -11 degrees to the axis X
                19.6116135138184, 19.6116135138184,
                // Passed through the origin line, at -37 degrees to the axis X
                60, 60,
                // Passed through the origin line, at -45 degrees to the axis X
                70.71067811865476, 70.71067811865476,
                #endregion
                
                #region [ Axis-parallel lines ]
                // X-axis-parallel line, above X-axis
                1, 1,
                // X-axis-parallel line, below X-axis
                1, 1,
                // Y-axis-parallel line, right-hand from Y-axis
                99, 99,
                // Y-axis-parallel line, left-hand from Y-axis
                101, 101,
                #endregion
                
                #region [ Not passed through the origin lines, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second
                1.0001009999994999, 1.0001009999994999,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                20.788310324647505, 20.788310324647505,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                61.4, 61.4,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                72.12489168102785, 72.12489168102785,
                #endregion
            },
            // Point (100, -1)
            new double[]
            {
                #region [ Passed through the origin lines ]
                // On X-axis line
                1, 1,
                // On Y-axis line
                100, 100,
                // Passed through the origin line, at between 0 and -1 degrees to the axis X
                0.9998999999995, 0.9998999999995,
                // Passed through the origin line, at -11 degrees to the axis X
                18.63103283812748, 18.63103283812748,
                // Passed through the origin line, at -37 degrees to the axis X
                59.2, 59.2,
                // Passed through the origin line, at -45 degrees to the axis X
                70.00357133746822, 70.00357133746822,
                #endregion
                
                #region [ Axis-parallel lines ]
                // X-axis-parallel line, above X-axis
                2, 2,
                // X-axis-parallel line, below X-axis
                0, 0,
                // Y-axis-parallel line, right-hand from Y-axis
                99, 99,
                // Y-axis-parallel line, left-hand from Y-axis
                101, 101,
                #endregion
                
                #region [ Not passed through the origin lines, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second
                2.0001009999990003, 2.0001009999990003,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                21.768891000338424, 21.768891000338424,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                62.2, 62.2,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                72.8319984622144, 72.8319984622144,
                #endregion
            },
            // Point (100, -2)
            new double[]
            {
                #region [ Passed through the origin lines ]
                // On X-axis line
                2, 2,
                // On Y-axis line
                100, 100,
                // Passed through the origin line, at between 0 and -1 degrees to the axis X
                1.9998999999990001, 1.9998999999990001,
                // Passed through the origin line, at -11 degrees to the axis X
                17.65045216243656, 17.65045216243656,
                // Passed through the origin line, at -37 degrees to the axis X
                58.4, 58.4,
                // Passed through the origin line, at -45 degrees to the axis X
                69.29646455628166, 69.29646455628166,
                #endregion
                
                #region [ Axis-parallel lines ]
                // X-axis-parallel line, above X-axis
                3, 3,
                // X-axis-parallel line, below X-axis
                1, 1,
                // Y-axis-parallel line, right-hand from Y-axis
                99, 99,
                // Y-axis-parallel line, left-hand from Y-axis
                101, 101,
                #endregion
                
                #region [ Not passed through the origin lines, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second
                3.0001009999984998, 3.0001009999984998,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                22.749471676029348, 22.749471676029348,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                63, 63,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                73.53910524340095, 73.53910524340095,
                #endregion
            },
            // Point (100, -3)
            new double[]
            {
                #region [ Passed through the origin lines ]
                // On X-axis line
                3, 3,
                // On Y-axis line
                100, 100,
                // Passed through the origin line, at between 0 and -1 degrees to the axis X
                2.9998999999985, 2.9998999999985,
                // Passed through the origin line, at -11 degrees to the axis X
                16.66987148674564, 16.66987148674564,
                // Passed through the origin line, at -37 degrees to the axis X
                57.6, 57.6,
                // Passed through the origin line, at -45 degrees to the axis X
                68.58935777509511, 68.58935777509511,
                #endregion
                
                #region [ Axis-parallel lines ]
                // X-axis-parallel line, above X-axis
                4, 4,
                // X-axis-parallel line, below X-axis
                2, 2,
                // Y-axis-parallel line, right-hand from Y-axis
                99, 99,
                // Y-axis-parallel line, left-hand from Y-axis
                101, 101,
                #endregion
                
                #region [ Not passed through the origin lines, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second
                4.000100999998, 4.000100999998,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                23.730052351720268, 23.730052351720268,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                63.8, 63.8,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                74.24621202458749, 74.24621202458749,
                #endregion
            },
        };

        public static SinglePoint?[][] Result_TwoLinesIntersectionPoints { get; } =
        {
            // On X-axis first line
            new SinglePoint?[]
            {
                #region [ Passed through the origin second line ]
                // On X-axis line
                null,
                // On Y-axis line
                new SinglePoint(0, 0),
                new SinglePoint(0, 0),
                // Passed through the origin line, at between 0 and -1 degrees to the axis X
                new SinglePoint(0, 0),
                new SinglePoint(0, 0),
                // Passed through the origin line, at -11 degrees to the axis X
                new SinglePoint(0, 0),
                new SinglePoint(0, 0),
                // Passed through the origin line, at -37 degrees to the axis X
                new SinglePoint(0, 0),
                new SinglePoint(0, 0),
                // Passed through the origin line, at -45 degrees to the axis X
                new SinglePoint(0, 0),
                new SinglePoint(0, 0),
                #endregion

                #region [ Axis-parallel second line ]
                // X-axis-parallel line, above X-axis
                null,
                null,
                // X-axis-parallel line, below X-axis
                null,
                null,
                // Y-axis-parallel line, right-hand from Y-axis
                new SinglePoint(1, 0),
                new SinglePoint(1, 0),
                // Y-axis-parallel line, left-hand from Y-axis
                new SinglePoint(-1, 0),
                new SinglePoint(-1, 0),
                #endregion

                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                new SinglePoint(-1000001, 0),
                new SinglePoint(-1000001, 0),
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                new SinglePoint(-6, 0),
                new SinglePoint(-6, 0),
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                new SinglePoint(-2.333333333333333, 0),
                new SinglePoint(-2.333333333333333, 0),
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                new SinglePoint(-2, 0),
                new SinglePoint(-2, 0),
                #endregion
            },
            // On X-axis first line
            new SinglePoint?[]
            {
                #region [ Passed through the origin second line ]
                // On Y-axis line
                new SinglePoint(0, 0),
                new SinglePoint(0, 0),
                // Passed through the origin line, at between 0 and -1 degrees to the axis X
                new SinglePoint(0, 0),
                new SinglePoint(0, 0),
                // Passed through the origin line, at -11 degrees to the axis X
                new SinglePoint(0, 0),
                new SinglePoint(0, 0),
                // Passed through the origin line, at -37 degrees to the axis X
                new SinglePoint(0, 0),
                new SinglePoint(0, 0),
                // Passed through the origin line, at -45 degrees to the axis X
                new SinglePoint(0, 0),
                new SinglePoint(0, 0),
                #endregion

                #region [ Axis-parallel second line ]
                // X-axis-parallel line, above X-axis
                null,
                null,
                // X-axis-parallel line, below X-axis
                null,
                null,
                // Y-axis-parallel line, right-hand from Y-axis
                new SinglePoint(1, 0),
                new SinglePoint(1, 0),
                // Y-axis-parallel line, left-hand from Y-axis
                new SinglePoint(-1, 0),
                new SinglePoint(-1, 0),
                #endregion

                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                new SinglePoint(-1000001, 0),
                new SinglePoint(-1000001, 0),
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                new SinglePoint(-6, 0),
                new SinglePoint(-6, 0),
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                new SinglePoint(-2.333333333333333, 0),
                new SinglePoint(-2.333333333333333, 0),
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                new SinglePoint(-2, 0),
                new SinglePoint(-2, 0),
                #endregion
            },
            // On Y-axis first line
            new SinglePoint?[]
            {
                #region [ Passed through the origin second line ]
                // On Y-axis line
                null,
                // Passed through the origin line, at between 0 and -1 degrees to the axis X
                new SinglePoint(0, 0),
                new SinglePoint(0, 0),
                // Passed through the origin line, at -11 degrees to the axis X
                new SinglePoint(0, 0),
                new SinglePoint(0, 0),
                // Passed through the origin line, at -37 degrees to the axis X
                new SinglePoint(0, 0),
                new SinglePoint(0, 0),
                // Passed through the origin line, at -45 degrees to the axis X
                new SinglePoint(0, 0),
                new SinglePoint(0, 0),
                #endregion
                
                #region [ Axis-parallel second line ]
                // X-axis-parallel line, above X-axis
                new SinglePoint(0, 1),
                new SinglePoint(0, 1),
                // X-axis-parallel line, below X-axis
                new SinglePoint(0, -1),
                new SinglePoint(0, -1),
                // Y-axis-parallel line, right-hand from Y-axis
                null,
                null,
                // Y-axis-parallel line, left-hand from Y-axis
                null,
                null,
                #endregion
                
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                new SinglePoint(0, 1.000001),
                new SinglePoint(0, 1.000001),
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                new SinglePoint(0, 1.2),
                new SinglePoint(0, 1.2),
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                new SinglePoint(0, 1.75),
                new SinglePoint(0, 1.75),
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                new SinglePoint(0, 2),
                new SinglePoint(0, 2),
                #endregion
            },
            // On Y-axis first line
            new SinglePoint?[]
            {
                #region [ Passed through the origin second line ]
                // Passed through the origin line, at between 0 and -1 degrees to the axis X
                new SinglePoint(0, 0),
                new SinglePoint(0, 0),
                // Passed through the origin line, at -11 degrees to the axis X
                new SinglePoint(0, 0),
                new SinglePoint(0, 0),
                // Passed through the origin line, at -37 degrees to the axis X
                new SinglePoint(0, 0),
                new SinglePoint(0, 0),
                // Passed through the origin line, at -45 degrees to the axis X
                new SinglePoint(0, 0),
                new SinglePoint(0, 0),
                #endregion
                
                #region [ Axis-parallel second line ]
                // X-axis-parallel line, above X-axis
                new SinglePoint(0, 1),
                new SinglePoint(0, 1),
                // X-axis-parallel line, below X-axis
                new SinglePoint(0, -1),
                new SinglePoint(0, -1),
                // Y-axis-parallel line, right-hand from Y-axis
                null,
                null,
                // Y-axis-parallel line, left-hand from Y-axis
                null,
                null,
                #endregion
                
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                new SinglePoint(0, 1.000001),
                new SinglePoint(0, 1.000001),
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                new SinglePoint(0, 1.2),
                new SinglePoint(0, 1.2),
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                new SinglePoint(0, 1.75),
                new SinglePoint(0, 1.75),
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                new SinglePoint(0, 2),
                new SinglePoint(0, 2),
                #endregion
            },
            // Passed through the origin first line, at between 0 and -1 degrees to the axis X
            new SinglePoint?[]
            {
                #region [ Passed through the origin second line ]
                // Passed through the origin line, at between 0 and -1 degrees to the axis X
                null,
                // Passed through the origin line, at -11 degrees to the axis X
                new SinglePoint(0, 0),
                new SinglePoint(0, 0),
                // Passed through the origin line, at -37 degrees to the axis X
                new SinglePoint(0, 0),
                new SinglePoint(0, 0),
                // Passed through the origin line, at -45 degrees to the axis X
                new SinglePoint(0, 0),
                new SinglePoint(0, 0),
                #endregion
                
                #region [ Axis-parallel second line ]
                // X-axis-parallel line, above X-axis
                new SinglePoint(-1000000, 1),
                new SinglePoint(-1000000, 1),
                // X-axis-parallel line, below X-axis
                new SinglePoint(1000000, -1),
                new SinglePoint(1000000, -1),
                // Y-axis-parallel line, right-hand from Y-axis
                new SinglePoint(1, -1E-6),
                new SinglePoint(1, -1E-6),
                // Y-axis-parallel line, left-hand from Y-axis
                new SinglePoint(-1, 1E-6),
                new SinglePoint(-1, 1E-6),
                #endregion
                
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                new SinglePoint(-500000.5, 0.5000005),
                new SinglePoint(-500000.5, 0.5000005),
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                new SinglePoint(-5.99997000015, 5.99997000015E-6),
                new SinglePoint(-5.99997000015, 5.99997000015E-6),
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                new SinglePoint(-2.333330222226, 2.333330222226E-6),
                new SinglePoint(-2.333330222226, 2.333330222226E-6),
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                new SinglePoint(-1.999998000002, 1.999998000002E-6),
                new SinglePoint(-1.999998000002, 1.999998000002E-6),
                #endregion
            },
            // Passed through the origin first line, at between 0 and -1 degrees to the axis X
            new SinglePoint?[]
            {
                #region [ Passed through the origin second line ]
                // Passed through the origin line, at -11 degrees to the axis X
                new SinglePoint(0, 0),
                new SinglePoint(0, 0),
                // Passed through the origin line, at -37 degrees to the axis X
                new SinglePoint(0, 0),
                new SinglePoint(0, 0),
                // Passed through the origin line, at -45 degrees to the axis X
                new SinglePoint(0, 0),
                new SinglePoint(0, 0),
                #endregion
                
                #region [ Axis-parallel second line ]
                // X-axis-parallel line, above X-axis
                new SinglePoint(-1000000, 1),
                new SinglePoint(-1000000, 1),
                // X-axis-parallel line, below X-axis
                new SinglePoint(1000000, -1),
                new SinglePoint(1000000, -1),
                // Y-axis-parallel line, right-hand from Y-axis
                new SinglePoint(1, -1E-6),
                new SinglePoint(1, -1E-6),
                // Y-axis-parallel line, left-hand from Y-axis
                new SinglePoint(-1, 1E-6),
                new SinglePoint(-1, 1E-6),
                #endregion
                
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                new SinglePoint(-500000.5, 0.5000005),
                new SinglePoint(-500000.5, 0.5000005),
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                new SinglePoint(-5.99997000015, 5.99997000015E-6),
                new SinglePoint(-5.99997000015, 5.99997000015E-6),
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                new SinglePoint(-2.333330222226, 2.333330222226E-6),
                new SinglePoint(-2.333330222226, 2.333330222226E-6),
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                new SinglePoint(-1.999998000002, 1.999998000002E-6),
                new SinglePoint(-1.999998000002, 1.999998000002E-6),
                #endregion
            },
            // Passed through the origin first line, at -11 degrees to the axis X
            new SinglePoint?[]
            {
                #region [ Passed through the origin second line ]
                // Passed through the origin line, at -11 degrees to the axis X
                null,
                // Passed through the origin line, at -37 degrees to the axis X
                new SinglePoint(0, 0),
                new SinglePoint(0, 0),
                // Passed through the origin line, at -45 degrees to the axis X
                new SinglePoint(0, 0),
                new SinglePoint(0, 0),
                #endregion
                
                #region [ Axis-parallel second line ]
                // X-axis-parallel line, above X-axis
                new SinglePoint(-5, 1),
                new SinglePoint(-5, 1),
                // X-axis-parallel line, below X-axis
                new SinglePoint(5, -1),
                new SinglePoint(5, -1),
                // Y-axis-parallel line, right-hand from Y-axis
                new SinglePoint(1, -0.2),
                new SinglePoint(1, -0.2),
                // Y-axis-parallel line, left-hand from Y-axis
                new SinglePoint(-1, 0.2),
                new SinglePoint(-1, 0.2),
                #endregion
                
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                new SinglePoint(-4.9999800001, 0.99999600002),
                new SinglePoint(-4.9999800001, 0.99999600002),
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                new SinglePoint(-3, 0.6),
                new SinglePoint(-3, 0.6),
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                new SinglePoint(-1.842105263158, 0.3684210526316),
                new SinglePoint(-1.842105263158, 0.3684210526316),
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                new SinglePoint(-1.666666666667, 0.3333333333333),
                new SinglePoint(-1.666666666667, 0.3333333333333),
                #endregion
            },
            // Passed through the origin first line, at -11 degrees to the axis X
            new SinglePoint?[]
            {
                #region [ Passed through the origin second line ]
                // Passed through the origin line, at -37 degrees to the axis X
                new SinglePoint(0, 0),
                new SinglePoint(0, 0),
                // Passed through the origin line, at -45 degrees to the axis X
                new SinglePoint(0, 0),
                new SinglePoint(0, 0),
                #endregion
                
                #region [ Axis-parallel second line ]
                // X-axis-parallel line, above X-axis
                new SinglePoint(-5, 1),
                new SinglePoint(-5, 1),
                // X-axis-parallel line, below X-axis
                new SinglePoint(5, -1),
                new SinglePoint(5, -1),
                // Y-axis-parallel line, right-hand from Y-axis
                new SinglePoint(1, -0.2),
                new SinglePoint(1, -0.2),
                // Y-axis-parallel line, left-hand from Y-axis
                new SinglePoint(-1, 0.2),
                new SinglePoint(-1, 0.2),
                #endregion
                
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                new SinglePoint(-4.9999800001, 0.99999600002),
                new SinglePoint(-4.9999800001, 0.99999600002),
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                new SinglePoint(-3, 0.6),
                new SinglePoint(-3, 0.6),
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                new SinglePoint(-1.842105263158, 0.3684210526316),
                new SinglePoint(-1.842105263158, 0.3684210526316),
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                new SinglePoint(-1.666666666667, 0.3333333333333),
                new SinglePoint(-1.666666666667, 0.3333333333333),
                #endregion
            },
            // Passed through the origin first line, at -37 degrees to the axis X
            new SinglePoint?[]
            {
                #region [ Passed through the origin second line ]
                // Passed through the origin line, at -37 degrees to the axis X
                null,
                // Passed through the origin line, at -45 degrees to the axis X
                new SinglePoint(0, 0),
                new SinglePoint(0, 0),
                #endregion
                
                #region [ Axis-parallel second line ]
                // X-axis-parallel line, above X-axis
                new SinglePoint(-1.333333333333333, 1),
                new SinglePoint(-1.333333333333333, 1),
                // X-axis-parallel line, below X-axis
                new SinglePoint(1.333333333333333, -1),
                new SinglePoint(1.333333333333333, -1),
                // Y-axis-parallel line, right-hand from Y-axis
                new SinglePoint(1, -0.75),
                new SinglePoint(1, -0.75),
                // Y-axis-parallel line, left-hand from Y-axis
                new SinglePoint(-1, 0.75),
                new SinglePoint(-1, 0.75),
                #endregion
                
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                new SinglePoint(-1.333332888889, 0.9999996666671),
                new SinglePoint(-1.333332888889, 0.9999996666671),
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                new SinglePoint(-1.263157894737, 0.9473684210526),
                new SinglePoint(-1.263157894737, 0.9473684210526),
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                new SinglePoint(-1.166666666667, 0.875),
                new SinglePoint(-1.166666666667, 0.875),
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                new SinglePoint(-1.142857142857, 0.8571428571429),
                new SinglePoint(-1.142857142857, 0.8571428571429),
                #endregion
            },
            // Passed through the origin first line, at -37 degrees to the axis X
            new SinglePoint?[]
            {
                #region [ Passed through the origin second line ]
                // Passed through the origin line, at -45 degrees to the axis X
                new SinglePoint(0, 0),
                new SinglePoint(0, 0),
                #endregion
                
                #region [ Axis-parallel second line ]
                // X-axis-parallel line, above X-axis
                new SinglePoint(-1.333333333333333, 1),
                new SinglePoint(-1.333333333333333, 1),
                // X-axis-parallel line, below X-axis
                new SinglePoint(1.333333333333333, -1),
                new SinglePoint(1.333333333333333, -1),
                // Y-axis-parallel line, right-hand from Y-axis
                new SinglePoint(1, -0.75),
                new SinglePoint(1, -0.75),
                // Y-axis-parallel line, left-hand from Y-axis
                new SinglePoint(-1, 0.75),
                new SinglePoint(-1, 0.75),
                #endregion
                
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                new SinglePoint(-1.333332888889, 0.9999996666671),
                new SinglePoint(-1.333332888889, 0.9999996666671),
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                new SinglePoint(-1.263157894737, 0.9473684210526),
                new SinglePoint(-1.263157894737, 0.9473684210526),
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                new SinglePoint(-1.166666666667, 0.875),
                new SinglePoint(-1.166666666667, 0.875),
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                new SinglePoint(-1.142857142857, 0.8571428571429),
                new SinglePoint(-1.142857142857, 0.8571428571429),
                #endregion
            },
            // Passed through the origin first line, at -45 degrees to the axis X
            new SinglePoint?[]
            {
                #region [ Passed through the origin second line ]
                // Passed through the origin line, at -45 degrees to the axis X
                null,
                #endregion
                
                #region [ Axis-parallel second line ]
                // X-axis-parallel line, above X-axis
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                // X-axis-parallel line, below X-axis
                new SinglePoint(1, -1),
                new SinglePoint(1, -1),
                // Y-axis-parallel line, right-hand from Y-axis
                new SinglePoint(1, -1),
                new SinglePoint(1, -1),
                // Y-axis-parallel line, left-hand from Y-axis
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                #endregion
                
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                #endregion
            },
            // Passed through the origin first line, at -45 degrees to the axis X
            new SinglePoint?[]
            {
                #region [ Axis-parallel second line ]
                // X-axis-parallel line, above X-axis
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                // X-axis-parallel line, below X-axis
                new SinglePoint(1, -1),
                new SinglePoint(1, -1),
                // Y-axis-parallel line, right-hand from Y-axis
                new SinglePoint(1, -1),
                new SinglePoint(1, -1),
                // Y-axis-parallel line, left-hand from Y-axis
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                #endregion
                
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                #endregion
            },
            // X-axis-parallel first line, above X-axis
            new SinglePoint?[]
            {
                #region [ Axis-parallel second line ]
                // X-axis-parallel line, above X-axis
                null,
                // X-axis-parallel line, below X-axis
                null,
                null,
                // Y-axis-parallel line, right-hand from Y-axis
                new SinglePoint(1, 1),
                new SinglePoint(1, 1),
                // Y-axis-parallel line, left-hand from Y-axis
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                #endregion
                
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                #endregion
            },
            // X-axis-parallel first line, above X-axis
            new SinglePoint?[]
            {
                #region [ Axis-parallel second line ]
                // X-axis-parallel line, below X-axis
                null,
                null,
                // Y-axis-parallel line, right-hand from Y-axis
                new SinglePoint(1, 1),
                new SinglePoint(1, 1),
                // Y-axis-parallel line, left-hand from Y-axis
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                #endregion
                
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                #endregion
            },
            // X-axis-parallel first line, below X-axis
            new SinglePoint?[]
            {
                #region [ Axis-parallel second line ]
                // X-axis-parallel line, below X-axis
                null,
                // Y-axis-parallel line, right-hand from Y-axis
                new SinglePoint(1, -1),
                new SinglePoint(1, -1),
                // Y-axis-parallel line, left-hand from Y-axis
                new SinglePoint(-1, -1),
                new SinglePoint(-1, -1),
                #endregion
                
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                new SinglePoint(-2000001, -1),
                new SinglePoint(-2000001, -1),
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                new SinglePoint(-11, -1),
                new SinglePoint(-11, -1),
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                new SinglePoint(-3.666666666667, -1),
                new SinglePoint(-3.666666666667, -1),
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                new SinglePoint(-3, -1),
                new SinglePoint(-3, -1),
                #endregion
            },
            // X-axis-parallel first line, below X-axis
            new SinglePoint?[]
            {
                #region [ Axis-parallel second line ]
                // Y-axis-parallel line, right-hand from Y-axis
                new SinglePoint(1, -1),
                new SinglePoint(1, -1),
                // Y-axis-parallel line, left-hand from Y-axis
                new SinglePoint(-1, -1),
                new SinglePoint(-1, -1),
                #endregion
                
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                new SinglePoint(-2000001, -1),
                new SinglePoint(-2000001, -1),
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                new SinglePoint(-11, -1),
                new SinglePoint(-11, -1),
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                new SinglePoint(-3.666666666667, -1),
                new SinglePoint(-3.666666666667, -1),
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                new SinglePoint(-3, -1),
                new SinglePoint(-3, -1),
                #endregion
            },
            // Y-axis-parallel first line, right-hand from Y-axis
            new SinglePoint?[]
            {
                #region [ Axis-parallel second line ]
                // Y-axis-parallel line, right-hand from Y-axis
                null,
                // Y-axis-parallel line, left-hand from Y-axis
                null,
                null,
                #endregion
                
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                new SinglePoint(1, 1.000002),
                new SinglePoint(1, 1.000002),
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                new SinglePoint(1, 1.4),
                new SinglePoint(1, 1.4),
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                new SinglePoint(1, 2.5),
                new SinglePoint(1, 2.5),
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                new SinglePoint(1, 3),
                new SinglePoint(1, 3),
                #endregion
            },
            // Y-axis-parallel first line, right-hand from Y-axis
            new SinglePoint?[]
            {
                #region [ Axis-parallel second line ]
                // Y-axis-parallel line, left-hand from Y-axis
                null,
                null,
                #endregion
                
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                new SinglePoint(1, 1.000002),
                new SinglePoint(1, 1.000002),
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                new SinglePoint(1, 1.4),
                new SinglePoint(1, 1.4),
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                new SinglePoint(1, 2.5),
                new SinglePoint(1, 2.5),
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                new SinglePoint(1, 3),
                new SinglePoint(1, 3),
                #endregion
            },
            // Y-axis-parallel first line, left-hand from Y-axis
            new SinglePoint?[]
            {
                #region [ Axis-parallel second line ]
                // Y-axis-parallel line, left-hand from Y-axis
                null,
                #endregion
                
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                #endregion
            },
            // Y-axis-parallel first line, left-hand from Y-axis
            new SinglePoint?[]
            {
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                #endregion
            },
            // Not passed through the origin first line, at between 0 and 1 degrees to the axis X, second quarter
            new SinglePoint?[]
            {
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                null,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                #endregion
            },
            // Not passed through the origin first line, at between 0 and 1 degrees to the axis X, second quarter
            new SinglePoint?[]
            {
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                #endregion
            },
            // Not passed through the origin first line, at 11 degrees to the axis X, second quarter
            new SinglePoint?[]
            {
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                null,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                #endregion
            },
            // Not passed through the origin first line, at 11 degrees to the axis X, second quarter
            new SinglePoint?[]
            {
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                #endregion
            },
            // Not passed through the origin first line, at 37 degrees to the axis X, second quarter
            new SinglePoint?[]
            {
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                null,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                #endregion
            },
            // Not passed through the origin first line, at 37 degrees to the axis X, second quarter
            new SinglePoint?[]
            {
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                new SinglePoint(-1, 1),
                new SinglePoint(-1, 1),
                #endregion
            },
            // Not passed through the origin first line, at 45 degrees to the axis X, second quarter
            new SinglePoint?[]
            {
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                null,
                #endregion
            }
        };

        public static bool[][] Result_AreTwoLinesParallel { get; } =
        {
            #region [ Passed through the origin first line ]
            new bool[] { true, false, false, false, false, false, false, false, false, false,
                false, true, true, true, true, false, false, false, false, false,
                false, false, false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false,
                true, true, true, true, false, false, false, false, false, false,
                false, false, false, false, false, false },
            new bool[] { true, false, false, false, false, false, false, false, false, false,
                false, false, false, true, true, true, true, false, false, false,
                false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false,
                false, false, true, true, true, true, false, false, false, false,
                false, false, false, false },
            new bool[] { true, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
                false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
                false, false },
            new bool[] { true, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
                false },
            new bool[] { false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false },
            new bool[] { true, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false },
            new bool[] { true, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false },
            #endregion

            #region [ Axis-parallel first line ]
            new bool[] { true, true, true, false, false, false, false, false, false, false,
                false, false, false, false, false },
            new bool[] { true, true, false, false, false, false, false, false, false, false,
                false, false, false, false },
            new bool[] { true, false, false, false, false, false, false, false, false, false,
                false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false,
                false, false },
            new bool[] { true, true, true, false, false, false, false, false, false, false,
                false },
            new bool[] { true, true, false, false, false, false, false, false, false, false },
            new bool[] { true, false, false, false, false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false },
            #endregion

            #region [ Not passed through the origin first line, second quarter ]
            new bool[] { true, false, false, false, false, false, false },
            new bool[] { false, false, false, false, false, false },
            new bool[] { true, false, false, false, false },
            new bool[] { false, false, false, false },
            new bool[] { true, false, false },
            new bool[] { false, false },
            new bool[] { true },
            #endregion
        };

        public static bool[][] Result_AreTwoLinesMatching { get; } =
        {
            #region [ Passed through the origin first line ]
            new bool[] { true, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false },
            new bool[] { true, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
                false, false, false, false },
            new bool[] { true, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
                false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
                false, false },
            new bool[] { true, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
                false },
            new bool[] { false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false },
            new bool[] { true, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false },
            new bool[] { true, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false },
            #endregion

            #region [ Axis-parallel first line ]
            new bool[] { true, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false,
                false, false, false, false },
            new bool[] { true, false, false, false, false, false, false, false, false, false,
                false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false,
                false, false },
            new bool[] { true, false, false, false, false, false, false, false, false, false,
                false },
            new bool[] { false, false, false, false, false, false, false, false, false, false },
            new bool[] { true, false, false, false, false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false },
            #endregion

            #region [ Not passed through the origin first line, second quarter ]
            new bool[] { true, false, false, false, false, false, false },
            new bool[] { false, false, false, false, false, false },
            new bool[] { true, false, false, false, false },
            new bool[] { false, false, false, false },
            new bool[] { true, false, false },
            new bool[] { false, false },
            new bool[] { true },
            #endregion
        };

        public static bool[][] Result_AreTwoLinesPerpendicular { get; } =
        {
            #region [ Passed through the origin first line ]
            new bool[] { false, true, true, false, false, false, false, false, false, false,
                false, false, false, false, false, true, true, true, true, false,
                false, false, false, false, false, false, false },
            new bool[] { true, true, false, false, false, false, false, false, false, false,
                false, false, false, false, true, true, true, true, false, false,
                false, false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, true,
                true, true, true, false, false, false, false, false, false, false,
                false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, true, true,
                true, true, false, false, false, false, false, false, false, false,
                false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
                false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
                false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false,
                false },
            new bool[] { false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, false, true, true },
            new bool[] { false, false, false, false, false, false, false, false, false, false,
                false, false, false, false, true, true },
            #endregion

            #region [ Axis-parallel first line ]
            new bool[] { false, false, false, true, true, true, true, false, false, false,
                false, false, false, false, false },
            new bool[] { false, false, true, true, true, true, false, false, false, false,
                false, false, false, false },
            new bool[] { false, true, true, true, true, false, false, false, false, false,
                false, false, false },
            new bool[] { true, true, true, true, false, false, false, false, false, false,
                false, false },
            new bool[] { false, false, false, false, false, false, false, false, false, false,
                false },
            new bool[] { false, false, false, false, false, false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false, false },
            new bool[] { false, false, false, false, false, false, false, false },
            #endregion

            #region [ Not passed through the origin first line, second quarter ]
            new bool[] { false, false, false, false, false, false, false },
            new bool[] { false, false, false, false, false, false },
            new bool[] { false, false, false, false, false },
            new bool[] { false, false, false, false },
            new bool[] { false, false, false },
            new bool[] { false, false },
            new bool[] { false },
            #endregion
        };

        public static double[][] Result_BetweenTwoLinesAngles { get; } =
        {
            // On X-axis first line
            new double[]
            {
                #region [ Passed through the origin second line ]
                // On X-axis line
                0,
                // On Y-axis line
                90, 90,
                // Passed through the origin line, at between 0 and -1 degrees to the axis X
                0.000057298, 0.000057298,
                // Passed through the origin line, at -11 degrees to the axis X
                11.309932474, 11.309932474,
                // Passed through the origin line, at -37 degrees to the axis X
                36.869897646, 36.869897646,
                // Passed through the origin line, at -45 degrees to the axis X
                45, 45,
                #endregion
                
                #region [ Axis-parallel second line ]
                // X-axis-parallel line, above X-axis
                0, 0,
                // X-axis-parallel line, below X-axis
                0, 0,
                // Y-axis-parallel line, right-hand from Y-axis
                90, 90,
                // Y-axis-parallel line, left-hand from Y-axis
                90, 90,
                #endregion
                
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                0.000057298, 0.000057298,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                11.309932474, 11.309932474,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                36.869897646, 36.869897646,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                45, 45,
                #endregion
            },
            // On X-axis first line
            new double[]
            {
                #region [ Passed through the origin second line ]
                // On Y-axis line
                90, 90,
                // Passed through the origin line, at between 0 and -1 degrees to the axis X
                0.000057298, 0.000057298,
                // Passed through the origin line, at -11 degrees to the axis X
                11.309932474, 11.309932474,
                // Passed through the origin line, at -37 degrees to the axis X
                36.869897646, 36.869897646,
                // Passed through the origin line, at -45 degrees to the axis X
                45, 45,
                #endregion
                
                #region [ Axis-parallel second line ]
                // X-axis-parallel line, above X-axis
                0, 0,
                // X-axis-parallel line, below X-axis
                0, 0,
                // Y-axis-parallel line, right-hand from Y-axis
                90, 90,
                // Y-axis-parallel line, left-hand from Y-axis
                90, 90,
                #endregion
                
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                0.000057298, 0.000057298,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                11.309932474, 11.309932474,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                36.869897646, 36.869897646,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                45, 45,
                #endregion
            },
            // On Y-axis first line
            new double[]
            {
                #region [ Passed through the origin second line ]
                // On Y-axis line
                0,
                // Passed through the origin line, at between 0 and -1 degrees to the axis X
                89.999942704, 89.999942704,
                // Passed through the origin line, at -11 degrees to the axis X
                78.690067526, 78.690067526,
                // Passed through the origin line, at -37 degrees to the axis X
                53.130102354, 53.130102354,
                // Passed through the origin line, at -45 degrees to the axis X
                45, 45,
                #endregion
                
                #region [ Axis-parallel second line ]
                // X-axis-parallel line, above X-axis
                90, 90,
                // X-axis-parallel line, below X-axis
                90, 90,
                // Y-axis-parallel line, right-hand from Y-axis
                0, 0,
                // Y-axis-parallel line, left-hand from Y-axis
                0, 0,
                #endregion
                
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                89.999942704, 89.999942704,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                78.690067526, 78.690067526,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                53.130102354, 53.130102354,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                45, 45,
                #endregion
            },
            // On Y-axis first line
            new double[]
            {
                #region [ Passed through the origin second line ]
                // Passed through the origin line, at between 0 and -1 degrees to the axis X
                89.999942704, 89.999942704,
                // Passed through the origin line, at -11 degrees to the axis X
                78.690067526, 78.690067526,
                // Passed through the origin line, at -37 degrees to the axis X
                53.130102354, 53.130102354,
                // Passed through the origin line, at -45 degrees to the axis X
                45, 45,
                #endregion
                
                #region [ Axis-parallel second line ]
                // X-axis-parallel line, above X-axis
                90, 90,
                // X-axis-parallel line, below X-axis
                90, 90,
                // Y-axis-parallel line, right-hand from Y-axis
                0, 0,
                // Y-axis-parallel line, left-hand from Y-axis
                0, 0,
                #endregion
                
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                89.999942704, 89.999942704,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                78.690067526, 78.690067526,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                53.130102354, 53.130102354,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                45, 45,
                #endregion
            },
            // Passed through the origin first line, at between 0 and -1 degrees to the axis X
            new double[]
            {
                #region [ Passed through the origin second line ]
                // Passed through the origin line, at between 0 and -1 degrees to the axis X
                0,
                // Passed through the origin line, at -11 degrees to the axis X
                11.309875178, 11.309875178,
                // Passed through the origin line, at -37 degrees to the axis X
                36.86984035, 36.86984035,
                // Passed through the origin line, at -45 degrees to the axis X
                44.999942704, 44.999942704,
                #endregion
                
                #region [ Axis-parallel second line ]
                // X-axis-parallel line, above X-axis
                0.000057298, 0.000057298,
                // X-axis-parallel line, below X-axis
                0.000057298, 0.000057298,
                // Y-axis-parallel line, right-hand from Y-axis
                89.999942704, 89.999942704,
                // Y-axis-parallel line, left-hand from Y-axis
                89.999942704, 89.999942704,
                #endregion
                
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                0.00011459, 0.00011459,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                11.30998977, 11.30998977,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                36.869954942, 36.869954942,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                45.000057296, 45.000057296,
                #endregion
            },
            // Passed through the origin first line, at between 0 and -1 degrees to the axis X
            new double[]
            {
                #region [ Passed through the origin second line ]
                // Passed through the origin line, at -11 degrees to the axis X
                11.309875178, 11.309875178,
                // Passed through the origin line, at -37 degrees to the axis X
                36.86984035, 36.86984035,
                // Passed through the origin line, at -45 degrees to the axis X
                44.999942704, 44.999942704,
                #endregion
                
                #region [ Axis-parallel second line ]
                // X-axis-parallel line, above X-axis
                0.000057298, 0.000057298,
                // X-axis-parallel line, below X-axis
                0.000057298, 0.000057298,
                // Y-axis-parallel line, right-hand from Y-axis
                89.999942704, 89.999942704,
                // Y-axis-parallel line, left-hand from Y-axis
                89.999942704, 89.999942704,
                #endregion
                
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                0.00011459, 0.00011459,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                11.30998977, 11.30998977,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                36.869954942, 36.869954942,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                45.000057296, 45.000057296,
                #endregion
            },
            // Passed through the origin first line, at -11 degrees to the axis X
            new double[]
            {
                #region [ Passed through the origin second line ]
                // Passed through the origin line, at -11 degrees to the axis X
                0,
                // Passed through the origin line, at -37 degrees to the axis X
                25.559965172, 25.559965172,
                // Passed through the origin line, at -45 degrees to the axis X
                33.690067526, 33.690067526,
                #endregion
                
                #region [ Axis-parallel second line ]
                // X-axis-parallel line, above X-axis
                11.309932474, 11.309932474,
                // X-axis-parallel line, below X-axis
                11.309932474, 11.309932474,
                // Y-axis-parallel line, right-hand from Y-axis
                78.690067526, 78.690067526,
                // Y-axis-parallel line, left-hand from Y-axis
                78.690067526, 78.690067526,
                #endregion
                
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                11.30998977, 11.30998977,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                22.619864948, 22.619864948,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                48.17983012, 48.17983012,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                56.309932474, 56.309932474,
                #endregion
            },
            // Passed through the origin first line, at -11 degrees to the axis X
            new double[]
            {
                #region [ Passed through the origin second line ]
                // Passed through the origin line, at -37 degrees to the axis X
                25.559965172, 25.559965172,
                // Passed through the origin line, at -45 degrees to the axis X
                33.690067526, 33.690067526,
                #endregion
                
                #region [ Axis-parallel second line ]
                // X-axis-parallel line, above X-axis
                11.309932474, 11.309932474,
                // X-axis-parallel line, below X-axis
                11.309932474, 11.309932474,
                // Y-axis-parallel line, right-hand from Y-axis
                78.690067526, 78.690067526,
                // Y-axis-parallel line, left-hand from Y-axis
                78.690067526, 78.690067526,
                #endregion
                
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                11.30998977, 11.30998977,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                22.619864948, 22.619864948,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                48.17983012, 48.17983012,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                56.309932474, 56.309932474,
                #endregion
            },
            // Passed through the origin first line, at -37 degrees to the axis X
            new double[]
            {
                #region [ Passed through the origin second line ]
                // Passed through the origin line, at -37 degrees to the axis X
                0,
                // Passed through the origin line, at -45 degrees to the axis X
                8.130102354, 8.130102354,
                #endregion
                
                #region [ Axis-parallel second line ]
                // X-axis-parallel line, above X-axis
                36.869897646, 36.869897646,
                // X-axis-parallel line, below X-axis
                36.869897646, 36.869897646,
                // Y-axis-parallel line, right-hand from Y-axis
                53.130102354, 53.130102354,
                // Y-axis-parallel line, left-hand from Y-axis
                53.130102354, 53.130102354,
                #endregion
                
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                36.869954942, 36.869954942,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                48.17983012, 48.17983012,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                73.739795292, 73.739795292,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                81.869897646, 81.869897646,
                #endregion
            },
            // Passed through the origin first line, at -37 degrees to the axis X
            new double[]
            {
                #region [ Passed through the origin second line ]
                // Passed through the origin line, at -45 degrees to the axis X
                8.130102354, 8.130102354,
                #endregion
                
                #region [ Axis-parallel second line ]
                // X-axis-parallel line, above X-axis
                36.869897646, 36.869897646,
                // X-axis-parallel line, below X-axis
                36.869897646, 36.869897646,
                // Y-axis-parallel line, right-hand from Y-axis
                53.130102354, 53.130102354,
                // Y-axis-parallel line, left-hand from Y-axis
                53.130102354, 53.130102354,
                #endregion
                
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                36.869954942, 36.869954942,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                48.17983012, 48.17983012,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                73.739795292, 73.739795292,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                81.869897646, 81.869897646,
                #endregion
            },
            // Passed through the origin first line, at -45 degrees to the axis X
            new double[]
            {
                #region [ Passed through the origin second line ]
                // Passed through the origin line, at -45 degrees to the axis X
                0,
                #endregion
                
                #region [ Axis-parallel second line ]
                // X-axis-parallel line, above X-axis
                45, 45,
                // X-axis-parallel line, below X-axis
                45, 45,
                // Y-axis-parallel line, right-hand from Y-axis
                45, 45,
                // Y-axis-parallel line, left-hand from Y-axis
                45, 45,
                #endregion
                
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                45.000057296, 45.000057296,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                56.309932474, 56.309932474,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                81.869897646, 81.869897646,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                90, 90,
                #endregion
            },
            // Passed through the origin first line, at -45 degrees to the axis X
            new double[]
            {
                #region [ Axis-parallel second line ]
                // X-axis-parallel line, above X-axis
                45, 45,
                // X-axis-parallel line, below X-axis
                45, 45,
                // Y-axis-parallel line, right-hand from Y-axis
                45, 45,
                // Y-axis-parallel line, left-hand from Y-axis
                45, 45,
                #endregion
                
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                45.000057296, 45.000057296,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                56.309932474, 56.309932474,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                81.869897646, 81.869897646,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                90, 90,
                #endregion
            },
            // X-axis-parallel first line, above X-axis
            new double[]
            {
                #region [ Axis-parallel second line ]
                // X-axis-parallel line, above X-axis
                0,
                // X-axis-parallel line, below X-axis
                0, 0,
                // Y-axis-parallel line, right-hand from Y-axis
                90, 90,
                // Y-axis-parallel line, left-hand from Y-axis
                90, 90,
                #endregion
                
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                0.000057298, 0.000057298,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                11.309932474, 11.309932474,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                36.869897646, 36.869897646,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                45, 45,
                #endregion
            },
            // X-axis-parallel first line, above X-axis
            new double[]
            {
                #region [ Axis-parallel second line ]
                // X-axis-parallel line, below X-axis
                0, 0,
                // Y-axis-parallel line, right-hand from Y-axis
                90, 90,
                // Y-axis-parallel line, left-hand from Y-axis
                90, 90,
                #endregion
                
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                0.000057298, 0.000057298,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                11.309932474, 11.309932474,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                36.869897646, 36.869897646,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                45, 45,
                #endregion
            },
            // X-axis-parallel first line, below X-axis
            new double[]
            {
                #region [ Axis-parallel second line ]
                // X-axis-parallel line, below X-axis
                0,
                // Y-axis-parallel line, right-hand from Y-axis
                90, 90,
                // Y-axis-parallel line, left-hand from Y-axis
                90, 90,
                #endregion
                
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                0.000057298, 0.000057298,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                11.309932474, 11.309932474,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                36.869897646, 36.869897646,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                45, 45,
                #endregion
            },
            // X-axis-parallel first line, below X-axis
            new double[]
            {
                #region [ Axis-parallel second line ]
                // Y-axis-parallel line, right-hand from Y-axis
                90, 90,
                // Y-axis-parallel line, left-hand from Y-axis
                90, 90,
                #endregion
                
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                0.000057298, 0.000057298,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                11.309932474, 11.309932474,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                36.869897646, 36.869897646,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                45, 45,
                #endregion
            },
            // Y-axis-parallel first line, right-hand from Y-axis
            new double[]
            {
                #region [ Axis-parallel second line ]
                // Y-axis-parallel line, right-hand from Y-axis
                0,
                // Y-axis-parallel line, left-hand from Y-axis
                0, 0,
                #endregion
                
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                89.999942704, 89.999942704,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                78.690067526, 78.690067526,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                53.130102354, 53.130102354,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                45, 45,
                #endregion
            },
            // Y-axis-parallel first line, right-hand from Y-axis
            new double[]
            {
                #region [ Axis-parallel second line ]
                // Y-axis-parallel line, left-hand from Y-axis
                0, 0,
                #endregion
                
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                89.999942704, 89.999942704,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                78.690067526, 78.690067526,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                53.130102354, 53.130102354,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                45, 45,
                #endregion
            },
            // Y-axis-parallel first line, left-hand from Y-axis
            new double[]
            {
                #region [ Axis-parallel second line ]
                // Y-axis-parallel line, left-hand from Y-axis
                0,
                #endregion
                
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                89.999942704, 89.999942704,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                78.690067526, 78.690067526,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                53.130102354, 53.130102354,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                45, 45,
                #endregion
            },
            // Y-axis-parallel first line, left-hand from Y-axis
            new double[]
            {
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                89.999942704, 89.999942704,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                78.690067526, 78.690067526,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                53.130102354, 53.130102354,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                45, 45,
                #endregion
            },
            // Not passed through the origin first line, at between 0 and 1 degrees to the axis X, second quarter
            new double[]
            {
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at between 0 and 1 degrees to the axis X, second quarter
                0,
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                11.309875178, 11.309875178,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                36.86984035, 36.86984035,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                44.999942704, 44.999942704,
                #endregion
            },
            // Not passed through the origin first line, at between 0 and 1 degrees to the axis X, second quarter
            new double[]
            {
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                11.309875178, 11.309875178,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                36.86984035, 36.86984035,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                44.999942704, 44.999942704,
                #endregion
            },
            // Not passed through the origin first line, at 11 degrees to the axis X, second quarter
            new double[]
            {
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at 11 degrees to the axis X, second quarter
                0,
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                25.559965172, 25.559965172,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                33.690067526, 33.690067526,
                #endregion
            },
            // Not passed through the origin first line, at 11 degrees to the axis X, second quarter
            new double[]
            {
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                25.559965172, 25.559965172,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                33.690067526, 33.690067526,
                #endregion
            },
            // Not passed through the origin first line, at 37 degrees to the axis X, second quarter
            new double[]
            {
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at 37 degrees to the axis X, second quarter
                0,
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                8.130102354, 8.130102354,
                #endregion            
            },
            // Not passed through the origin first line, at 37 degrees to the axis X, second quarter
            new double[]
            {
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                8.130102354, 8.130102354,
                #endregion
            },
            // Not passed through the origin first line, at 45 degrees to the axis X, second quarter
            new double[]
            {
                #region [ Not passed through the origin second line, second quarter ]
                // Not passed through the origin line, at 45 degrees to the axis X, second quarter
                0,
                #endregion
            },
        };

        public struct LineSegment
        {
            public double X1 { get; }
            public double Y1 { get; }
            public double X2 { get; }
            public double Y2 { get; }

            public LineSegment(double x1, double y1, double x2, double y2)
            {
                X1 = x1;
                Y1 = y1;
                X2 = x2;
                Y2 = y2;
            }
        }

        public struct LineABCCoefficients
        {
            public double A { get; }
            public double B { get; }
            public double C { get; }

            public LineABCCoefficients(double a, double b, double c)
            {
                A = a;
                B = b;
                C = c;
            }
        }

        public struct SinglePoint
        {
            public double X { get; }
            public double Y { get; }

            public SinglePoint(double x, double y)
            {
                X = x;
                Y = y;
            }
        }
    }
}
