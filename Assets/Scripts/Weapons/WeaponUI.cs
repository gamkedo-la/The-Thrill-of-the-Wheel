using UnityEngine;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] Transform weaponVisualsLocation = null;

    private void Start()
    {
        // FindObjectOfType<WeaponInventory>().onSwitchWeapon += SwitchWeaponTo;
    }

    public void SwitchWeaponTo()
    {
        // if (weapon == null)
        // {
        //     DeactivateCurrentWeaponVisual();
        // }
        // else
        // {
        //     bool weaponVisualsAlreadyExists = ActivateWeaponVisualIfAlreadyExists(weapon);
        //     if (!weaponVisualsAlreadyExists) InstantiateNewWeaponVisual(weapon);
        // }
    }

    private void DeactivateCurrentWeaponVisual()
    {
        foreach (Transform child in weaponVisualsLocation)
        {
            if (child.gameObject.activeSelf)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    private bool ActivateWeaponVisualIfAlreadyExists(GameObject weapon)
    {
        bool weaponVisualsAlreadyExists = false;
        foreach (Transform child in weaponVisualsLocation)
        {
            if (child.name == weapon.transform.GetChild(0).name)
            {
                child.gameObject.SetActive(true);
                weaponVisualsAlreadyExists = true;
                break;
            }
        }

        return weaponVisualsAlreadyExists;
    }

    private void InstantiateNewWeaponVisual(GameObject weapon)
    {
        GameObject weaponInstance = Instantiate(
                        weapon.transform.GetChild(0).gameObject,
                        weaponVisualsLocation);

        weaponInstance.layer = LayerMask.NameToLayer("UI");
        weaponInstance.name = weapon.transform.GetChild(0).name;

        weaponInstance.transform.localRotation = Quaternion.identity;
        weaponInstance.transform.localPosition = Vector3.zero;
        weaponInstance.transform.localScale = Vector3.one;
    }
}
