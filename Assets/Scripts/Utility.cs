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

    public static Vector3 adjustX(this Vector3 v, float addend)
    {
        v.x += addend;
        return v;
    }

    public static Vector3 adjustY(this Vector3 v, float addend)
    {
        v.y += addend;
        return v;
    }

    public static Vector3 adjustVector(this Vector3 v, float addend)
    {
        v = v.adjustX(addend);
        v = v.adjustY(addend);
        return v;
    }
}
