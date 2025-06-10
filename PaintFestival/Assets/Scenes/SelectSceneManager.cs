using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectSceneManager : MonoBehaviour
{
    public GameObject selectPanel;

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
