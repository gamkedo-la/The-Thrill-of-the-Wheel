using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustDevil : MonoBehaviour
{
    [SerializeField] float _rotationSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, Time.deltaTime * _rotationSpeed, Space.Self);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            // TODO: Push player
            Debug.Log("Entered the Dust Devil");
        }
    }
}
