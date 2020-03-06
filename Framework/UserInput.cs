using System.Collections.Generic;
using System.Windows.Forms;

namespace Breakout3D.Framework
{
    public class UserInput
    {
        private Dictionary<Keys, bool> PressedKeys { get; } = new Dictionary<Keys, bool>();
    }
}