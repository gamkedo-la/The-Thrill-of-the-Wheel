using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    [SerializeField] List<GameObject> availableWeaponsPrefab;

    private int equipedWeaponIdx = 0;
    [SerializeField] private GameObject equipedWeapon = null;
    
    // Start is called before the first frame update
    void Start()
    {
        availableWeaponsPrefab.Insert(0, null);  // set unequip weapon
    }

    public void SwitchWeapon()
    {
        if (equipedWeaponIdx > 0)
        {
            Debug.Log("Unequip " + equipedWeapon.name);
            Destroy(equipedWeapon);
            equipedWeapon = null;
        }

        equipedWeaponIdx += 1;
        if (equipedWeaponIdx >= availableWeaponsPrefab.Count)
        {
            equipedWeaponIdx = 0;
        }
        else
        {
            equipedWeapon = Instantiate(availableWeaponsPrefab[equipedWeaponIdx], transform);
            Debug.Log("Equip " + equipedWeapon.name);
        }
    }
}
