using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static bool Approximately(this Vector2 v, Vector2 v2)
    {
        float closifierFactor = 100;
        return
            Mathf.Approximately(v.x / closifierFactor, v2.x / closifierFactor)
            && Mathf.Approximately(v.y / closifierFactor, v2.y / closifierFactor);
    }
}
