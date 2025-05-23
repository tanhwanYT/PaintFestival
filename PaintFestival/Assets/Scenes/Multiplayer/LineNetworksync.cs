using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineNetworksync : MonoBehaviourPun
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
    public void AddPoint(Vector2 pos)
    {
        points.Add(pos);
        lr.positionCount = points.Count;
        lr.SetPosition(points.Count - 1, pos);
        col.points = points.ToArray();
    }
}
