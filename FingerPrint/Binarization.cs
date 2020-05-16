﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint
{
    /// <summary>
    /// Singleton
    /// </summary>
    class Binarization
    {
        private static Binarization instance = new Binarization();

        private Binarization()
        {

        }

        public static Binarization GetInstance()
        {
            return instance;
        }

        public Bitmap OtsuMethod(Bitmap Image, bool isTopThereshold)
        {
            int[] hist = new int[256];
            hist =  Histogram.GetInstance().CreateHistogram(Image, 0);
            double p1, p2;
            double[] VarianceBetweenTable = new double[256];

            int MeanIAll = MeanIntensitiesK(0, 255, hist); //mean intensivities of pixels in the whole image.

            for (int k = 1; k < 256; k++)
            {
                p1 = ProbabilityK(0, k, hist) * Math.Pow((MeanIntensitiesK(0, k, hist) - MeanIAll), 2);
                p2 = ProbabilityK(k + 1, 255, hist) * Math.Pow((MeanIntensitiesK(k + 1, 255, hist) - MeanIAll), 2);
                if (p1 == 0)
                    p1 = 1;
                if (p2 == 0)
                    p2 = 1;

                VarianceBetweenTable[k] = p1 + p2;
            }

            int point = FindMaximum(VarianceBetweenTable);
            
           return  BinarizeThereshol(Image, point, isTopThereshold);
        }

        private int ProbabilityK(int begin, int end, int[] hist)
        {
            int sum = 0;

            for (int i = begin; i <= end; i++)
            {
                sum += hist[i];
            }
            return sum;
        }

        private int MeanIntensitiesK(int begin, int end, int[] hist)
        {
            int sum = 0;
            for (int i = begin; i < end; i++)
            {
                sum += i * hist[i];
            }
            return sum;
        }


        private int FindMaximum(double[] list)
        {
            double max = 0;
            int idx = 0;

            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] > max)
                {
                    max = list[i];
                    idx = i;
                }
            }

            return idx;
        }

        public Bitmap BinarizeThereshol(Bitmap Image, int point, Boolean isTopThereshold)
        {
            System.Drawing.Rectangle rect = new Rectangle(0, 0, Image.Width, Image.Height);
            BitmapData bitmapData = Image.LockBits(rect, ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            IntPtr ptr = bitmapData.Scan0;// wskaźnik na pierwszą linię obrazka
            int size = bitmapData.Width * bitmapData.Height * 3;

            byte[] data = new byte[size];// tablica z wartościami RGB
            for (int y = 0; y < bitmapData.Height; y++)
            {
                IntPtr mem = (IntPtr)((long)bitmapData.Scan0 + y * bitmapData.Stride);
                Marshal.Copy(mem, data, y * bitmapData.Width * 3, bitmapData.Width * 3);// skopiuj całą bitmapę do tablicy
            }

            for (int i = 0; i < size; i += 3)// kazda komórka zawiera jedną z trzech wartości- więc przesuwa się co 3 pixele
            {
                if (data[i] > point)
                {
                    data[i] = (isTopThereshold) ? (byte)255 : (byte)0;
                    data[i + 1] = (isTopThereshold) ? (byte)255 : (byte)0;
                    data[i + 2] = (isTopThereshold) ? (byte)255 : (byte)0;
                }
                else
                {
                    data[i] = (isTopThereshold) ? (byte)0 : (byte)255;
                    data[i + 1] = (isTopThereshold) ? (byte)0 : (byte)255;
                    data[i + 2] = (isTopThereshold) ? (byte)0 : (byte)255;
                }
            }

            for (int y = 0; y < bitmapData.Height; y++)
            {
                IntPtr mem = (IntPtr)((long)bitmapData.Scan0 + y * bitmapData.Stride);
                Marshal.Copy(data, y * bitmapData.Width * 3, mem, bitmapData.Width * 3);// skopiuj całą bitmapę do tablicy
            }

            Image.UnlockBits(bitmapData);

            MemoryStream stream = new MemoryStream();

            Image.Save(stream, ImageFormat.Bmp);
            return Image;
        }




    }
}
