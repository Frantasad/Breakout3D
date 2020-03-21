using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using OpenGL;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace Breakout3D.Framework
{
    public class Texture : IDisposable
    {
        public uint TextureId { get; private set; }
        
        public void Load(string filename)
        {
            if (!File.Exists(filename))
            {
                throw new FileNotFoundException($"File \"{filename}\" does not exist!");
            }
            var bitmap = new Bitmap(filename);
            
            TextureId = Gl.GenTexture();
            Gl.BindTexture(TextureTarget.Texture2d, TextureId);
            
            var data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            Gl.TexImage2D(TextureTarget.Texture2d, 0, InternalFormat.Rgba, data.Width, data.Height, 0, OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);
            bitmap.UnlockBits(data);
            
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureWrapS, TextureWrapMode.Repeat);
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureWrapT, TextureWrapMode.Repeat);
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMinFilter, TextureMinFilter.Linear);
            Gl.TexParameteri(TextureTarget.Texture2d, TextureParameterName.TextureMagFilter, TextureMagFilter.Linear);
            
            Gl.BindTexture(TextureTarget.Texture2d, 0);
        }

        public void Bind(TextureUnit unit)
        {
            Gl.ActiveTexture(unit);
            Gl.BindTexture(TextureTarget.Texture2d, TextureId);
        }

        public void Dispose()
        {
            Gl.DeleteTextures(TextureId);
        }
    }
}