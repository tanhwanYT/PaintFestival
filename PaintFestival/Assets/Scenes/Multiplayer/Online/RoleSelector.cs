using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using ExitGames.Client.Photon;

public class RoleSelector : MonoBehaviourPunCallbacks
{
    public GameObject roleSelectionPanel;

    private Button KeyboardButton;
    private Button MouseButton;
    void Start()
    {
        roleSelectionPanel.SetActive(true);
        KeyboardButton = GameObject.Find("KeyboardButton").GetComponent<Button>();
        MouseButton = GameObject.Find("MouseButton").GetComponent<Button>();

        KeyboardButton.onClick.AddListener(() => SelectRole("Keyboard"));
        MouseButton.onClick.AddListener(() => SelectRole("Mouse"));
    }

    void SelectRole(string role)
    {
        if (IsRoleAlreadyTake(role))
        {
            Debug.Log($"you cant");
            return;
        }

        Hashtable hash = new Hashtable { { "role", role } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);

        if (PhotonNetwork.IsConnectedAndReady)
        {
            if (PhotonNetwork.InLobby)
            {
                PhotonNetwork.JoinOrCreateRoom("Myroom", new RoomOptions { MaxPlayers = 2 }, TypedLobby.Default);
            }
            else
            {
                PhotonNetwork.JoinLobby(); 
            }
        }
    }

    bool IsRoleAlreadyTake(string role)
    {
        foreach (var player in PhotonNetwork.PlayerListOthers)
        {
            if (player.CustomProperties.TryGetValue("role", out object r))
            {
                if ((string)r == role)
                    return true;
            }
        }
        return false;
    }
    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom("Myroom", new RoomOptions { MaxPlayers = 2 }, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        roleSelectionPanel.SetActive(false);
        Debug.Log("choose Role: " + PhotonNetwork.LocalPlayer.CustomProperties["role"]);
    }
}
