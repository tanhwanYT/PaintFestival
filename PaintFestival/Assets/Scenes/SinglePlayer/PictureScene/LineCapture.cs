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

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer�� �����ϴ�. �� ��ũ��Ʈ�� SpriteRenderer�� �ִ� ������Ʈ�� �پ�� �մϴ�.");
        }

    }

    public void CaptureLineSprite()
    {
        var boundingBox = GameObject.Find("Main Camera").GetComponent<PictureLineMaker>().GetFullDrawingBoundingBox();

        if (boundingBox.width <= 0 || boundingBox.height <= 0)
        {
            Debug.LogWarning("��ȿ���� ���� �ٿ�� �ڽ��Դϴ�.");
            return;
        }

        AdjustCameraToBoundingBox(boundingBox);

        if (boundingBox.width <= 0 || boundingBox.height <= 0)
        {
            Debug.LogWarning("��ȿ���� ���� �ٿ�� �ڽ��Դϴ�.");
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
