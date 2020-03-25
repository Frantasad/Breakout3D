using System;
using System.Collections.Generic;
using Breakout3D.Framework;
using Breakout3D.Libraries;
using OpenGL;

namespace Breakout3D
{
    public class Game : IDisposable
    {
        private GameWindow m_Window;
        
        private readonly ShaderProgram m_LitProgram = new ShaderProgram();
        private readonly ShaderProgram m_TextureProgram = new ShaderProgram();
        private readonly Camera m_Camera = new Camera();
        private readonly Light m_SunLight = new Light();

        private readonly Material m_DefaultMaterial = new Material();
        private readonly Material m_BatMaterial = new Material();
        private readonly Material m_ChromeMaterial = new Material();
        private readonly List<Material> m_BrickMaterials = new List<Material>();
        
        private readonly Texture m_FloorTexture = new Texture();

        private readonly Transform m_SphereTransform = new Transform();
        private readonly Transform m_FloorTransform = new Transform();
        private readonly List<Transform> m_BrickTransforms = new List<Transform>();
        private readonly List<Transform> m_BatTransforms = new List<Transform>{new Transform(), new Transform(), new Transform()};
        
        private Geometry m_SphereGeometry = new Geometry();
        private Geometry m_BrickGeometry = new Geometry();
        private Geometry m_FloorGeometry = new Geometry();
        private Geometry m_BatGeometry = new Geometry();

        public Game(GameWindow window)
        {
            m_Window = window;
        }

        // Initialize whole scene
        public void InitScene()
        {
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
            m_DefaultMaterial.Set(new Vec3(0.8f, 0, 0), true, 200.0f, 1.0f);
            m_ChromeMaterial.Init();
            m_ChromeMaterial.Set(new Vec3(0f), new Vec3(0.55f), new Vec3(0.7f), 32f, 1.0f);
            m_BatMaterial.Init();
            m_BatMaterial.Set(Color.FromRGB(213,8,24), false, 200f, 1.0f);
            var blueMaterial = new Material(Color.FromRGB(54,88,229), true, 200.0f, 1.0f);
            blueMaterial.Init();
            var greenMaterial = new Material(Color.FromRGB(52,222,157), true, 200.0f, 1.0f);
            greenMaterial.Init();
            var yellowMaterial = new Material(Color.FromRGB(238,198,28), true, 200.0f, 1.0f);
            yellowMaterial.Init();

            m_BrickMaterials.Add(blueMaterial, yellowMaterial, greenMaterial);
            
            m_FloorTexture.Load("./Textures/floor.png");

            // Init Light
            m_SunLight.Init();
            m_SunLight.Set(
                new Vec3(5, 10, 0), 
                new Vec3(0.3f, 0.3f, 0.3f),
                new Vec3(0.7f, 0.7f, 0.7f),
                new Vec3(0.7f, 0.7f, 0.7f));

            // Init Camera
            m_Camera.Init();
            Resize();
            m_Camera.LookAt(new Vec3(0, 100, 100f), new Vec3(0, 0, 0), Vec3.Up);
            
            // Init objects
            m_SphereGeometry = GeometryGenerator.Sphere();
            m_SphereTransform.Init();
            m_SphereTransform.Set(new Vec3(0, 1.5f, 0), Mat3.Identity, Vec3.Unit*3);
            
            m_FloorGeometry = GeometryGenerator.CircleFloor();
            m_FloorTransform.Init();
            m_FloorTransform.Set(Vec3.Zero, Mat3.Identity, Vec3.Unit*50);

            m_BrickGeometry = GeometryGenerator.Brick();
            for (var i = 0; i < 12; i++)
            {
                var transform = new Transform();
                transform.Init();
                transform.Set(new Vec3(0, 0, 12), Mat3.Identity, Vec3.Unit);
                transform.RotateAround(Vec3.Up, i * (360 / 12), Vec3.Zero);
                m_BrickTransforms.Add(transform);
            }

            m_BatGeometry = GeometryGenerator.Bat();
            m_BatTransforms[0].Init();
            m_BatTransforms[0].Set(new Vec3(0, 0, 40), Mat3.Identity, Vec3.Unit);
            m_BatTransforms[1].Init();
            m_BatTransforms[1].Set(new Vec3(0, 0, 40), Mat3.Identity, Vec3.Unit);
            m_BatTransforms[1].RotateAround(Vec3.Up, 120, Vec3.Zero);
            m_BatTransforms[2].Init();
            m_BatTransforms[2].Set(new Vec3(0, 0, 40), Mat3.Identity, Vec3.Unit);
            m_BatTransforms[2].RotateAround(Vec3.Up, -120, Vec3.Zero);
            
            // Init GL environment
            Gl.ClearColor(0.1f, 0.1f, 0.12f, 0.1f);
            Gl.Enable(EnableCap.DepthTest);
            //Gl.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
        }
        
        // Update of the game - here should be all the physics
        public void UpdateScene()
        {
            foreach (var transform in m_BatTransforms)
            {
                transform.RotateAround(Vec3.Up, 0.2f * Input.XAxis * Time.DeltaTime, Vec3.Zero);
            }
        }

        // Render the whole scene - here should be only rendering
        public void RenderScene()
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
            m_SphereGeometry.Bind();
            m_SphereGeometry.Draw();
            
            // Draw Floor
            m_TextureProgram.Use();
            
            m_FloorTexture.Bind(TextureUnit.Texture0);
            m_DefaultMaterial.Bind();
            m_FloorTransform.Bind();
            m_FloorGeometry.Bind();
            m_FloorGeometry.Draw();
            
            // Draw Bricks
            m_LitProgram.Use();
            
            m_BrickGeometry.Bind();
            for (var i = 0; i < m_BrickTransforms.Count; i++)
            {
                m_BrickTransforms[i].Bind();
                m_BrickMaterials[i % 3].Bind();
                m_BrickGeometry.Draw();
            }

            // Draw Bats
            m_BatGeometry.Bind();
            m_BatMaterial.Bind();
            foreach (var transform in m_BatTransforms)
            {
                transform.Bind();
                m_BatGeometry.Draw();
            }
        }
        
        // Called on windows resize event
        public void Resize()
        {
            Gl.Viewport(0, 0, m_Window.Width, m_Window.Height);
            m_Camera.SetProjection(Mat4.Perspective(45.0f, m_Window.Width/(float)m_Window.Height, 0.1f, 1000.0f));
        }

        public void Dispose()
        {
            m_LitProgram.Dispose();
            m_TextureProgram.Dispose();
            
            m_Camera.Dispose();
            m_SunLight.Dispose();
            
            m_DefaultMaterial.Dispose();
            m_BatMaterial.Dispose();
            m_ChromeMaterial.Dispose();
            m_FloorTexture.Dispose();
            
            m_SphereTransform.Dispose();
            m_FloorTransform.Dispose();
            m_SphereGeometry.Dispose();
            
            m_BrickGeometry.Dispose();
            m_FloorGeometry.Dispose();
            m_BatGeometry.Dispose();
        }
    }
}