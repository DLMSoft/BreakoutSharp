using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace BreakoutSharp.Engine.Graphics.UI {
    enum SpriteBlend {
        AlphaBlend,
        Add,
        Subtract,
        ReversedSubtract,
        Min,
        Max
    }

    class Sprite : IDisposable {
        public struct SpriteVertex {
            public Vector2 Position;
            public Vector2 TexCoord;
        }
        public Texture2D Texture { get; }
        
        public Padding Padding { get; set; }

        public int Width {
            get {
                return width;
            }

            set {
                width = value;
                rebuildVertices = true;
            }
        }

        public int Height {
            get {
                return height;
            }
            set {
                height = value;
                rebuildVertices = true;
            }
        }

        public Vector4 Color { get; set; }

        public float Opacity { get; set; }

        public SpriteBlend Blend { get; set; }

        public int VertexCount { get; private set; }
        
        int width;
        int height;
        bool rebuildVertices;

        int vbo = -1;
        int vao = -1;
        int ebo = -1;

        public Sprite(Texture2D texture) {
            Texture = texture;
            Padding = new Padding();
            Opacity = 1.0f;
            Color = new Vector4(1, 1, 1, 1);
            Blend = SpriteBlend.AlphaBlend;
            Width = Texture.Width;
            Height = Texture.Height;
            rebuildVertices = true;
        }

        public void ResetSize() {
            Width = Texture.Width;
            Height = Texture.Height;
        }

        public void BuildVertices() {
            List<SpriteVertex> vertexList = new List<SpriteVertex>();
            List<int> indexList = new List<int>();
        
            float leftRatio = Padding.Left / (float)width;
            float topRatio = Padding.Top / (float)height;
            float rightRatio = Padding.Right / (float)width;
            float bottomRatio = Padding.Bottom / (float)height;
            
            #region Inner Top-Left
            vertexList.Add(new SpriteVertex {
                Position = new Vector2(Padding.Left, Padding.Top),
                TexCoord = new Vector2(leftRatio, topRatio),
            });
            #endregion
            #region Inner Top-Right
            vertexList.Add(new SpriteVertex {
                Position = new Vector2(width - Padding.Right, Padding.Top),
                TexCoord = new Vector2(1.0f - rightRatio, topRatio),
            });
            #endregion
            #region Inner Bottom-Left
            vertexList.Add(new SpriteVertex {
                Position = new Vector2(Padding.Left, height - Padding.Bottom),
                TexCoord = new Vector2(leftRatio, 1.0f - bottomRatio),
            });
            #endregion
            #region Inner Bottom-Right
            vertexList.Add(new SpriteVertex {
                Position = new Vector2(width - Padding.Right, height - Padding.Bottom),
                TexCoord = new Vector2(1.0f - rightRatio, 1.0f - bottomRatio),
            });
            #endregion
            
            // Inner indices
            indexList.AddRange(new [] { 0, 1, 2, 1, 3, 2 });
            
            #region Paddings
            #region Left-Side
            int l1 = 0;
            int l2 = 0;
            if (Padding.HasLeft) {
                l1 = vertexList.Count;
                vertexList.Add(new SpriteVertex {
                    Position = new Vector2(0, Padding.Top),
                    TexCoord = new Vector2(0, topRatio)
                });

                l2 = vertexList.Count;
                vertexList.Add(new SpriteVertex {
                    Position = new Vector2(0, height - Padding.Bottom),
                    TexCoord = new Vector2(0, 1.0f - bottomRatio)
                });

                indexList.AddRange(new [] { l1, 0, l2, 0, 2, l2 });
            }
            #endregion
            
            #region Top-Side
            int t1 = 0;
            int t2 = 0;
            if (Padding.HasLeft) {
                t1 = vertexList.Count;
                vertexList.Add(new SpriteVertex {
                    Position = new Vector2(Padding.Left, 0),
                    TexCoord = new Vector2(leftRatio, 0)
                });

                t2 = vertexList.Count;
                vertexList.Add(new SpriteVertex {
                    Position = new Vector2(width - Padding.Right, 0),
                    TexCoord = new Vector2(1.0f - rightRatio, 0)
                });

                indexList.AddRange(new [] { t1, t2, 0, t2, 1, 0 });
            }
            #endregion

            #region Right-Side
            int r1 = 0;
            int r2 = 0;
            if (Padding.HasLeft) {
                r1 = vertexList.Count;
                vertexList.Add(new SpriteVertex {
                    Position = new Vector2(width, Padding.Top),
                    TexCoord = new Vector2(1, topRatio)
                });

                r2 = vertexList.Count;
                vertexList.Add(new SpriteVertex {
                    Position = new Vector2(width, height - Padding.Top),
                    TexCoord = new Vector2(1, 1.0f - bottomRatio)
                });

                indexList.AddRange(new [] { 1, r1, 3, r1, r2, 3 });
            }
            #endregion

            #region Bottom-Side
            int b1 = 0;
            int b2 = 0;
            if (Padding.HasLeft) {
                b1 = vertexList.Count;
                vertexList.Add(new SpriteVertex {
                    Position = new Vector2(Padding.Left, height),
                    TexCoord = new Vector2(leftRatio, 1)
                });

                b2 = vertexList.Count;
                vertexList.Add(new SpriteVertex {
                    Position = new Vector2(width - Padding.Right, height),
                    TexCoord = new Vector2(1.0f - rightRatio, 1)
                });

                indexList.AddRange(new [] { 2, 3, b1, 3, b2, b1 });
            }
            #endregion

            #region Top-Left-Cornor
            if (Padding.HasTopLeft) {
                var index = vertexList.Count;
                vertexList.Add(new SpriteVertex {
                    Position = new Vector2(0, 0),
                    TexCoord = new Vector2(0, 0)
                });

                indexList.AddRange(new [] { index, t1, l1, t1, 0, l1 });
            }
            #endregion

            #region Top-Right-Cornor
            if (Padding.HasTopRight) {
                var index = vertexList.Count;
                vertexList.Add(new SpriteVertex {
                    Position = new Vector2(width, 0),
                    TexCoord = new Vector2(1, 0)
                });

                indexList.AddRange(new [] { t2, index, 1, index, r1, 1 });
            }
            #endregion

            #region Bottom-Right-Cornor
            if (Padding.HasBottomRight) {
                var index = vertexList.Count;
                vertexList.Add(new SpriteVertex {
                    Position = new Vector2(width, height),
                    TexCoord = new Vector2(1, 1)
                });

                indexList.AddRange(new [] { 3, r2, b2, r2, index, b2 });
            }
            #endregion

            #region Bottom-Left-Cornor
            if (Padding.HasBottomLeft) {
                var index = vertexList.Count;
                vertexList.Add(new SpriteVertex {
                    Position = new Vector2(0, height),
                    TexCoord = new Vector2(0, 1)
                });

                indexList.AddRange(new [] { l2, 2, index, 2, b1, index });
            }
            #endregion
            #endregion
            
            VertexCount = indexList.Count;

            var vertices = vertexList.ToArray();
            var indices = indexList.ToArray();

            var buffers = new int[2];
            GL.GenBuffers(2, buffers);

            vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            vbo = buffers[0];
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * 4 * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            ebo = buffers[1];
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(int), indices, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), new IntPtr(0));
            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 4 * sizeof(float), new IntPtr(2 * sizeof(float)));
            GL.EnableVertexAttribArray(1);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.BindVertexArray(0);

            rebuildVertices = false;
        }

        public void Dispose() {
            Texture.Dispose();

            if (vao != -1) {
                GL.DeleteVertexArray(vao);
                vao = -1;
            }

            if (vbo != -1) {
                GL.DeleteBuffer(vbo);
                vbo = -1;
            }

            if (ebo != -1) {
                GL.DeleteBuffer(vbo);
                ebo = -1;
            }
        }
        
        public void BindElementBuffer() {
            if (rebuildVertices) {
                if (vao != -1) {
                    GL.DeleteVertexArray(vao);
                    vao = -1;
                }

                if (vbo != -1) {
                    GL.DeleteBuffer(vbo);
                    vbo = -1;
                }

                if (ebo != -1) {
                    GL.DeleteBuffer(vbo);
                    ebo = -1;
                }

                BuildVertices();
            }

            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
        }
    }
}