using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSM : MonoBehaviour
{
    public static bool GetConfirmation()
    {
        return Input.GetMouseButtonDown(0);//������ȷ��
    }
    public static bool GetCancell()
    {
        return Input.GetMouseButtonDown(1);//����Ҽ�����/�ܾ�
    }
    public virtual void TransitionState()
    {

    }
}
