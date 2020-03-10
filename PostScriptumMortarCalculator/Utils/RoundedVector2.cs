using System;

namespace PostScriptumMortarCalculator.Utils
{
    public struct RoundedVector2
    {
        private const double c_PRECISION = 0.001d;
        private const int c_DECIMALS = 2;

        public double X { get; }
        public double Y { get; }


        public RoundedVector2(double x, double y)
        {
            X = Math.Round(x, c_DECIMALS);
            Y = Math.Round(y, c_DECIMALS);
        }

        public static double Distance(RoundedVector2 v1, RoundedVector2 v2)
        {
            var dx = v1.X - v2.X;
            var dy = v1.Y - v2.Y;
            return Math.Round(Math.Sqrt((dx * dx) + (dy * dy)), c_DECIMALS);
        }

        public static double Angle(RoundedVector2 v1, RoundedVector2 v2)
        {
            var dx = v1.X - v2.X;
            var dy = v1.Y - v2.Y;
            var radian = Math.Atan2(dy, dx);
            var angle = (radian * (180 / Math.PI) + 270) % 360;

            return Math.Round(angle, c_DECIMALS);
        }

        public static RoundedVector2 LerpBetween(RoundedVector2 v1, RoundedVector2 v2, double perc)
        {
            var lx = v1.X + (v2.X - v1.X) * perc;
            var ly = v1.Y + (v2.Y - v1.Y) * perc;
            return new RoundedVector2(lx, ly);
        }

        public static RoundedVector2 operator /(RoundedVector2 v1, RoundedVector2 v2) =>
            new RoundedVector2(v1.X / v2.X, v1.Y / v2.Y);

        public static RoundedVector2 operator /(RoundedVector2 v, double by) =>
            new RoundedVector2(v.X / by, v.Y / by);
        
        public static RoundedVector2 operator -(RoundedVector2 v) =>
            new RoundedVector2(-v.X, -v.Y);
        
        public static RoundedVector2 operator *(RoundedVector2 v, double by) =>
            new RoundedVector2(v.X * by, v.Y * by);

        public static bool operator ==(RoundedVector2 v1, RoundedVector2 v2) =>
            v1.Equals(v2);

        public static bool operator !=(RoundedVector2 v1, RoundedVector2 v2) =>
            !(v1 == v2);

        public static bool operator ==(RoundedVector2 v1, object obj) =>
            v1.Equals(obj);

        public static bool operator !=(RoundedVector2 v1, object obj) => 
            !(v1 == obj);

        public override bool Equals(object obj)
        {
            if (obj is RoundedVector2 other)
                return Math.Abs(this.X - other.X) < c_PRECISION && Math.Abs(this.Y - other.Y) < c_PRECISION;
            return false;
        }

        public bool Equals(RoundedVector2 other)
        {
            return Math.Abs(this.X - other.X) < c_PRECISION && Math.Abs(this.Y - other.Y) < c_PRECISION;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }
    }
}