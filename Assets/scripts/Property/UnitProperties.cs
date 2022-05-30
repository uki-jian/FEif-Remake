using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UnitProperties
{
    public List<UnitProperty> unitProperties;
}
[Serializable]
public class UnitProperty
{
    public int u_id;
    public string u_name;
    public string u_name_eng;
    public bool u_isNobel;
    public int u_army;  //ËùÊô¾ü¶Ó
    public string u_class;
    public int u_level;
    public int u_exp;
    public int u_weapon_level;
    public int u_weapon_exp;
    public int u_moveSpeed;
    public int u_hitpoint;
    public int u_attack;
    public int u_magicAttack;
    public int u_skill;
    public int u_speed;
    public int u_luckiness;
    public int u_defence;
    public int u_magicDefence;
    
    public List<string> u_abilities;
}