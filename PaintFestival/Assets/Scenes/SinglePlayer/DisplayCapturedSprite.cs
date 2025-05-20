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
            Debug.LogWarning("���� ������ ĸó�� ��������Ʈ�� �����ϴ�.");
        }
    }
}
