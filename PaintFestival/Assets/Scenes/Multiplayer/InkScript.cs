using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InkScript : MonoBehaviour
{
    private GameObject InkManager;
    private Transform PlayerTransform;

    Ink ink;


    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
         PlayerTransform = player.transform;

        InkManager = GameObject.Find("GameManager");
        ink = InkManager.GetComponent<Ink>();
    }

    private void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!PhotonNetwork.IsConnected)
        {
            ink.ink += 10;
        }
        else if (ink != null && ink.photonView != null)
        {
            ink.photonView.RPC("AddInk", RpcTarget.All, 10f);
        }
        //      gameObject.SetActive(false);

        float range_X = Random.Range(PlayerTransform.position.x + 3, PlayerTransform.position.x + 7);
        float range_Y = Random.Range(-4,7);
        Vector2 RandomPostion = new Vector2(range_X, range_Y);
        transform.position = RandomPostion;

 //     gameObject.SetActive(true);
    }
    public void SetInk(Ink inkRef)
    {
        ink = inkRef;
    }
}
