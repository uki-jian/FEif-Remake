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

    public static Vector3 patternOffset=new Vector3(0,0.1f,0);  //贴图相对于格子的偏移量

    public bool IsChosen { get => isChosen; set => isChosen = value; }

    public int walkPoint; //剩余移动点数(求路径用）
    public GridUnit father; //父亲节点(求路径用）

    public float FValue;    //AStar comprehensive estimate
    public float GValue;    //AStar to origin estimate
    public float HValue;    //AStar to destination estimate

    //public Material mat_patternAttack;
    //public UnitRole avatar;

    public GridUnit(GridProperty _gridProperty, GameObject _choose, GameObject _attack)
    {
        isExist = true;
        IsChosen = false;
        
        gridProperty = _gridProperty;
        patternChoose = _choose;
        patternAttack = _attack;

        patternChoose.SetActive(false);
        patternAttack.SetActive(false);

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
    }
    public void SetAttack()
    {
        patternAttack.SetActive(true);
        patternChoose.SetActive(false);
    }
    public void SetReset()
    {
        patternAttack.SetActive(false);
        patternChoose.SetActive(false);
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

    //public int CompareTo(GridUnit other)
    //{
    //    if (this.FValue > other.FValue) return 1;
    //    else return -1;
    //}
}
