#region Using directives
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BreakoutSharp.Engine.Directing;
using BreakoutSharp.Engine.Graphics;
using BreakoutSharp.Engine.Resourcing;
#endregion

namespace BreakoutSharp.Engine {
    abstract class Game : IDisposable {
        public static Game Instance { get; private set; }

        public abstract string Title { get; }
        public abstract int ScreenWidth { get; }
        public abstract int ScreenHeight { get; }
        public abstract Scene DefaultScene { get; }

        public Game() {
            Instance = this;
            renderers = new List<Renderer>();
        }

        public void Run() { 
            SceneManager.SetInitScene(DefaultScene);
            using (window = new EngineWindow(Title, ScreenWidth, ScreenHeight)) {
                window.Run();
            }
        }

        public void Exit() {
            Debug.WriteLine("Game exit.");
            window.Exit();
        }

        public void Render() {
            var sortedRenderers = from r in renderers orderby r.SortIndex ascending, r.Id ascending select r;
            foreach (var r in sortedRenderers) {
                r.InternalRender();
            }
        }

        public void AddRenderer(Renderer r) {
            renderers.Add(r);
        }

        public void RemoveRenderer(Renderer r) {
            renderers.Remove(r);
        }

        public void Dispose() {
            renderers.Clear();
            renderers = null;

            ResourceManager.Cleanup();
        }

        List<Renderer> renderers;
        EngineWindow window;
    }
}