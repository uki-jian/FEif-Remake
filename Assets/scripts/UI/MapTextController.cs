using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapTextController : MonoBehaviour
{
    public Text mapText;
    public GridManager gridManagement;
    int width, height;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        width = gridManagement.num_w;
        height = gridManagement.num_h;
        //Debug.Log("CONTROL: W:" + width);
        mapText.text = "";
        for (int i = 0; i < height; i++)
        {
            int ii = height - i - 1;
            for (int j = 0; j < width; j++)
            {
                mapText.text += (gridManagement.unitMap[ii, j]+1) +"   ";
            }
            mapText.text += "\n";
        }
    }
}

