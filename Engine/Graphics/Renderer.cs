#region Using directives
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using OpenTK;
#endregion

namespace BreakoutSharp.Engine.Graphics {
    abstract class Renderer : IDisposable, IEquatable<Renderer> {
        static List<Renderer> renderers;

        public static int Count { get; private set; }
        
        static Renderer() {
            Count = 0;
            renderers = new List<Renderer>();
        }

        public int Id { get; }

        public Renderer() {
            Id = Count++;

            renderers.Add(this);
        }

        public void Dispose() {
            renderers.Remove(this);
        }

        public abstract void Render(Matrix4 matrix);

        public bool Equals([AllowNull] Renderer other) {
            if (other.Id == Id)
                return true;
            return  false;
        }
    }
}