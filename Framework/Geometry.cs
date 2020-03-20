using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using OpenGL;

namespace Breakout3D.Framework
{
    public struct GeometryData
    {
        public List<float> Vertices;
        public List<float> TexCoords;
        public List<float> Normals;
    }
    
    public class Geometry : IDisposable
    {
        public const int POSITION_LOCATION = 0;
        public const int NORMAL_LOCATION = 1;
        public const int TEX_COORD_LOCATION = 2;

        private uint m_VertexBuffer;
        private uint m_NormalBuffer;
        private uint m_TexCoordBuffer;
        
        public uint VAO { get; private set; }
        private PrimitiveType m_PrimitiveType = PrimitiveType.Triangles;
        private int m_VerticesCount;
        
        public Geometry(PrimitiveType primitiveType = PrimitiveType.Triangles)
        {
            m_PrimitiveType = primitiveType;
        }
        
        public void Bind()
        {
            Gl.BindVertexArray(VAO);
        }

        public void Draw()
        {
            Gl.DrawArrays(m_PrimitiveType, 0, m_VerticesCount);
        }

        public void Dispose()
        {
            Gl.DeleteBuffers(m_VertexBuffer, m_NormalBuffer, m_TexCoordBuffer);
            Gl.DeleteVertexArrays(VAO);
        }

        public static Geometry GenerateGeometry(GeometryData geometryData,
            PrimitiveType primitiveType = PrimitiveType.Triangles)
        {
            return GenerateGeometry(geometryData.Vertices.ToArray(), geometryData.Normals.ToArray(), geometryData.TexCoords.ToArray(), primitiveType);
        }

        public static Geometry GenerateGeometry(float[] vertices, float[] normals, float[] texCoords, PrimitiveType primitiveType = PrimitiveType.Triangles)
        {
            if (vertices.Length%3 != 0 || normals.Length%3 != 0 || texCoords.Length%2 != 0)
            {
                throw new ArgumentException($"Invalid count of input variables");
            }
            var geometry = new Geometry(primitiveType);
            geometry.m_VerticesCount = vertices.Length/3;

            geometry.VAO = Gl.GenVertexArray();
            Gl.BindVertexArray(geometry.VAO);

            geometry.m_VertexBuffer = Gl.GenBuffer();
            Gl.BindBuffer(BufferTarget.ArrayBuffer, geometry.m_VertexBuffer);
            Gl.BufferData(BufferTarget.ArrayBuffer, (uint) (sizeof(float) * vertices.Length), vertices, BufferUsage.StaticDraw);
            Gl.VertexAttribPointer(POSITION_LOCATION, 3, VertexAttribType.Float, false, 0, IntPtr.Zero);
            Gl.EnableVertexAttribArray(POSITION_LOCATION);

            geometry.m_NormalBuffer = Gl.GenBuffer();
            Gl.BindBuffer(BufferTarget.ArrayBuffer, geometry.m_NormalBuffer);
            Gl.BufferData(BufferTarget.ArrayBuffer, (uint) (sizeof(float) * normals.Length), normals, BufferUsage.StaticDraw);
            Gl.VertexAttribPointer(NORMAL_LOCATION, 3, VertexAttribType.Float, false, 0, IntPtr.Zero);
            Gl.EnableVertexAttribArray(NORMAL_LOCATION);

            geometry.m_TexCoordBuffer = Gl.GenBuffer();
            Gl.BindBuffer(BufferTarget.ArrayBuffer, geometry.m_TexCoordBuffer);
            Gl.BufferData(BufferTarget.ArrayBuffer, (uint) (sizeof(float) * texCoords.Length), texCoords, BufferUsage.StaticDraw);
            Gl.VertexAttribPointer(TEX_COORD_LOCATION, 2, VertexAttribType.Float, false, 0, IntPtr.Zero);
            Gl.EnableVertexAttribArray(TEX_COORD_LOCATION);
            
            Gl.BindBuffer(BufferTarget.ArrayBuffer, 0);
            return geometry;
        }
    }
}