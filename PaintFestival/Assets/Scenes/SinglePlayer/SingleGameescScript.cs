using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SingleGameescScript : MonoBehaviour
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
        }
    }

    public void OnExitRoomClicked()
    {
        SceneManager.LoadScene("PictureScene");
    }
}
