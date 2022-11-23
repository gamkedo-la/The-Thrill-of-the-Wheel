using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagmaSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _magmaProjectilePrefab;
    [SerializeField] private float _spawnTimer;
    [SerializeField] private float _verticalForce;
    [SerializeField] private float _horizontalForce;

    private void OnEnable() {
        InvokeRepeating("ShootMagma", 0f, _spawnTimer);
    }

    private void OnDisable() {
        CancelInvoke();
    }

    private void ShootMagma() {
        GameObject projectile = Instantiate(_magmaProjectilePrefab, transform.position, Quaternion.identity);
        float randomX = Random.Range(-1, 1) * _horizontalForce;
        float randomZ = Random.Range(-1, 1) * _horizontalForce;
        projectile.GetComponent<Rigidbody>().AddForce(new Vector3(randomX, _verticalForce, randomZ));
    }
}
