using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using OpenGL;

namespace Breakout3D.Framework
{
    public abstract class Geometry : IDisposable
    {
        public const int POSITION_LOCATION = 0;
        public const int NORMAL_LOCATION = 1;
        public const int TEX_COORD_LOCATION = 2;

        public uint VAO { get; protected set; }

        protected PrimitiveType m_PrimitiveType = PrimitiveType.Triangles;

        public void Bind()
        {
            Gl.BindVertexArray(VAO);
        }

        public abstract void Draw();
        public abstract void Dispose();
    }

    public class TriangleGeometry : Geometry
    {
        public uint VertexBuffer { get; private set; }
        public uint NormalBuffer { get; private set; }
        public uint TexCoordBuffer { get; private set; }

        int VerticesCount;

        public TriangleGeometry(PrimitiveType primitiveType = PrimitiveType.Triangles)
        {
            m_PrimitiveType = primitiveType;
        }

        public override void Draw()
        {
            Gl.DrawArrays(m_PrimitiveType, 0, VerticesCount);
        }

        public override void Dispose()
        {
            Gl.DeleteBuffers(VertexBuffer, NormalBuffer, TexCoordBuffer);
            Gl.DeleteVertexArrays(VAO);
        }

        public static TriangleGeometry CreateSingleTriangle()
        {
            var triangleVertices = new[]
            {
                0.0f, 0.5f, 0,
                -0.5f, -0.5f, 0,
                0.5f, -0.5f, 0
            };
            var triangleNormals = new[]
            {
                0f, 0f, 1f,
                0f, 0f, 1f,
                0f, 0f, 1f
            };
            var triangleTexCoords = new[]
            {
                0.5f, 1.0f,
                0.0f, 0.0f,
                1.0f, 0.0f
            };

            var geometry = new TriangleGeometry();

            geometry.VerticesCount = 3;

            geometry.VAO = Gl.GenVertexArray();
            Gl.BindVertexArray(geometry.VAO);

            geometry.VertexBuffer = Gl.GenBuffer();
            Gl.BindBuffer(BufferTarget.ArrayBuffer, geometry.VertexBuffer);
            Gl.BufferData(BufferTarget.ArrayBuffer, (uint) (sizeof(float) * triangleVertices.Length), triangleVertices,
                BufferUsage.StaticDraw);
            Gl.VertexAttribPointer(POSITION_LOCATION, 3, VertexAttribType.Float, false, 0, IntPtr.Zero);
            Gl.EnableVertexAttribArray(POSITION_LOCATION);

            geometry.NormalBuffer = Gl.GenBuffer();
            Gl.BindBuffer(BufferTarget.ArrayBuffer, geometry.NormalBuffer);
            Gl.BufferData(BufferTarget.ArrayBuffer, (uint) (sizeof(float) * triangleNormals.Length), triangleNormals,
                BufferUsage.StaticDraw);
            Gl.VertexAttribPointer(NORMAL_LOCATION, 3, VertexAttribType.Float, false, 0, IntPtr.Zero);
            Gl.EnableVertexAttribArray(NORMAL_LOCATION);

            geometry.TexCoordBuffer = Gl.GenBuffer();
            Gl.BindBuffer(BufferTarget.ArrayBuffer, geometry.TexCoordBuffer);
            Gl.BufferData(BufferTarget.ArrayBuffer, (uint) (sizeof(float) * triangleTexCoords.Length),
                triangleTexCoords, BufferUsage.StaticDraw);
            Gl.VertexAttribPointer(TEX_COORD_LOCATION, 2, VertexAttribType.Float, false, 0, IntPtr.Zero);
            Gl.EnableVertexAttribArray(TEX_COORD_LOCATION);

            return geometry;
        }

        public static TriangleGeometry LoadSimpleObj(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException($"File \"{filename}\" does not exist!");
            }

            var triangleVertices = new List<float>();
            var triangleNormals = new List<float>();
            var triangleTexCoords = new List<float>();

            using (var sr = new StreamReader(filename))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (line == null) break;

                    if (line.StartsWith("v"))
                    {
                        var parts = line.Split(' ');
                        triangleVertices.Add(float.Parse(parts[1], CultureInfo.InvariantCulture));
                        triangleVertices.Add(float.Parse(parts[2], CultureInfo.InvariantCulture));
                        triangleVertices.Add(float.Parse(parts[3], CultureInfo.InvariantCulture));
                    }

                    if (line.StartsWith("vt"))
                    {
                        var parts = line.Split(' ');
                        triangleTexCoords.Add(float.Parse(parts[1], CultureInfo.InvariantCulture));
                        triangleTexCoords.Add(float.Parse(parts[2], CultureInfo.InvariantCulture));
                    }
                }
            }

            var geometry = new TriangleGeometry(PrimitiveType.TriangleStrip);
            geometry.VerticesCount = triangleVertices.Count/3;

            geometry.VAO = Gl.GenVertexArray();
            Gl.BindVertexArray(geometry.VAO);

            geometry.VertexBuffer = Gl.GenBuffer();
            Gl.BindBuffer(BufferTarget.ArrayBuffer, geometry.VertexBuffer);
            Gl.BufferData(BufferTarget.ArrayBuffer, (uint) (sizeof(float) * triangleVertices.Count),
                triangleVertices.ToArray(), BufferUsage.StaticDraw);
            Gl.VertexAttribPointer(POSITION_LOCATION, 3, VertexAttribType.Float, false, 0, IntPtr.Zero);
            Gl.EnableVertexAttribArray(POSITION_LOCATION);

            geometry.NormalBuffer = Gl.GenBuffer();
            Gl.BindBuffer(BufferTarget.ArrayBuffer, geometry.NormalBuffer);
            Gl.BufferData(BufferTarget.ArrayBuffer, (uint) (sizeof(float) * triangleNormals.Count),
                triangleNormals.ToArray(), BufferUsage.StaticDraw);
            Gl.VertexAttribPointer(NORMAL_LOCATION, 3, VertexAttribType.Float, false, 0, IntPtr.Zero);
            Gl.EnableVertexAttribArray(NORMAL_LOCATION);

            geometry.TexCoordBuffer = Gl.GenBuffer();
            Gl.BindBuffer(BufferTarget.ArrayBuffer, geometry.TexCoordBuffer);
            Gl.BufferData(BufferTarget.ArrayBuffer, (uint) (sizeof(float) * triangleTexCoords.Count),
                triangleTexCoords.ToArray(), BufferUsage.StaticDraw);
            Gl.VertexAttribPointer(TEX_COORD_LOCATION, 2, VertexAttribType.Float, false, 0, IntPtr.Zero);
            Gl.EnableVertexAttribArray(TEX_COORD_LOCATION);

            return geometry;
        }
    }
}