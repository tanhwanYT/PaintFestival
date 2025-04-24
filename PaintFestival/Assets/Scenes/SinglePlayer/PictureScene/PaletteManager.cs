using UnityEngine;
using UnityEngine.Events;

public class PaletteManager : MonoBehaviour
{
    [Header("데이터")]
    public ColorList colorList;          // ScriptableObject
    [Header("프리팹")]
    public GameObject buttonPrefab;      // 방금 만든 PaletteButton 프리팹
    [Header("배치 설정")]
    public Transform origin;             // 첫 버튼 위치
    public float spacing = 1.5f;         // 버튼 간격

    public UnityEvent<Color> OnColorSelected;

    void Start()
    {
        BuildPalette();
    }

    void BuildPalette()
    {
        for (int i = 0; i < colorList.colors.Length; i++)
        {
            // 위치 계산 (가로로 배치 예시)
            Vector3 pos = origin.position + Vector3.right * spacing * i;
            var go = Instantiate(buttonPrefab, pos, Quaternion.identity, transform);
            var btn = go.GetComponent<PaletteButton>();
            btn.SetUp(colorList.colors[i], i, SelectColor);
        }
    }

    void SelectColor(Color c, int idx)
    {
        OnColorSelected?.Invoke(c);
        Debug.Log($"Palette: 색상 {idx} 선택 → {c}");
    }
}
