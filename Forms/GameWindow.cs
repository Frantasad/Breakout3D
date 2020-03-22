using System;
using System.Windows.Forms;
using Breakout3D.Framework;
using Breakout3D.Libraries;
using OpenGL;

namespace Breakout3D
{
    public partial class GameWindow : Form
    {
        private readonly ShaderProgram m_LitProgram = new ShaderProgram();
        private readonly ShaderProgram m_TextureProgram = new ShaderProgram();
        private readonly Camera m_Camera = new Camera();
        private readonly Light m_SunLight = new Light();

        private readonly Material m_DefaultMaterial = new Material();
        private readonly Material m_ChromeMaterial = new Material();
        private readonly Texture m_FloorTexture = new Texture();

        private Geometry m_Sphere;
        private readonly Transform m_SphereTransform = new Transform();
        private Geometry m_Floor;
        private readonly Transform m_FloorTransform = new Transform();

        public GameWindow()
        {
            InitializeComponent();
        }

        // Initialize whole scene
        private void InitScene(object sender, GlControlEventArgs e)
        {
            // Cap to 60 FPS
            RenderControl.AnimationTime = 1000 / 60;

            // Init shaders
            m_LitProgram.Init();
            m_LitProgram.AddShader(ShaderType.VertexShader, "./Shaders/lit_vertex.glsl");
            m_LitProgram.AddShader(ShaderType.FragmentShader, "./Shaders/lit_fragment.glsl");
            m_LitProgram.Link();
            
            m_TextureProgram.Init();
            m_TextureProgram.AddShader(ShaderType.VertexShader, "./Shaders/texture_vertex.glsl");
            m_TextureProgram.AddShader(ShaderType.FragmentShader, "./Shaders/texture_fragment.glsl");
            m_TextureProgram.Link();

            // Init materials
            m_DefaultMaterial.Init();
            m_DefaultMaterial.Set(new Vec3(0.6f, 0, 0), true, 200.0f, 1.0f);
            m_ChromeMaterial.Init();
            m_ChromeMaterial.Set(new Vec3(0f), new Vec3(0.55f), new Vec3(0.7f), 32f, 1.0f);
            
            m_FloorTexture.Load("./Textures/wood.png");

            // Init Light
            m_SunLight.Init();
            m_SunLight.Position = new Vec3(5, 10, 0);

            // Init Camera
            Gl.Viewport(0, 0, Width, Height);
            m_Camera.Init();
            m_Camera.SetProjection(Mat4.Perspective(45.0f, Width/(float)Height, 0.1f, 1000.0f));
            m_Camera.LookAt(new Vec3(0, 100, 100f), new Vec3(0, 0, 0), Vec3.Up);
            
            // Init objects
            m_Sphere = GeometryGenerator.Sphere();
            m_SphereTransform.Init();
            m_SphereTransform.Set(new Vec3(0, 1.5f, 0), Mat3.Identity, Vec3.Unit*3);
            
            m_Floor = GeometryGenerator.CircleFloor();
            m_FloorTransform.Init();
            m_FloorTransform.Set(Vec3.Zero, Mat3.Identity, Vec3.Unit*100);
            
            // Init GL environment
            Gl.ClearColor(0.1f, 0.1f, 0.12f, 0.1f);
            Gl.Enable(EnableCap.DepthTest);
        }

        // Called on windows repaint event
        private void OnRepaint(object sender, GlControlEventArgs e)
        {
            HandleTime();
            HandleInput();
            UpdateScene();
            RenderScene();
        }

        // Render the whole scene - here should be only rendering
        private void RenderScene()
        {
            // Clear all and setup
            Gl.Clear(ClearBufferMask.ColorBufferBit);
            Gl.Clear(ClearBufferMask.DepthBufferBit);
            Gl.ClearDepth(1.0);
            
            m_Camera.Bind();
            m_SunLight.Bind();

            // Draw Sphere
            m_LitProgram.Use();
            
            m_ChromeMaterial.Bind();
            m_SphereTransform.Bind();
            m_Sphere.Bind();
            m_Sphere.Draw();
            
            // Draw Floor
            m_TextureProgram.Use();
            
            m_FloorTexture.Bind(TextureUnit.Texture0);
            m_DefaultMaterial.Bind();
            m_FloorTransform.Bind();
            m_Floor.Bind();
            m_Floor.Draw();
            
        }

        // Update of the game - here should be all the physics
        private void UpdateScene()
        {
            m_FloorTransform.Rotate(Vec3.Up, 0.2f * Input.XAxis * Time.DeltaTime);
        }

        // Dispose all components
        private void DestroyScene(object sender, GlControlEventArgs e)
        {
            m_LitProgram.Dispose();
            m_TextureProgram.Dispose();
            
            m_Camera.Dispose();
            m_SunLight.Dispose();
            
            m_DefaultMaterial.Dispose();
           
            m_Sphere.Dispose();
            m_SphereTransform.Dispose();
            m_Floor.Dispose();
            m_FloorTransform.Dispose();
        }
        
        private void HandleInput()
        {
            Input.HandleInput();
        }
        
        private void HandleTime()
        {
            Time.RestartMeasure();
            UpdateTimeValue.Text = $"{Time.DeltaTime}ms";
        }

        // Called on windows resize event
        private void OnResize(object sender, EventArgs e)
        {
            Gl.Viewport(0, 0, Width, Height);
            m_Camera.SetProjection(Mat4.Perspective(45.0f, Width/(float)Height, 0.1f, 1000.0f));
        }
    }
}