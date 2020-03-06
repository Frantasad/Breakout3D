using System;

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
        
        public float x00; public float x01; public float x02; public float x03;
        public float x10; public float x11; public float x12; public float x13;
        public float x20; public float x21; public float x22; public float x23;
        public float x30; public float x31; public float x32; public float x33;

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
                     0 => x00, 1 => x01,  2 => x02,  3 => x03,
                     4 => x10, 5 => x11,  6 => x12,  7 => x13,
                     8 => x20, 9 => x21, 10 => x22, 11 => x23,
                    12 => x30,13 => x31, 14 => x32, 15 => x33,
                    _ => throw new IndexOutOfRangeException()
                };
            }
            set 
            {
                switch (i)
                {
                    case 0: x00 = value; break; case 1: x01 = value; break; case 2: x02 = value; break; case 3: x03 = value; break;
                    case 4: x10 = value; break; case 5: x11 = value; break; case 6: x12 = value; break; case 7: x13 = value; break;
                    case 8: x20 = value; break; case 9: x21 = value; break; case 10: x22 = value; break; case 11: x23 = value; break;
                    case 12: x30 = value; break; case 13: x31 = value; break; case 14: x32 = value; break; case 15: x33 = value; break;
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
            if (!(Math.Abs(aspect - float.MinValue) > 0))
            {
                throw new ArgumentException();
            }

            var tanHalfFov = (float) Math.Tan(fov / 2);
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

        public override string ToString()
        {
            return $"Matrix4:\n[{this[0,0]}, {this[0,1]}, {this[0,2]}, {this[0,3]}" +
                   $"\n {this[1,0]}, {this[1,1]}, {this[1,2]}, {this[1,3]}" +
                   $"\n {this[2,0]}, {this[2,1]}, {this[2,2]}, {this[2,3]}" +
                   $"\n {this[3,0]}, {this[3,1]}, {this[3,2]}, {this[3,3]}]";
        }
    }
}