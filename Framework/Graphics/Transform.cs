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
        
        public TransformData(Vec3 position, Vec3 rotation, Vec3 scale)
        {
            Model = Mat4.Translation(position) * Mat4.Rotation(rotation) * Mat4.Scale(scale);
            InverseTransposeModel = new Mat3(Model).Transpose.Inverse;
        }
        
        public TransformData(Mat4 model)
        {
            Model = model;
            InverseTransposeModel = new Mat3(model).Transpose.Inverse;
        }
    }

    public class Transform : UniformBuffer<TransformData>
    {
        public override uint Binding => Buffers.TRANSFORM_BINDING;

        private Vec3 m_Position;
        private Vec3 m_Rotation;
        private Vec3 m_Scale;
        
        public Transform(): base(new TransformData(Vec3.Zero, Mat3.Identity, Vec3.Unit))
        {
            m_Position = Vec3.Zero;
            m_Rotation = Vec3.Zero;
            m_Scale = Vec3.Unit;
        }
        
        public Transform(Vec3 position, Vec3 rotation, Vec3 scale)
            :base(new TransformData(position, rotation, scale))
        {
            m_Position = position;
            m_Rotation = rotation;
            m_Scale = scale;
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
        
        public Vec3 Rotation
        {
            get => m_Rotation;
            set
            {
                m_Rotation = value;
                RecalculateData();
                UpdateData();
            } 
        }
        
        public void RotateAround(Vec3 eulerAngles, Vec3 point)
        {
            m_Data = new TransformData(
                Mat4.Translation(point) * Mat4.Rotation(eulerAngles) * Mat4.Translation(-point) * m_Data.Model);
            m_Rotation = (m_Rotation + eulerAngles) % 360;
            m_Position = new Vec3(m_Data.Model[0,3], m_Data.Model[1,3], m_Data.Model[2,3]);
            UpdateData();
        }
        
        private void RecalculateData()
        {
            m_Data = new TransformData(m_Position, m_Rotation, m_Scale);
        }
    }
}