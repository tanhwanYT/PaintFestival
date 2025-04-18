using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LineMaker : MonoBehaviour
{
    public GameObject linePrefab;

    private GameObject InkManager;
    Ink ink;
    LineRenderer lr;
    EdgeCollider2D col;
    List<Vector2> points  = new List<Vector2>();

    private void Start()
    {
        InkManager = GameObject.Find("InkManager");
        ink = InkManager.GetComponent<Ink>();

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && ink.ink > 0)
        {
            GameObject go = Instantiate(linePrefab);
            go.layer = LayerMask.NameToLayer("PlatForm");

            lr = go.GetComponent<LineRenderer>();
            col = go.GetComponent<EdgeCollider2D>();  
            points.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            lr.positionCount = 1;
            lr.SetPosition(0, points[0]);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 pos  = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Vector2.Distance(points[points.Count - 1], pos) > 0.1f)
            {
                points.Add(pos);
                lr.positionCount++;
                lr.SetPosition(lr.positionCount - 1, pos);
                col.points = points.ToArray();

                ink.ink -= 0.5f;
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            points.Clear();
        }
    }
}
