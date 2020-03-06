using System.Collections.Generic;
using System.Globalization;
using System.IO;
using OpenGL;

namespace Breakout3D.Framework
{
    public static class GeometryGenerator
    {
        public static Geometry SingleTriangle()
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
            return Geometry.GenerateGeometry(triangleVertices, triangleNormals, triangleTexCoords);
        }

        public static Geometry LoadSimpleObj(string filename, PrimitiveType primitiveType = PrimitiveType.Triangles)
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
            return Geometry.GenerateGeometry(triangleVertices.ToArray() ,triangleNormals.ToArray(), triangleTexCoords.ToArray(), PrimitiveType.TriangleFan);
        }
    }
}