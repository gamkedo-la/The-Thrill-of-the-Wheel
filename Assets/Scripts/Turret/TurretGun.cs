using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretGun : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] float rateOfFire = 1f;

    [SerializeField] Transform turretGunPoint;   


    private void Start()
    {
        if(turretGunPoint == null)
            turretGunPoint = GetComponentInChildren<TurretGunPoint>().transform;
    }
    public float GetRateOfFire()
    {
        return rateOfFire;   
    }

    public void Fire()
    {
        Instantiate(projectile, turretGunPoint.position, transform.rotation);     
    }
}
