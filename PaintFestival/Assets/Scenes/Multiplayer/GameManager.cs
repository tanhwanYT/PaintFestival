using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefabOffline;
    public GameObject pencilPrefabOffline;
    public GameObject inkPrefabOffline;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        object roleObj;
        PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("role", out roleObj);
        string role = roleObj?.ToString() ?? "None";

        if (PhotonNetwork.IsConnected)
        {
            if (role == "Keyboard")
            {
                PhotonNetwork.Instantiate("Player", new Vector3(-7.5f, 1.5f, 0), Quaternion.identity);
                GameObject inkObj = PhotonNetwork.Instantiate("Ink", new Vector3(2f, 3f, 0), Quaternion.identity);

                foreach (var inkScript in FindObjectsOfType<InkScript>())
                {
                    inkScript.SetInk(inkObj.GetComponent<Ink>());
                }
            }
            else if (role == "Mouse")
            {
                PhotonNetwork.Instantiate("Pencil", new Vector3(0f, 0f, 0f), Quaternion.identity);
            }
        }
        else
        {
            Instantiate(playerPrefabOffline, new Vector3(-7.5f, 1.5f, 0), Quaternion.identity);
            Instantiate(pencilPrefabOffline, new Vector3(0f, 0f, 0f), Quaternion.identity);
            Instantiate(inkPrefabOffline, new Vector3(2f, 3f, 0), Quaternion.identity);
        }

    }
}
