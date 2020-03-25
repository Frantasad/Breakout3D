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
        public static Geometry Sphere()
        {
            var data = LoadFromSimpleObj("./Models/Sphere.obj");
            data.Normals = new List<Vec3>();
            foreach (var vertex in data.Vertices)
            {
                data.Normals.Add(vertex.Normalized);
            }
            return Geometry.GenerateGeometry(data);
        }
        
        public static Geometry CircleFloor(int segmentCount = 64)
        {
            var segmentAngle = 360f / segmentCount;
            var center = Vec3.Zero;
            var firstPoint = Vec3.Forward;

            var data = new GeometryData();
            for (var i = 0; i <= segmentCount; i++)
            {
                var pointA = Mat3.Rotation(Vec3.Up, segmentAngle*i) * firstPoint;
                var pointB = Mat3.Rotation(Vec3.Up, segmentAngle*(i+1)) * firstPoint;
                
                data.Vertices.Add(center);
                data.TexCoords.Add(new Vec2(center.X + 1f, center.Z + 1f)*0.5f);
                data.Vertices.Add(pointA);
                data.TexCoords.Add(new Vec2(pointA.X + 1f , pointA.Z + 1f)*0.5f);
                data.Vertices.Add(pointB);
                data.TexCoords.Add(new Vec2(pointB.X + 1f, pointB.Z + 1f)*0.5f);
                data.Normals.Add(Vec3.Up, Vec3.Up, Vec3.Up);
            }
            return Geometry.GenerateGeometry(data);
        }
        
        public static Geometry Brick()
        {
            var points = new []{new Vec3(0, 0, 12), new Vec3(0, 0, 15), new Vec3(0, 4, 15), new Vec3(0, 4, 12)};
            var rPoints = Array.ConvertAll(points, vertex => Mat3.Rotation(Vec3.Up, 15) * vertex);
            var lPoints = Array.ConvertAll(points, vertex => Mat3.Rotation(Vec3.Up, -15) * vertex);
            var data = new GeometryData();
            
            GenerateQuad(rPoints, ref data.Vertices, ref data.Normals); // Right face
            GenerateQuad(lPoints.Reverse().ToArray(), ref data.Vertices, ref data.Normals); // Left face
            GenerateQuad(new []{rPoints[3], rPoints[2], lPoints[2], lPoints[3]}, ref data.Vertices, ref data.Normals); // Top face
            GenerateQuad(new []{lPoints[0], lPoints[1], rPoints[1], rPoints[0]}, ref data.Vertices, ref data.Normals); // Bottom face
            GenerateQuad(new []{rPoints[0], rPoints[3], lPoints[3], lPoints[0]}, ref data.Vertices, ref data.Normals); // Front face
            GenerateQuad(new []{lPoints[1], lPoints[2], rPoints[2], rPoints[1]}, ref data.Vertices, ref data.Normals); // Back face

            for (var i = 0; i < data.Vertices.Count; i++)
            {
                data.Vertices[i] -= new Vec3(0, 0, 12);
            }
            return Geometry.GenerateGeometry(data);
        }

        public static Geometry Bat(int segmentCount = 6, float angleSize = 30)
        {
            var points = new []{new Vec3(0, 0, 43), new Vec3(0, 0, 46), new Vec3(0, 3, 46), new Vec3(0, 3, 43)};
            points = Array.ConvertAll(points, vertex => Mat3.Rotation(Vec3.Up, -angleSize/2) * vertex);
            var data = new GeometryData();

            var segmentAngle = angleSize / segmentCount;
            for (var i = 0; i < segmentCount; i++)
            {
                var rPoints = Array.ConvertAll(points, vertex => Mat3.Rotation(Vec3.Up, segmentAngle * (i+1)) * vertex);
                var lPoints = Array.ConvertAll(points, vertex => Mat3.Rotation(Vec3.Up, segmentAngle * i) * vertex);
                GenerateQuad(new []{rPoints[3], rPoints[2], lPoints[2], lPoints[3]}, ref data.Vertices, ref data.Normals); // Top face
                GenerateQuad(new []{lPoints[0], lPoints[1], rPoints[1], rPoints[0]}, ref data.Vertices, ref data.Normals); // Bottom face
                var tmp = new List<Vec3>();
                Vec3 normR, normL;
                GenerateQuad(new []{rPoints[0], rPoints[3], lPoints[3], lPoints[0]}, ref data.Vertices, ref tmp); // Front face
                normR = (-rPoints[0]).Normalized;
                normL = (-lPoints[0]).Normalized;
                data.Normals.Add(normR, normR, normL, normR, normL, normL);
                GenerateQuad(new []{lPoints[1], lPoints[2], rPoints[2], rPoints[1]}, ref data.Vertices, ref tmp); // Back face
                normR = rPoints[0].Normalized;
                normL = lPoints[0].Normalized;
                data.Normals.Add(normL, normL, normR, normL, normR, normR);
                if (i == segmentCount - 1) GenerateQuad(rPoints, ref data.Vertices, ref data.Normals); // Right face
                if (i == 0) GenerateQuad(lPoints.Reverse().ToArray(), ref data.Vertices, ref data.Normals); // Left face
            }
            
            for (var i = 0; i < data.Vertices.Count; i++)
            {
                data.Vertices[i] -= new Vec3(0, 0, 40);
            }
            return Geometry.GenerateGeometry(data);
        }
        
        private static void GenerateQuad(Vec3[] points, ref List<Vec3> vertices, ref List<Vec3> normals)
        {
            if (points.Length != 4) throw new ArgumentOutOfRangeException(nameof(points),"Quad must have 4 vertices!");
            GenerateTriangle(new []{points[0], points[1], points[2]}, ref vertices, ref normals);
            GenerateTriangle(new []{points[0], points[2], points[3]}, ref vertices, ref normals);
        }
        
        private static void GenerateTriangle(Vec3[] points, ref List<Vec3> vertices, ref List<Vec3> normals)
        {
            if (points.Length != 3) throw new ArgumentOutOfRangeException(nameof(points),"Triangle must have 3 vertices!");
            var a = points[0];
            var b = points[1];
            var c = points[2];
            vertices.Add(a, b, c);
            var p = b - a;
            var q = c - a;
            var norm = p.Cross(q);
            normals.Add(norm, norm, norm);
        }

        public static GeometryData LoadFromSimpleObj(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException($"File \"{filename}\" does not exist!");
            }
            
            var objVertices = new List<Vec3>();
            var objTexCoords = new List<Vec2>();
            
            var vertices = new List<Vec3>();
            var texCoords = new List<Vec2>();

            using (var sr = new StreamReader(filename))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (line == null) break;

                    if (line.StartsWith("v "))
                    {
                        var parts = line.Split(' ');
                        objVertices.Add(new Vec3(
                            float.Parse(parts[1], CultureInfo.InvariantCulture),
                            float.Parse(parts[2], CultureInfo.InvariantCulture),
                            float.Parse(parts[3], CultureInfo.InvariantCulture)));
                    }

                    if (line.StartsWith("vt "))
                    {
                        var parts = line.Split(' ');
                        objTexCoords.Add(new Vec2(
                            float.Parse(parts[1], CultureInfo.InvariantCulture),
                            float.Parse(parts[2], CultureInfo.InvariantCulture)));
                    }
                    
                    if (line.StartsWith("f "))
                    {
                        var parts = line.Split(' ');
                        
                        var v1Indices = Array.ConvertAll(parts[1].Split('/'), int.Parse);
                        var v2Indices = Array.ConvertAll(parts[2].Split('/'), int.Parse);
                        var v3Indices = Array.ConvertAll(parts[3].Split('/'), int.Parse);

                        vertices.Add(objVertices[v1Indices[0] - 1], objVertices[v2Indices[0] - 1], objVertices[v3Indices[0] - 1]);
                    }
                }
            }
            return new GeometryData()
            {
                Vertices = vertices,
                Normals = new List<Vec3>(),
                TexCoords = new List<Vec2>(),
            };
        }
        
        public static Geometry GenerateFromSimpleObj(string filename, PrimitiveType primitiveType = PrimitiveType.Triangles)
        {
            return Geometry.GenerateGeometry(LoadFromSimpleObj(filename), PrimitiveType.TriangleFan);
        }
    }
}