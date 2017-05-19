using System.Runtime.InteropServices;

namespace ExtractPngOfExtIcon
{
    partial class Program
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left, top, right, bottom;
        }

    }
}
