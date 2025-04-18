using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeirdWallScript : MonoBehaviour
{
    public float speed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 target = new Vector2(transform.position.x + 1.0f, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }
}
