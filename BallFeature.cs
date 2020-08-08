#region Using directives
using OpenTK;
using BreakoutSharp.Engine.Directing;
using BreakoutSharp.Engine;
#endregion

namespace BreakoutSharp {
    class BallFeature : Feature {
        const float BALL_RADIUS = 13.0f;
        public float Speed { get; set; }
        public bool Sticking { get; set; } = true;
        public GameObject Paddle { get; set; } = null;

        Vector2 velocity;
        float StickingOffset = 0.0f;

        public BallFeature(GameObject owner) : base (owner) {}

        public override void OnLoad() {
            velocity = new Vector2(0, 0);
        }

        void ShootLeft() {
            velocity = new Vector2(-1, -1).Normalized() * Speed;
            Sticking = false;
        }

        void ShootRight() {
            velocity = new Vector2(1, -1).Normalized() * Speed;
            Sticking = false;
        }

        public override void OnUpdate(double elapsed) {
            if (GameObject.Transform.Position.Y >= Game.Instance.ScreenHeight + BALL_RADIUS) {
                
                return;
            }

            if (Sticking) {
                if (Input.GetKeyTriggering(KeyCode.Z)) {
                    if (Input.GetKeyPressing(KeyCode.Left)) {
                        ShootLeft();
                    }
                    else {
                        ShootRight();
                    }
                    return;
                }

                GameObject.Transform.Position.X = Paddle.Transform.Position.X + StickingOffset;
                return;
            }
            
            GameObject.Transform.Position += velocity * (float)elapsed;

            if (GameObject.Transform.Position.X + BALL_RADIUS >= Game.Instance.ScreenWidth) {
                GameObject.Transform.Position.X = Game.Instance.ScreenWidth - BALL_RADIUS;
                velocity.X = -velocity.X;
            }

            if (GameObject.Transform.Position.X - BALL_RADIUS <= 0) {
                GameObject.Transform.Position.X = BALL_RADIUS;
                velocity.X = -velocity.X;
            }

            if (GameObject.Transform.Position.Y - BALL_RADIUS <= 0) {
                GameObject.Transform.Position.Y = BALL_RADIUS;
                velocity.Y = -velocity.Y;
            }
        }
    }
}