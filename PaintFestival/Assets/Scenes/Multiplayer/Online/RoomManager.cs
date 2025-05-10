using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviourPunCallbacks
{
    private GameObject Mouse;
    void Start()
    {
        Mouse = GameObject.Find("NetworkMouse");
    }

    public override void OnJoinedRoom()
    {
        Vector2 spawnPos = Vector2.zero;
        PhotonNetwork.Instantiate(Mouse.name, spawnPos, Quaternion.identity);
    }
}
