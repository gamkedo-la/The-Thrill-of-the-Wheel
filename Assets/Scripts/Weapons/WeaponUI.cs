using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private Image _weaponVisual;
    [SerializeField] private TextMeshProUGUI _weaponAmmo;
    [SerializeField] private Sprite[] _weaponSprites;
    [SerializeField] private Dictionary<string, int> _spriteIndexes;
    [SerializeField] private WeaponInventory playerInventory;

    private void Start()
    {
        _spriteIndexes = new Dictionary<string, int>() {
            {"turret", 0},
            {"barrel", 1},
            {"missiles", 2},
            {"sonic", 3},
            {"acid", 4},
        };
    }

    public void SetWeaponInventory(WeaponInventory inventory) {
        playerInventory = inventory;
        inventory.onSwitchWeapon += SwitchWeaponTo;
    }

    public void SwitchWeaponTo(string name, int amount)
    {
        if(_weaponVisual.sprite == null){
            _weaponVisual.color = new Color(1, 1, 1, 1);
        }
        if(name == "none") {
            _weaponVisual.color = new Color(1, 1, 1, 0);
            _weaponVisual.sprite = null;
            _weaponAmmo.text = "";
            return;
        }
        _weaponVisual.sprite = _weaponSprites[_spriteIndexes[name]];
        _weaponAmmo.text = "" + amount;
    }
}
