#region Using directives
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using BreakoutSharp.Engine.Directing;
using BreakoutSharp.Engine.Resourcing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
#endregion

namespace BreakoutSharp.Engine.Graphics.Rendering {
    class SpriteRenderer : Renderer {
        public static Shader DefaultShader { get; }

        public UI.Sprite Sprite { get; set; }

        static SpriteRenderer() {
            DefaultShader = ResourceManager.LoadShader("sprite.glsl", "default_sprite", ResourcePool.Global);
        }

        public SpriteRenderer(GameObject owner) : base(owner) {
            Sprite = null;
        }

        private SpriteRenderer(GameObject owner, SpriteRenderer parent) : base(owner) {
            Sprite = parent.Sprite;
        }

        public override Renderer Clone(GameObject obj) {
            return new SpriteRenderer(obj, this);
        }

        public override void Render(Matrix4 matrix) {
            if (Sprite == null)
                return;

            GL.Disable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);

            Sprite.Texture.Bind();
            DefaultShader.Use();

            DefaultShader.SetUniform("u_Resolution", new Vector2(Game.Instance.ScreenWidth, Game.Instance.ScreenHeight));
            DefaultShader.SetUniform("u_Model", matrix);

            DefaultShader.SetUniform("u_Color", Sprite.Color);
            DefaultShader.SetUniform("u_Opacity", Sprite.Opacity);
            
        }
    }
}