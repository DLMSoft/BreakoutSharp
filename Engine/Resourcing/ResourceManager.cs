#region Using directives
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Text;

using BreakoutSharp.Engine.Graphics;
using System.Linq;
#endregion

namespace BreakoutSharp.Engine.Resourcing {
    enum ResourcePool {
        Scene,
        Global
    }

    static class ResourceManager {
        private class LoadedResource {
            public ResourcePool Pool { get; set; }
            public IResource Resource { get; set; }
        }

        static Dictionary<string, LoadedResource> resources = new Dictionary<string, LoadedResource>();

        #region Shader
        public static Shader LoadShader(string fileName, string name, ResourcePool pool = ResourcePool.Scene) {
            var filePath = "assets/shaders/" + fileName;
            if (!File.Exists(filePath))
                return null;

            var resName = "SHADER#" + name;

            if (resources.ContainsKey(resName))
                return resources[resName].Resource as Shader;
            
            try {
                var shaderContent = File.ReadAllText(filePath, Encoding.UTF8);

                var result = new Shader(shaderContent);

                resources.Add(resName, new LoadedResource {
                    Pool = pool,
                    Resource = result
                });

                return result;
            }
            catch (Exception ex) {
                Debug.WriteLine(ex);
                return null;
            }
        }
        
        public static Shader GetShader(string name) {
            var resName = "SHADER#" + name;

            if (!resources.ContainsKey(resName))
                return null;
            
            return resources[resName].Resource as Shader;
        }
        #endregion

        #region Texture2D
        public static Texture2D LoadTexture2D(string fileName, string name, ResourcePool pool = ResourcePool.Scene) {
            var filePath = "assets/textures/" + fileName;

            if (!File.Exists(filePath))
                return null;

            var resName = "2D#" + name;

            if (resources.ContainsKey(resName))
                return resources[resName].Resource as Texture2D;

            try {
                using (var bmp = new Bitmap(filePath)) {
                    var result = new Texture2D(bmp);

                    resources.Add(resName, new LoadedResource {
                        Pool = pool,
                        Resource = result
                    });

                    return result;
                }
            }
            catch (Exception ex) {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public static Texture2D GetTexture2D(string name) {
            var resName = "2D#" + name;

            if (!resources.ContainsKey(resName))
                return null;
            
            return resources[resName].Resource as Texture2D;
        }
        #endregion

        #region Cleanup
        public static void Cleanup(ResourcePool pool) {
            var targets = from r in resources where r.Value.Pool == pool select r;

            foreach (var i in targets) {
                if (i.Value.Pool != pool)
                    continue;

                if (i.Value.Resource.Disposed)
                    continue;
                
                i.Value.Resource.Dispose();

                resources.Remove(i.Key);
            }
        }

        public static void Cleanup() {
            foreach (var i in resources.Values) {
                if (i.Resource.Disposed)
                    continue;
                
                i.Resource.Dispose();
            }

            resources.Clear();
        }
        #endregion
    }
}