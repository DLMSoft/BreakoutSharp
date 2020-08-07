#region Using directives
using OpenTK;
#endregion

namespace BreakoutSharp.Engine.Directing {
    sealed class Transform2D {
        public Vector2 Position;
        public Vector2 Offset;
        public Vector2 Scale;
        public float Rotation;

        public Transform2D() {
            Position = new Vector2(0.0f, 0.0f);
            Offset = new Vector2(0.0f, 0.0f);
            Scale = new Vector2(1.0f, 1.0f);
            Rotation = 0.0f;
        }

        public Matrix4 UpdateTransform() {
            var trans = Matrix4.CreateTranslation(Position.X, Position.Y, 0.0f);
            var scale = Matrix4.CreateScale(Scale.X, Scale.Y, 1.0f);
            var rot = Matrix4.CreateRotationZ(Rotation);
            var offset = Matrix4.CreateTranslation(-Offset.X, -Offset.Y, 0.0f);

            return offset * rot * scale * trans;
        }
    }
}