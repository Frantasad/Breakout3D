using System;
using Breakout3D.Framework;

namespace Breakout3D.Libraries
{
    public struct Vec3
    {
        private const float Epsilon = 0.00001f;
        
        public static readonly Vec3 Zero = new Vec3(0, 0, 0);
        public static readonly Vec3 Unit = new Vec3(1, 1, 1);
        public static readonly Vec3 Up = new Vec3(0, 1, 0);
        public static readonly Vec3 Forward = new Vec3(0, 0, 1);
        public static readonly Vec3 Right = new Vec3(1, 0, 0);

        public readonly float X;
        public readonly float Y;
        public readonly float Z;

        public float Magnitude => (float) Math.Sqrt(X*X + Y*Y + Z*Z);
        
        public Vec3 Normalized
        {
            get
            {
                var mag = Magnitude;
                if (mag > Epsilon)
                    return this / mag;
                return Zero; 
            }
        }

        public Vec3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        
        public Vec3(float x)
        {
            X = x;
            Y = x;
            Z = x;
        }
        
        public Vec3 Cross(Vec3 other)
        {
            return Cross(this, other);
        }
        
        public static Vec3 Cross(Vec3 first, Vec3 second)
        {
            return new Vec3(
                first.Y * second.Z - first.Z * second.Y,
                first.Z * second.X - first.X * second.Z,
                first.X * second.Y - first.Y * second.X);
        }
        
        public float Dot(Vec3 other)
        {
            return Vec3.Dot(this, other);
        }
        
        public static float Dot(Vec3 first, Vec3 second)
        {
            return first.X * second.X + first.Y * second.Y + first.Z * second.Z;
        }

        public static Vec3 Normal(Vec3 v0, Vec3 v1, Vec3 v2)
        {
            var a = v1 - v0;
            var b = v2 - v0;
            return Cross(a, b).Normalized;
        }

        public static Vec3 operator +(Vec3 first, Vec3 second)
        {
            return new Vec3(first.X + second.X, first.Y + second.Y, first.Z + second.Z);
        }
        
        public static Vec3 operator %(Vec3 first, float scalar)
        {
            return new Vec3(first.X % scalar, first.Y % scalar, first.Z % scalar);
        }

        public static Vec3 operator -(Vec3 first, Vec3 second)
        {
            return new Vec3(first.X - second.X, first.Y - second.Y, first.Z - second.Z);
        }

        public static Vec3 operator -(Vec3 first)
        {
            return new Vec3(-first.X, -first.Y, -first.Z);
        }

        public static Vec3 operator *(Vec3 vector, float scalar)
        {
            return new Vec3(vector.X * scalar, vector.Y * scalar, vector.Z * scalar);
        }

        public static Vec3 operator *(float scalar, Vec3 vector)
        {
            return vector * scalar;
        }

        public static Vec3 operator /(Vec3 vector, float scalar)
        {
            return new Vec3(vector.X / scalar, vector.Y / scalar, vector.Z / scalar);
        }
        
        public static bool operator==(Vec3 first, Vec3 second)
        {
            return Math.Abs(first.X - second.X) < Epsilon && 
                   Math.Abs(first.Y - second.Y) < Epsilon && 
                   Math.Abs(first.Z - second.Z) < Epsilon;
        }
        
        public static bool operator!=(Vec3 first, Vec3 second)
        {
            return !(first == second);
        }
        
        public bool Equals(Vec3 other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Vec3) obj);
        }

        public override int GetHashCode()
        {
            var hashCode = X.GetHashCode();
            hashCode = (hashCode * 397) ^ Y.GetHashCode();
            hashCode = (hashCode * 397) ^ Z.GetHashCode();
            return hashCode;
        }

        public readonly float[] ToArray()
        {
            return new[] {X, Y, Z};
        }
        
        public override string ToString()
        {
            return $"Vector3: [{X}, {Y}, {Z}]";
        }
    }
}