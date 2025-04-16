using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ink : MonoBehaviour
{
    private float Maxink = 100.0f;
    public float ink = 100.0f;
    public Slider inkSlider;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inkSlider.value = ink / Maxink;
    }
}
