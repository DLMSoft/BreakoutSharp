#region Using directives
using System.Collections.Generic;
using BreakoutSharp.Engine.Resourcing;
#endregion

namespace BreakoutSharp.Engine.Directing {
    class Scene {
        Dictionary<string, GameObject> gameObjects;

        public Scene() {
            gameObjects = new Dictionary<string, GameObject>();
        }

        public void Prepare() {
            OnPrepare();
        }

        public void Start(double elapsed) {
            OnStart(elapsed);
        }

        public void Update(double elapsed) {
            OnUpdate(elapsed);
            foreach (var obj in gameObjects.Values) {
                obj.Update(elapsed);
            }
        }

        public void Stop(double elapsed) {
            OnStop(elapsed);
        }

        public void Terminate() {
            OnTerminate();
            gameObjects.Clear();
            ResourceManager.Cleanup(ResourcePool.Scene);
        }

        public void AddObject(GameObject obj) {
            if (gameObjects.ContainsKey(obj.Name)) {
                obj.Name = obj.Name + '#' + obj.Id;
            }
            gameObjects.Add(obj.Name, obj);
        }

        public virtual void OnPrepare() {}
        public virtual void OnStart(double elapsed) {
            SceneManager.State = SceneState.Running;
        }
        public virtual void OnUpdate(double elapsed) {

        }
        public virtual void OnStop(double elapsed) {
            SceneManager.State = SceneState.Terminating;
        }
        public virtual void OnTerminate() {}
    }
}