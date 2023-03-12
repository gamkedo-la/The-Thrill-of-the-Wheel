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
        
        //we collide with a child so it never triggered
        //if(other.TryGetComponent<CarController>(out var carController)){
        //    carController.SetBoost();

        CarController carController = other.GetComponentInParent<CarController>();
        if (carController) {
            Debug.Log("... and it has a car controller component! yay!");
            carController.SetBoost();
        } else {
            Debug.Log("...but it was not a car. Ignoring.");
        }
    }

}
