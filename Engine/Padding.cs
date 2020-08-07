namespace BreakoutSharp.Engine {
    struct Padding {
        public Padding(int all) {
            Left = all;
            Top = all;
            Right = all;
            Bottom = all;
        }

        public Padding(int horizontal, int vertical) {
            Left = horizontal;
            Top = vertical;
            Right = horizontal;
            Bottom = vertical;
        }
        
        public Padding(int left, int top, int right, int bottom) {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
        public bool HasLeft { get { return Left > 0; } }
        public bool HasTopLeft { get { return Left > 0 && Top > 0; } }
        public bool HasTop { get { return Top > 0; } }
        public bool HasTopRight { get { return Right > 0 && Top > 0; } }
        public bool HasRight { get { return Right > 0; } }
        public bool HasBottomRight { get { return Right > 0 && Bottom > 0; } }
        public bool HasBottom { get { return Bottom > 0; } }
        public bool HasBottomLeft { get { return Left > 0 && Bottom > 0; } }
    }
}