using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCapture : MonoBehaviour
{
    private Camera lineCamera;
    private RenderTexture renderTexture;

    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // "LineCamera"��� �̸��� ������Ʈ���� Camera ������Ʈ ��������
        GameObject camObj = GameObject.Find("LineCamera");
        if (camObj != null)
        {
            lineCamera = camObj.GetComponent<Camera>();
            renderTexture = lineCamera.targetTexture; // ī�޶��� TargetTexture �Ҵ�
        }
        else
        {
            Debug.LogError("LineCamera ������Ʈ�� ã�� �� �����ϴ�.");
        }

        // �� ��ũ��Ʈ�� ���� ������Ʈ�� SpriteRenderer ��������
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer�� �����ϴ�. �� ��ũ��Ʈ�� SpriteRenderer�� �ִ� ������Ʈ�� �پ�� �մϴ�.");
        }

    }
    
    public void CaptureLineSprite()
    {
        RenderTexture.active = renderTexture;

        Texture2D tex = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
        tex.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        tex.Apply();

        RenderTexture.active = null;

        Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100f);
        spriteRenderer.sprite = sprite;
    }
}
