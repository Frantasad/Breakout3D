using System;
using System.Collections.Generic;
using System.Linq;
using OpenGL;

namespace Breakout3D.Framework
{
    public class GameObject : IDisposable
    {
        public bool Enabled { get; set; }
        
        public Transform Transform { get; }
        public ShaderProgram? ShaderProgram { get; }
        public Material? Material { get; }
        public Geometry? Geometry { get; }
        public RigidBody? RigidBody { get; }
        public Texture? Texture { get; }
        public Collider? Collider { get; }

        public List<Component> Components { get; }

        public GameObject(params Component[] components)
        {
            Components = components.ToList();
            Transform = GetComponent<Transform>()!;
            ShaderProgram = GetComponent<ShaderProgram>();
            Material = GetComponent<Material>();
            Geometry = GetComponent<Geometry>();
            RigidBody = GetComponent<RigidBody>();
            Texture = GetComponent<Texture>();
            Collider = GetComponent<Collider>();

            foreach (var component in Components)
            {
                component.GameObject = this;
            }

            Enabled = true;
        }

        public void AddComponent<T>(T component) where T : Component
        {
            Components.Add(component);
        }
        
        public void RemoveComponent<T>(T component) where T : Component
        {
            Components.Remove(component);
        }
        
        public T? GetComponent<T>() where T : Component
        {
            foreach (var component in Components)
            {
                if (!(component is T match)) continue;
                return match;
            }
            return null;
        }

        public void Draw()
        {
            ShaderProgram.Use();
            Transform.Bind(); 
            Texture?.Bind(TextureUnit.Texture0);
            Material?.Bind();
            Geometry?.Bind();
            Geometry?.Draw();
        }

        public void Dispose()
        {
            ShaderProgram.Dispose();
            foreach (var component in Components)
            {
                component.Dispose();
            }
        }

        public void OnStart()
        {
            foreach (var component in Components)
            {
                component.OnStart();
            }
        }
        
        public void OnUpdate()
        {
            foreach (var component in Components)
            {
                component.OnUpdate();
            }
        }

        public void OnLateUpdate()
        {
            foreach (var component in Components)
            {
                component.OnLateUpdate();
            }
        }
    }
}