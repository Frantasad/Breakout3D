using System;
using System.Runtime.InteropServices;
using Breakout3D.Libraries;
using OpenGL;

namespace Breakout3D.Framework
{
    public struct TransformData
    {
        public Mat4 Model;    // Model matrix
        public Mat3 InverseTransposeModel;  // Inverse of the transpose of the top-left part 3x3 of the model matrix

        public TransformData(Vec3 position, Mat3 rotation, Vec3 scale)
        {
            Model = Mat4.Translation(position) * new Mat4(rotation) * Mat4.Scale(scale);
            InverseTransposeModel = new Mat3(Model).Transpose.Inverse;
        }
    }

    public class Transform : UniformBuffer<TransformData>
    {
        public override uint Binding => Buffers.TRANSFORM_BINDING;

        private Vec3 m_Position;
        private Mat3 m_Rotation; 
        private Vec3 m_Scale;
        
        public Transform(): base(new TransformData(Vec3.Zero, Mat3.Identity, Vec3.Unit))
        {
            m_Position = Vec3.Zero;
            m_Rotation = Mat3.Identity;
            m_Scale = Vec3.Unit;
        }

        public void Set(Vec3 position, Vec3 rotationAxis, float angle, Vec3 scale)
        {
            Set(position, Mat3.Rotation(rotationAxis, angle), scale);
        }
        
        public void Set(Vec3 position, Mat3 rotation, Vec3 scale)
        {
            m_Position = position;
            m_Scale = scale;
            m_Rotation = rotation;
            RecalculateData();
            UpdateData();
        }
        
        public Vec3 Scale
        {
            get => m_Scale;
            set
            {
                m_Scale = value;
                RecalculateData();
                UpdateData();
            } 
        }
        
        public Vec3 Position
        {
            get => m_Position;
            set
            {
                m_Position = value;
                RecalculateData();
                UpdateData();
            } 
        }
        
        public Mat3 Rotation
        {
            get => m_Rotation;
            set
            {
                m_Rotation = value;
                RecalculateData();
                UpdateData();
            } 
        }

        public void Rotate(Vec3 rotationAxis, float angle)
        {
            Rotation = Mat3.Rotation(rotationAxis, angle) * Rotation;
        }

        private void RecalculateData()
        {
            m_Data = new TransformData(m_Position, m_Rotation, m_Scale);
        }
    }
}