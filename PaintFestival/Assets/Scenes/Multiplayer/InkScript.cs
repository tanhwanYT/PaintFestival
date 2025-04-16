using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkScript : MonoBehaviour
{
    private GameObject InkManager;
    Ink ink;

    // Start is called before the first frame update
    void Start()
    {
        InkManager = GameObject.Find("InkManager");
        ink = InkManager.GetComponent<Ink>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ink.ink += 10;

        gameObject.SetActive(false);
    }
}
