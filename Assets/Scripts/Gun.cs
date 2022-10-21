using System;
using System.Collections;
using UnityEngine;


// [RequireComponent(typeof(Animator))]
public class Gun : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _range;
    [SerializeField] private bool _hasSpread = false;
    [SerializeField] private Vector3 _spreadVarianceVector = new Vector3(.1f, .1f, .1f);
    [SerializeField] private ParticleSystem _shootingSystem;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private ParticleSystem _impactSystem;
    [SerializeField] private TrailRenderer _bulletTrail;
    [SerializeField] private float _shootDelay = .5f;
    [SerializeField] private LayerMask _mask;

    private Animator _anim;
    private float _lastShootTime;

    private void Awake() {
        _anim = GetComponent<Animator>();

    }

    void Update() {
        if(Input.GetButton("Fire1")) {
            Debug.Log("attempt shoot");
            Shoot();
        }
    }

    private void Shoot()
    {
        if(_lastShootTime + _shootDelay < Time.time) {
            RaycastHit hit;

            Vector3 direction = GetDirection();


            if(Physics.Raycast(transform.position, direction, out hit, _range)) {
                TrailRenderer trail = Instantiate(_bulletTrail, transform.position, Quaternion.identity);

                StartCoroutine(SpawnTrail(trail, hit));

                _lastShootTime = Time.time;
                // Debug.Log(hit.transform.name);
            }
        }
    }

    private Vector3 GetDirection()
    {
        Vector3 direction = transform.forward;

        if(_hasSpread) {
            direction += new Vector3 (
                UnityEngine.Random.Range(-_spreadVarianceVector.x, _spreadVarianceVector.x),
                UnityEngine.Random.Range(-_spreadVarianceVector.x, _spreadVarianceVector.x),
                UnityEngine.Random.Range(-_spreadVarianceVector.x, _spreadVarianceVector.x)
            );
            direction.Normalize();
        }

        return direction;
    }

    private IEnumerator SpawnTrail (TrailRenderer trail, RaycastHit hit) {
        float time = 0;
        Vector3 startPosition = trail.transform.position;

        while (time<1) {
            trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
            time += Time.deltaTime / trail.time;
            yield return null;
        }

        // _anim.SetBool("isShooting", false);

        trail.transform.position = hit.point;
        // Instantiate(_impactSystem, hit.point, Quaternion.LookRotation(hit.normal));

        Destroy(trail.gameObject, trail.time);
    }

}
