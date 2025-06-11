using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class escManager : MonoBehaviourPunCallbacks
{
    public GameObject settingsPanel;
    public Button exitButton;

    void Start()
    {
        settingsPanel.SetActive(false);
        exitButton.onClick.AddListener(OnExitRoomClicked);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool isNowActive = !settingsPanel.activeSelf;
            settingsPanel.SetActive(isNowActive);

            if (isNowActive)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    void OnExitRoomClicked()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {

        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("role"))
        {
            var reset = new ExitGames.Client.Photon.Hashtable { { "role", null } };
            PhotonNetwork.LocalPlayer.SetCustomProperties(reset);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene("LobbyScene");
    }
}
