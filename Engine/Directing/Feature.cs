#region Using directives
using System;
#endregion

namespace BreakoutSharp.Engine.Directing {
    abstract class Feature {
        public GameObject GameObject { get; }

        public Feature(GameObject owner) {
            GameObject = owner;
        }
        public virtual void OnLoad() {}
        public virtual void OnUnload() {}
        public virtual void OnUpdate(double elapsed) {}
    }
}