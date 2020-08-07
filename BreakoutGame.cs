#region Using directives
using OpenTK;
using BreakoutSharp.Engine;
#endregion

namespace BreakoutSharp {
    sealed class BreakoutGame : Game {
        public override string Title => "Breakout";

        public override int ScreenWidth => 960;

        public override int ScreenHeight => 640;
    }
}