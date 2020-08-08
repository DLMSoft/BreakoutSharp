#region Using directives
using System;
using System.IO;
using BreakoutSharp.Engine;
using BreakoutSharp.Engine.Directing;
using BreakoutSharp.Engine.Graphics.Rendering;
using BreakoutSharp.Engine.Graphics.UI;
using BreakoutSharp.Engine.Resourcing;
using OpenTK;
#endregion

namespace BreakoutSharp {
    class StageScene : Scene {
        const int BRICK_WIDTH = 960 / 15;
        const int BRICK_HEIGHT = 640 / 16;
        const int MAX_LEVELS =  5;

        string levelContent;

        GameObject background;
        GameObject paddle;
        GameObject[] bricks;

        int remeaningBricks = 0;

        int levelNumber = 0;

        public StageScene(int stage) : base() {
            levelNumber = stage;

            var fileName = $"assets/levels/stage_{stage.ToString("D3")}.lvl";
            
            if (!File.Exists(fileName))
                throw new FileNotFoundException();

            levelContent = File.ReadAllText(fileName);
        }

        void MakeBricks() {
            var brickTexture = ResourceManager.LoadTexture2D("block.png", "brick");
            var brickSolidTexture = ResourceManager.LoadTexture2D("block_solid.png", "brick_solid");
            bricks = new GameObject[5];

            var colors = new [] {
                new Vector4(0.8f, 0.8f, 0.7f, 1.0f),
                new Vector4(0.0f, 0.5f, 1.0f, 1.0f),
                new Vector4(0.0f, 0.75f, 0.0f, 1.0f),
                new Vector4(0.75f, 0.75f, 0.25f, 1.0f),
                new Vector4(1.0f, 0.5f, 0.0f, 1.0f)
            };
            
            bricks[0] = new GameObject();
            bricks[0].Activated = false;
            bricks[0].Name = "brick";
            bricks[0].Renderer = new SpriteRenderer(bricks[0]) {
                SortIndex = 1,
                Sprite = new Sprite(brickSolidTexture) {
                    Color = colors[0],
                    Width = BRICK_WIDTH * 2,
                    Height = BRICK_HEIGHT * 2,
                    Padding = new Padding(6, 6)
                }
            };
            bricks[0].Transform.Scale = new Vector2(0.5f, 0.5f);
            AddObject(bricks[0]);

            for (var i = 1; i < 5; i ++) {
                var b = new GameObject();
                b.Activated = false;
                b.Name = "brick";
                b.Renderer = new SpriteRenderer(b) {
                    SortIndex = 1,
                    Sprite = new Sprite(brickTexture) {
                        Color = colors[i],
                        Width = BRICK_WIDTH * 2,
                        Height = BRICK_HEIGHT * 2,
                        Padding = new Padding(8, 6)
                    }
                };
                b.Transform.Scale = new Vector2(0.5f, 0.5f);
                AddObject(b);

                bricks[i] = b;
            }
        }

        public override void OnPrepare() {
            MakeBricks();

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
            paddle.Name = "paddle";
            paddle.Renderer = new SpriteRenderer(paddle) {
                Sprite = paddleSprite
            };
            paddle.Renderer.SortIndex = 999;
            paddle.Transform.Position = new Vector2(480, 626);
            paddle.Transform.Scale = new Vector2(0.2f, 0.2f);
            paddle.Transform.Offset = new Vector2(256, 64);
            AddObject(paddle);

            var lines = levelContent.Split('\n');
            
            if (lines.Length > 12) {
                throw new FormatException();
            }

            int cx = 0, cy = 0;

            foreach (var line in lines) {
                var cells = line.TrimEnd().Split(' ');
                if (cells.Length != 15) {
                    throw new FormatException();
                }
                
                cx = 0;

                foreach (var cell in cells) {
                    var brickType = 0;
                    if (!int.TryParse(cell.Trim(), out brickType)) {
                        throw new FormatException();
                    }

                    if (brickType > 1) {
                        remeaningBricks ++;
                    }

                    if (brickType == 0) {
                        cx++;
                        continue;
                    }

                    var brick = bricks[brickType - 1].Clone() as GameObject;
                    brick.Activated = true;
                    brick.Name = $"brick@{cx},{cy}";
                    brick.Transform.Position = new Vector2(cx * BRICK_WIDTH, cy * BRICK_HEIGHT);
                    AddObject(brick);

                    cx ++;
                }

                cy ++;
            }
        }

        public override void OnUpdate(double elapsed) {
            if (Input.GetKeyPressing(KeyCode.Left)) {
                paddle.Transform.Position.X -= (float)(960 * elapsed);

                if (paddle.Transform.Position.X < 48) {
                    paddle.Transform.Position.X = 48;
                }
            }

            if (Input.GetKeyPressing(KeyCode.Right)) {
                paddle.Transform.Position.X += (float)(960 * elapsed);

                if (paddle.Transform.Position.X > 912) {
                    paddle.Transform.Position.X = 912;
                }
            }

            if (Input.GetKeyTriggering(KeyCode.Escape)) {
                Game.Instance.Exit();
            }

            if (Input.GetKeyTriggering(KeyCode.PageUp)) {
                if (levelNumber == 1)
                    return;
                SceneManager.NextScene = new StageScene(levelNumber - 1);
                SceneManager.State = SceneState.Stopping;
                return;
            }

            if (Input.GetKeyTriggering(KeyCode.PageDown)) {
                if (levelNumber == MAX_LEVELS)
                    return;
                SceneManager.NextScene = new StageScene(levelNumber + 1);
                SceneManager.State = SceneState.Stopping;
                return;
            }
        }

        public override void OnTerminate() {
            
        }
    }
}