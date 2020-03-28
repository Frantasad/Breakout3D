using System.Drawing;
using Breakout3D.Libraries;

namespace Breakout3D.Framework
{
    public static class Color
    {
        public static Vec3 RGB(int r, int g, int b)
        {
            return new Vec3(r, g, b)/255;
        }
        
        public static Vec3 Html(string html)
        {
            var color = ColorTranslator.FromHtml(html);
            return RGB(color.R, color.G, color.B);
        }
    }
}