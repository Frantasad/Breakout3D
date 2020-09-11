using System;
using System.Collections.Generic;
using System.Text;
using OpenGL;

namespace Breakout3D.Framework
{
    public class ShaderProgram : Component
    {
        public uint ProgramId { get; private set; }
        public List<Shader> Shaders { get;} = new List<Shader>();
        
        public int LocationPosition;

        public ShaderProgram()
        {
            ProgramId = Gl.CreateProgram();
        }

        public bool AddShader(ShaderType shaderType, string fileName)
        {
            var shader = new Shader(shaderType, fileName);
            if (!shader.Init())
            {
                return false;
            }
            Shaders.Add(shader);
            Gl.AttachShader(ProgramId, shader.ShaderId);
            return true;
        }

        public void Use()
        {
            Gl.UseProgram(ProgramId);
        }
        
        public bool Link()
        {
            Gl.LinkProgram(ProgramId);
            Gl.GetProgram(ProgramId, ProgramProperty.LinkStatus, out var status);
            if (status == 0)
            {
                const int logMaxLength = 1024;
                var infoLog = new StringBuilder(logMaxLength);
                Gl.GetProgramInfoLog(ProgramId, 1024, out _, infoLog);
                throw new InvalidOperationException($"Failed to link program: {infoLog}");
            }
            return true;
        }
        
        public override void Dispose()
        {
            foreach (var shader in Shaders)
            {
                shader.Dispose();
            }
            Gl.DeleteProgram(ProgramId);
        }
    }
}