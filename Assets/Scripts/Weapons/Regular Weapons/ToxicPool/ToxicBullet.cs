using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicBullet : MonoBehaviour
{
    [SerializeField] GameObject _toxicPoolPrefab;

    private void OnCollisionEnter(Collision other) {
        if(other.transform.CompareTag("Terrain")) {
            Instantiate(_toxicPoolPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
