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

    for (Int32 y = 0; y < area.Height; ++y)
    {
        for (Int32 x = 0; x < area.Width; ++x)
        {
            // tu jakie� dzia�ania na ka�dym pikselu z wycinka 
        }
    }
    return result;
}
```


