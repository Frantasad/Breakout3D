using System;

namespace Breakout3D.Framework
{
    public static class MathHelper
    {
        public static float ToRadians(float angle)
        {
            return (float) (Math.PI / 180) * angle;
        }
    }
}