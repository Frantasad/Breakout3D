using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OpenGL;

namespace Breakout3D.Framework
{
    public class Shader : IDisposable
    {
        public uint ShaderId { get; private set; }
        public ShaderType ShaderType { get; }
        public string FileName { get; }

        public Shader(ShaderType shaderType, string fileName)
        {
            ShaderType = shaderType;
            FileName = fileName;
        }

        public static string[] LoadShaderSource(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException($"File \"{filename}\" does not exist!");
            }
            
            var shaderSourceLines = new List<string>();
            using (var sr = new StreamReader(filename)) 
            {
                while (!sr.EndOfStream) {
                    var line = sr.ReadLine();

                    if (!line.EndsWith("\n"))
                        line += "\n";
                    shaderSourceLines.Add(line);
                }
            }

            return shaderSourceLines.ToArray();
        }

        public bool Init()
        {
            var shaderSource = LoadShaderSource(FileName);
            ShaderId = Gl.CreateShader(ShaderType);
            
            Gl.ShaderSource(ShaderId, shaderSource);
            Gl.CompileShader(ShaderId);

            Gl.GetShader(ShaderId, ShaderParameterName.CompileStatus, out var status);
            if (status == 0)
            {
                const int logMaxLength = 1024;
                var infoLog = new StringBuilder(logMaxLength);
                Gl.GetShaderInfoLog(ShaderId, logMaxLength, out _, infoLog);
                throw new InvalidOperationException($"Failed to compile shader: {infoLog}");
            }
            return true;
        }

        public void Dispose()
        {
            Gl.DeleteShader(ShaderId);
        }
    }
}