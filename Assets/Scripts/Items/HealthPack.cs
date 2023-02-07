using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour
{
    public int healthPack = 25;

    void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("Player")) {
            col.GetComponent<HealthController>().ChangeLife(healthPack);
            Destroy(gameObject);
        }
    }
}
