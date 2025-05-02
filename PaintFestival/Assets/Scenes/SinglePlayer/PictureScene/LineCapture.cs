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
        // "LineCamera"라는 이름의 오브젝트에서 Camera 컴포넌트 가져오기
        GameObject camObj = GameObject.Find("LineCamera");
        if (camObj != null)
        {
            lineCamera = camObj.GetComponent<Camera>();
            renderTexture = lineCamera.targetTexture; // 카메라의 TargetTexture 할당
        }
        else
        {
            Debug.LogError("LineCamera 오브젝트를 찾을 수 없습니다.");
        }

        // 이 스크립트가 붙은 오브젝트의 SpriteRenderer 가져오기
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer가 없습니다. 이 스크립트는 SpriteRenderer가 있는 오브젝트에 붙어야 합니다.");
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
