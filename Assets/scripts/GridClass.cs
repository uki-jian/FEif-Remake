using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridClass : MonoBehaviour
{
    public Pos pos = new Pos();    //x, z����������
    bool isChosen = false;  //�Ƿ�ѡ��
    public bool isExist;    //�Ƿ����
    public float altitude;  //ÿ����Ը߶ȣ�����ڻ�׼�߶ȣ�
    readonly public GridProperty gridProperty; //grid��������:ֻ��

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
