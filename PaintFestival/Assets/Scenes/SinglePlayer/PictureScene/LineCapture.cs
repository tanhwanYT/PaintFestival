using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LineCapture : MonoBehaviour
{
    public static class CapturedImageData
    {
        public static Sprite capturedSprite;
    }

    private Camera lineCamera;
    private RenderTexture renderTexture;

    private SpriteRenderer spriteRenderer;

    private string saveFilename = "captureDrawing";
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

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer가 없습니다. 이 스크립트는 SpriteRenderer가 있는 오브젝트에 붙어야 합니다.");
        }

    }

    public void CaptureLineSprite()
    {
        var boundingBox = GameObject.Find("Main Camera").GetComponent<PictureLineMaker>().GetFullDrawingBoundingBox();

        if (boundingBox.width <= 0 || boundingBox.height <= 0)
        {
            Debug.LogWarning("유효하지 않은 바운딩 박스입니다.");
            return;
        }

        AdjustCameraToBoundingBox(boundingBox);

        if (boundingBox.width <= 0 || boundingBox.height <= 0)
        {
            Debug.LogWarning("유효하지 않은 바운딩 박스입니다.");
            return;
        }

        AdjustCameraToBoundingBox(boundingBox);

        int width = Mathf.CeilToInt(boundingBox.width * 100);
        int height = Mathf.CeilToInt(boundingBox.height * 100);

        renderTexture.Release(); 
        renderTexture.width = width;
        renderTexture.height = height;
        renderTexture.Create();

        lineCamera.targetTexture = renderTexture;
        RenderTexture.active = renderTexture;
        lineCamera.Render(); 

        Texture2D tex = new Texture2D(width, height, TextureFormat.ARGB32, false);
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        tex.Apply();

        RenderTexture.active = null;

        Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100f);
        spriteRenderer.sprite = sprite;

        byte[] pngbyte = tex.EncodeToPNG();

        CapturedImageData.capturedSprite = sprite;
        File.WriteAllBytes(Path.Combine(Application.persistentDataPath, saveFilename + ".png"), pngbyte);
        SceneManager.LoadScene("SingleGameScene");
    }


    void AdjustCameraToBoundingBox(Rect boundingBox)
    {
        if (lineCamera == null) return;

        Vector3 center = new Vector3(
            boundingBox.x + boundingBox.width / 2f,
            boundingBox.y + boundingBox.height / 2f,
            lineCamera.transform.position.z 
        );

        lineCamera.transform.position = center;

        float verticalSize = boundingBox.height / 2f;
        float horizontalSize = boundingBox.width / lineCamera.aspect / 2f;

        lineCamera.orthographicSize = Mathf.Max(verticalSize, horizontalSize);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Application.OpenURL("file://" + Application.persistentDataPath);
        }
    }
}
