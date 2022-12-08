using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiWaypoint : MonoBehaviour
{
    public Transform followThis;
    public float followSpeed;
    public Vector3 followOffset;

    private void Update() {
        if (!followThis) return;
        Vector3 targetPos = followThis.TransformPoint(followOffset);
        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);
    }

}
