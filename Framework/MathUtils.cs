using System;

namespace Breakout3D.Framework
{
    public static class MathUtils
    {
        public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, float deltaTime)
        {
            smoothTime = Math.Max(0.0001f, smoothTime);
            var num1 = 2f / smoothTime;
            var num2 = num1 * deltaTime;
            var num3 = (float) (1.0 / (1.0 + num2 + 0.479999989271164 * num2 * num2 + 0.234999999403954 * num2 * num2 * num2));
            var num4 = current - target;
            var num5 = target;
            target = current - num4;
            var num7 = (currentVelocity + num1 * num4) * deltaTime;
            currentVelocity = (currentVelocity - num1 * num7) * num3;
            var num8 = target + (num4 + num7) * num3;
            if (num5 - current > 0.0 == num8 > num5)
            {
                num8 = num5;
                currentVelocity = (num8 - num5) / deltaTime;
            }
            return num8;
        }
    }
    
}