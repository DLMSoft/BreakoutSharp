using System;
using BreakoutSharp.Engine.Directing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace BreakoutSharp.Engine {
    sealed class EngineWindow : GameWindow {
        public EngineWindow(string title, int width, int height) : base (width, height) {
            Title = title;
            WindowBorder = WindowBorder.Fixed;
        }

        protected override void OnLoad(EventArgs e) {
            GL.Viewport(0, 0, 960, 640);
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
        }

        protected override void OnUpdateFrame(FrameEventArgs e) {
            var elapsed = e.Time;

            SceneManager.Update(elapsed);
        }

        protected override void OnRenderFrame(FrameEventArgs e) {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            Game.Instance.Render();

            SwapBuffers();
        }
    }
}