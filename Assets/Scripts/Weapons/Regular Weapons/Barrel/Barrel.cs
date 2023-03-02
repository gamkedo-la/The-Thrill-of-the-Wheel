using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    [SerializeField] MeshRenderer _barrelModel;
    [SerializeField] GameObject _explosion;
    bool _aboutToExplode;
    [SerializeField] float maxSize;
    [SerializeField] float force;
    [SerializeField] private float upwardsEffect;
    int BLAST_DAMAGE = 30;

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
        if(other.CompareTag("Player") || other.CompareTag("Enemy")) {
            StartCoroutine(ApplyForceToTarget(other.gameObject));
        }
    }

    private IEnumerator ApplyForceToTarget(GameObject target)
    {
        Rigidbody rb = target.GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0,upwardsEffect,0),ForceMode.Impulse);

        yield return new WaitForSeconds(0.1f);
        target.GetComponent<HealthController>().ChangeLife(-BLAST_DAMAGE);

        rb.AddExplosionForce(force,transform.position,maxSize,0,ForceMode.Impulse);
    }
}
