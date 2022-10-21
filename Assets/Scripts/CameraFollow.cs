using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform target;
    [SerializeField] private float translateSpeed;
    [SerializeField] private float rotationSpeed;


    private void FixedUpdate() {
        HandleTranslation();
        HandleRotation();
    }

    private void HandleRotation()
    {
        Vector3 targetPos = target.TransformPoint(offset);
        transform.position = Vector3.Lerp(transform.position, targetPos, translateSpeed * Time.deltaTime);
    }

    private void HandleTranslation()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion rot = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotationSpeed * Time.deltaTime);
    }
}
