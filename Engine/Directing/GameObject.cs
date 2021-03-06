#region Using directives
using System;
using System.Collections.Generic;
using BreakoutSharp.Engine.Graphics;
#endregion

namespace BreakoutSharp.Engine.Directing {
    sealed class GameObject : IDisposable, ICloneable {
        public static int Count { get; private set; } = 0;
        public int Id { get; }
        public string Name { get; set; }
        public Transform2D Transform { get; }
        public Renderer Renderer { get; set; }
        public bool Activated { get; set; }
        public bool Initialized { get; private set; }
        public bool Disposed { get; private set; }

        public GameObject() {
            Id = Count++;

            Name = "object#" + Id;

            Transform = new Transform2D();
            Renderer = null;
            Activated = true;
            Initialized = false;
            Disposed = false;

            features = new List<Feature>();
        }

        private GameObject(GameObject parent) {
            Id = Count++;

            Name = "object#" + Id;

            Transform = new Transform2D();
            Transform.Offset = parent.Transform.Offset;
            Transform.Position = parent.Transform.Position;
            Transform.Scale = parent.Transform.Scale;
            Transform.Rotation = parent.Transform.Rotation;

            Renderer = parent.Renderer.Clone(this);

            Activated = parent.Activated;
            Initialized = false;
            Disposed = false;
            
            features = new List<Feature>();
            features.AddRange(parent.features);
        }

        public void Update(double elapsed) {
            if (Disposed)
                return;

            if (!Initialized) {
                Initialized = true;
                foreach (var f in features) {
                    f.OnLoad();
                }
            }

            if (!Activated)
                return;

            foreach (var f in features) {
                f.OnUpdate(elapsed);
            }
        }

        public void Render() {
            if (Disposed)
                return;

            if (!Initialized || !Activated || Renderer == null)
                return;

            var matrix = Transform.UpdateTransform();

            Renderer.Render(matrix);
        }

        public void Dispose() {
            if (Disposed)
                return;

            Disposed = true;

            foreach (var f in features) {
                f.OnUnload();
            }
            
            features.Clear();

            Renderer.Dispose();
        }

        public void AddFeature(Feature feature) {
            features.Add(feature);
        }

        public T GetFeature<T>() where T : Feature {
            foreach (var f in features) {
                if (f is T)
                    return f as T;
            }

            return null;
        }

        public object Clone() {
            return new GameObject(this);
        }

        List<Feature> features;
    }
}