using System;

namespace Breakout3D.Framework
{
    public abstract class Component : IDisposable
    {
        public GameObject GameObject { get; set; }

        public virtual void Dispose() { }
        
        public virtual void OnStart() { }

        public virtual void OnUpdate() { }
        
        public virtual void OnLateUpdate() { }
    }
}