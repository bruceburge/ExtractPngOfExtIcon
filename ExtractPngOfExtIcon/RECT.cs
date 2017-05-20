using System.Runtime.InteropServices;

namespace ExtensionIconExtractor
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct RECT
    {
        public int left, top, right, bottom;
    }

}
