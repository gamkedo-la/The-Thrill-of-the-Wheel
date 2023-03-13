using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaProjectile : MonoBehaviour
{
    public GameObject impactVFXPrefab;
    private void OnCollisionStay(Collision other)
    {
        Invoke("Despawn", 4f);
    }
    private void OnCollisionEnter(Collision other)
    {
        // if the magma ball hits something:
        // get impact point
        Instantiate(impactVFXPrefab, other.GetContact(0).point, Quaternion.identity);
        Despawn();
        // instantiate vfx at impact point
    }
    private void Despawn()
    {
        // instantiate vfx at impact point
        Destroy(gameObject);
    }
}
