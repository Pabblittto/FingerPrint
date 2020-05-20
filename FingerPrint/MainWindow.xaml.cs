﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Drawing;
using FingerPrint.ExtraWindows;

namespace FingerPrint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OpenFileDialog imageDialog = new OpenFileDialog();
        SaveFileDialog imgSaveDialog = new SaveFileDialog();

        TheresholdInput inputwindow;

        Bitmap Img;

        public MainWindow()
        {
            InitializeComponent();

            imgSaveDialog.Filter = "Png image(*.png)|*.png|Jpeg image(*.jpeg)|*.jpeg|Bmp image(*.bmp)|*.bmp|gif image(*.gif)|*.gif|Tiff image(*.tiff)|*.tiff";
            imgSaveDialog.Title = "Save image as...";

            imageDialog.Title = "Select a picture";
            imageDialog.Filter = "Image files (.jpg,png,bmp,gif,tiff,tif)|*.tiff;*.jpg;*.png;*.gif;*.tif";

        }

        /// <summary>
        /// Funkcja aktualizuje obraz na ekranie, warunkiem jest to że nowy obraz, który ma zostac wyświetlony musi być
        /// zapisany do obiektu Img typu Bitmap 
        /// </summary>
        public void UpdateImageOnScreen()
        {

            using (MemoryStream memory = new MemoryStream())
            {
                Img.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                GC.KeepAlive(memory);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                modifiedImage.Source = bitmapimage;
            }
        }


        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // zapisywanie obrazu 
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            imgSaveDialog.FileName = "";
            if (imgSaveDialog.ShowDialog() == true)
            {
                System.Drawing.Imaging.ImageFormat format = System.Drawing.Imaging.ImageFormat.Png;

                string extension = System.IO.Path.GetExtension(imgSaveDialog.FileName);
                switch (extension)
                {
                    case ".jpeg":
                        format = System.Drawing.Imaging.ImageFormat.Jpeg;
                        break;
                    case ".gif":
                        format = System.Drawing.Imaging.ImageFormat.Gif;
                        break;
                    case ".tiff":
                        format = System.Drawing.Imaging.ImageFormat.Tiff;
                        break;
                    case ".png":
                        format = System.Drawing.Imaging.ImageFormat.Png;
                        break;
                    case ".bmp":
                        format = System.Drawing.Imaging.ImageFormat.Bmp;
                        break;
                }

                Img.Save(imgSaveDialog.FileName, format);
            }
        }

        // wczytywanie obrazu 
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            save.IsEnabled = true;
            if (imageDialog.ShowDialog() == true)
            {
                Img = new Bitmap(imageDialog.FileName);// creating img for workshop

                MemoryStream ms = new MemoryStream();
                Img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                Img = new Bitmap(ms);
                using (MemoryStream memory = new MemoryStream())
                {
                    Img.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                    memory.Position = 0;
                    GC.KeepAlive(memory);
                    BitmapImage bitmapimage = new BitmapImage();
                    bitmapimage.BeginInit();
                    bitmapimage.StreamSource = memory;
                    bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapimage.EndInit();

                    loadedImage.Source = bitmapimage;
                }

                UpdateImageOnScreen();// set workshio displaying image
            }
        }

        private void ZoomSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            double newUserZoomValue = e.NewValue;
            ScaleTransform scaleTransform = new ScaleTransform(newUserZoomValue, newUserZoomValue);
            modifiedImage.LayoutTransform = scaleTransform;
        }


        private void Otsu_Click(object sender, RoutedEventArgs e)
        {
           var image= Binarization.GetInstance().OtsuMethod(Img, true);
            Img = image;
            UpdateImageOnScreen();
        }

        private void TheresholdBin_Click(object sender, RoutedEventArgs e)
        {
            inputwindow = new TheresholdInput();
            if (inputwindow.ShowDialog() == true)
            {
                int value = inputwindow.result;
                var tmp=Binarization.GetInstance().BinarizeThereshol(Img, value,true);
                Img = tmp;
                UpdateImageOnScreen();
            }
        }

        private void GrayScale_Click(object sender, RoutedEventArgs e)
        {
            var tmp = GrayScale.GetInstance().TurnIntoGray(Img);
            Img = tmp;
            UpdateImageOnScreen();
        }

        private void Szkieletyzacja_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var tmp = Szkieletyzacja.GetInstance().Szkieletyzuj(Img);
                Img = tmp;
                MessageBox.Show("Operacja zakończona.");
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }


            UpdateImageOnScreen();
        }


        private void Rozgalezienia_Click(object sender, RoutedEventArgs e)
        {
            try {
                var tmp = Rozgalezienia.GetInstance().Find(Img);
                Img = tmp;
                MessageBox.Show("Operacja zakończona.");
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            UpdateImageOnScreen();
        }
    }
}
