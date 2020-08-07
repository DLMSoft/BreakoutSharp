#region Using direcitves
using System;
#endregion

namespace BreakoutSharp.Engine.Directing {
    enum SceneState {
        Idle,
        Starting,
        Running,
        Stopping,
        Terminating
    }

    static class SceneManager {
        public static Scene CurrentScene { get; private set; }
        public static Scene NextScene { get; set; }
        public static SceneState State { get; set; }

        public static void SetInitScene(Scene scene) {
            if (CurrentScene != null)
                return;

            CurrentScene = scene;
            State = SceneState.Idle;
        }

        public static void Update(double elapsed) {
            if (CurrentScene == null) {
                Game.Instance.Exit();
                return;
            }

            if (State == SceneState.Idle) {
                CurrentScene.Prepare();
                State = SceneState.Starting;
                return;
            }

            if (State == SceneState.Starting) {
                CurrentScene.Start(elapsed);
                return;
            }

            if (State == SceneState.Running) {
                CurrentScene.Update(elapsed);
                return;
            }

            if (State == SceneState.Stopping) {
                CurrentScene.Stop(elapsed);
                return;
            }

            if (State == SceneState.Terminating) {
                CurrentScene.Terminate();
                CurrentScene = NextScene;
                State = SceneState.Idle;
                return;
            }
        }
    }
}