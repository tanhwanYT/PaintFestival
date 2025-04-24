using UnityEngine;
using UnityEngine.Events;

public class PaletteManager : MonoBehaviour
{
    [Header("������")]
    public ColorList colorList;          // ScriptableObject
    [Header("������")]
    public GameObject buttonPrefab;      // ��� ���� PaletteButton ������
    [Header("��ġ ����")]
    public Transform origin;             // ù ��ư ��ġ
    public float spacing = 1.5f;         // ��ư ����

    public UnityEvent<Color> OnColorSelected;

    void Start()
    {
        BuildPalette();
    }

    void BuildPalette()
    {
        for (int i = 0; i < colorList.colors.Length; i++)
        {
            // ��ġ ��� (���η� ��ġ ����)
            Vector3 pos = origin.position + Vector3.right * spacing * i;
            var go = Instantiate(buttonPrefab, pos, Quaternion.identity, transform);
            var btn = go.GetComponent<PaletteButton>();
            btn.SetUp(colorList.colors[i], i, SelectColor);
        }
    }

    void SelectColor(Color c, int idx)
    {
        OnColorSelected?.Invoke(c);
        Debug.Log($"Palette: ���� {idx} ���� �� {c}");
    }
}
