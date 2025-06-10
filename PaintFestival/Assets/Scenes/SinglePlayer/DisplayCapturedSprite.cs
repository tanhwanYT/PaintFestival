using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LineCapture;

[RequireComponent(typeof(PolygonCollider2D))]
public class DisplayCapturedSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rd;
    private PolygonCollider2D polly;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rd = GetComponent<Rigidbody2D>();
        polly = GetComponent<PolygonCollider2D>();

        if (CapturedImageData.capturedSprite != null)
        {
            spriteRenderer.sprite = CapturedImageData.capturedSprite;

            Destroy(polly);
            polly = gameObject.AddComponent<PolygonCollider2D>();
        }
        else
        {
            Debug.LogWarning("이전 씬에서 캡처된 스프라이트가 없습니다.");
        }
    }

    private Vector2[] GetPathFromSprite(Sprite sprite)
    {
        List<Vector2> shape = new List<Vector2>();
        sprite.GetPhysicsShape(0, shape);
        return shape.ToArray();
    }
}
