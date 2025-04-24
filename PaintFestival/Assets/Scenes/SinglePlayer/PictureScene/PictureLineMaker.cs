using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PictureLineMaker : MonoBehaviour
{
    public GameObject linePrefab;
    private GameObject PaletteManager;
    private PaletteManager palette;
    private Color currentColor = Color.black;

    LineRenderer lr;
    List<Vector2> points = new List<Vector2>();

    private void Start()
    {
        PaletteManager = GameObject.Find("PaletteManager");
        palette = PaletteManager.GetComponent<PaletteManager>();

        palette.OnColorSelected.AddListener(OnColorChanged);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject go = Instantiate(linePrefab);

            lr = go.GetComponent<LineRenderer>();

            lr.material.color = currentColor;

            points.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            lr.positionCount = 1;
            lr.SetPosition(0, points[0]);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (points.Count > 0 && Vector2.Distance(points[points.Count - 1], pos) > 0.1f)
            {
                points.Add(pos);
                lr.positionCount++;
                lr.SetPosition(lr.positionCount - 1, pos);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            points.Clear();
        }
    }
    private void OnColorChanged(Color newColor)
    {
        currentColor = newColor;
    }
    private void OnDestroy()
    {
        palette.OnColorSelected.RemoveListener(OnColorChanged);
    }
}
