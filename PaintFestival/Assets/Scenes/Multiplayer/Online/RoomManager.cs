using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;
using UnityEngine.SceneManagement;

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

    public void LeaveRoom()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("role"))
        {
            var reset = new ExitGames.Client.Photon.Hashtable { { "role", null} };
            PhotonNetwork.LocalPlayer.SetCustomProperties(reset);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        PhotonNetwork.LeaveRoom();
        SceneManager.LoadScene("LobbyScene");
    }
}
