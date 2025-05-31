using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Collections;

public class RoleUI_TMP : MonoBehaviour
{
    public TextMeshProUGUI roleText;

    IEnumerator Start()
    {
        if (!PhotonNetwork.IsConnectedAndReady || PhotonNetwork.OfflineMode)
        {
            roleText.gameObject.SetActive(false);
            yield break;
        }

        yield return new WaitForSeconds(0.2f);

        object roleObj;
        if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("role", out roleObj))
        {
            roleText.text = $"Role: {roleObj}";
        }
    }
}