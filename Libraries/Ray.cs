using System;

namespace Breakout3D.Libraries
{
    public class Ray
    {
        public Vec3 Point { get; }
        public Vec3 Direction { get; }

        public Ray(Vec3 point, Vec3 direction)
        {
            Point = point;
            Direction = direction;
        }

        public float DistanceTo(Ray ray)
        {
            throw new NotImplementedException();
        }
        
        public float DistanceTo(Vec3 point)
        {
            throw new NotImplementedException();
        }
        
    }
}