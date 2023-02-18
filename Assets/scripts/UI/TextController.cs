using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public Text nameText;
    public Text defenceText;
    public Text avoidanceText;
    public Text hitpointText;
    public GridManager gridManagement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string name = gridManagement.currentGrid.gridProperty.name;
        int defenceBounus = gridManagement.currentGrid.gridProperty.defenceBounus;
        int avoidanceBounus = gridManagement.currentGrid.gridProperty.avoidanceBounus;
        int hitpointBounus = gridManagement.currentGrid.gridProperty.hitpointBounus;

        nameText.text = name;
        defenceText.text = defenceBounus.ToString();
        avoidanceText.text = avoidanceBounus.ToString();
        hitpointText.text = hitpointBounus.ToString();
    }
}
