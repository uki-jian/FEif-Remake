using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//WASD¿ØÖÆÉãÏñ»úÆ½ÒÆ
public class CameraTranslate : MonoBehaviour
{
    float moveSpeed = 3;
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * Time.deltaTime * moveSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.back * Time.deltaTime * moveSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * Time.deltaTime * moveSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * Time.deltaTime * moveSpeed;        }
    }
}