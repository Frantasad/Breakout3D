using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Breakout3D.Libraries;
using OpenGL;

namespace Breakout3D.Framework
{
    public static class GeometryGenerator
    {
        public static Geometry SingleTriangle()
        {
            var vertices = new[]
            {
                 0.0f,  0.5f, 0,
                -0.5f, -0.5f, 0,
                 0.5f, -0.5f, 0
            };
            var normals = new[]
            {
                0f, 0f, 1f,
                0f, 0f, 1f,
                0f, 0f, 1f
            };
            var texCoords = new[]
            {
                0.5f, 1.0f,
                0.0f, 0.0f,
                1.0f, 0.0f
            };
            return Geometry.GenerateGeometry(vertices, normals, texCoords);
        }
        
        public static Geometry Sphere()
        {
            var data = LoadFromSimpleObj("./Models/Sphere.obj");
            
            for(var i = 0; i < data.Vertices.Count; i += 3)
            {
                var normal = new Vec3(data.Vertices[i], data.Vertices[i+1], data.Vertices[i+2]);
                data.Normals.AddRange(new []{normal.Normalized.X, normal.Normalized.Y, normal.Normalized.Z});
            }
            return Geometry.GenerateGeometry(data);
        }
        
        public static Geometry CircleFloor()
        {
            var data = LoadFromSimpleObj("./Models/CircleFloor.obj");
            
            for(var i = 0; i < data.Vertices.Count; i += 3)
            {
                data.Normals.AddRange(new []{0f, 1f, 0f});
                data.TexCoords.AddRange(new []{data.Vertices[i] + 0.5f, data.Vertices[i+2] + 0.5f});
            }
            return Geometry.GenerateGeometry(data);
        }

        public static GeometryData LoadFromSimpleObj(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException($"File \"{filename}\" does not exist!");
            }

            var objVertices = new List<float>();
            var objTexCoords = new List<float>();
            
            var vertices = new List<float>();
            var texCoords = new List<float>();

            using (var sr = new StreamReader(filename))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (line == null) break;

                    if (line.StartsWith("v "))
                    {
                        var parts = line.Split(' ');
                        objVertices.Add(float.Parse(parts[1], CultureInfo.InvariantCulture));
                        objVertices.Add(float.Parse(parts[2], CultureInfo.InvariantCulture));
                        objVertices.Add(float.Parse(parts[3], CultureInfo.InvariantCulture));
                    }

                    if (line.StartsWith("vt "))
                    {
                        var parts = line.Split(' ');
                        objTexCoords.Add(float.Parse(parts[1], CultureInfo.InvariantCulture));
                        objTexCoords.Add(float.Parse(parts[2], CultureInfo.InvariantCulture));
                    }
                    
                    if (line.StartsWith("f "))
                    {
                        var parts = line.Split(' ');
                        
                        var v1Indices = Array.ConvertAll(parts[1].Split('/'), int.Parse);
                        var v2Indices = Array.ConvertAll(parts[2].Split('/'), int.Parse);
                        var v3Indices = Array.ConvertAll(parts[3].Split('/'), int.Parse);

                        var v1i = v1Indices[0] - 1;
                        var v2i = v2Indices[0] - 1;
                        var v3i = v3Indices[0] - 1;
                        
                        vertices.AddRange(new []{objVertices[v1i*3], objVertices[v1i*3 + 1], objVertices[v1i*3 + 2]}); 
                        vertices.AddRange(new []{objVertices[v2i*3], objVertices[v2i*3 + 1], objVertices[v2i*3 + 2]}); 
                        vertices.AddRange(new []{objVertices[v3i*3], objVertices[v3i*3 + 1], objVertices[v3i*3 + 2]});

                        if (objTexCoords.Count > 0)
                        {
                            var v1t = v1Indices[0] - 1;
                            var v2t = v2Indices[0] - 1;

                            texCoords.AddRange(new []{objTexCoords[v1t*2], objTexCoords[v1t*2 + 1]}); 
                            texCoords.AddRange(new []{objTexCoords[v2t*2], objTexCoords[v2t*2 + 1]}); 
                        }
                    }
                }
            }
            return new GeometryData()
            {
                Vertices = vertices,
                Normals = new List<float>(),
                TexCoords = texCoords
            };
        }
        
        public static Geometry GenerateFromSimpleObj(string filename, PrimitiveType primitiveType = PrimitiveType.Triangles)
        {
            return Geometry.GenerateGeometry(LoadFromSimpleObj(filename), PrimitiveType.TriangleFan);
        }
    }
}