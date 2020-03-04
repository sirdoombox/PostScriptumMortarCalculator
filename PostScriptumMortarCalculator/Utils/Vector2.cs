using System;

namespace PostScriptumMortarCalculator.Utils
{
    public struct Vector2
    {
        public double X { get; }
        public double Y { get; }

        public Vector2(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static double Distance(Vector2 v1, Vector2 v2)
        {
            var dx = v1.X - v2.X;
            var dy = v1.Y - v2.Y;
            return Math.Sqrt((dx*dx) + (dy * dy));
        }
    }
}