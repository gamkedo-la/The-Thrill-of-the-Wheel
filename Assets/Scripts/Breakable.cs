using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    [SerializeField] private GameObject _brokenPrefab;
    [SerializeField] private float _breakForce = 2;
    [SerializeField] private float _collisionMultiplier = 100;
    [SerializeField] private bool _isBroken;

    private void OnCollisionEnter(Collision other) {
        if(_isBroken){
            return;
        }

        if(other.relativeVelocity.magnitude >= _breakForce){
            _isBroken = true;
            var fracturedObject = Instantiate(_brokenPrefab, transform.position, transform.rotation);

            var rigidbodies = fracturedObject.GetComponentsInChildren<Rigidbody>();

            foreach (var rb in rigidbodies)
            {
                rb.AddExplosionForce(other.relativeVelocity.magnitude * _collisionMultiplier, other.contacts[0].point, 2);
            }

            Destroy(gameObject);
        }
    }
}
