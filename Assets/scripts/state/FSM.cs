using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    public static bool GetConfirmation()
    {
        return Input.GetMouseButtonDown(0);//鼠标左键确认
    }
    public static bool GetCancell()
    {
        return Input.GetMouseButtonDown(1);//鼠标右键返回/拒绝
    }
    public virtual void TransitionState()
    {

    }
}
