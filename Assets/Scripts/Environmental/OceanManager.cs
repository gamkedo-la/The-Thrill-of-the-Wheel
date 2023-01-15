using UnityEngine;

public class OceanManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            //TODO: Add respawn logic here
            Debug.Log("Drowning");
        }
    }
}
