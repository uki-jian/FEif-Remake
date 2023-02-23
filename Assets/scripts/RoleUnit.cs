using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleUnit : MonoBehaviour
{
    public int full_hitpoints;
    public int remained_hitpoints;
    public int attack_points = 3;
    public int attack_dist = 1;
    public int move_speed = 2;
    public int posx, posz;
    public Pos pos = new Pos();//真实的位置
    public Pos displayedPos = new Pos();//选择移动指令时，暂时的位置
    bool isMoved = true;
    public bool IsMoved {
        get{
            return isMoved;
        }
        set
        {
            isMoved = value;
        }
    }
    public bool isDead = false;

    public int unitID;

    public GameObject avater;//图片真实的位置
    public GameObject displayedAvatar;//选择移动指令时，图片暂时的位置
    public Sprite portrait;
    public UnitProperty unitProperty;
    public RoleManager unitManager;
    public List<GridUnit> moveGrids;
    public List<GridUnit> attackGrids;

    void Start()
    {
        full_hitpoints = 10;
        remained_hitpoints = full_hitpoints;
        attack_points = 3;

        pos = new Pos(posx, posz);
        displayedPos = new Pos(posx, posz);
        //Debug.Log("Attackp:" + attack_points);

        if (unitID >= 0 && unitID < unitManager.jsonParser.unitProperties.unitProperties.Count)
            unitProperty = unitManager.jsonParser.unitProperties.unitProperties[unitID];
        //unitProperty = new UnitProperty();
        IsMoved = false;
        moveGrids = new List<GridUnit>();
        attackGrids = new List<GridUnit>();
    }
    private void Update()
    {
        //if (unitManager.jsonParser.unitProperties != null)
        //{
        //    print("YESSSSS!");
        //    if (unitID >= 0 && unitID < unitManager.jsonParser.unitProperties.unitProperties.Count)
        //        unitProperty = unitManager.jsonParser.unitProperties.unitProperties[unitID];
        //}

        if (remained_hitpoints <= 0 && isDead == false)
        {
            isDead = true;
            Die();
        }
    }
    public bool HasCloneAvatar()
    {
        return displayedAvatar;
    }
    public void CloneAvatar()
    {
        if(displayedAvatar == null)
        {
            displayedAvatar = GameObject.Instantiate(avater);
            displayedAvatar.GetComponent<SpriteRenderer>().color /= 2;
            //displayedAvatar.transform.position += new Vector3(1, 0, 0);
        }
        
    }
    public void DistroyCloneAvatar()
    {
        if(HasCloneAvatar())
            GameObject.DestroyImmediate(displayedAvatar);
    }
    void Die()
    {
        avater.SetActive(false);
    }
    public bool SetDisplayedPos(Pos p)
    {
        if (IsMoveable(p))
        {
            displayedPos = p;
            return true;
        }
        return false;
    }
    public bool SetPos(Pos p)
    {
        if (IsMoveable(p))
        {
            pos = p;
            return true;
        }
        return false;
    }
    public bool SetPos(int i, int j)
    {
        Pos arr = new Pos(i, j);
        if (IsMoveable(arr))
        {
            pos = arr;
            return true;
        }
        return false;
    }
    public bool SetPos(int[] i)
    {
        System.Diagnostics.Debug.Assert(i.Length == 2);
        Pos arr = new Pos(i[0], i[1]);
        if (IsMoveable(arr))
        {
            pos = arr;
            return true;
        }
        return false;
    }
    public Pos GetDisplayedPos()
    {
        return displayedPos;
    }
    public Pos GetPos()
    {
        return pos;
    }
    public bool DisplayedPosEquals2Pos()
    {
        return Pos.Equal(pos, displayedPos);
    }
    bool IsMoveable(Pos dst)
    {
        int distance = RoleManager.Calc2UnitsDistance(pos, dst);
        if (distance > move_speed) return false;
        return true;
    }
    public void Move2Clone()
    {
        if (HasCloneAvatar())
        {
            pos = displayedPos;
        }
    }
}
