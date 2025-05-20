using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LineCapture;

public class DisplayCapturedSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (CapturedImageData.capturedSprite != null)
        {
            spriteRenderer.sprite = CapturedImageData.capturedSprite;
        }
        else
        {
            Debug.LogWarning("이전 씬에서 캡처된 스프라이트가 없습니다.");
        }
    }
}
