using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ExtensionIconExtractor
{
    public class Extractor
    {

        const string IID_IImageList = "46EB5926-582E-4017-9FDF-E8998DAA0950";
        const string IID_IImageList2 = "192B9D83-50FC-457B-90A0-2B82A8B5DAE1";

        //Working example as of 2017/05/19 on windows 10 x64
        //example found here.
        //http://stackoverflow.com/a/28530403/1572750

        /// <summary>
        /// Get the icon assoicated with a given extension
        /// The size of the Image will be either 16x16,32x32,48x48, or 256x256 depending on which is choosen in the <paramref name="size"/> parameter
        /// </summary>
        /// <param name="ext">Extension of the file to get icon from</param>
        /// <param name="size">The size of the image of the icon you want back</param>
        /// <returns>Image in Png format</returns>
        public static Image GetPngFromExtension(string ext, IconSizes size)
        {
            ext = ext.Replace("*", "").Replace(".", ""); //clean the param up.

            IntPtr hIcon = GetIcon(GetIconIndex("*." + ext), size);
            Image result = null;
            // from native to managed
            try
            {
                using (Icon ico = (Icon)Icon.FromHandle(hIcon).Clone())
                {
                    // save to stream to convert to png, then back.
                    using (MemoryStream stream = new MemoryStream())
                    {
                        ico.ToBitmap().Save(stream, ImageFormat.Png);
                        result = Image.FromStream(stream);
                    }
                }
            }
            catch(Exception ex)
            {
                //TODO: log maybe?
                throw;
            }
            finally
            {
                NativeMethods.DestroyIcon(hIcon); // don't forget to cleanup
            }
            
            return result;
        }

        public static Image GetPngFromExtension(string ext, SHIL_IconSizes size)
        {
            ext = ext.Replace("*", "").Replace(".", "");
            IntPtr hIcon = GetIcon(GetIconIndex("*." + ext), size);
            Image result = null;
            // from native to managed
            using (Icon ico = (Icon)Icon.FromHandle(hIcon).Clone())
            {
                // save to stream to convert to png, then back.
                using (MemoryStream stream = new MemoryStream())
                {
                    ico.ToBitmap().Save(stream, ImageFormat.Png);
                    result = Image.FromStream(stream);
                }
            }
            NativeMethods.DestroyIcon(hIcon); // don't forget to cleanup
            return result;
        }


        static IntPtr GetIcon(int iImage, IconSizes size)
        {
            IImageList spiml = null;
            Guid guil = new Guid(IID_IImageList2);//or IID_IImageList

            NativeMethods.SHGetImageList((int)size, ref guil, ref spiml);
            IntPtr hIcon = IntPtr.Zero;
            spiml.GetIcon(iImage, NativeMethods.ILD_TRANSPARENT | NativeMethods.ILD_IMAGE, ref hIcon);

            return hIcon;
        }

        static IntPtr GetIcon(int iImage, SHIL_IconSizes size)
        {
            IImageList spiml = null;
            Guid guil = new Guid(IID_IImageList2);//or IID_IImageList

            NativeMethods.SHGetImageList((int)size, ref guil, ref spiml);
            IntPtr hIcon = IntPtr.Zero;
            spiml.GetIcon(iImage, NativeMethods.ILD_TRANSPARENT | NativeMethods.ILD_IMAGE, ref hIcon);

            return hIcon;
        }
        static int GetIconIndex(string pszFile)
        {
            SHFILEINFO sfi = new SHFILEINFO();
            NativeMethods.SHGetFileInfo(pszFile
                , 0
                , ref sfi
                , (uint)System.Runtime.InteropServices.Marshal.SizeOf(sfi)
                , (uint)(SHGFI.SysIconIndex | SHGFI.LargeIcon | SHGFI.UseFileAttributes));
            return sfi.iIcon;
        }

    }
}
