using System.Diagnostics;

namespace Breakout3D.Framework
{
    public static class Time
    {
        private static readonly Stopwatch m_DeltaTimeStopwatch = new Stopwatch();
        
        public static float DeltaTime { get; private set; }

        public static void RestartMeasure()
        {
            m_DeltaTimeStopwatch.Stop();
            DeltaTime = m_DeltaTimeStopwatch.ElapsedMilliseconds;
            m_DeltaTimeStopwatch.Restart();
        }
    }
}