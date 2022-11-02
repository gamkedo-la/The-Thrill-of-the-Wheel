using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] Image weaponImage = null;
    [SerializeField] TextMeshProUGUI ammoText = null;

    private void Start()
    {
        FindObjectOfType<WeaponInventory>().onSwitchWeapon += SwitchWeaponTo;
    }

    public void SwitchWeaponTo(GameObject weapon)
    {
        if (weapon == null) Debug.Log("Unequip weapon");
        else Debug.Log("Equip " + weapon.name);
    }
}
