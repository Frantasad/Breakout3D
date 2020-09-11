namespace Breakout3D.Framework
{
    public class Segment
    {
        public static readonly Segment Brick = new Segment(30,12,15,4);
        public static readonly Segment Bat = new Segment(30,43,46,3);
     
        public float AngleSize { get; }
        public float Close { get; }
        public float Far { get; }
        public float Height { get; }

        public Segment(float angleSize, float close, float far, float height)
        {
            AngleSize = angleSize;
            Close = close;
            Far = far;
            Height = height;
        }
    }
}