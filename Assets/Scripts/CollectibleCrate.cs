using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleCrate : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.name);
        if(other.CompareTag("Player")) {
            Debug.Log("entro jugador");
        }
    }
}
