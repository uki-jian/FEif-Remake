using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    public Button m_Button;
    public Text m_Text;
    void Start()
    {
        m_Button.onClick.AddListener(ButtonOnClickEvent);
    }
    public void ButtonOnClickEvent()
    {
        m_Text.text = "Êó±êµã»÷";
    }
}
