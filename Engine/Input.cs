#region Using directives
using System;
using System.Runtime.InteropServices;
#endregion

namespace BreakoutSharp.Engine {
    enum KeyCode : int {
        #region Main mouse buttons
        Mouse1 = 0x01,
        Mouse2 = 0x02,
        #endregion

        Cancel = 0x03,
        
        #region Extra mouse buttons
        Mouse3 = 0x04,
        Mouse4 = 0x05,
        Mouse5 = 0x06,
        #endregion

        #region Main area keys
        Backspace = 0x08,
        Tab = 0x09,
        Clear = 0x0C,
        Enter = 0x0D,
        Shift = 0x10,
        Ctrl = 0x11,
        Alt = 0x12,
        Pause = 0x13,
        CapsLock = 0x14,
        Escape = 0x1B,
        Space = 0x20,
        #endregion

        #region Main area symbols
        Semicolon = 0xBA,
        Plus = 0xBB,
        Comma = 0xBC,
        Minus = 0xBD,
        Period = 0xBE,
        Slash = 0xBF,
        Grave = 0x0C,
        LeftSBracket = 0xDB,
        ReversedSlash = 0xDC,
        RightSBracket = 0xDD,
        Quote = 0xDE,
        #endregion

        #region Cursor control keys
        PageUp = 0x21,
        PageDown = 0x22,
        End = 0x23,
        Home = 0x24,
        Left = 0x25,
        Up = 0x26,
        Right = 0x27,
        Down = 0x28,
        Select = 0x29,
        Print = 0x2A,
        Execute = 0x2B,
        PrintScreen = 0x2C,
        Insert = 0x2D,
        Delete = 0x2E,
        Help = 0x2F,
        #endregion

        #region Digital keys
        D0 = 0x30,
        D1 = 0x31,
        D2 = 0x32,
        D3 = 0x33,
        D4 = 0x34,
        D5 = 0x35,
        D6 = 0x36,
        D7 = 0x37,
        D8 = 0x38,
        D9 = 0x39,
        #endregion

        #region Alphabet keys
        A = 0x41,
        B = 0x42,
        C = 0x43,
        D = 0x44,
        E = 0x45,
        F = 0x46,
        G = 0x47,
        H = 0x48,
        I = 0x49,
        J = 0x4A,
        K = 0x4B,
        L = 0x4C,
        M = 0x4D,
        N = 0x4E,
        O = 0x4F,
        P = 0x50,
        Q = 0x51,
        R = 0x52,
        S = 0x53,
        T = 0x54,
        U = 0x55,
        V = 0x56,
        W = 0x57,
        X = 0x58,
        Y = 0x59,
        Z = 0x5A,
        #endregion

        #region Natural keyboard keys
        Lwin = 0x5B,
        Rwin = 0x5C,
        Apps = 0x5D,
        Sleep = 0x5F,
        #endregion

        #region NumPad keys
        NumPad0 = 0x60,
        NumPad1 = 0x61,
        NumPad2 = 0x62,
        NumPad3 = 0x63,
        NumPad4 = 0x64,
        NumPad5 = 0x65,
        NumPad6 = 0x66,
        NumPad7 = 0x67,
        NumPad8 = 0x68,
        NumPad9 = 0x69,
        NumPadMultiply = 0x6A,
        NumPadAdd = 0x6B,
        NumPadSeparator = 0x6C,
        NumPadSubtract = 0x6D,
        NumPadDecimal = 0x6E,
        NumPadDivide = 0x6F,
        #endregion
        
        #region Function keys
        F1 = 0x70,
        F2 = 0x71,
        F3 = 0x72,
        F4 = 0x73,
        F5 = 0x74,
        F6 = 0x75,
        F7 = 0x76,
        F8 = 0x77,
        F9 = 0x78,
        F10 = 0x79,
        F11 = 0x7A,
        F12 = 0x7B,
        #endregion
        NumLock = 0x90,
        ScrollLock = 0x91,
        LeftShift = 0xA0,
        RightShift = 0xA1,
        LeftCtrl = 0xA2,
        RightCtrl = 0xA3,
        LeftAlt = 0xA4,
        RightAlt = 0xA5

    }

    static class Input {
        struct KeyState {
            public bool Pressing;
            public bool Triggering;
        }

        [DllImport("user32.dll", EntryPoint="GetAsyncKeyState", ExactSpelling=true)]
        static extern short GetAsynKeyState(int vKey);

        static KeyState[] states = new KeyState[256];

        public static void Update() {
            for (var i = 1; i < 255; i++) {
                var result = GetAsynKeyState(i);
                if ((result & 0x8000) != 0) {
                    if (!states[i].Pressing) {
                        states[i].Triggering = true;
                        states[i].Pressing = true;
                        continue;
                    }

                    states[i].Triggering = false;
                    continue;
                }
                
                states[i].Pressing = false;
                states[i].Triggering = false;
            }
        }

        public static bool GetKeyPressing(KeyCode key) {
            return states[(int)key].Pressing;
        }

        public static bool GetKeyTriggering(KeyCode key) {
            return states[(int)key].Triggering;
        }
    }
}