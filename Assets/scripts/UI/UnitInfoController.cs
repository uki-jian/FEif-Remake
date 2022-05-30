using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitInfoController : MonoBehaviour
{
    public UnitManagement unitManager;
    public Text textName;
    public Text textClass;
    public Text textLevel;
    public Text textExp;
    public Text textMov;
    public Text textHp;
    public Text textAtt;
    public Text textMga;
    public Text textSkl;
    public Text textSpd;
    public Text textLck;
    public Text textDef;
    public Text textMgd;
    public Image portrait;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UnitClass unit = unitManager.chosenUnit;
        if (unit == null)
        {
            unit = new UnitClass();
            unit.portrait = null;
        }
        else portrait.sprite = unit.portrait;

        UnitProperty up = unit.unitProperty;
        if (up == null) up = new UnitProperty();
        textName.text = up.u_name;
        textClass.text = up.u_class;
        textLevel.text = up.u_level.ToString();
        textExp.text = up.u_exp.ToString();
        textMov.text = up.u_moveSpeed.ToString();
        textHp.text = up.u_hitpoint.ToString();
        textAtt.text = up.u_attack.ToString();
        textMga.text = up.u_magicAttack.ToString();
        textSkl.text = up.u_skill.ToString();
        textSpd.text = up.u_speed.ToString();
        textLck.text = up.u_luckiness.ToString();
        textDef.text = up.u_defence.ToString();
        textMgd.text = up.u_magicDefence.ToString();
        //public int u_attack;
        //public int u_magicAttack;
        //public int u_skill;
        //public int u_speed;
        //public int u_luckiness;
        //public int u_defence;
        //public int u_magicDefence;
    }
}
