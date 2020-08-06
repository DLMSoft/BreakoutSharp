#region Using directives
using System;
using System.Drawing;
using GdipImaging = System.Drawing.Imaging;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;
using BreakoutSharp.Engine.Resourcing;
#endregion

namespace BreakoutSharp.Engine.Graphics {
    class Texture2D : IResource {
        int textureId;

        public ResourceType Type => ResourceType.Texture;

        public bool Disposed { get; private set; }
        public int Width { get; }
        public int Height { get; }

        public Texture2D(Bitmap bmp) {
            Disposed = false;
            Width = bmp.Width;
            Height = bmp.Height;
            var data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), GdipImaging.ImageLockMode.ReadOnly, GdipImaging.PixelFormat.Format32bppArgb);
            
            textureId = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, textureId);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp.Width, bmp.Height, 0, PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            bmp.UnlockBits(data);
        }

        public void Bind() {
            if (Disposed)
                throw new ObjectDisposedException(null);

            GL.BindTexture(TextureTarget.Texture2D, textureId);
        }

        public void Dispose() {
            if (Disposed)
                return;

            Disposed = true;
            GL.DeleteTexture(textureId);
        }
    }
}