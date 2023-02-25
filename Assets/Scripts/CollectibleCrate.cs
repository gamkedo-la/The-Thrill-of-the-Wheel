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
        Debug.Log(other.name);
        if(other.CompareTag("Player") || other.CompareTag("Enemy")) {
            int weaponIndex = Random.Range(0,4);
            other.GetComponent<WeaponInventory>().PickWeapon(_weaponValues[weaponIndex]);
            _crateParent.SetActive(false);
            Invoke("ReenableCrate", 2f);
        }
    }

    private void ReenableCrate() {
        _crateParent.SetActive(true);
    }
}
