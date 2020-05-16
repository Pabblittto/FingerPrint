# Singletony 
wi�kszo�� obiekt�w to singletony. Uzycie banalne pewnie wiecie:
``` C#
Class.GetInstance().funkcja();
```
W klasie 
```C#
 class Functions{} ;
``` 
Znajduj�s ie przydatne funkcje kt�re mog� sie przyda�. Jedna zwraca tablice 2D kt�ra reprezentuje obraz.Druna natomiast  na podstawie
wspomnianej tablicy 2D i Onketu Rect jest w stanie obiczy� hitogram z wskazanego wycinka.

Aby utworzy� podobne funkcja nela�y napisa� w�asn� funkcj� o strukturze: 
``` C#
class Zwracanytyp{};// mo�e by� jaki� typ prosty

public Zwracanytyp funkcja(Rect rect, int[,] AllArea){

    Zwracanytyp result = new Zwracanytyp();

    for (Int32 y = 0; y < rect.Height; ++y)
    {
        for (Int32 x = 0; x < rect.Width; ++x)
        {
            result[allArea[y + rect.Y, x + rect.X]]++;
			// tu jakies dzia�ania na pixelach , nalezy poami�ta� o dodawniu 
			//warto�ci lokalnych x,y do warto�ci rect.Y i rect.X
        }
    }
    return result;
}
```


