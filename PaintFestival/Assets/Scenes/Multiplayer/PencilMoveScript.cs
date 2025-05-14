using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PencilMoveScript : MonoBehaviour
{
    public float pencilXpos;
    public float pencilYpos;
    private void Start()
    {
        Cursor.visible = false;
    }
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 offset = new Vector2(pencilXpos, pencilYpos);
        transform.position = mousePos + offset;
    }
}
