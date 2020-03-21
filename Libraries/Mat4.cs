using System;
using Breakout3D.Framework;
using OpenGL;

namespace Breakout3D.Libraries
{
    public struct Mat4
    {
        public static readonly Mat4 Zero = new Mat4(0);

        public static readonly Mat4 Identity = new Mat4(
            1, 0, 0, 0,
            0, 1, 0, 0,
            0, 0, 1, 0,
            0, 0, 0, 1);

        public float x00; public float x10; public float x20; public float x30;
        public float x01; public float x11; public float x21; public float x31;
        public float x02; public float x12; public float x22; public float x32;
        public float x03; public float x13; public float x23; public float x33;

        public Mat4(
            float x00, float x01, float x02, float x03,
            float x10, float x11, float x12, float x13,
            float x20, float x21, float x22, float x23,
            float x30, float x31, float x32, float x33)
        {
            this.x00 = x00; this.x01 = x01; this.x02 = x02; this.x03 = x03;
            this.x10 = x10; this.x11 = x11; this.x12 = x12; this.x13 = x13;
            this.x20 = x20; this.x21 = x21; this.x22 = x22; this.x23 = x23;
            this.x30 = x30; this.x31 = x31; this.x32 = x32; this.x33 = x33;
        }

        public Mat4(float value)
        {
            x00 = x01 = x02 = x03 = x10 = x11 = x12 = x13 = x20 = x21 = x22 = x23 = x30 = x31 = x32 = x33 = value;
        }
        
        public Mat4(Mat3 matrix)
        {
            x00 = matrix.x00; x01 = matrix.x01; x02 = matrix.x02; x03 = 0;
            x10 = matrix.x10; x11 = matrix.x11; x12 = matrix.x12; x13 = 0;
            x20 = matrix.x20; x21 = matrix.x21; x22 = matrix.x22; x23 = 0;
            x30 = 0;          x31 = 0;          x32 = 0;          x33 = 1;
        }

