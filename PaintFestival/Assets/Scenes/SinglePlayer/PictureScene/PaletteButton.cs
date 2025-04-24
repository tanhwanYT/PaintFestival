using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaletteButton : MonoBehaviour
{
    private SpriteRenderer spr;
    private Color mycolor;
    private int myindex;
    private System.Action<Color, int> callback;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
    }

    public void SetUp(Color c, int idx, System.Action<Color, int> cb)
    {
        mycolor = c;
        myindex = idx;
        callback = cb;
        spr.color = mycolor;
    }

    private void OnMouseDown()
    {
        callback?.Invoke(mycolor, myindex);
    }
}
