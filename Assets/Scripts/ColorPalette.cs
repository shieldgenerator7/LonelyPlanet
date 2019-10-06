using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPalette : MonoBehaviour
{
    public List<Color> possibleColors;

    public Color RandomColor
    {
        get => possibleColors[
            Random.Range(0, possibleColors.Count)
            ];
    }
}
