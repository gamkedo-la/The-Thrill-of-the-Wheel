
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

        _isTargetInSight = Mathf.Abs(angleToTarget) > _maxAngleToShoot && distanceToTarget < _maxGunDistance;

        if(_isTargetInSight) {
            _gunController.AttemptShoot();
        }

        if(weaponInventory.HasWeapons()) {
            int missileIndex = weaponInventory.GetWeaponIndex("missiles");
            int barrelIndex = weaponInventory.GetWeaponIndex("barrel");
            int turretIndex = weaponInventory.GetWeaponIndex("turret");
            int sonicIndex = weaponInventory.GetWeaponIndex("sonic");

            if(missileIndex != -1) {
                // missiles are always shot if available
                weaponInventory.UpdateEquipedWeapon(missileIndex);
                weaponInventory.FireEquippedWeaponEnemy(_player);
            }

            if(barrelIndex != -1) {
                Vector3 toTarget = (_player.position - transform.position).normalized;
                // player is behind car
                if (Vector3.Dot(toTarget, transform.forward) < 0) {
                    weaponInventory.UpdateEquipedWeapon(barrelIndex);
                    weaponInventory.FireEquippedWeaponEnemy(_player);
                }
            }

            if(turretIndex != -1) {
                if(distanceToTarget < 13) {
                    weaponInventory.UpdateEquipedWeapon(turretIndex);
                    weaponInventory.FireEquippedWeaponEnemy(_player);
                }
            }

            if(sonicIndex != -1) {
                if(distanceToTarget < 10) {
                    weaponInventory.UpdateEquipedWeapon(sonicIndex);
                    weaponInventory.FireEquippedWeaponEnemy(_player);
                }
            }
        }
    }

    public void SetTarget(Transform target) {
        _player = target;
    }
}
