using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public float healthPack = 25f;

    void OnTriggerEnter(Collider col)
    {
        Destroy(gameObject);
    }
}
