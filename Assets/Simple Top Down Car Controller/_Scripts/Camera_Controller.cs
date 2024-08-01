using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Camera_Controller : MonoBehaviour
{
    [Header("Settings")]
    public Transform Target;
    public Vector3 Offset;

    void LateUpdate()
    {
        if (Target == null) return;

        transform.position = Target.position + Offset;
    }
}
