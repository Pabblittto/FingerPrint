using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrint
{
    class Functions
    {

        private static Functions instance = new Functions();

        private Functions()
        {

        }


        public static Functions GetInstanc(int a)
        {
            return instance;
        }

        /// <summary>
        /// W przypadku kiedy mamy zbinaryzowany obraz (lub w skali szarości) wszystkie wartości 
        /// danego pixela są jednakowe, więc dany pixel można wyrazić tylko jedną wartością 
        /// (tą która jest aktualnie we wszystkich kolorach w danym pixelu). funkcja zwraca tablice 2D
        /// która jest reprezentacją danego obrazu (piksele od lewej do prawej wierszami). Zwracana tablica posiadan nastepujące indexy:
        /// table [y,x] więc jeżeli chcemy pobrać wartośc pixela, który jest w drugim rzędzie, piąty od
        /// lewej to piszemy tablica[1,4] (numeracja od 0)
        /// </summary>
        /// <param name="data">Tablica obrazu</param>
        /// <returns></returns>  
        /// 
        public int[,] Return2DTable(int[] data, int imageWidth, int imageHeight)
        {
            int[,] ImageInTable = new int[imageHeight, imageWidth];

            for (int i = 0; i < imageHeight; i++)
            {
                for (int j = 0; j < imageWidth; j++)
                {
                    ImageInTable[i, j] = data[i * (imageWidth * 3) + j * 3];
                }
            }// tablica z wartościami poszczególnych pikseli

            return ImageInTable;
        }

        /// <summary>
        /// Funkcja zwraca histogram stworzony na podstawie wycinka obrazu
        /// </summary>
        /// <param name="area">Obiekt wycinka w którym chcemy policzyć histogram </param>
        /// <param name="allArea">Obraz w tablicy 2D</param>
        /// <returns></returns>
        public int[] CreateHistogramFromrect(System.Drawing.Rectangle area, int[,] allArea)
        {
            int[] result = new int[256];

            for (Int32 y = 0; y < area.Height; ++y)
            {
                for (Int32 x = 0; x < area.Width; ++x)
                {
                    result[allArea[y + area.Y, x + area.X]]++;
                }
            }
            return result;
        }


        

    }
}
