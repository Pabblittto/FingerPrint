# Singletony 
wiêkszoœæ obiektów to singletony. Uzycie banalne pewnie wiecie:
``` C#
Class.GetInstance().funkcja();
```
W klasie 
```C#
 class Functions{} ;
``` 
Znajduj¹s ie przydatne funkcje które mog¹ sie przydaæ. Jedna zwraca tablice 2D która reprezentuje obraz.Druna natomiast  na podstawie
wspomnianej tablicy 2D i Onketu Rect jest w stanie obiczyæ hitogram z wskazanego wycinka.

Aby utworzyæ podobne funkcja nela¿y napisaæ w³asn¹ funkcjê o strukturze: 
``` C#
class Zwracanytyp{};// mo¿e byæ jakiœ typ prosty

public Zwracanytyp funkcja(Rect rect, int[,] AllArea){

    Zwracanytyp result = new Zwracanytyp();

    for (Int32 y = 0; y < rect.Height; ++y)
    {
        for (Int32 x = 0; x < rect.Width; ++x)
        {
            result[allArea[y + rect.Y, x + rect.X]]++;
			// tu jakies dzia³ania na pixelach , nalezy poamiêtaæ o dodawniu 
			//wartoœci lokalnych x,y do wartoœci rect.Y i rect.X
        }
    }
    return result;
}
```


