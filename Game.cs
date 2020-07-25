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
        private readonly GameWindow m_Window;
        
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

        private Camera m_CurrentCamera;
        public Camera CurrentCamera
        {
            get => m_CurrentCamera;
            set
            {
                m_CurrentCamera = value;
                Resize(m_CurrentCamera);
                CurrentCamera.Bind();
            }
        }

        public readonly Camera PerspectiveCamera;
        public readonly Camera TopCamera;
        public readonly Light SunLight;
        public readonly GameObject Ball;
        public readonly GameObject Floor;
        public readonly List<GameObject> Bricks;
        public readonly List<GameObject> Bats;

        public Game(GameWindow window)
        {
            var a = new Ray(new Vec3(0,5,0), Vec3.Up);
            var b = new Ray(new Vec3(0,0,0), Vec3.Right);
            Console.Out.WriteLine(a.DistanceTo(b));
            
            
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
            SunLight = new Light(new Vec3(5, 10, 0),
                new Vec3(0.3f, 0.3f, 0.3f),
                new Vec3(0.8f, 0.8f, 0.8f),
                new Vec3(0.7f, 0.7f, 0.7f));

            // Init Camera
            PerspectiveCamera = new Camera();
            Resize(PerspectiveCamera);
            PerspectiveCamera.LookAt(new Vec3(0, 90, 70), new Vec3(0, 0, 10), Vec3.Up);
            
            TopCamera = new Camera();
            Resize(TopCamera);
            TopCamera.LookAt(new Vec3(0, 130, 0), new Vec3(0, 0, 0), -Vec3.Forward);

            // Init game objects
            Ball = new GameObject(
                litProgram, 
                GeometryGenerator.Sphere(), 
                new Transform(new Vec3(0, 1.5f, 0), Vec3.Zero, Vec3.Unit * 3),
                new Material(new Vec3(0f), new Vec3(0.55f), new Vec3(0.7f), 32f, 1.0f));
            
            Floor = new GameObject(
                textureProgram,
                GeometryGenerator.CircleFloor(),
                new Transform(Vec3.Zero, Vec3.Zero, Vec3.Unit * 50),
                new Material(new Vec3(0.8f, 0, 0), true, 400.0f, 1.0f),
                new Texture("./Textures/floor.png"));
            
            Bats = new List<GameObject>();
            var batGeometry = GeometryGenerator.Bat();
            var batMaterial = new Material(Color.RGB(213, 8, 24), false, 200f, 1.0f);
            for (var i = 0; i < 3; i++)
            {
                var transform = new Transform(new Vec3(0, 0, 40), Vec3.Zero, Vec3.Unit);
                transform.RotateAround(120 * i * Vec3.Up, Vec3.Zero);
                Bats.Add(new GameObject(
                    litProgram,
                    batGeometry,
                    transform,
                    batMaterial));
            }

            Bricks = new List<GameObject>();
            var brickGeometry = GeometryGenerator.Brick();
            var brickMaterials = new [] {Color.RGB(54, 88, 229), Color.RGB(238, 198, 28)}
                .Select(color => new Material(color, true, 200.0f, 0.8f)).ToList();
            for (var y = 0; y < 3; y++)
            {
                for (var i = 0; i < 12; i++)
                {
                    var transform = new Transform(new Vec3(0, y*4, 12), Vec3.Zero, Vec3.Unit);
                    transform.RotateAround(i * (360 /(float) 12) * Vec3.Up, Vec3.Zero);
                    Bricks.Add(new GameObject(
                        litProgram,
                        brickGeometry,
                        transform,
                        brickMaterials[(i + y) % brickMaterials.Count]));
                }  
            }
            
            // Init GL environment
            var clearColor = Color.RGB(25, 25, 30);
            Gl.ClearColor(clearColor.X, clearColor.Y, clearColor.Z, 1);
            Gl.Enable(EnableCap.DepthTest);
            Gl.Enable(EnableCap.Blend);
            Gl.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            Gl.Enable(EnableCap.CullFace);
            Gl.CullFace(CullFaceMode.Back);
            Gl.PolygonMode(MaterialFace.Front, PolygonMode.Fill);

            CurrentCamera = PerspectiveCamera;
            SunLight.Bind();
        }

        // Update of the game (game logic, physics and animations)
        public void UpdateScene()
        {
            foreach (var bat in Bats)
            {
                bat.Transform.RotateAround(0.2f * Input.XAxis * Time.DeltaTime * Vec3.Up, Vec3.Zero);
            }
        }

        // Render the whole scene (only rendering)
        public void RenderScene()
        {
            // Clear all and setup
            Gl.Clear(ClearBufferMask.ColorBufferBit);
            Gl.Clear(ClearBufferMask.DepthBufferBit);
            Gl.ClearDepth(1.0);
            
            Ball.Draw();
            Floor.Draw();
            foreach (var bat in Bats)
            {
                bat.Draw();
            }
            foreach (var brick in Bricks)
            {
                brick.Draw();
            }
        }
        
        // Called on windows resize event
        public void Resize(Camera camera)
        {
            camera.SetPerspective(45.0f, m_Window.OpenGlWindow.Width, m_Window.OpenGlWindow.Height, 0.1f, 1000.0f);
        }

        public void Dispose()
        {
            PerspectiveCamera.Dispose();
            SunLight.Dispose();
            Ball.Dispose();
            Floor.Dispose();
            Bats.Dispose();
            Bricks.Dispose();
        }
    }
}