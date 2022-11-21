using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaProjectile : MonoBehaviour
{
    private void OnCollisionStay(Collision other) {
        Invoke("Despawn", 4f);
    }

    private void Despawn() {
        Destroy(gameObject);
    }
}
