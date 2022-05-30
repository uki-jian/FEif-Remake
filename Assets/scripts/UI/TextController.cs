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
    public GridManagement gridManagement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string name = gridManagement.chosenGrid.gridProperty.name;
        int defenceBounus = gridManagement.chosenGrid.gridProperty.defenceBounus;
        int avoidanceBounus = gridManagement.chosenGrid.gridProperty.avoidanceBounus;
        int hitpointBounus = gridManagement.chosenGrid.gridProperty.hitpointBounus;

        nameText.text = name;
        defenceText.text = defenceBounus.ToString();
        avoidanceText.text = avoidanceBounus.ToString();
        hitpointText.text = hitpointBounus.ToString();
    }
}
