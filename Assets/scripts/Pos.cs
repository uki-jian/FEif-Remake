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
    public bool Equals(Pos a)
    {
        return GetHashCode() == a.GetHashCode();
    }
    public override int GetHashCode()
    {
        return (x * 100 + z).ToString().GetHashCode();
        //return x.ToString().GetHashCode()+ z.ToString().GetHashCode();
    }
    public static bool Equal(Pos a, Pos b)
    {
        return a.x == b.x && a.z == b.z;
    }
    
    public Pos GetLeft(int step=1)
    {
        return new Pos(x - step, z);
    }
    public Pos GetRight(int step = 1)
    {
        return new Pos(x + step, z);
    }
    public Pos GetUp(int step = 1)
    {
        return new Pos(x, z + step);
    }
    public Pos GetDown(int step = 1)
    {
        return new Pos(x, z - step);
    }
    public List<Pos> GetEquidistanceGrids(int dist = 1)
    {
        List<Pos> res = new List<Pos>();
        for(int i=0; i<dist; i++)
        {
            int j = dist - i;
            res.Add(new Pos(x + i, z + j));
            res.Add(new Pos(x + j, z - i));
            res.Add(new Pos(x - i, z - j));
            res.Add(new Pos(x - j, z + i));
        }
        return res;
    }

    public int CalcMagnitude(Pos dest)
    {
        return Mathf.Abs(x - dest.x) + Mathf.Abs(z - dest.z);
    }
    static public int CalcMagnitude(Pos origin, Pos dest)
    {
        return Mathf.Abs(origin.x - dest.x) + Mathf.Abs(origin.z - dest.z);
    }
}
