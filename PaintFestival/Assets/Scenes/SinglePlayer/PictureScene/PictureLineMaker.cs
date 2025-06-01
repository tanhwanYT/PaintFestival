using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PictureLineMaker : MonoBehaviour
{
    public GameObject linePrefab;
    private GameObject PaletteManager;
    private PaletteManager palette;
    private Color currentColor = Color.black;
    private int lineSortingOrder = 0;
    private bool isEraserMode = false;
    private List<GameObject> lines = new List<GameObject>();

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
 /*       if(isEraserMode && Input.GetMouseButton(0))
        {
            Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float eraseRadius = 2.0f;

            foreach(GameObject line in lines)
            {
                LineRenderer lr = line.GetComponent<LineRenderer>();
                int pointcount = lr.positionCount;
                List<Vector3> newPostion = new List<Vector3>();

                for(int i = 0; i < pointcount; i++)
                {
                    Vector3 point = lr.GetPosition(i);
                    float dist = Vector2.Distance(mouseWorld, point);

                    if(dist < eraseRadius)
                    {
                        newPostion.Add(point);
                    }
                }

                if(newPostion.Count > 0)
                {
                    Destroy(line);
                }
                else
                {
                    lr.positionCount = newPostion.Count;
                    for(int i = 0; i < newPostion.Count; i++)
                    {
                        lr.SetPosition(i, newPostion[i]);
                    }
                }
            }
        }
 */

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("Button"))
            {
                return;
            }

            GameObject go = Instantiate(linePrefab);
            lines.Add(go);

            lr = go.GetComponent<LineRenderer>();
            lr.material.color = currentColor;
            lr.sortingOrder = ++lineSortingOrder;

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0f; 

            points.Add(mousePos);
            lr.positionCount = 1;
            lr.SetPosition(0, mousePos);

        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (points.Count > 0 && Vector2.Distance(points[points.Count - 1], mousePos) > 0.1f)
            {
                points.Add(mousePos);
                lr.positionCount++;
                lr.SetPosition(lr.positionCount - 1, mousePos);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            points.Clear();
        }

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Z))
        {
            UndoLastLine();
        }

/*        if (Input.GetKeyDown(KeyCode.E))
        {
            isEraserMode = !isEraserMode;
        } */
    }
    private void OnColorChanged(Color newColor)
    {
        currentColor = newColor;
    }
    private void OnDestroy()
    {
        palette.OnColorSelected.RemoveListener(OnColorChanged);
    }
    private void UndoLastLine()
    {
        if(lines.Count > 0)
        {
            GameObject LastLine = lines[lines.Count - 1];
            lines.RemoveAt(lines.Count - 1);
            Destroy(LastLine);
            lineSortingOrder--;
        }
    }
}
