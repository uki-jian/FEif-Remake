using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//[Serializable]//这个类不能序列化！
public class GridProperties
{
    public int width;
    public int height;
    public List<GridProperty> gridProperties = new List<GridProperty>();
    //public GridProperties()
    //{
    //    gridProperties = new List<GridProperty>(100);
    //}
}
[Serializable]
public class GridProperty
{
    public int w_index;
    public int h_index;
    public string name;
    public bool isAccessible;
    public int defenceBounus;
    public int avoidanceBounus;
    public int hitpointBounus;
    public int AccessibilityIndex;
    public int altitude;

}