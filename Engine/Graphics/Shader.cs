#region Using directives
using System;
using System.Collections.Generic;
using BreakoutSharp.Engine.Resourcing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
#endregion

namespace BreakoutSharp.Engine.Graphics {
    class Shader : IResource {
        int programId;
        
        public ResourceType Type => ResourceType.Shader;

        public bool Disposed { get; private set; }

        public Dictionary<string, int> uniforms;

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

            uniforms = new Dictionary<string, int>();
        }

        public void SetUniform(string name, float value) {
            int location = 0;

            if (uniforms.ContainsKey(name)) {
                location = uniforms[name];
            }
            else {
                location = GL.GetUniformLocation(programId, name);
                uniforms.Add(name, location);
            }

            GL.Uniform1(location, value);
        }

        public void SetUniform(string name, double value) {
            int location = 0;

            if (uniforms.ContainsKey(name)) {
                location = uniforms[name];
            }
            else {
                location = GL.GetUniformLocation(programId, name);
                uniforms.Add(name, location);
            }

            GL.Uniform1(location, value);
        }

        public void SetUniform(string name, int value) {
            int location = 0;

            if (uniforms.ContainsKey(name)) {
                location = uniforms[name];
            }
            else {
                location = GL.GetUniformLocation(programId, name);
                uniforms.Add(name, location);
            }

            GL.Uniform1(location, value);
        }

        public void SetUniform(string name, Vector2 value) {
            int location = 0;

            if (uniforms.ContainsKey(name)) {
                location = uniforms[name];
            }
            else {
                location = GL.GetUniformLocation(programId, name);
                uniforms.Add(name, location);
            }

            GL.Uniform2(location, value);
        }

        public void SetUniform(string name, Vector3 value) {
            int location = 0;

            if (uniforms.ContainsKey(name)) {
                location = uniforms[name];
            }
            else {
                location = GL.GetUniformLocation(programId, name);
                uniforms.Add(name, location);
            }

            GL.Uniform3(location, value);
        }

        public void SetUniform(string name, Vector4 value) {
            int location = 0;

            if (uniforms.ContainsKey(name)) {
                location = uniforms[name];
            }
            else {
                location = GL.GetUniformLocation(programId, name);
                uniforms.Add(name, location);
            }

            GL.Uniform4(location, value);
        }

        public void SetUniform(string name, Matrix4 value) {
            int location = 0;

            if (uniforms.ContainsKey(name)) {
                location = uniforms[name];
            }
            else {
                location = GL.GetUniformLocation(programId, name);
                uniforms.Add(name, location);
            }

            GL.UniformMatrix4(location, false, ref value);
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