using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpecialWeapon
{


    CarController CarController { get; }

    public void Fire();
    public void RegisterWeapon();
}
