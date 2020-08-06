#region Using directives
using System;
#endregion

namespace BreakoutSharp.Engine {
    abstract class Game {
        public static Game Instance { get; private set; }

        public abstract string Title { get; }
        public abstract int ScreenWidth { get; }
        public abstract int ScreenHeight { get; }

        public Game() {
            Instance = this;
        }

        public void Run() {
            using (window = new EngineWindow(Title, ScreenWidth, ScreenHeight)) {
                window.Run();
            }
        }

        public void Exit() {
            window.Exit();
        }

        EngineWindow window;
    }
}