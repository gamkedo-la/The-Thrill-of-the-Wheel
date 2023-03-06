using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Speed boost was hit by " + other.name);
        if(other.TryGetComponent<CarController>(out var carController)){
            carController.SetBoost();
        }
    }

}
