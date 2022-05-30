using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Jsondata
{
    public List<PlayerData> myplayerdata;
}
[Serializable]
public class PlayerData
{
    public int id;
    public string password;
    public float coins;
}