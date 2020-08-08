#region Using directives
using OpenTK;
using BreakoutSharp.Engine;
using BreakoutSharp.Engine.Directing;
#endregion

namespace BreakoutSharp {
    sealed class BreakoutGame : Game {
        public override string Title => "Breakout";

        public override int ScreenWidth => 960;

        public override int ScreenHeight => 640;

        public override Scene DefaultScene {
            get {
                return new StageScene(1);
            }
        }

        public static void Main(string[] args) {
            using (var game = new BreakoutGame()) {
                game.Run();
            }
        }
    }
}