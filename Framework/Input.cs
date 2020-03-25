using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Breakout3D.Framework
{
    public static class Input
    {
        private static Dictionary<Keys, bool> PressedKeys { get; } = new Dictionary<Keys, bool>()
        {
            {Keys.Left, false},
            {Keys.Right, false},
            {Keys.Up, false},
            {Keys.Down, false}
        };
        
        public static float XAxis { get; private set; }

        public static bool IsKeyPressed(Keys key)
        {
            return PressedKeys.TryGetValue(key, out var pressed) && pressed;
        }

        private static float m_XVelocity;
        public static void HandleInput()
        {
            var tmpXAxis = 0;
            if (PressedKeys[Keys.Left]) tmpXAxis -= 1;
            if (PressedKeys[Keys.Right]) tmpXAxis += 1;
            XAxis = MathUtils.SmoothDamp(XAxis, tmpXAxis, ref m_XVelocity, 100f, Time.DeltaTime);
        }
        
        public static void PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (PressedKeys.ContainsKey(e.KeyCode))
            {
                e.IsInputKey = true;
            }
        }
        
        public static void KeyDown(object sender, KeyEventArgs e)
        {
            if (PressedKeys.ContainsKey(e.KeyCode))
            {
                PressedKeys[e.KeyCode] = true;
            }
        }

        public static void KeyUp(object sender, KeyEventArgs e)
        {
            if (PressedKeys.ContainsKey(e.KeyCode))
            {
                PressedKeys[e.KeyCode] = false;
            }
        }
    }
}