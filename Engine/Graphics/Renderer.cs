#region Using directives
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using BreakoutSharp.Engine.Directing;
using OpenTK;
#endregion

namespace BreakoutSharp.Engine.Graphics {
    abstract class Renderer : IDisposable, IEquatable<Renderer> {
        public static int Count { get; private set; } = 0;

        public GameObject GameObject { get; }

        public int Id { get; }
        
        public int SortIndex { get; set; }

        public Renderer(GameObject owner) {
            Id = Count++;

            GameObject = owner;

            Game.Instance.AddRenderer(this);
        }

        public abstract Renderer Clone(GameObject obj);

        public void Dispose() {
            Game.Instance.RemoveRenderer(this);
        }

        public void InternalRender() {
            var matrix = GameObject.Transform.UpdateTransform();

            Render(matrix);
        }

        public abstract void Render(Matrix4 matrix);
        
        public bool Equals([AllowNull] Renderer other) {
            if (other.Id == Id)
                return true;
            return  false;
        }

        public static int CompareSortIndex(Renderer left, Renderer right) {
            if (left.SortIndex == right.SortIndex) {
                return left.Id - right.Id;
            }

            return left.SortIndex - right.SortIndex;
        }
    }
}