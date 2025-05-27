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

    private bool hasLoadedScene = false;
    void Start()
    {
        roleSelectionPanel.SetActive(true);
        KeyboardButton = GameObject.Find("KeyboardButton").GetComponent<Button>();
        MouseButton = GameObject.Find("MouseButton").GetComponent<Button>();
    }

    private void Update()
    {
        if (!hasLoadedScene &&  PhotonNetwork.CurrentRoom.PlayerCount == 2 && AllPlayersSelectedRoles())
        {
            hasLoadedScene = true;
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("GameScene");
            }
        }
    }

    public void SelectRole(string role)
    {
        if (IsRoleAlreadyTake(role))
        {
            Debug.Log($"you cant");
            return;
        }

        Hashtable hash = new Hashtable { { "role", role } };
        PhotonNetwork.LocalPlayer.SetCustomProperties(hash);

        roleSelectionPanel.SetActive(false);
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

    private bool AllPlayersSelectedRoles()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (!player.CustomProperties.ContainsKey("role"))
                return false;
        }
        return true;
    }
}
