using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SingleescScript : MonoBehaviour
{
    public GameObject settingsPanel;
    public Button exitButton;
    public Slider bgmSlider;
    public Slider sfxSlider;

    void Start()
    {
        settingsPanel.SetActive(false);
        exitButton.onClick.AddListener(OnExitRoomClicked);

        bgmSlider.onValueChanged.AddListener((v) => SoundManager.Instance.SetBGMVolume(v));
        sfxSlider.onValueChanged.AddListener((v) => SoundManager.Instance.SetSFXVolume(v));
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
        SceneManager.LoadScene("SelectScene");
    }

}
