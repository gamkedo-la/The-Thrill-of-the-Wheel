using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaPit : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            //TODO: Add respawn with damage code here.
            Debug.Log("Entered pit");
        }
    }
}
