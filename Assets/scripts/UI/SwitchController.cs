using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchController : MonoBehaviour
{
    public Button miniMapButton;
    public Button missionInfoButton;

    public GameObject miniMapImage;
    public GameObject missionInfoImage;
    // Start is called before the first frame update
    void Start()
    {
        MiniMapButtonOnClickEvent(); //初始化为可看见小地图
        miniMapButton.onClick.AddListener(MiniMapButtonOnClickEvent);
        missionInfoButton.onClick.AddListener(MissionInfoButtonClickEvent);
    }

    void MiniMapButtonOnClickEvent()
    {
        miniMapImage.SetActive(true);
        missionInfoImage.SetActive(false);
    }
    void MissionInfoButtonClickEvent()
    {
        missionInfoImage.SetActive(true);
        miniMapImage.SetActive(false);
    }
}
