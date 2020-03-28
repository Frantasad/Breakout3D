using Breakout3D.Libraries;
using OpenGL;

namespace Breakout3D.Framework
{
    public struct CameraData
    {
        public Mat4 Projection;
        public Mat4 View;
        public Vec3 EyePosition;

        public CameraData(Mat4 projection, Mat4 view, Vec3 eyePosition)
        {
            Projection = projection;
            View = view;
            EyePosition = eyePosition;
        }
    }
    public class Camera : UniformBuffer<CameraData>
    {
        public override uint Binding => Buffers.CAMERA_BINDING;
        
        public Camera() : base(new CameraData(Mat4.Identity, Mat4.Identity, Vec3.Zero)) {}
        
        public void LookAt(Vec3 eye, Vec3 target, Vec3 upVector)
        {
            m_Data.View = Mat4.LookAt(eye, target, upVector);
            m_Data.EyePosition = eye;
            UpdateData();
        }
        
        public void SetPerspective(float fov, int width, int height, float near, float far)
        {
            m_Data.Projection = Mat4.Perspective(fov, width, height, near, far);
            Gl.Viewport(0, 0, width, height);
            UpdateData();
        }
    }
}