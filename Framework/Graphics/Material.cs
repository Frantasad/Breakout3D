﻿using System;
using System.Runtime.InteropServices;
using Breakout3D.Libraries;
using OpenGL;

namespace Breakout3D.Framework
{
    public struct PhongMaterial
    {
        public Vec3 Ambient;
        public float Alpha;
        public Vec3 Diffuse;
        public float Shininess;
        public Vec3 Specular;
        
        public PhongMaterial(Vec3 ambient, Vec3 diffuse, Vec3 specular, float shininess, float alpha)
        {
            Ambient = ambient;
            Diffuse = diffuse;
            Specular = specular;
            Shininess = shininess;
            Alpha = alpha;
        }
    }

    public class Material : UniformBuffer<PhongMaterial>
    {
        public override uint Binding => Buffers.MATERIAL_BINDING;
        
        public Material(): base(new PhongMaterial(Vec3.Unit, Vec3.Unit, Vec3.Unit, 0.0f, 1.0f)){}

        public Material(Vec3 ambient, Vec3 diffuse, Vec3 specular, float shininess, float alpha)
            : base(new PhongMaterial(ambient, diffuse, specular, shininess, alpha)){}
        
        public Material(Vec3 color, bool whiteSpecular, float shininess, float alpha)
            : base(new PhongMaterial(
                color, 
                color, 
                (Math.Abs(shininess) < 0.0001f) ? Vec3.Zero : (whiteSpecular ? Vec3.Unit : color), 
                shininess, 
                alpha)){}
    }
}