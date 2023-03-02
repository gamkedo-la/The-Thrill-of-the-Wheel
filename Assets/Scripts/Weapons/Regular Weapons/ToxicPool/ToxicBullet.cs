using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicBullet : MonoBehaviour
{
    [SerializeField] GameObject _toxicPoolPrefab;
    [SerializeField] float _bulletForce;
    
    public void Shoot (Vector3 forward) {
        GetComponent<Rigidbody>().AddForce(forward * _bulletForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision other) {
        if(other.transform.CompareTag("Terrain")) {
            Instantiate(_toxicPoolPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
