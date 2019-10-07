using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject focusTarget;
    public float screenShakeDuration;

    private float screenShakeStartTime = -1;

    private Vector3 screenShakeVector;
    public Vector3 ScreenShakeVector
    {
        get => (screenShakeStartTime > 0
            && Time.time < screenShakeStartTime + screenShakeDuration
            && !GameManager.menuLoaded())
            ? screenShakeVector
            : Vector3.zero;
        set
        {
            screenShakeVector = value;
            screenShakeStartTime = Time.time;
        }
    }

    private Rigidbody2D focusRB2D;

    // Start is called before the first frame update
    void Start()
    {
        focusRB2D = focusTarget.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos = focusTarget.transform.position + ScreenShakeVector;
        pos.z = transform.position.z;
        transform.position = pos;
        //Screen Shake Updating
        screenShakeVector *= -1;
    }
}
