﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsChecker : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.transform.position *= -1;
    }
}
