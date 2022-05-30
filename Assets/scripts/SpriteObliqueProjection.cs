using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteObliqueProjection : MonoBehaviour
{
    public GameObject[] gameObjects;
    public Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.transform.rotation = camera.transform.rotation;
        }
    }
}
