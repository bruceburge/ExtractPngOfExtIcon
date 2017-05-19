using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace ExtractPngOfExtIcon
{
    partial class Program
    {

        const string IID_IImageList = "46EB5926-582E-4017-9FDF-E8998DAA0950";
        const string IID_IImageList2 = "192B9D83-50FC-457B-90A0-2B82A8B5DAE1";

        //Working example as of 2017/05/19 on windows 10 x64
        //example found here.
        //http://stackoverflow.com/a/28530403/1572750
        public static void Main(string[] args)
        {
            var ext = "config";
            IntPtr hIcon = GetJumboIcon(GetIconIndex("*." + ext));

            // from native to managed
            using (Icon ico = (Icon)Icon.FromHandle(hIcon).Clone())
            {
                // save to file (or show in a picture box)
                ico.ToBitmap().Save(ext + "icon.png", ImageFormat.Png);
                //Linqpad dump
                //ico.ToBitmap().Dump();
            }
            Shell32.DestroyIcon(hIcon); // don't forget to cleanup
        }

        static int GetIconIndex(string pszFile)
        {
            SHFILEINFO sfi = new SHFILEINFO();
            Shell32.SHGetFileInfo(pszFile
                , 0
                , ref sfi
                , (uint)System.Runtime.InteropServices.Marshal.SizeOf(sfi)
                , (uint)(SHGFI.SysIconIndex | SHGFI.LargeIcon | SHGFI.UseFileAttributes));
            return sfi.iIcon;
        }

        // 256*256
        static IntPtr GetJumboIcon(int iImage)
        {
            IImageList spiml = null;
            Guid guil = new Guid(IID_IImageList2);//or IID_IImageList

            Shell32.SHGetImageList(Shell32.SHIL_JUMBO, ref guil, ref spiml);
            IntPtr hIcon = IntPtr.Zero;
            spiml.GetIcon(iImage, Shell32.ILD_TRANSPARENT | Shell32.ILD_IMAGE, ref hIcon);

            return hIcon;
        }

    }
}
