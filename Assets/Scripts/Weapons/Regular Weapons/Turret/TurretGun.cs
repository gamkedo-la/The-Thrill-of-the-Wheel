using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretGun : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] float rateOfFire = 1f;

    [SerializeField] Transform turretGunPoint;
    [SerializeField] GameObject turretParent;


    private void Start()
    {
        if(turretGunPoint == null)
            turretGunPoint = GetComponentInChildren<TurretGunPoint>().transform;
    }
    public float GetRateOfFire()
    {
        return rateOfFire;   
    }

    public void Fire(Vector3 target)
    {
        GameObject bullet = Instantiate(projectile, turretGunPoint.position, transform.rotation);
        bullet.GetComponent<ProjectileWithoutRigidbody>().target = target;
        bullet.GetComponent<ProjectileWithoutRigidbody>().origin = turretParent;
    }
}
