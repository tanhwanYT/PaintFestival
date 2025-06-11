using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField roomNameInput;
    public TextMeshProUGUI errorText; 
    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private string GetRoomName()
    {
        return string.IsNullOrEmpty(roomNameInput.text) ? "DefaultRoom" : roomNameInput.text;
    }

    public void CreateRoom()
    {
        string roomName = GetRoomName();

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
        SceneManager.LoadScene("RoomScene");
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
        SceneManager.LoadScene("RoomScene");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print("OnJoinRoomfailed, " + returnCode + " ," + message);

        errorText.text = "Room Name Wrong";
        StopAllCoroutines(); 
        StartCoroutine(ClearErrorTextAfterDelay());
    }

    private IEnumerator ClearErrorTextAfterDelay()
    {
        yield return new WaitForSeconds(2.5f);
        errorText.text = "";
    }
}
