using System;
using System.Runtime.InteropServices;
using Breakout3D.Libraries;
using OpenGL;

namespace Breakout3D.Framework
{
    public static class Buffers
    {
        public const uint CAMERA_BINDING = 0;
        public const uint TRANSFORM_BINDING = 1;
        public const uint MATERIAL_BINDING = 2;
        public const uint LIGHT_BINDING = 3;
    }
    
    public abstract class UniformBuffer<T> : IDisposable where T : struct
    {
        protected T m_Data;
        public T Data
        {
            get => m_Data;
            set
            {
                m_Data = value;
                UpdateData();
            }
        }

        public uint BufferId { get; private set; } = 0;

        public UniformBuffer(T data)
        {
            m_Data = data;
        }

        public abstract uint Binding { get; }

        public void Init()
        {
            if(BufferId > 0) Dispose();
            
            BufferId = Gl.GenBuffer();
            UpdateData();
        }

        public void Bind()
        {
            Gl.BindBufferBase(BufferTarget.UniformBuffer, Binding, BufferId);
        }

        public void UpdateData()
        {
            var bufferSize = (uint) Marshal.SizeOf(typeof(T));
            Gl.BindBuffer(BufferTarget.UniformBuffer, BufferId);
            Gl.BufferData(BufferTarget.UniformBuffer, bufferSize, m_Data, BufferUsage.StaticDraw);
            Gl.BindBuffer(BufferTarget.UniformBuffer, 0);
        }

        public void Dispose()
        {
            Gl.DeleteBuffers(1, BufferId);
            BufferId = 0;
        }
    }
}