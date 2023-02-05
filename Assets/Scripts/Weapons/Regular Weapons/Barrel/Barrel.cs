using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] MeshRenderer _barrelModel;
    [SerializeField] GameObject _explosion;
    bool _aboutToExplode;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("RandomScaleUp", 2.0f, .3f);
        Invoke("EnableExplosionParticle", 4.0f);
        Invoke("DeleteSelf", 6.0f);
    }

    void RandomScaleUp() {
        transform.localScale = new Vector3(Random.Range(1.0f, 1.5f), Random.Range(1.0f, 1.3f), Random.Range(1.0f, 1.5f));
    }

    void EnableExplosionParticle() {
        CancelInvoke("RandomScaleUp");
        _barrelModel.enabled = false;
        _explosion.SetActive(true);
    }

    void DeleteSelf() {
        GameObject.Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            Debug.Log("Push Player");
        }
    }
}
