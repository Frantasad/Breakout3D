using System;
using System.Collections.Generic;
using System.Linq;
using Breakout3D.Framework;
using Breakout3D.Libraries;
using OpenGL;

namespace Breakout3D
{
    public class Game : IDisposable
    {
        private  GameWindow m_Window;
        
        private readonly Camera m_Camera;
        private readonly Light m_SunLight;
        
        private int m_Score;
        public int Score
        {
            get => m_Score;
            set
            { 
                m_Score = value;
                m_Window.UpdateScore(value);
            }
        }
        
        private int m_Lives;
        public int Lives
        {
            get => m_Lives;
            set
            { 
                m_Lives = value;
                m_Window.UpdateLives(value);
            }
        }

        private GameObject m_Ball;
        private GameObject m_Floor;
        private List<GameObject> m_Bricks;
        private List<GameObject> m_Bats;

        public Game(GameWindow window)
        {
            m_Window = window;
            
            // Init shaders
            var litProgram = new ShaderProgram();
            litProgram.AddShader(ShaderType.VertexShader, "./Shaders/lit_vertex.glsl");
            litProgram.AddShader(ShaderType.FragmentShader, "./Shaders/lit_fragment.glsl");
            litProgram.Link();

            var textureProgram = new ShaderProgram();
            textureProgram.AddShader(ShaderType.VertexShader, "./Shaders/texture_vertex.glsl");
            textureProgram.AddShader(ShaderType.FragmentShader, "./Shaders/texture_fragment.glsl");
            textureProgram.Link();

            // Init Light
            m_SunLight = new Light(new Vec3(5, 10, 0),
                new Vec3(0.3f, 0.3f, 0.3f),
                new Vec3(0.7f, 0.7f, 0.7f),
                new Vec3(0.7f, 0.7f, 0.7f));

            // Init Camera
            m_Camera = new Camera();
            Resize();
            m_Camera.LookAt(new Vec3(0, 100, 100f), new Vec3(0, 0, 0), Vec3.Up);

            // Init game objects
            m_Ball = new GameObject(
                litProgram, 
                GeometryGenerator.Sphere(), 
                new Transform(new Vec3(0, 1.5f, 0), Vec3.Zero, Vec3.Unit * 3),
                new Material(new Vec3(0f), new Vec3(0.55f), new Vec3(0.7f), 32f, 1.0f));
            
            m_Floor = new GameObject(
                textureProgram,
                GeometryGenerator.CircleFloor(),
                new Transform(Vec3.Zero, Vec3.Zero, Vec3.Unit * 50),
                new Material(new Vec3(0.8f, 0, 0), true, 200.0f, 1.0f),
                new Texture("./Textures/floor.png"));
            
            m_Bats = new List<GameObject>();
            var batGeometry = GeometryGenerator.Bat();
            var batMaterial = new Material(Color.FromRGB(213, 8, 24), false, 200f, 1.0f);
            for (var i = 0; i < 3; i++)
            {
                var transform = new Transform(new Vec3(0, 0, 40), Vec3.Zero, Vec3.Unit);
                transform.RotateAround(120 * i * Vec3.Up, Vec3.Zero);
                m_Bats.Add(new GameObject(
                    litProgram,
                    batGeometry,
                    transform,
                    batMaterial));
            }
            
            var brickColors = new []
            {
                Color.FromRGB(54, 88, 229), 
                Color.FromRGB(52, 222, 157), 
                Color.FromRGB(238, 198, 28)
            };
            m_Bricks = new List<GameObject>();
            var brickGeometry = GeometryGenerator.Brick();
            var brickMaterials = brickColors.Select(color => new Material(color, true, 200.0f, 1.0f)).ToList();
            for (var i = 0; i < 12; i++)
            {
                var transform = new Transform(new Vec3(0, 0, 12), Vec3.Zero, Vec3.Unit);
                transform.RotateAround(i * (360 /(float) 12) * Vec3.Up, Vec3.Zero);
                m_Bricks.Add(new GameObject(
                    litProgram,
                    brickGeometry,
                    transform,
                    brickMaterials[i % brickMaterials.Count]));
            }

            // Init GL environment
            var clearColor = Color.FromRGB(25, 25, 30);
            Gl.ClearColor(clearColor.X, clearColor.Y, clearColor.Z, 1);
            Gl.Enable(EnableCap.DepthTest);
            // Gl.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
        }

        // Update of the game - here should be all the physics
        public void UpdateScene()
        {
            foreach (var bat in m_Bats)
            {
                bat.Transform.RotateAround(0.2f * Input.XAxis * Time.DeltaTime *Vec3.Up, Vec3.Zero);
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

            m_Ball.Draw();
            m_Floor.Draw();
            foreach (var bat in m_Bats)
            {
                bat.Draw();
            }
            foreach (var brick in m_Bricks)
            {
                brick.Draw();
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
            m_Camera.Dispose();
            m_SunLight.Dispose();

            m_Ball.Dispose();
            m_Floor.Dispose();
            foreach (var bat in m_Bats)
            {
                bat.Dispose();
            }
            foreach (var brick in m_Bricks)
            {
                brick.Dispose();
            }
        }
    }
}