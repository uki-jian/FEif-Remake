using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pos
{
    public int x;
    public int z;
    public Pos(int _x, int _z)
    {
        x = _x;
        z = _z;
    }
    public Pos()
    {
        x = 0;
        z = 0;
    }
    public static bool Equal(Pos a, Pos b)
    {
        return a.x == b.x && a.z == b.z;
    }
}
