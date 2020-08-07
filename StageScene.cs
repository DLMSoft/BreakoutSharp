#region Using directives
using System;
using BreakoutSharp.Engine;
using BreakoutSharp.Engine.Directing;
using BreakoutSharp.Engine.Graphics.Rendering;
using BreakoutSharp.Engine.Graphics.UI;
using BreakoutSharp.Engine.Resourcing;
using OpenTK;
#endregion

namespace BreakoutSharp {
    class StageScene : Scene {
        GameObject background;
        GameObject paddle;

        public override void OnPrepare() {
            var bgSprite = new Sprite(ResourceManager.LoadTexture2D("background.jpg", "background"));
            bgSprite.Width = 960;
            bgSprite.Height = 640;

            background = new GameObject();
            background.Renderer = new SpriteRenderer(background) {
                Sprite = bgSprite
            };
            AddObject(background);

            var paddleSprite = new Sprite(ResourceManager.LoadTexture2D("paddle.png", "paddle"));
            paddleSprite.Padding = new Padding(64, 0);
            paddle = new GameObject();
            paddle.Renderer = new SpriteRenderer(paddle) {
                Sprite = paddleSprite
            };
            paddle.Transform.Position = new Vector2(480, 320);
            paddle.Transform.Scale = new Vector2(0.5f, 0.5f);
            paddle.Transform.Offset = new Vector2(256, 64);
            AddObject(paddle);
        }

        public override void OnUpdate(double elapsed) {

        }

        public override void OnTerminate() {
            
        }
    }
}