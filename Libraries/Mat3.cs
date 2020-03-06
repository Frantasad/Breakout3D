using System;

namespace Breakout3D.Libraries
{
    // Matrix 3x3 implementation with padding to 3x4 for pushing to GL buffer
    public struct Mat3
    {
        public static readonly Mat3 Zero = new Mat3(0);

        public static readonly Mat3 Identity = new Mat3(
            1, 0, 0,
            0, 1, 0,
            0, 0, 1);
        
        public float x11;
        public float x12;
        public float x13;
        private float pad1; // only for memory padding;
        public float x21;
        public float x22;
        public float x23;
        private float pad2; // only for memory padding;
        public float x31;
        public float x32;
        public float x33;
        private float pad3; // only for memory padding;
        
        public float this[int i]
        {
            get
            {
                return i switch
                {
                    0 => x11,
                    1 => x12,
                    2 => x13,
                    3 => x21,
                    4 => x22,
                    5 => x23,
                    6 => x31,
                    7 => x32,
                    8 => x33,
                    _ => throw new IndexOutOfRangeException()
                };
            }
            set 
            {
                switch (i)
                {
                    case 0: x11 = value; break;
                    case 1: x12 = value; break;
                    case 2: x13 = value; break;
                    case 3: x21 = value; break;
                    case 4: x22 = value; break;
                    case 5: x23 = value; break;
                    case 6: x31 = value; break;
                    case 7: x32 = value; break;
                    case 8: x33 = value; break;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }
        public float this[int i, int j]
        {
            get => this[i + j * 3];
            set => this[i + j * 3] = value;
        }

        public Mat3 Transposed => Transpose(this);
        public Mat3 Inverted => Inverse(this);

        public float Determinant =>
            this[0, 0] * (this[1, 1] * this[2, 2] - this[1, 2] * this[2, 1]) + 
            this[0, 1] * -(this[1, 0] * this[2, 2] - this[1, 2] * this[2, 0]) + 
            this[0, 2] * (this[1, 0] * this[2, 1] - this[1, 1] * this[2, 0]);

        public Vec3 Diagonal => new Vec3(x11, x22, x33);
        
        private Mat3(float[,] mat)
        {
            x11 = x12 = x13 = x21 = x22 = x23 = x31 = x32 = x33 = 0;
            pad1 = pad2 = pad3 = 0;
            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    this[x, y] = mat[x,y];
                }
            }
        }

        public static Mat3 GetRotationMatrix(Vec3 rotationAxis, float angle)
        {
            rotationAxis = rotationAxis.Normalized;

            var c = (float) Math.Cos(angle);
            var s = (float) Math.Sin(angle);
            var t = 1 - c;
            var x = rotationAxis.X;
            var y = rotationAxis.Y;
            var z = rotationAxis.Z;
            
            return new Mat3(
                t*x*x+c,   t*x*y-z*s, t*x*z + y*s,
                t*x*y+z*s, t*y*y+c,   t*y*z-x*s,
                t*x*z-y*s, t*y*z+x*s, t*z*z + c );
        }
        
        public Mat3(Vec3 diagonal)
        {
            x12 = x13 = x21 = x23 = x31 = x32 = 0;
            x11 = diagonal.X;
            x22 = diagonal.Y;
            x33 = diagonal.Z;
            pad1 = pad2 = pad3 = 0;
        }

        public Mat3(float x11, float x12, float x13, 
            float x21, float x22, float x23, 
            float x31, float x32, float x33)
        {
            this.x11 = x11;
            this.x12 = x12;
            this.x13 = x13;
            this.x21 = x21;
            this.x22 = x22;
            this.x23 = x23;
            this.x31 = x31;
            this.x32 = x32;
            this.x33 = x33;
            pad1 = pad2 = pad3 = 0;
        }

        public Mat3(float value)
        {
            x11 = x12 = x13 = x21 = x22 = x23 = x31 = x32 = x33 = 0;
            pad1 = pad2 = pad3 = 0;
            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    this[x, y] = value;
                }
            }
        }
        
        public Vec3 GetRow(int row)
        {
            return new Vec3(this[row,0], this[row,1], this[row,2]);
        }
        
        public Vec3 GetCol(int col)
        {
            return new Vec3(this[0, col], this[1, col], this[2, col]);
        }

        public static Mat3 operator +(Mat3 first, Mat3 second)
        {
            var result = new float[3, 3];
            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    result[x, y] = first[x,y] + second[x,y];
                }
            }
            return new Mat3(result);
        }
        
        public static Mat3 operator -(Mat3 first, Mat3 second)
        {
            var result = new float[3, 3];
            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    result[x, y] = first[x,y] - second[x,y];
                }
            }
            return new Mat3(result);
        }
        
        public static Mat3 operator *(Mat3 matrix, Mat3 second)
        {
            var result = new float[3, 3];
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
            return new Mat3(result);
        }
        
        public static Mat3 operator *(Mat3 matrix, float scalar)
        {
            var result = new Mat3();
            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    result[x, y] *= scalar;
                }
            }
            return result;
        }
        
        public static Mat3 operator *(float scalar, Mat3 matrix)
        {
            return matrix*scalar;
        }
        
        public static Mat3 Transpose(Mat3 matrix)
        {
            var result = new float[3,3];
            for (var x = 0; x < 3; x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    result[y, x] = matrix[x, y];
                }
            }
            return new Mat3(result);
        }

        public static Mat3 Inverse(Mat3 matrix)
        {
            var a = matrix[0, 0];
            var b = matrix[0, 1];
            var c = matrix[0, 2];
            var d = matrix[1, 0];
            var e = matrix[1, 1];
            var f = matrix[1, 2];
            var g = matrix[2, 0];
            var h = matrix[2, 1];
            var i = matrix[2, 2];
            var resultMat = new Mat3(
                (e * i - f * h), -(b * i - c * h), (b * f - c * e), 
                -(d * i - f * g), (a * i - c * g), -(a * f - c * d),
                (d * h - e * g), -(a * h - b * g), (a * e - b * d));
            return (1 / matrix.Determinant) * resultMat;
        }

        public override string ToString()
        {
            return $"Matrix3:\n[{this[0,0]}, {this[0,1]}, {this[0,2]} " +
                   $"\n {this[1,0]}, {this[1,1]}, {this[1,2]} " +
                   $"\n {this[2,0]}, {this[2,1]}, {this[2,2]}]";
        }
    }
}