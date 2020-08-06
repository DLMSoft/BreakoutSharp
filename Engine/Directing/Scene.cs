#region Using directives
using System.Collections.Generic;
using BreakoutSharp.Engine.Resourcing;
#endregion

namespace BreakoutSharp.Engine.Directing {
    class Scene {
        public virtual void OnPrepare() {}
        public virtual void OnStart() {}
        public virtual void OnStop() {}
        public virtual void OnTerminate() {}

        public void Prepare() {
            OnPrepare();
        }

        public void Start() {
            OnStart();
        }

        public void Stop() {
            OnStop();
        }

        public void Terminate() {
            OnTerminate();

            ResourceManager.Cleanup(ResourcePool.Scene);
        }

        public virtual void OnUpdate(double elapsed) {}
    }
}