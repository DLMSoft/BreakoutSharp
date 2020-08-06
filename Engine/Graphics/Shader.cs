#region Using directives
using System;
using System.IO;
using BreakoutSharp.Engine.Resourcing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
#endregion

namespace BreakoutSharp.Engine.Graphics {
    class Shader : IResource {
        int programId;
        
        public ResourceType Type => ResourceType.Shader;

        public bool Disposed { get; private set; }

        public Shader(string input) {
            Disposed = false;

            programId = GL.CreateProgram();

            var vertId = GL.CreateShader(ShaderType.VertexShader);
            var vertContent = "#define COMPILE_VERT\r\n" + input;
            GL.ShaderSource(vertId, vertContent);
            GL.CompileShader(vertId);
            GL.AttachShader(programId, vertId);

            var fragId = GL.CreateShader(ShaderType.VertexShader);
            var fragContent = "#define COMPILE_FRAG\r\n" + input;
            GL.ShaderSource(fragId, fragContent);
            GL.CompileShader(fragId);
            GL.AttachShader(programId, fragId);

            GL.LinkProgram(programId);

            GL.DeleteShader(vertId);
            GL.DeleteShader(fragId);
        }

        public void Use() {
            if (Disposed)
                throw new ObjectDisposedException(null);

            GL.UseProgram(programId);
        }

        public void Dispose() {
            if (Disposed)
                return;
            Disposed = true;
            GL.DeleteProgram(programId);
        }
    }
}