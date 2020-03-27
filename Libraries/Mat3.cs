using System;
using Breakout3D.Framework;

namespace Breakout3D.Libraries
{
    // Matrix 3x3 implementation with padding to 3x4 for pushing to GL buffer
    public struct Mat3
    {
        public static readonly Mat3 Zero = new Mat3(0);

        public static readonly Mat3 Identity = new Mat3
        {
            [0, 0] = 1,
            [1, 1] = 1,
            [2, 2] = 1
        };
        
        // pads are only for opengl memory padding;
        public float x00; public float x10; public float x20; private float pad1;
        public float x01; public float x11; public float x21; private float pad2;
        public float x02; public float x12; public float x22; private float pad3;
        
        public float this[int i]
        {
            get
            {
                return i switch
                {
                    0 => x00, 1 => x01, 2 => x02,
                    3 => x10, 4 => x11, 5 => x12,
                    6 => x20, 7 => x21, 8 => x22,
                    _ => throw new IndexOutOfRangeException()
                };
            }
            set 
            {
                switch (i)
                {
                    case 0: x00 = value; break; case 1: x01 = value; break; case 2: x02 = value; break;
                    case 3: x10 = value; break; case 4: x11 = value; break; case 5: x12 = value; break; 
                    case 6: x20 = value; break; case 7: x21 = value; break; case 8: x22 = value; break;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }
        public float this[int i, int j]
        {
            get => this[i * 3 + j];
            set => this[i * 3 + j] = value;
        }

        public float Determinant =>
            this[0, 0] * (this[1, 1] * this[2, 2] - this[1, 2] * this[2, 1]) + 
            this[0, 1] * -(this[1, 0] * this[2, 2] - this[1, 2] * this[2, 0]) + 
            this[0, 2] * (this[1, 0] * this[2, 1] - this[1, 1] * this[2, 0]);

        public Vec3 Diagonal => new Vec3(x00, x11, x22);
        
        public Mat3(Vec3 diagonal)
        {
            x01 = x02 = x10 = x12 = x20 = x21 = 0;
            x00 = diagonal.X;
            x11 = diagonal.Y;
            x22 = diagonal.Z;
            pad1 = pad2 = pad3 = 0;
        }
        
        public Mat3(float x00, float x01, float x02, 
            float x10, float x11, float x12, 
            float x20, float x21, float x22)
        {
            this.x00 = x00; this.x01 = x01; this.x02 = x02;
            this.x10 = x10; this.x11 = x11; this.x12 = x12;
            this.x20 = x20; this.x21 = x21; this.x22 = x22;
            pad1 = pad2 = pad3 = 0;
            //pad1 = pad2 = pad3 = 0;
        }

        public Mat3(float value)
        {
            x00 = x01 = x02 = x10 = x11 = x12 = x20 = x21 = x22 = value;
            pad1 = pad2 = pad3 = 0;
            //pad1 = pad2 = pad3 = 0;
        }
        
        public Mat3(Mat4 matrix)
        {
            x00 = matrix.x00; x01 = matrix.x01; x02 = matrix.x02;
            x10 = matrix.x10; x11 = matrix.x11; x12 = matrix.x12;
            x20 = matrix.x20; x21 = matrix.x21; x22 = matrix.x22;
            pad3 = pad2 = pad1 = 0;
            //pad1 = pad2 = pad3 = 0;
        }
        
        public Vec3 GetRow(int row)
        {
            return new Vec3(this[row,0], this[row,1], this[row,2]);
        }
        
        public Vec3 GetCol(int col)
        {
            return new Vec3(this[0, col], this[1, col], this[2, col]);
        }

        public static Mat3 Rotation(Vec3 eulerAngles)
        {
            var radians = new Vec3(
                MathUtils.ToRadians(eulerAngles.X), 
                MathUtils.ToRadians(eulerAngles.Y), 
                MathUtils.ToRadians(eulerAngles.Z));
            var cosY = (float) Math.Cos(radians.Y);
            var sinY = (float) Math.Sin(radians.Y);
            var cosP = (float) Math.Cos(radians.X);
            var sinP = (float) Math.Sin(radians.X);
            var cosR = (float) Math.Cos(radians.Z);
            var sinR = (float) Math.Sin(radians.Z);

            return new Mat3
            {
                x00 = cosY * cosR + sinY * sinP * sinR,
                x01 = cosP * sinR,
                x02 = sinR * cosY * sinP - sinY * cosR,
                x10 = cosR * sinY * sinP - sinR * cosY,
                x11 = cosR * cosP,
                x12 = sinY * sinR + cosR * cosY * sinP,
                x20 = cosP * sinY,
                x21 = -sinP,
                x22 = cosP * cosY
            };
        }
        
        public static Vec3 EulerAngles(Mat3 matrix)
        {
            float x, y, z;
            x = (float) Math.Asin(-matrix[2, 1]);
            if (Math.Cos(x) > 0.0001) 
            {
                y = (float) Math.Atan2(matrix[2,0], matrix[2,2]);
                z = (float) Math.Atan2(matrix[0,1], matrix[1,1]);
            }
            else
            {
                y = 0.0f; 
                z = (float) Math.Atan2(-matrix[1,0], matrix[0,0]);
            }
            return new Vec3(MathUtils.ToDegrees(x), MathUtils.ToDegrees(y), MathUtils.ToDegrees(z));
        }

        public static Mat3 Scale(Vec3 scale)
        {
            return new Mat3(scale);
        }


        public static Mat3 operator +(Mat3 first, Mat3 second)
        {
            var result = new Mat3(); 
            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    result[x, y] = first[x,y] + second[x,y];
                }
            }

            return result;
        }
        
