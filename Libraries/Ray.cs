using System;

namespace Breakout3D.Libraries
{
    public class Ray
    {
        private const float Epsilon = (float) 10e-6;

    public Vec3 Origin { get; }
        public Vec3 Direction { get; }

        public Ray(Vec3 origin, Vec3 direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public float DistanceTo(Ray ray)
        {
            return 0;
        }

        public float DistanceTo(Vec3 point)
        {
            return Direction.Cross(point - Origin).Magnitude;
        }
    }
}