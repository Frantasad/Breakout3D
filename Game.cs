using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Breakout3D.Framework;
using Breakout3D.Libraries;
using OpenGL;

namespace Breakout3D
{
    public class Game : IDisposable
    {
        public static Game Instance { get; private set; }
        
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
        
        public float BatSpeed { get; private set; }

        public readonly Camera PerspectiveCamera;
        public readonly Camera TopCamera;
        public readonly Light SunLight;
        public readonly GameObject Ball;
        public readonly GameObject Floor;
        public readonly List<GameObject> Bricks;
        public readonly List<GameObject> Bats;
        
        public readonly List<GameObject> AllGameObjects;

        public Game(GameWindow window)
        {
            Instance = this;
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
                new Transform(new Vec3(20, 1.5f, 0), Vec3.Zero, Vec3.Unit * 3),
                new RigidBody(1000f), 
                new SphereCollider(1.5f), 
                new Material(new Vec3(0f), new Vec3(0.55f), new Vec3(0.7f), 32f, 1.0f));
            
            Floor = new GameObject(
                textureProgram,
                GeometryGenerator.CircleFloor(),
                new Transform(Vec3.Zero, Vec3.Zero, Vec3.Unit * 50),
                new Material(new Vec3(0.8f, 0, 0), true, 400.0f, 1.0f),
                new InfinitePlaneCollider(),
                new Texture("./Textures/floor.png"));
            
            Bats = new List<GameObject>();
            var batGeometry = GeometryGenerator.Bat();
            var batMaterial = new Material(Color.RGB(213, 8, 24), false, 200f, 1.0f);
            for (var i = 0; i < 3; i++)
            {
                var transform = new Transform(new Vec3(0, 0, Segment.Bat.Close), Vec3.Zero, Vec3.Unit);
                transform.RotateAround(120 * i * Vec3.Up, Vec3.Zero);
                Bats.Add(new GameObject(
                    litProgram,
                    batGeometry,
                    transform,
                    new SegmentCollider(Segment.Bat), 
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
                    var transform = new Transform(new Vec3(0, y*4, Segment.Brick.Close), Vec3.Zero, Vec3.Unit);
                    transform.RotateAround((i * (360f / 12f)) * Vec3.Up, Vec3.Zero);
                    Bricks.Add(new GameObject(
                        litProgram,
                        brickGeometry,
                        transform,
                        new RigidBody(1000f), 
                        new SegmentCollider(Segment.Brick), 
                        brickMaterials[(i + y) % brickMaterials.Count]));
                }  
            }

            AllGameObjects = new List<GameObject>{Ball, Floor};
            AllGameObjects.AddRange(Bats);
            AllGameObjects.AddRange(Bricks);
            
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
            OnStart();
        }
        
        // Render the whole scene (only rendering)
        public void RenderScene()
        {
            // Clear all and setup
            Gl.Clear(ClearBufferMask.ColorBufferBit);
            Gl.Clear(ClearBufferMask.DepthBufferBit);
            Gl.ClearDepth(1.0);

            foreach (var gameObject in AllGameObjects)
            {
                if(!gameObject.Enabled) continue;
                gameObject.Draw();
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
        
               
        // First frame of the game
        public void OnStart()
        {
            // Start all GameObjects
            foreach (var gameObject in AllGameObjects)
            {
                if(!gameObject.Enabled) continue;
                gameObject.OnStart();
            }

            foreach (var bat in Bats)
            {
                bat.Transform.RotateAround(0.2f * Input.XAxis * Time.DeltaTime * Vec3.Up, Vec3.Zero);
            }

            Ball.Collider.Collision += (other, collision, normal) =>
            {
                if (Bricks.Contains(other.GameObject))
                {
                    // Remove brick
                    other.GameObject.Enabled = false;
                    Bricks.Remove(other.GameObject);
                }
                
                if (Bats.Contains(other.GameObject))
                {
                    // Rotate velocity a bit if bats are moving
                    Ball.RigidBody.Velocity = Mat3.Rotation(Vec3.Up * BatSpeed * 8f) * Ball.RigidBody.Velocity;
                }
            };

            Ball.RigidBody.Velocity = new Vec3(0.02f,0,0);

            Input.KeyPressed += key =>
            {
                if (key != Keys.Up) return;
                
                var index = new Random().Next(Bricks.Count);
                Bricks[index].Enabled = false;
                Bricks.RemoveAt(index);
            }; 
            
            Input.KeyPressed += key =>
            {
                if (key != Keys.Down) return;

                Ball.RigidBody.Velocity =  Mat3.Rotation(Vec3.Up * 10) * Ball.RigidBody.Velocity;
            }; 
            
        }

        // Update of the game (game logic, physics and animations)
        public void OnUpdate()
        {
            BatSpeed = 0.15f * Input.XAxis * Time.DeltaTime;
            foreach (var bat in Bats)
            {
                bat.Transform.RotateAround(BatSpeed * Vec3.Up, Vec3.Zero);
            }

            foreach (var brick in Bricks)
            {
                brick.RigidBody?.ApplyForce(new Vec3(0,-0.1f,0));
            }

            // Update all GameObjects
            foreach (var gameObject in AllGameObjects)
            {
                if(!gameObject.Enabled) continue;
                gameObject.OnUpdate();
            }
            
            // Update all GameObjects later
            foreach (var gameObject in AllGameObjects)
            {
                if(!gameObject.Enabled) continue;
                gameObject.OnLateUpdate();
            }
        }
    }
}