using System.Runtime.InteropServices;

namespace ExtractPngOfExtIcon
{
    partial class Program
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            int x;
            int y;
        }

    }
}
