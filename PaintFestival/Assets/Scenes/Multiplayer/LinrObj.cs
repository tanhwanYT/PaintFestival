using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinrObj : MonoBehaviour
{
    private LineRenderer lr;
    private EdgeCollider2D col;
    private List<Vector2> points = new List<Vector2>();

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        col = GetComponent<EdgeCollider2D>();
    }

    [PunRPC]
    public void AddPoint(Vector2 worldPos)
    {
        Vector2 localPos = transform.InverseTransformPoint(worldPos);
        points.Add(localPos);
        lr.positionCount = points.Count;
        lr.SetPosition(points.Count - 1, localPos);
        col.points = points.ToArray();
    }
}
