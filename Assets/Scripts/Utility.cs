using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static bool Approximately(this Vector2 v, Vector2 v2)
    {
        return
            Mathf.Approximately(v.x, v2.x)
            && Mathf.Approximately(v.y, v2.y);
    }
}
