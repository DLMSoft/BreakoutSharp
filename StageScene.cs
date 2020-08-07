#region Using directives
using BreakoutSharp.Engine;
using BreakoutSharp.Engine.Directing;
#endregion

namespace BreakoutSharp {
    class StageScene : Scene {
        GameObject background;

        public override void OnPrepare() {
            background = new GameObject();
        }

        public override void OnUpdate(double elapsed) {

        }

        public override void OnTerminate() {
            
        }
    }
}