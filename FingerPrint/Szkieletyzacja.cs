using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint
{
    class Szkieletyzacja
    {
        private static Szkieletyzacja instance = new Szkieletyzacja();

        private Szkieletyzacja()
        {
                
        }

        public static Szkieletyzacja GetInstance()
        {
            return instance;
        }


        public Bitmap Szkieletyzuj(Bitmap Img)
        {
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, Img.Width, Img.Height);
            BitmapData bitmapData = Img.LockBits(rect, ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            IntPtr ptr = bitmapData.Scan0;// wskaźnik na pierwszą linię obrazka
            int BitsPerPixel = 24;// na jeden pixel składają się trzy 8-bitowe wartości RGB
            int size = bitmapData.Width * bitmapData.Height * 3;

            byte[] data = new byte[size];// tablica z wartościami RGB
            for (int y = 0; y < bitmapData.Height; y++)
            {
                IntPtr mem = (IntPtr)((long)bitmapData.Scan0 + y * bitmapData.Stride);
                Marshal.Copy(mem, data, y * bitmapData.Width * 3, bitmapData.Width * 3);// skopiuj całą bitmapę do tablicy
            }

                



            /// Poniżej kod do zamiany obrazu z data do obiektu Bitmap
            for (int y = 0; y < bitmapData.Height; y++)
            {
                IntPtr mem = (IntPtr)((long)bitmapData.Scan0 + y * bitmapData.Stride);
                Marshal.Copy(data, y * bitmapData.Width * 3, mem, bitmapData.Width * 3);// skopiuj całą bitmapę do tablicy
            }

            Img.UnlockBits(bitmapData);

            return Img;
        }

    }
}
