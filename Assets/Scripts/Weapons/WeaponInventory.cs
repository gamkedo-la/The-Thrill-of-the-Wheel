using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;
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
    private DriveInputs _driveInputs;
    [SerializeField] List<WeaponStruct> weapons;
    private int equipedWeaponIndex;

    public event Action<string, int> onSwitchWeapon;

    // Weapon variables
    public Transform gunPoint;    
    public GameObject missilePrefab;
    [SerializeField] Transform barrelPoint;
    [SerializeField] Transform barrelPrefab;
    [SerializeField] GameObject turret;
    
    // Start is called before the first frame update
    private void Awake() {
        _driveInputs = new DriveInputs();
    }

    private void OnEnable() {
        _driveInputs.Player.SwitchWeaponLeft.performed += SwitchWeapon;
        _driveInputs.Player.SwitchWeaponLeft.Enable();

        _driveInputs.Player.SwitchWeaponRight.performed += SwitchWeapon;
        _driveInputs.Player.SwitchWeaponRight.Enable();

        _driveInputs.Player.FireAlternative.performed += FireEquippedWeapon;
        _driveInputs.Player.FireAlternative.Enable();

    }

    private void OnDisable() {
        _driveInputs.Player.SwitchWeaponLeft.Disable();
        _driveInputs.Player.SwitchWeaponRight.Disable();
        _driveInputs.Player.FireAlternative.Disable();
    }

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

    public void PickWeapon(string name) {
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
        int ammo = _weaponAmmoAddValues[name];
        WeaponStruct newWeapon = new WeaponStruct(name, ammo);
        if(name == "turret") {
            turret.SetActive(true);
        }
        weapons.Add(newWeapon);
        if(equipedWeaponIndex == -1) {
            UpdateEquipedWeapon(0);
        }
    }

    void UseWeapon() {
        if(equipedWeaponIndex == -1) return;
        WeaponStruct temp = weapons[equipedWeaponIndex];
        temp.currentAmmo --;
        if (temp.currentAmmo < 1) {
            if(temp.name == "turret") {
                turret.SetActive(false);
            }
            weapons.RemoveAt(equipedWeaponIndex);
            int newIndex = equipedWeaponIndex - 1;
            if(newIndex == -1 && weapons.Count > 0) {
                UpdateEquipedWeapon(0);
            } else {
                UpdateEquipedWeapon(newIndex);
            }
        } else {
            weapons[equipedWeaponIndex] = temp;
        }
    }

    

    void FireEquippedWeapon (InputAction.CallbackContext obj) {
        if(weapons.Count < 1) return;

        string currentWeapon = weapons[equipedWeaponIndex].name;
        switch (currentWeapon)
        {
            case "missiles":
                Transform closestEnemy = GameManager.Instance.GetClosestEnemy();
                GameObject Missile = Instantiate(missilePrefab, gunPoint.position, Quaternion.identity);
                Missile.GetComponent<HomingMissile>().SetTarget(closestEnemy);
                break;
            case "turret":
                turret.GetComponent<Turret>().FireTurret();
                break;
            case "barrel":
                Instantiate(barrelPrefab, barrelPoint.position, Quaternion.identity);
                break;
            case "sonic":
                gameObject.GetComponent<SonicBlast>().Fire();
                break;
        }
    }

    void UpdateEquipedWeapon (int newIndex) {
        equipedWeaponIndex = newIndex;
        if(newIndex == -1) {
            onSwitchWeapon("none", 0);
        } else {
            onSwitchWeapon(weapons[newIndex].name, weapons[newIndex].currentAmmo);
        }
    }
    
    public void SwitchWeapon(InputAction.CallbackContext obj)
    {
        string actionName = obj.action.name;
        if(weapons.Count < 2) return; // if less than 2 weapons you cant switch
        bool isOnEdge;
        int newIndex;
        if(actionName=="SwitchWeaponRight") {
            isOnEdge = equipedWeaponIndex + 1 < weapons.Count;
            newIndex = isOnEdge ? equipedWeaponIndex + 1 : 0;
        } else {
            isOnEdge = equipedWeaponIndex - 1 > -1;
            newIndex = isOnEdge ? equipedWeaponIndex - 1 : weapons.Count - 1;
        }
        UpdateEquipedWeapon(newIndex);
    }
}
