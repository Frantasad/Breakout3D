using System;
using Breakout3D.Framework;

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

        public float x00;
        public float x01;
        public float x02;
        public float x03;
        public float x10;
        public float x11;
        public float x12;
        public float x13;
        public float x20;
        public float x21;
        public float x22;
        public float x23;
        public float x30;
        public float x31;
        public float x32;
        public float x33;

        public Mat4(
            float x00, float x01, float x02, float x03,
            float x10, float x11, float x12, float x13,
            float x20, float x21, float x22, float x23,
            float x30, float x31, float x32, float x33)
        {
            this.x00 = x00;
            this.x01 = x01;
            this.x02 = x02;
            this.x03 = x03;
            this.x10 = x10;
            this.x11 = x11;
            this.x12 = x12;
            this.x13 = x13;
            this.x20 = x20;
            this.x21 = x21;
            this.x22 = x22;
            this.x23 = x23;
            this.x30 = x30;
            this.x31 = x31;
            this.x32 = x32;
            this.x33 = x33;
        }

        public Mat4(float value)
        {
            x00 = x01 = x02 = x03 = x10 = x11 = x12 = x13 = x20 = x21 = x22 = x23 = x30 = x31 = x32 = x33 = 0;
            for (var x = 0; x < 4; x++)
            {
                for (var y = 0; y < 4; y++)
                {
                    this[x, y] = value;
                }
            }
        }

        public float this[int i]
        {
            get
            {
                return i switch
                {
                    0 => x00, 1 => x01, 2 => x02, 3 => x03,
                    4 => x10, 5 => x11, 6 => x12, 7 => x13,
                    8 => x20, 9 => x21, 10 => x22, 11 => x23,
                    12 => x30, 13 => x31, 14 => x32, 15 => x33,
                    _ => throw new IndexOutOfRangeException()
                };
            }
            set
            {
                switch (i)
                {
                    case 0:
                        x00 = value;
                        break;
                    case 1:
                        x01 = value;
                        break;
                    case 2:
                        x02 = value;
                        break;
                    case 3:
                        x03 = value;
                        break;
                    case 4:
                        x10 = value;
                        break;
                    case 5:
                        x11 = value;
                        break;
                    case 6:
                        x12 = value;
                        break;
                    case 7:
                        x13 = value;
                        break;
                    case 8:
                        x20 = value;
                        break;
                    case 9:
                        x21 = value;
                        break;
                    case 10:
                        x22 = value;
                        break;
                    case 11:
                        x23 = value;
                        break;
                    case 12:
                        x30 = value;
                        break;
                    case 13:
                        x31 = value;
                        break;
                    case 14:
                        x32 = value;
                        break;
                    case 15:
                        x33 = value;
                        break;
                    default: throw new IndexOutOfRangeException();
                }
            }
        }

        public float this[int i, int j]
        {
            get => this[i + j * 4];
            set => this[i + j * 4] = value;
        }

        public static Mat4 Perspective(float fov, float aspect, float zNear, float zFar)
        {
            var fovRad = MathHelper.ToRadians(fov);
            if (!(Math.Abs(aspect - float.MinValue) > 0))
            {
                throw new ArgumentException();
            }

            var tanHalfFov = (float) Math.Tan(fovRad / 2);
            var result = new Mat4
            {
                [0, 0] = 1 / (aspect * tanHalfFov),
                [1, 1] = 1 / (tanHalfFov),
                [2, 2] = -(zFar + zNear) / (zFar - zNear),
                [2, 3] = -1,
                [3, 2] = -(2 * zFar * zNear) / (zFar - zNear)
            };
            return result;
        }

        public static Mat4 LookAtView(Vec3 from, Vec3 to, Vec3 upVector)
        {
            var forward = (from - to).Normalized;
            var right = Vec3.Cross(upVector.Normalized, forward);
            var up = Vec3.Cross(forward, right);

            return Inverse(new Mat4(
                right.X, right.Y, right.Z, 0,
                up.X, up.Y, up.Z, 0,
                forward.X, forward.Y, forward.Z, 0,
                from.X, from.Y, from.Z, 1));
        }

        public static Mat4 Inverse(Mat4 matrix)
        {
            var a2323 = matrix.x22 * matrix.x33 - matrix.x23 * matrix.x32 ;
            var a1323 = matrix.x21 * matrix.x33 - matrix.x23 * matrix.x31 ;
            var a1223 = matrix.x21 * matrix.x32 - matrix.x22 * matrix.x31 ;
            var a0323 = matrix.x20 * matrix.x33 - matrix.x23 * matrix.x30 ;
            var a0223 = matrix.x20 * matrix.x32 - matrix.x22 * matrix.x30 ;
            var a0123 = matrix.x20 * matrix.x31 - matrix.x21 * matrix.x30 ;
            var a2313 = matrix.x12 * matrix.x33 - matrix.x13 * matrix.x32 ;
            var a1313 = matrix.x11 * matrix.x33 - matrix.x13 * matrix.x31 ;
            var a1213 = matrix.x11 * matrix.x32 - matrix.x12 * matrix.x31 ;
            var a2312 = matrix.x12 * matrix.x23 - matrix.x13 * matrix.x22 ;
            var a1312 = matrix.x11 * matrix.x23 - matrix.x13 * matrix.x21 ;
            var a1212 = matrix.x11 * matrix.x22 - matrix.x12 * matrix.x21 ;
            var a0313 = matrix.x10 * matrix.x33 - matrix.x13 * matrix.x30 ;
            var a0213 = matrix.x10 * matrix.x32 - matrix.x12 * matrix.x30 ;
            var a0312 = matrix.x10 * matrix.x23 - matrix.x13 * matrix.x20 ;
            var a0212 = matrix.x10 * matrix.x22 - matrix.x12 * matrix.x20 ;
            var a0113 = matrix.x10 * matrix.x31 - matrix.x11 * matrix.x30 ;
            var a0112 = matrix.x10 * matrix.x21 - matrix.x11 * matrix.x20 ;

            var det = matrix.x00 * ( matrix.x11 * a2323 - matrix.x12 * a1323 + matrix.x13 * a1223 ) 
                - matrix.x01 * ( matrix.x10 * a2323 - matrix.x12 * a0323 + matrix.x13 * a0223 ) 
                + matrix.x02 * ( matrix.x10 * a1323 - matrix.x11 * a0323 + matrix.x13 * a0123 ) 
                - matrix.x03 * ( matrix.x10 * a1223 - matrix.x11 * a0223 + matrix.x12 * a0123 ) ;
            det = 1 / det;

            return new Mat4{
                x00 = det *   ( matrix.x11 * a2323 - matrix.x12 * a1323 + matrix.x13 * a1223 ),
                x01 = det * - ( matrix.x01 * a2323 - matrix.x02 * a1323 + matrix.x03 * a1223 ),
                x02 = det *   ( matrix.x01 * a2313 - matrix.x02 * a1313 + matrix.x03 * a1213 ),
                x03 = det * - ( matrix.x01 * a2312 - matrix.x02 * a1312 + matrix.x03 * a1212 ),
                x10 = det * - ( matrix.x10 * a2323 - matrix.x12 * a0323 + matrix.x13 * a0223 ),
                x11 = det *   ( matrix.x00 * a2323 - matrix.x02 * a0323 + matrix.x03 * a0223 ),
                x12 = det * - ( matrix.x00 * a2313 - matrix.x02 * a0313 + matrix.x03 * a0213 ),
                x13 = det *   ( matrix.x00 * a2312 - matrix.x02 * a0312 + matrix.x03 * a0212 ),
                x20 = det *   ( matrix.x10 * a1323 - matrix.x11 * a0323 + matrix.x13 * a0123 ),
                x21 = det * - ( matrix.x00 * a1323 - matrix.x01 * a0323 + matrix.x03 * a0123 ),
                x22 = det *   ( matrix.x00 * a1313 - matrix.x01 * a0313 + matrix.x03 * a0113 ),
                x23 = det * - ( matrix.x00 * a1312 - matrix.x01 * a0312 + matrix.x03 * a0112 ),
                x30 = det * - ( matrix.x10 * a1223 - matrix.x11 * a0223 + matrix.x12 * a0123 ),
                x31 = det *   ( matrix.x00 * a1223 - matrix.x01 * a0223 + matrix.x02 * a0123 ),
                x32 = det * - ( matrix.x00 * a1213 - matrix.x01 * a0213 + matrix.x02 * a0113 ),
                x33 = det *   ( matrix.x00 * a1212 - matrix.x01 * a0212 + matrix.x02 * a0112 )
            };
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