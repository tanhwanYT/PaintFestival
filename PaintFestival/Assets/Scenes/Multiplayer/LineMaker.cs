using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class LineMaker : MonoBehaviourPun
{
    public GameObject linePrefab; 
    private Ink ink;
    private LineRenderer lr;
    private EdgeCollider2D col;
    private List<Vector2> points = new List<Vector2>();

    private bool isOnline;
    private GameObject currentLine;

    void Start()
    {
        GameObject inkManager = GameObject.Find("GameManager");
        ink = inkManager.GetComponent<Ink>();
        isOnline = PhotonNetwork.IsConnected;
    }

    void Update()
    {
        if (!IsMineOrOffline() || !IsMousePlayer()) return;
        if (ink.ink <= 0) return;

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 start = GetMouseWorldPos();
            CreateLine(start);
        }
        else if (Input.GetMouseButton(0) && currentLine != null)
        {
            Vector2 pos = GetMouseWorldPos();
            if (points.Count > 0 && Vector2.Distance(points[points.Count - 1], pos) > 0.1f)
            {
                if (PhotonNetwork.IsConnected)
                {
                    currentLine.GetComponent<PhotonView>().RPC("AddPoint", RpcTarget.AllBuffered, pos);
                    ink.ink -= 0.5f;
                }
                else
                {
                    currentLine.GetComponent<LinrObj>().AddPoint(pos);
                    ink.ink -= 0.5f;
                }
                
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            points.Clear();
            currentLine = null;
        }
    }

    Vector2 GetMouseWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0f;
        return (Vector2)Camera.main.ScreenToWorldPoint(mousePos);
    }

    void CreateLine(Vector2 start)
    {
        if (isOnline)
        {
            currentLine = PhotonNetwork.Instantiate("Line", start, Quaternion.identity);
        }
        else
        {
            currentLine = Instantiate(linePrefab, start, Quaternion.identity);
        }

        currentLine.layer = LayerMask.NameToLayer("PlatForm");
        lr = currentLine.GetComponent<LineRenderer>();
        col = currentLine.GetComponent<EdgeCollider2D>();


        points.Clear();
        points.Add(start);
        lr.positionCount = 1;
        lr.SetPosition(0, start);
    }

    bool IsMineOrOffline()
    {
        return !PhotonNetwork.IsConnected || photonView.IsMine;
    }

    bool IsMousePlayer()
    {
        if (!PhotonNetwork.IsConnected)
            return true;

        if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("role", out object role))
        {
            return role.ToString() == "Mouse";
        }
        return false;
    }
}
