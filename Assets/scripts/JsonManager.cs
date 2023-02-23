using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class JsonManager : MonoBehaviour
{
    public UnitProperties unitProperties;
    public GridProperties gridProperties;
    public bool iWantWrite; //是否写入：小心不要被覆盖
    // Use this for initialization
    void Start()
    {
        iWantWrite = true;
        iWantWrite = false;
        
        



        string jsonName = "/gridProps.json";
        string jsonpath = Application.streamingAssetsPath + jsonName;

        
        if(iWantWrite)WriteJson(jsonpath);
        StartCoroutine(ReadJson());

        //print("UNIT_COUNT:" + unitProperties.unitProperties.Count);
        //print("GRID_COUNT:" + gridProperties.gridProperties.Count);
    }

    /// <summary>
    /// 写json
    /// </summary>
    public void WriteJson(string jsonpath)
    {
        if (!File.Exists(jsonpath))
        {
            File.Create(jsonpath);
        }
        string serialData="";

        //CreateNewUnits();
        CreateNewGrids(out serialData);

        File.WriteAllText(jsonpath, serialData);

    }


    /// <summary>
    /// 读json
    /// </summary>
    /// <returns></returns>
    IEnumerator ReadJson()
    {
        string jsonpath = Application.streamingAssetsPath + "/";

        unitProperties = new UnitProperties();
        unitProperties = JsonUtility.FromJson<UnitProperties>(GetJsonData(jsonpath + "unitProps.json"));

        gridProperties = new GridProperties();
        gridProperties = JsonUtility.FromJson<GridProperties>(GetJsonData(jsonpath + "gridProps.json"));
        //print("UNITNUM:" +unitProperties.unitProperties.Count);
        yield return new WaitForSeconds(0);
    }
    string GetJsonData(string jsonPath)
    {
        string serialData;
        if (File.Exists(jsonPath))
        {
            //print("FILE_FOUND");
            serialData = File.ReadAllText(jsonPath);
            //print("SERIAL_DATA：" + serialData);
        }
        else serialData = "FILE NOT FOUND!";
        return serialData;
    }
    void CreateNewUnits(out string serialData)
    {
        if(unitProperties == null)
        {
            unitProperties = new UnitProperties();
            unitProperties.unitProperties = new List<UnitProperty>();
        }

        for (int i = 0; i < 10; i++)
        {
            UnitProperty up = new UnitProperty();
            up.u_name = "神威";
            up.u_class = "武士";
            up.u_level = 1;
            up.u_exp = 0;
            up.u_weapon_level = 2;
            up.u_weapon_exp = 10;
            up.u_abilities = new List<string>();
            up.u_abilities.Add("不可思议的魅力");
            up.u_abilities.Add("高贵的血统");
            unitProperties.unitProperties.Add(up);

        }
        serialData = JsonUtility.ToJson(unitProperties, true);
    }

    void CreateNewGrids(out string serialData)
    {
        if (gridProperties == null)
        {
            gridProperties = new GridProperties();
            gridProperties.gridProperties = new List<GridProperty>();
            gridProperties.width = 10;
            gridProperties.height = 10;
        }

        for (int i = 0; i < gridProperties.width * gridProperties.height; i++)
        {
            GridProperty gp = new GridProperty();
            gp.name = "地面";
            gp.w_index = i % gridProperties.width;
            gp.h_index = i / gridProperties.width;
            gp.isAccessible = true;
            gp.defenceBounus = 0;
            gp.avoidanceBounus = 0;
            gp.hitpointBounus = 0;
            gp.AccessibilityIndex = 1;


            if (gp.w_index >= 3 && gp.w_index <= 6 && gp.h_index >= 3 && gp.h_index <= 6)
            {
                gp.name = "最下层";
                gp.altitude = 0;
                gp.defenceBounus = 1;
                gp.avoidanceBounus = 10;
                gp.hitpointBounus = 1;
            }
            else if (gp.w_index >= 2 && gp.w_index <= 7 && gp.h_index >= 2 && gp.h_index <= 7)
            {
                gp.name = "中间层";
                gp.altitude = 1;
                gp.defenceBounus = -1;
                gp.avoidanceBounus = -10;
                gp.hitpointBounus = -1;
            }
            else gp.altitude = 2;

            gridProperties.gridProperties.Add(gp);
            //public string name;
            //public bool isAccessible;
            //public int defenceBounus;
            //public int avoidanceBounus;
            //public int hitpointBounus;
            //public int AccessibilityIndex;
        }
        serialData = JsonUtility.ToJson(gridProperties, true);
    }

}

//public class WeaponProperty
//{
//    public string w_name;
//    public string w_class;
//    public int w_attack;
//    public int w_critical;
//    public int w_avoidance;
//}