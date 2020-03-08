using System;
using System.Runtime.InteropServices;
using Breakout3D.Libraries;
using OpenGL;

namespace Breakout3D.Framework
{
    public struct TransformData
    {
        public Mat3 Rotation;
        public Mat3 Scale;
        public Vec3 Position;
        
        public TransformData(Vec3 position, Mat3 rotation, Vec3 scale)
        {
            Position = position;
            Rotation = rotation;
            Scale = new Mat3(scale);
        }
        
        public TransformData(Vec3 position, Vec3 rotationAxis, float angle, Vec3 scale)
        {
            Position = position;
            Rotation = Mat3.Rotation(rotationAxis, angle);
            Scale = new Mat3(scale);
        }
    }

    public class Transform : UniformBuffer<TransformData>
    {
        public override uint Binding => Buffers.TRANSFORM_BINDING;
        
        public Transform(): base(new TransformData(Vec3.Zero, Mat3.Identity, Vec3.Unit)){}
        
        public Vec3 Scale
        {
            get => m_Data.Scale.Diagonal;
            set
            {
                m_Data.Scale = new Mat3(value);
                UpdateData();
            } 
        }
        
        public Vec3 Position
        {
            get => m_Data.Position;
            set
            {
                m_Data.Position = value;
                UpdateData();
            } 
        }

        public void Rotate(Vec3 rotationAxis, float angle)
        {
            m_Data.Rotation = Mat3.Rotation(rotationAxis, angle) * m_Data.Rotation;
            UpdateData();
        }
    }
}