        public float this[int i]
        {
            get
            {
                return i switch
                {
                     0 => x00,  1 => x01,  2 => x02,  3 => x03,
                     4 => x10,  5 => x11,  6 => x12,  7 => x13,
                     8 => x20,  9 => x21, 10 => x22, 11 => x23,
                    12 => x30, 13 => x31, 14 => x32, 15 => x33,
                    _ => throw new IndexOutOfRangeException()
                };
            }
            set
            {
                switch (i)
                {
                    case  0: x00 = value; break; case  1: x01 = value; break; case  2: x02 = value; break; case  3: x03 = value; break;
                    case  4: x10 = value; break; case  5: x11 = value; break; case  6: x12 = value; break; case  7: x13 = value; break;
                    case  8: x20 = value; break; case  9: x21 = value; break; case 10: x22 = value; break; case 11: x23 = value; break;
                    case 12: x30 = value; break; case 13: x31 = value; break; case 14: x32 = value; break; case 15: x33 = value; break;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }

        public float this[int i, int j]
        {
            get => this[i * 4 + j];
            set => this[i * 4 + j] = value;
        }

        public static Mat4 Perspective(float fovy, float aspectRatio, float near, float far)
        {
            if (fovy <= 0.0 || fovy >= 180.0)
                throw new ArgumentOutOfRangeException(nameof(fovy), "Not in range (0, 180)");
            if (Math.Abs(near) < 1.40129846432482E-45)
                throw new ArgumentOutOfRangeException(nameof(near), "Zero not allowed");
            if (Math.Abs(far) < Math.Abs(near))
                throw new ArgumentOutOfRangeException(nameof(far), "Less than near");
            
            var top = near * (float) Math.Tan(Angle.ToRadians(fovy / 2f));
            var right = top * aspectRatio;
            var left = -right;
            var bottom = -top;
            return new Mat4
            {
                [0,0] = 2.0f * near / (right - left),
                [1,1] = 2.0f * near / (top - bottom),
                [0,2] = (right + left) / (right - left),
                [1,2] = (top + bottom) / (top - bottom),
                [2,2] = (-far - near) / ( far - near),
                [3,2] = -1f,
                [2,3] = -2.0f * far * near / (far - near)
            };
        }
        
        public static Mat4 Translation(Vec3 vector)
        {
            return new Mat4
            {
                [0,0] = 1f, 
                [1,1] = 1f, 
                [2,2] = 1f,
                [3,3] = 1f, 
                [0,3] = vector.X,
                [1,3] = vector.Y,
                [2,3] = vector.Z
            };
        }
        
        public static Mat4 Scale(Vec3 scale)
        {
            return new Mat4(Mat3.Scale(scale));
        }
        
        public static Mat4 Rotation(Vec3 rotationAxis, float angle)
        {
            return new Mat4(Mat3.Rotation(rotationAxis, angle));
        }
        
        public static Mat4 LookAt(Vec3 eye, Vec3 target, Vec3 upVector)
        {
            var f = (target - eye).Normalized;
            var s = f.Cross(upVector.Normalized).Normalized;
            var u = s.Cross(f).Normalized;

            return new Mat4(
                 s.X,  s.Y,  s.Z, 0,
                 u.X,  u.Y,  u.Z, 0, 
                -f.X, -f.Y, -f.Z, 0,
                 0, 0, 0, 1) * Translation(-eye);
        }
        
        public static Mat4 operator *(Mat4 first, Mat4 second) 
        { 
            return new Mat4
            {
                [0,0] = (first[0,0] * second[0,0] + first[0,1] * second[1,0] + first[0,2] * second[2,0] + first[0,3] * second[3,0]),
                [1,0] = (first[1,0] * second[0,0] + first[1,1] * second[1,0] + first[1,2] * second[2,0] + first[1,3] * second[3,0]),
                [2,0] = (first[2,0] * second[0,0] + first[2,1] * second[1,0] + first[2,2] * second[2,0] + first[2,3] * second[3,0]),
                [3,0] = (first[3,0] * second[0,0] + first[3,1] * second[1,0] + first[3,2] * second[2,0] + first[3,3] * second[3,0]),
                [0,1] = (first[0,0] * second[0,1] + first[0,1] * second[1,1] + first[0,2] * second[2,1] + first[0,3] * second[3,1]),
                [1,1] = (first[1,0] * second[0,1] + first[1,1] * second[1,1] + first[1,2] * second[2,1] + first[1,3] * second[3,1]),
                [2,1] = (first[2,0] * second[0,1] + first[2,1] * second[1,1] + first[2,2] * second[2,1] + first[2,3] * second[3,1]),
                [3,1] = (first[3,0] * second[0,1] + first[3,1] * second[1,1] + first[3,2] * second[2,1] + first[3,3] * second[3,1]),
                [0,2] = (first[0,0] * second[0,2] + first[0,1] * second[1,2] + first[0,2] * second[2,2] + first[0,3] * second[3,2]),
                [1,2] = (first[1,0] * second[0,2] + first[1,1] * second[1,2] + first[1,2] * second[2,2] + first[1,3] * second[3,2]),
                [2,2] = (first[2,0] * second[0,2] + first[2,1] * second[1,2] + first[2,2] * second[2,2] + first[2,3] * second[3,2]), 
                [3,2] = (first[3,0] * second[0,2] + first[3,1] * second[1,2] + first[3,2] * second[2,2] + first[3,3] * second[3,2]),
                [0,3] = (first[0,0] * second[0,3] + first[0,1] * second[1,3] + first[0,2] * second[2,3] + first[0,3] * second[3,3]),
                [1,3] = (first[1,0] * second[0,3] + first[1,1] * second[1,3] + first[1,2] * second[2,3] + first[1,3] * second[3,3]),
                [2,3] = (first[2,0] * second[0,3] + first[2,1] * second[1,3] + first[2,2] * second[2,3] + first[2,3] * second[3,3]),
                [3,3] = (first[3,0] * second[0,3] + first[3,1] * second[1,3] + first[3,2] * second[2,3] + first[3,3] * second[3,3]),
            };
        }
        
        public Mat4 Transpose =>
            new Mat4(
                this[0, 0], this[1, 0], this[2, 0], this[3, 0],
                this[0, 1], this[1, 1], this[2, 1], this[3, 1],
                this[0, 2], this[1, 2], this[2, 2], this[3, 2],
                this[0, 3], this[1, 3], this[2, 3], this[3, 3]);
        
        public Mat4 Inverse
        {
            get
            {
                 var num1  = this[1,1] * this[2,2] * this[3,3] - this[1,1] * this[3,2] * this[2,3] - this[1,2] * this[2,1] * this[3,3] + this[1,2] * this[3,1] * this[2,3] + this[1,3] * this[2,1] * this[3,2] - this[1,3] * this[3,1] * this[2,2];
                 var num2  = -this[0,1] * this[2,2] * this[3,3] + this[0,1] * this[3,2] * this[2,3] + this[0,2] * this[2,1] * this[3,3] - this[0,2] * this[3,1] * this[2,3] - this[0,3] * this[2,1] * this[3,2] + this[0,3] * this[3,1] * this[2,2];
                 var num3  = this[0,1] * this[1,2] * this[3,3] - this[0,1] * this[3,2] * this[1,3] - this[0,2] * this[1,1] * this[3,3] + this[0,2] * this[3,1] * this[1,3] + this[0,3] * this[1,1] * this[3,2] - this[0,3] * this[3,1] * this[1,2];
                 var num4  = -this[0,1] * this[1,2] * this[2,3] + this[0,1] * this[2,2] * this[1,3] + this[0,2] * this[1,1] * this[2,3] - this[0,2] * this[2,1] * this[1,3] - this[0,3] * this[1,1] * this[2,2] + this[0,3] * this[2,1] * this[1,2];
                 var num5  = -this[1,0] * this[2,2] * this[3,3] + this[1,0] * this[3,2] * this[2,3] + this[1,2] * this[2,0] * this[3,3] - this[1,2] * this[3,0] * this[2,3] - this[1,3] * this[2,0] * this[3,2] + this[1,3] * this[3,0] * this[2,2];
                 var num6  = this[0,0] * this[2,2] * this[3,3] - this[0,0] * this[3,2] * this[2,3] - this[0,2] * this[2,0] * this[3,3] + this[0,2] * this[3,0] * this[2,3] + this[0,3] * this[2,0] * this[3,2] - this[0,3] * this[3,0] * this[2,2];
                 var num7  = -this[0,0] * this[1,2] * this[3,3] + this[0,0] * this[3,2] * this[1,3] + this[0,2] * this[1,0] * this[3,3] - this[0,2] * this[3,0] * this[1,3] - this[0,3] * this[1,0] * this[3,2] + this[0,3] * this[3,0] * this[1,2];
                 var num8  = this[0,0] * this[1,2] * this[2,3] - this[0,0] * this[2,2] * this[1,3] - this[0,2] * this[1,0] * this[2,3] + this[0,2] * this[2,0] * this[1,3] + this[0,3] * this[1,0] * this[2,2] - this[0,3] * this[2,0] * this[1,2];
                 var num9  = this[1,0] * this[2,1] * this[3,3] - this[1,0] * this[3,1] * this[2,3] - this[1,1] * this[2,0] * this[3,3] + this[1,1] * this[3,0] * this[2,3] + this[1,3] * this[2,0] * this[3,1] - this[1,3] * this[3,0] * this[2,1];
                 var num10 = -this[0,0] * this[2,1] * this[3,3] + this[0,0] * this[3,1] * this[2,3] + this[0,1] * this[2,0] * this[3,3] - this[0,1] * this[3,0] * this[2,3] - this[0,3] * this[2,0] * this[3,1] + this[0,3] * this[3,0] * this[2,1];
                 var num11 = this[0,0] * this[1,1] * this[3,3] - this[0,0] * this[3,1] * this[1,3] - this[0,1] * this[1,0] * this[3,3] + this[0,1] * this[3,0] * this[1,3] + this[0,3] * this[1,0] * this[3,1] - this[0,3] * this[3,0] * this[1,1];
                 var num12 = -this[0,0] * this[1,1] * this[2,3] + this[0,0] * this[2,1] * this[1,3] + this[0,1] * this[1,0] * this[2,3] - this[0,1] * this[2,0] * this[1,3] - this[0,3] * this[1,0] * this[2,1] + this[0,3] * this[2,0] * this[1,1];
                 var num13 = -this[1,0] * this[2,1] * this[3,2] + this[1,0] * this[3,1] * this[2,2] + this[1,1] * this[2,0] * this[3,2] - this[1,1] * this[3,0] * this[2,2] - this[1,2] * this[2,0] * this[3,1] + this[1,2] * this[3,0] * this[2,1];
                 var num14 = this[0,0] * this[2,1] * this[3,2] - this[0,0] * this[3,1] * this[2,2] - this[0,1] * this[2,0] * this[3,2] + this[0,1] * this[3,0] * this[2,2] + this[0,2] * this[2,0] * this[3,1] - this[0,2] * this[3,0] * this[2,1];
                 var num15 = -this[0,0] * this[1,1] * this[3,2] + this[0,0] * this[3,1] * this[1,2] + this[0,1] * this[1,0] * this[3,2] - this[0,1] * this[3,0] * this[1,2] - this[0,2] * this[1,0] * this[3,1] + this[0,2] * this[3,0] * this[1,1];
                 var num16 = this[0,0] * this[1,1] * this[2,2] - this[0,0] * this[2,1] * this[1,2] - this[0,1] * this[1,0] * this[2,2] + this[0,1] * this[2,0] * this[1,2] + this[0,2] * this[1,0] * this[2,1] - this[0,2] * this[2,0] * this[1,1];
                 var det = this[0,0] * num1 + this[1,0] * num2 + this[2,0] * num3 + this[3,0] * num4;
                 if (Math.Abs(det) < 9.99999997475243E-07) 
                     throw new InvalidOperationException("Matrix not invertible");
                 var idet = 1f / det; 
                 return new Mat4(
                     idet * num1, idet * num2, idet * num3, idet * num4, 
                     idet * num5, idet * num6, idet * num7, idet * num8, 
                     idet * num9, idet * num10, idet * num11, idet * num12, 
                     idet * num13, idet * num14, idet * num15, idet * num16);
            }
        }

        public override string ToString()
        {
            return $"Matrix4:\n[{this[0,0]}, {this[0,1]}, {this[0,2]}, {this[0,3]}" +
                   $"\n {this[1,0]}, {this[1,1]}, {this[1,2]}, {this[1,3]}" +
                   $"\n {this[2,0]}, {this[2,1]}, {this[2,2]}, {this[2,3]}" +
                   $"\n {this[3,0]}, {this[3,1]}, {this[3,2]}, {this[3,3]}]";
        }
    }
}