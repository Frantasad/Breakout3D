using System;
using Breakout3D.Framework;

namespace Breakout3D.Libraries
{
    public class Ray
    {
        public Vec3 Origin { get; }
        public Vec3 Direction { get; }

        public Ray(Vec3 origin, Vec3 direction)
        {
            Origin = origin;
            Direction = direction;
        }

        public float DistanceTo(Ray other)
        {
            return Distance(this, other);
        }

        public float DistanceTo(Vec3 point)
        {
            var vector = point - Origin;
            if (vector.Normalized.Dot(Direction) < 0) return vector.Magnitude;

            return Direction.Cross(vector).Magnitude / Direction.Magnitude;
        }
        
        public static float Distance(Ray first, Ray second)
        {
            var firstDirSq = first.Direction.Dot(first.Direction);
            var secondDirSq = second.Direction.Dot(second.Direction);
            var firstSecondDirMult = first.Direction.Dot(second.Direction);

            var firstSecondVec = first.Origin - second.Origin;
            var secondFirstVec = second.Origin - first.Origin;

            var div = firstDirSq * secondDirSq - firstSecondDirMult * firstSecondDirMult;
            if (Math.Abs(div) < MathUtils.Epsilon)
            {
                return Math.Min(first.DistanceTo(second.Origin), second.DistanceTo(first.Origin));
            }
            
            var s = (secondFirstVec.Dot(first.Direction) * secondDirSq + firstSecondVec.Dot(second.Direction) * firstSecondDirMult) / div;
            var t = (firstSecondVec.Dot(second.Direction) * firstDirSq + secondFirstVec.Dot(first.Direction) * firstSecondDirMult) / div;

            s = Math.Max(s, 0);
            t = Math.Max(t, 0);

            var a = first.Origin + s * first.Direction;
            var b = second.Origin + t * second.Direction;

            return a.DistanceTo(b);
        }
    }
}