using System;
using System.Drawing;
using BreakoutSharp.Engine.Directing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace BreakoutSharp.Engine {
    sealed class EngineWindow : GameWindow {
        public EngineWindow(string title, int width, int height) : base (width, height) {
            Title = title;
            WindowBorder = WindowBorder.Fixed;
            ClientSize = new Size(width, height);
        }

        protected override void OnLoad(EventArgs e) {
            GL.Viewport(0, 0, ClientSize.Width, ClientSize.Height);
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
        }

        protected override void OnUpdateFrame(FrameEventArgs e) {
            var elapsed = e.Time;

            Input.Update();
            SceneManager.Update(elapsed);
        }

        protected override void OnRenderFrame(FrameEventArgs e) {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);

            Game.Instance.Render();

            SwapBuffers();
        }
    }
}