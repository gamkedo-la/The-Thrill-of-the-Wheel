
using UnityEngine;

public class EnemyWeapons : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] Gun _gunController;
    [SerializeField] float _maxGunDistance;
    [SerializeField] float _maxAngleToShoot;
    [SerializeField] WeaponInventory weaponInventory;
    bool _isTargetInSight;

    void Update()
    {
        if(!_player) return;

        Vector3 directionToTarget = transform.position - _player.position;
        float angleToTarget = Vector3.Angle(transform.forward, directionToTarget);
        float distanceToTarget = Vector3.Distance(transform.position, _player.position);

        int missileIndex = weaponInventory.GetWeaponIndex("missiles");
        int barrelIndex = weaponInventory.GetWeaponIndex("barrel");
        int turretIndex = weaponInventory.GetWeaponIndex("turret");
        int sonicIndex = weaponInventory.GetWeaponIndex("sonic");

        if(missileIndex != -1) {
            Debug.Log("Has Missile");
            weaponInventory.UpdateEquipedWeapon(missileIndex);
            weaponInventory.FireEquippedWeaponEnemy(_player);
        }

        if(barrelIndex != -1) {
            Debug.Log("Has Barrel");
            weaponInventory.UpdateEquipedWeapon(barrelIndex);
            weaponInventory.FireEquippedWeaponEnemy(_player);
        }

        if(turretIndex != -1) {
            Debug.Log("Has Turret");
            weaponInventory.UpdateEquipedWeapon(turretIndex);
            weaponInventory.FireEquippedWeaponEnemy(_player);
        }

        if(sonicIndex != -1) {
            Debug.Log("Has Sonic");
            weaponInventory.UpdateEquipedWeapon(sonicIndex);
            weaponInventory.FireEquippedWeaponEnemy(_player);
        }

        _isTargetInSight = Mathf.Abs(angleToTarget) > _maxAngleToShoot && distanceToTarget < _maxGunDistance;

        if(_isTargetInSight) {
            _gunController.AttemptShoot();
        }
    }

    public void SetTarget(Transform target) {
        _player = target;
    }
}
