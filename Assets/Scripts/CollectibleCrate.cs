using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleCrate : MonoBehaviour
{
    private string[] _weaponValues;
    [SerializeField] private GameObject _crateParent;

    private void Awake() {
        _weaponValues = new string[] {"turret", "barrel", "missiles", "sonic", "acid"};
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player") || other.CompareTag("Enemy")) {
            int weaponIndex = Random.Range(0,5);
            WeaponInventory inventory = other.GetComponent<WeaponInventory>();
            // Added for armadillo
            if(other.GetComponent<WeaponInventory>() == null) {
                inventory = other.GetComponentInParent<WeaponInventory>();
            }

            inventory.PickWeapon(_weaponValues[weaponIndex]);
            _crateParent.SetActive(false);
            Invoke("ReenableCrate", 2f);
            AudioManager.Instance.PlaySFX("Barrel Drop");
        }
    }

    private void ReenableCrate() {
        _crateParent.SetActive(true);
    }
}
