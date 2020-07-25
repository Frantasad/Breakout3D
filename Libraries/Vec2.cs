using System;

namespace Breakout3D.Libraries
{
    public struct Vec2
    {
        private const float Epsilon = (float) 10e-5;
        
        public static readonly Vec2 Up = new Vec2(0, 1);
        public static readonly Vec2 Right = new Vec2(1, 0);
        
        public readonly float X;
        public readonly float Y;
        
        public Vec2(float x, float y)
        {
            X = x;
            Y = y;
        }
        
        public Vec2(float x)
        {
            X = x;
            Y = x;
        }
        
        public static Vec2 operator +(Vec2 first, Vec2 second)
        {
            return new Vec2(first.X + second.X, first.Y + second.Y);
        }

        public static Vec2 operator -(Vec2 first, Vec2 second)
        {
            return new Vec2(first.X - second.X, first.Y - second.Y);
        }

        public static Vec2 operator -(Vec2 first)
        {
            return new Vec2(-first.X, -first.Y);
        }
        
        public static Vec2 operator *(Vec2 vector, float scalar)
        {
            return new Vec2(vector.X * scalar, vector.Y * scalar);
        }

        public static Vec2 operator *(float scalar, Vec2 vector)
        {
            return vector * scalar;
        }
        
        public static bool operator==(Vec2 first, Vec2 second)
        {
            return Math.Abs(first.X - second.X) < Epsilon &&
                   Math.Abs(first.Y - second.Y) < Epsilon;
        }
                
        public static bool operator!=(Vec2 first, Vec2 second)
        {
            return !(first == second);
        }
        
        public bool Equals(Vec2 other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        public override bool Equals(object? obj)
        {
            return obj is Vec2 other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X.GetHashCode() * 397) ^ Y.GetHashCode();
            }
        }

        public readonly float[] ToArray()
        {
            return new[] {X, Y};
        }
        
        public override string ToString()
        {
            return $"Vector2: [{X}, {Y}]";
        }
    }
}