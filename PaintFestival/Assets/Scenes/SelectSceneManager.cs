using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectSceneManager : MonoBehaviour
{
    public GameObject selectPanel;

    void Start()
    {
        AudioClip testBGM = Resources.Load<AudioClip>("Sounds/bgm_test");
        AudioClip testSFX = Resources.Load<AudioClip>("Sounds/sfx_jump");

        SoundManager.Instance.PlayBGM(testBGM);
        SoundManager.Instance.PlaySFX(testSFX);
    }
    public void selectMultiScene()
    {
        selectPanel.SetActive(true);
    }

    public void selectSingleScene()
    {
        SceneManager.LoadScene("PictureScene");
    }


    public void OnOfflineButtonClicked()
    {
        selectPanel.SetActive(false);
        SceneManager.LoadScene("GameScene");
    }
}
