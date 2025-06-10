using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Ink : MonoBehaviourPun, IPunObservable
{
    private float Maxink = 100.0f;
    public float ink = 100.0f;
    [SerializeField] private Slider inkSlider;

    [SerializeField] private GameObject InkPrefabOffline;

    void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Instantiate("Ink", new Vector3(2f, 3f, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(InkPrefabOffline, new Vector3(2f, 3f, 0), Quaternion.identity);
        }
    }

    void Update()
    {
        if (PhotonNetwork.IsConnected)
        {
            if (photonView.IsMine)
            {
                if (ink > Maxink) ink = Maxink;
            }
            else
            {
            }
        }
        else
        {
            if (ink > Maxink) ink = Maxink;
        }

        inkSlider.value = ink / Maxink;

    }

    // 네트워크 값 주고받기
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(ink);
        }
        else 
        {
            ink = (float)stream.ReceiveNext();
        }
    }
}
