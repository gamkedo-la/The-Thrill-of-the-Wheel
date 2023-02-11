
using UnityEngine;

public class EnemyWeapons : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] Gun _gunController;
    [SerializeField] float _maxGunDistance;
    [SerializeField] float _maxAngleToShoot;
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
    }

    public void SetTarget(Transform target) {
        _player = target;
    }
}
