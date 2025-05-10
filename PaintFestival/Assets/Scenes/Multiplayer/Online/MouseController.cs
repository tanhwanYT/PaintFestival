using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MouseController : MonoBehaviourPun
{
    private Camera cam;
    public SpriteRenderer cursorRenderer;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        cursorRenderer.color = GetColorFromActorNumber(photonView.Owner.ActorNumber);
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine)
        {
            Vector2 mousePos = Input.mousePosition;
            Vector2 worldPos = cam.ScreenToWorldPoint(mousePos);
            transform.position = worldPos;
        }
    }

    Color GetColorFromActorNumber(int actorNumber)
    {
        // 플레이어 고유 번호로 색 구분
        Random.InitState(actorNumber * 999);
        return Random.ColorHSV(0f, 1f, 0.7f, 1f, 0.8f, 1f);
    }
}
