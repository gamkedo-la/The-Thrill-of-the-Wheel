using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleCrate : MonoBehaviour
{
    private string[] _weaponValues;
    [SerializeField] private GameObject _crateParent;

    private void Awake() {
        _weaponValues = new string[] {"turret", "barrel", "missiles", "sonic"};
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            int weaponIndex = Random.Range(0,4);
            other.GetComponent<CarController>().OnWeaponPickup(_weaponValues[weaponIndex]);
            _crateParent.SetActive(false);
            Invoke("ReenableCrate", 2f);
        }
    }

    private void ReenableCrate() {
        _crateParent.SetActive(true);
    }
}