        public static Mat3 operator -(Mat3 first, Mat3 second)
        {
            var result = new Mat3(); 
            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    result[x, y] = first[x,y] - second[x,y];
                }
            }

            return result;
        }
        
        public static Mat3 operator *(Mat3 matrix, Mat3 second)
        {
            var result = new Mat3();
            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    var sum = 0f;
                    var firstRow = matrix.GetRow(x).ToArray();
                    var secondCol = second.GetCol(y).ToArray();
                    for (var i = 0; i < 3; i++)
                    {
                        sum += firstRow[i] * secondCol[i];
                    }
                    result[x, y] = sum;
                }
            }
            return result;
        }
        
        public static Mat3 operator *(Mat3 matrix, float scalar)
        {
            return new Mat3(
                matrix[0, 0] * scalar, matrix[0, 1] * scalar, matrix[0, 2] * scalar,
                matrix[1, 0] * scalar, matrix[1, 1] * scalar, matrix[1, 2] * scalar,
                matrix[2, 0] * scalar, matrix[2, 1] * scalar, matrix[2, 2] * scalar);
        }
        
        public static Mat3 operator *(float scalar, Mat3 matrix)
        {
            return matrix*scalar;
        }

        public static Vec3 operator *(Mat3 matrix, Vec3 vector)
        {
            return new Vec3(
                matrix[0,0] * vector.X + matrix[1,0] * vector.Y + matrix[2,0] * vector.Z,
                matrix[0,1] * vector.X + matrix[1,1] * vector.Y + matrix[2,1] * vector.Z,
                matrix[0,2] * vector.X + matrix[1,2] * vector.Y + matrix[2,2] * vector.Z);
        }
        
        public Mat3 Transpose =>
            new Mat3(
                this[0, 0], this[1, 0], this[2, 0],
                this[0, 1], this[1, 1], this[2, 1],
                this[0, 2], this[1, 2], this[2, 2]);

        public Mat3 Inverse
        {
            get
            {
                var a = this[0, 0]; var b = this[0, 1]; var c = this[0, 2];
                var d = this[1, 0]; var e = this[1, 1]; var f = this[1, 2];
                var g = this[2, 0]; var h = this[2, 1]; var i = this[2, 2];
                var resultMat = new Mat3(
                    (e * i - f * h), -(b * i - c * h), (b * f - c * e), 
                    -(d * i - f * g), (a * i - c * g), -(a * f - c * d),
                    (d * h - e * g), -(a * h - b * g), (a * e - b * d));
                return (1 / Determinant) * resultMat;
            }
        }

        public override string ToString()
        {
            return $"Matrix3:\n[{this[0,0]}, {this[0,1]}, {this[0,2]} " +
                   $"\n {this[1,0]}, {this[1,1]}, {this[1,2]} " +
                   $"\n {this[2,0]}, {this[2,1]}, {this[2,2]}]";
        }
    }
}