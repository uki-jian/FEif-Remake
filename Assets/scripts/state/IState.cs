using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void OnEnter(); //进入状态的方法
    void OnUpdate(); //维持状态的方法
    void OnExit(); //退出状态的方法
}