using System;
using System.Windows.Forms;
using Breakout3D.Framework;
using Breakout3D.Libraries;

namespace Breakout3D
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
      
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GameWindow());
           
           /*
            var farLeft = new Vec3(4,0,14);
            var closeLeft = new Vec3(-4,0,14);
            var farRight = new Vec3(3,0,11);
            var closeRight = new Vec3(-3,0,11);
            
            var point = new Vec3(0,0,12);
            
            Console.WriteLine($"{Vec3.CheckLeftRight(point, closeLeft, farLeft)}");
            Console.WriteLine($"{Vec3.CheckLeftRight(point, closeRight, farRight)}");
            */
        }
    }
}