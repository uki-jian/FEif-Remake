using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRetract : MonoBehaviour
{
    public Button m_Button;
    public GameObject m_gameObject;
    public bool isRetracted;
    public Vector3 move_distance;
    // Start is called before the first frame update
    void Start()
    {
        isRetracted = false;
        m_Button.onClick.AddListener(ButtonOnClickEvent);
    }

    // Update is called once per frame
    public void ButtonOnClickEvent()
    {
        if (isRetracted)
        {
            m_gameObject.transform.position -= move_distance;   
        }
        else
        {
            m_gameObject.transform.position += move_distance;
        }

        isRetracted = !isRetracted;
    }
}
