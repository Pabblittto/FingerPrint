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
    class Rozgalezienia
    {
        private static Rozgalezienia instance = new Rozgalezienia();

        private Rozgalezienia()
        {
                
        }

        public static Rozgalezienia GetInstance()
        {
            return instance;
        }

        public Bitmap Find(Bitmap Img)
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


            /// /////////////////////////////////////// 
            /// Miejsce na kod, w tablicy data zajduje sie obraz
            /// znajduje się on w taki sposób że co trzy komórki znajduje się pierwszy odcień (R) danego pixela
            /// to znaczy żeby sie dostać do wartosci R w drugim pikselu trzeba iśc do 3 komórki
            /// ponieważ trzy pierwsze komórki są zajmopwane przez wartości R,G,B pierwszego pixela
            /// żeby zapisać zaminy które wykonał algorytm trzeba zapisać nowe wartości do tablicy data
            /// wtedy kod na dole funkcji wsadzi te dane do obiektu Img i będzie git.
            /// Sprawdz wszystkie Funkcje znajdujące się w Singletonie Functions może ci się przydadzą :)
            /// Może dodatkowo niech funkcja zwraca tablice obiektów typu Point które okreslają w ktorych miejscach te
            /// rozgałęzienia wystepują


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
