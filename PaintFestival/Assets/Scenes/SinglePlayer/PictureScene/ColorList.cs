using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Palette/ColorList")]
public class ColorList : ScriptableObject
{
    [Header("Palette Colors")]
    public Color[] colors;
}
