#region Using directives
using System;
#endregion

namespace BreakoutSharp.Engine.Resourcing {
    enum ResourceType {
        Unknown = 0,
        Shader = 1,
        Texture = 2
    }

    interface IResource : IDisposable {
        ResourceType Type { get; }
        bool Disposed { get; }
    }
}