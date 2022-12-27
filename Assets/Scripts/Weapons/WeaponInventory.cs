using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInventory : MonoBehaviour
{
    [System.Serializable] 
    private struct WeaponStruct
    {
        public string name;
        public int currentAmmo;
        public WeaponStruct(string name, int ammo) {
            this.name = name;
            this.currentAmmo = ammo;
        }
    }

    private Dictionary<string, int> _weaponAmmoAddValues;
    private Dictionary<string, int> _weaponMaxAmmoValues;

    [SerializeField] List<WeaponStruct> weapons;
    private int equipedWeaponIndex;

    public event Action<GameObject> onSwitchWeapon;
    
    // Start is called before the first frame update
    void Start()
    {
        weapons = new List<WeaponStruct>();
        _weaponAmmoAddValues = new Dictionary<string, int>() {
            {"turret", 10},
            {"barrel", 2},
            {"missiles", 5},
            {"sonic", 3},
        };
        _weaponMaxAmmoValues = new Dictionary<string, int>() {
            {"turret", 20},
            {"barrel", 6},
            {"missiles", 10},
            {"sonic", 9},
        };
        equipedWeaponIndex = -1;
    }

    void PickWeapon(string name) {
        int weaponIndex = weapons.FindIndex(0, weapons.Count, weapon => weapon.name == name);
        if(weaponIndex != -1) {
            AddExistingWeapon(name, weaponIndex);
        } else {
            AddNewWeapon(name);
        }
    }

    void AddExistingWeapon(string name, int weaponIndex){
        WeaponStruct temp = weapons[weaponIndex];
        int currentAmmo = temp.currentAmmo;
        int maxAmmo = _weaponMaxAmmoValues[name];
        int possibleNewAmmo = currentAmmo + _weaponAmmoAddValues[name];
        temp.currentAmmo = possibleNewAmmo > maxAmmo ? maxAmmo : possibleNewAmmo;
        weapons[weaponIndex] = temp;
    }

    void AddNewWeapon(string name) {
        if(equipedWeaponIndex == -1) {
            UpdateEquipedWeapon(0);
        }
        int ammo = _weaponAmmoAddValues[name];
        WeaponStruct newWeapon = new WeaponStruct(name, ammo);
        weapons.Add(newWeapon);
    }

    void UseWeapon() {
        if(equipedWeaponIndex == -1) return;
        WeaponStruct temp = weapons[equipedWeaponIndex];
        temp.currentAmmo --;
        if (temp.currentAmmo < 1) {
            weapons.RemoveAt(equipedWeaponIndex);
            equipedWeaponIndex--;
            if(equipedWeaponIndex == -1 && weapons.Count > 0) {
                UpdateEquipedWeapon(0);
            }
        } else {
            weapons[equipedWeaponIndex] = temp;
        }
    }

    void UpdateEquipedWeapon (int newIndex) {
        equipedWeaponIndex = newIndex;
    }
    
    public void SwitchWeapon(bool upwards)
    {
        if(weapons.Count < 2) return; // if less than 2 weapons you cant switch
        bool isOnEdge;
        int newIndex;
        if(upwards) {
            isOnEdge = equipedWeaponIndex + 1 < weapons.Count;
            newIndex = isOnEdge ? equipedWeaponIndex + 1 : 0;
        } else {
            isOnEdge = equipedWeaponIndex - 1 > -1;
            newIndex = isOnEdge ? equipedWeaponIndex - 1 : weapons.Count - 1;
        }
        UpdateEquipedWeapon(newIndex);
    }
}
