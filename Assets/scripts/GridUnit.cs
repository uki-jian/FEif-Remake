using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridUnit : MonoBehaviour
{
    public Pos pos = new Pos();    //x, z的索引坐标
    bool isChosen = false;  //是否被选中
    public bool isExist;    //是否存在
    public float altitude;  //每格相对高度（相对于基准高度）
    readonly public GridProperty gridProperty; //grid基本属性:只读

    public GameObject patternChoose; //格子实体（选择的贴图）
    public GameObject patternAttack; //格子实体（选择的贴图）
    public GameObject patternDanger; //格子实体（选择的贴图）
    public GameObject patternFullDanger; //格子实体（选择的贴图）

    public static Vector3 patternOffset=new Vector3(0,0.1f,0);  //贴图相对于格子的偏移量

    public bool IsChosen { get => isChosen; set => isChosen = value; }

    public RoleUnit roleOn;

    public int walkPoint; //剩余移动点数(求路径用）
    public GridUnit father; //父亲节点(求路径用）

    public float FValue;    //AStar comprehensive estimate
    public float GValue;    //AStar to origin estimate
    public float HValue;    //AStar to destination estimate

    static public int maxCost = 100;

    //public Material mat_patternAttack;
    //public UnitRole avatar;

    public GridUnit(GridProperty _gridProperty, GameObject _choose, GameObject _attack, GameObject _danger, GameObject _fullDanger)
    {
        isExist = true;
        IsChosen = false;
        
        gridProperty = _gridProperty;
        patternChoose = _choose;
        patternAttack = _attack;
        patternDanger = _danger;
        patternFullDanger = _fullDanger;

        patternChoose.SetActive(false);
        patternAttack.SetActive(false);
        patternDanger.SetActive(false);
        patternFullDanger.SetActive(false);

        altitude = gridProperty.altitude;
    }

    //public bool Equals(GridUnit g)
    //{
    //    return (this.pos == g.pos);
    //}
    public bool Equals(GridUnit a)
    {
        return GetHashCode() == a.GetHashCode();
    }
    public override int GetHashCode()
    {
        return (pos.x * 100 + pos.z).ToString().GetHashCode();
    }
    public void AvatarGoIn(ref RoleUnit unit)
    {
        //avatar = ref unit;
    }
    public void AvatarGoOut()
    {
        //avatar = null;
    }
    public void SetPos(int i, int j)
    {
        pos = new Pos(i, j);
    }
    public void SetPos(Pos p)
    {
        pos = p;
    }
    public Pos GetPos()
    {
        return pos;
    }

    //public void SetPattern(GameObject inst)
    //{
    //    pattern = inst;
    //    SetPatternActive(false);
    //}
    public void SetChoose()
    {
        patternChoose.SetActive(true);
        patternAttack.SetActive(false);
        patternDanger.SetActive(false);
        patternFullDanger.SetActive(false);
    }
    public void SetAttack()
    {
        patternChoose.SetActive(false);
        patternAttack.SetActive(true);
        patternDanger.SetActive(false);
        patternFullDanger.SetActive(false);
    }
    public void SetDanger()
    {
        patternChoose.SetActive(false);
        patternAttack.SetActive(false);
        patternDanger.SetActive(true);
        patternFullDanger.SetActive(false);
    }
    public void SetFullDanger()
    {
        patternChoose.SetActive(false);
        patternAttack.SetActive(false);
        patternDanger.SetActive(false);
        patternFullDanger.SetActive(true);
    }
    public void SetReset()
    {
        patternChoose.SetActive(false);
        patternAttack.SetActive(false);
        patternDanger.SetActive(false);
        patternFullDanger.SetActive(false);
    }

    public void SetPatternColor(int color=0)
    {
        //(0,162,255,170)
        //Material mat = pattern.GetComponent<MeshRenderer>().material; //.color = new Color32(144, 0, 255, 170);
        //mat = Instantiate(mat);
        //mat.color = new Color32(144, 0, 255, 170);
        //pattern.GetComponent<MeshRenderer>().material = mat;
        //pattern.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Material/gridMat2");
        //pattern = Resources.Load<GameObject>("Prefab/gridImage2");

    }

    public int CalcAccessCost(RoleUnit role)
    {
        int accessCost = 0;
        if (roleOn && !RoleManager.IsSameArmy(role, roleOn))
        {
            accessCost = GridUnit.maxCost;
        }
        else
        {
            accessCost = gridProperty.AccessibilityIndex;
        }
        return accessCost;
    }
}
