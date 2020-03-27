using System;
using OpenGL;

namespace Breakout3D.Framework
{
    public class GameObject : IDisposable
    {
        public ShaderProgram ShaderProgram { get;}
        public Geometry Geometry { get;}
        public Transform Transform { get;}
        public Material Material { get;}
        public Texture Texture { get;}

        public GameObject(ShaderProgram shaderProgram, Geometry geometry, Transform transform, Material material, Texture texture = null)
        {
            ShaderProgram = shaderProgram;
            Geometry = geometry;
            Transform = transform;
            Material = material;
            Texture = texture;
        }

        public void Draw()
        {
            ShaderProgram.Use();    
            Transform.Bind();
            Texture?.Bind(TextureUnit.Texture0);
            Material.Bind();
            Geometry.Bind();
            Geometry.Draw();
        }

        public void Dispose()
        {
            ShaderProgram.Dispose();
            Geometry.Dispose();
            Transform.Dispose();
            Material.Dispose();
            Texture.Dispose();
        }
    }
}