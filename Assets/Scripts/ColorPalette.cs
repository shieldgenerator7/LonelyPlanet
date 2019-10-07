using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Color Palette", menuName = "Color Palette", order = 0)]
public class ColorPalette : ScriptableObject
{
    public List<Color> possibleColors;

    public Color RandomColor
    {
        get => possibleColors[
            Random.Range(0, possibleColors.Count)
            ];
    }
}
