using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridClass : MonoBehaviour
{
    public Pos pos = new Pos();    //x, z的索引坐标
    bool isChosen = false;  //是否被选中
    public bool isExist;    //是否存在
    public float altitude;  //每格相对高度（相对于基准高度）
    readonly public GridProperty gridProperty; //grid基本属性:只读

    public bool IsChosen { get => isChosen; set => isChosen = value; }

    //public UnitRole avatar;

    public GridClass(GridProperty _gridProperty)
    {
        isExist = true;
        IsChosen = false;
        
        gridProperty = _gridProperty;
        altitude = gridProperty.altitude;
    }
    public void AvatarGoIn(ref UnitClass unit)
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
}
