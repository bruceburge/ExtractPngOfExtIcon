﻿using System;
using System.Runtime.InteropServices;

namespace ExtractPngOfExtIcon
{
    partial class Program
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct IMAGEINFO
        {
            public IntPtr hbmImage;
            public IntPtr hbmMask;
            public int Unused1;
            public int Unused2;
            public RECT rcImage;
        }

    }
}
