using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using Breakout3D.Framework;
using Breakout3D.Libraries;
using OpenGL;

namespace Breakout3D
{
    public partial class GameWindow : Form
    {
        private readonly ShaderProgram m_Program = new ShaderProgram();
        private readonly Material m_DefaultMaterial = new Material();
        private readonly Transform m_BallTransform = new Transform();
        private readonly Camera m_Camera = new Camera();
        private readonly Light m_SunLight = new Light();
        
        private Geometry m_Triangle;
        
        private readonly Stopwatch m_DeltaTimeStopwatch = new Stopwatch();
        private float m_DeltaTime;

        private int m_RotateInput = 0;

        private Dictionary<Keys, bool> PressedKeys { get; } = new Dictionary<Keys, bool>()
        {
            {Keys.Left, false},
            {Keys.Right, false}
        };

        public GameWindow()
        {
            InitializeComponent();
        }
        
        private void InitScene(object sender, GlControlEventArgs e)
        {
            // Cap to 60 FPS
            RenderControl.AnimationTime = 1000 / 60;
            
            m_Program.Init();
            m_Program.AddShader(ShaderType.VertexShader, "./Shaders/lit_vertex.glsl");
            m_Program.AddShader(ShaderType.FragmentShader, "./Shaders/lit_fragment.glsl");
            m_Program.Link();

            m_DefaultMaterial.Init();
            m_DefaultMaterial.Data = new PhongMaterial(new Vec3(0.6f,0,0), true, 200.0f, 1.0f);

            m_BallTransform.Init();
            m_BallTransform.Data = new TransformData(Vec3.Zero, Mat3.Identity, Vec3.Unit/2);
            
            m_SunLight.Init();

            m_Camera.Init();
            m_Camera.SetCamera(
                Mat4.Perspective(45, (float)Width/Height, -10f, 1000f),
                Mat4.LookAtView(new Vec3(0, 0, 0), new Vec3(0, 5, 10), Vec3.Up));
            
            //m_Triangle = GeometryGenerator.LoadSimpleObj("Models/Cube.obj");
            m_Triangle = GeometryGenerator.SingleTriangle();
            Gl.ClearColor(0.1f, 0.1f, 0.12f, 0.1f);
        }

        private void RenderScene(object sender, GlControlEventArgs e)
        {
            // Measure DeltaTime
            m_DeltaTimeStopwatch.Stop();
            m_DeltaTime = m_DeltaTimeStopwatch.ElapsedMilliseconds;
            m_DeltaTimeStopwatch.Restart();
            UpdateTimeValue.Text = $"{m_DeltaTime} ms";

            HandleInput();
            UpdateScene();
            
            Gl.Viewport(0, 0, Width, Height);
            Gl.Clear(ClearBufferMask.ColorBufferBit);
            
            m_Program.Use();
            
            m_DefaultMaterial.Bind();
            m_BallTransform.Bind();
            m_SunLight.Bind();
            m_Camera.Bind();

            m_Triangle.Bind();
            m_Triangle.Draw();
        }

        private void UpdateScene()
        {
            m_BallTransform.Rotate(Vec3.Up, 0.5f * m_RotateInput * m_DeltaTime);
        }

        private void HandleInput()
        {
            m_RotateInput = 0;
            if (PressedKeys[Keys.Left])
            {
                StatusText.Text = "Left pressed";
                m_RotateInput = -1;
            }

            if (PressedKeys[Keys.Right])
            {
                StatusText.Text = "Right pressed";
                m_RotateInput = 1;
            } 
        }

        private void DestroyScene(object sender, GlControlEventArgs e)
        {
            m_Program.Dispose();
            m_DefaultMaterial.Dispose();
        }

        private void RenderControl_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (PressedKeys.ContainsKey(e.KeyCode))
            {
                e.IsInputKey = true;
            }
        }
        
        private void RenderControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (PressedKeys.ContainsKey(e.KeyCode))
            {
                PressedKeys[e.KeyCode] = true;
            }
        }

        private void RenderControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (PressedKeys.ContainsKey(e.KeyCode))
            {
                PressedKeys[e.KeyCode] = false;
            }
        }

        private void GameWindow_Resize(object sender, EventArgs e)
        {
            m_Camera.SetProjection(Mat4.Perspective(45, (float)Width/Height, -10f, 1000f));
        }
    }
}