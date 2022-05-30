using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightGlitter : MonoBehaviour
{
    public Light light;
    float max_intensity;
    float min_intensity;
    // Start is called before the first frame update
    void Start()
    {
        max_intensity = light.intensity;
        min_intensity = max_intensity / 2;
    }

    // Update is called once per frame
    void Update()
    {
        //light.intensity = max_intensity * Mathf.Sin(Time.time);
        //if(Random.Range(0f,1f)>0.99)
        //light.intensity = Random.Range(min_intensity, max_intensity);

        //½¥±äÊ½ÉÁË¸£¨À¯Öò£©
        light.intensity = min_intensity + (max_intensity-min_intensity)/2 * Mathf.Sin(Time.time);
        //ºöÃ÷ºö°µÉÁË¸(µçµÆ¶ÌÂ·£©
        //if (Random.Range(0f, 1f) > 0.99)
        //    light.intensity = min_intensity;
        //else light.intensity = max_intensity;
    }
}
