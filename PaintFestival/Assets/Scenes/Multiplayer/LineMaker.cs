using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LineMaker : MonoBehaviourPun
{
    public GameObject linePrefab;
    private GameObject InkManager;
    Ink ink;
    LineRenderer lr;
    EdgeCollider2D col;
    List<Vector2> points  = new List<Vector2>();

    private bool isOnline;
    private GameObject currentLine;

    private void Start()
    {
        InkManager = GameObject.Find("InkManager");
        ink = InkManager.GetComponent<Ink>();
        isOnline = PhotonNetwork.IsConnected;
    }
    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine || !IsMousePlayer()) return;

        if (Input.GetMouseButtonDown(0) && ink.ink > 0)
        {
            if (isOnline)
            {
                currentLine = PhotonNetwork.Instantiate("LinePrefab", Vector3.zero, Quaternion.identity);
            }
            else
            {
                currentLine = Instantiate(linePrefab);
            }

            currentLine.layer = LayerMask.NameToLayer("PlatForm");

            lr = currentLine.GetComponent<LineRenderer>();
            col = currentLine.GetComponent<EdgeCollider2D>();
            points.Clear();

            Vector2 start = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            points.Add(start);
            lr.positionCount = 1;
            lr.SetPosition(0, start);
        }
        else if (Input.GetMouseButton(0) && currentLine != null)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (points.Count > 0 && Vector2.Distance(points[points.Count - 1], pos) > 0.1f)
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

    bool IsMousePlayer()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("role", out object role))
        {
            return role.ToString() == "Mouse";
        }
        return false;
    }
}
