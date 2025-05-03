using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField roomNameInput;

    private string GetRoomName()
    {
        return string.IsNullOrEmpty(roomNameInput.text) ? "DefaultRoom" : roomNameInput.text;
    }

    public void CreateRoom()
    {
        string roomName = GetRoomName();
        byte n = 10;

        RoomOptions options = new RoomOptions();

        options.MaxPlayers = 2;
        options.IsVisible = true;

        PhotonNetwork.CreateRoom(roomName, options);
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print("OnCreatedRoom");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        print("OnCreatedfailed, " + returnCode + " ," + message);

    }

    public void joinroom()
    {
        string roomName = GetRoomName();
        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print("onjoinroom");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print("OnJoinRoomfailed, " + returnCode + " ," + message);
        joinroom();
    }
}
