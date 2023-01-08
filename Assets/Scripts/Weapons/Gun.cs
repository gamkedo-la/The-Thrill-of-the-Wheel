using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;


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
    [SerializeField] private float maxShootDistance;
    // Input variables
    private DriveInputs _driveInputs;

    private Animator _anim;
    private float _lastShootTime;

    private void Awake() {
        _anim = GetComponent<Animator>();
        _driveInputs = new DriveInputs();
        _driveInputs.Player.FireRegular.performed += Shoot;
    }

    private void OnEnable() {
        _driveInputs.Player.FireRegular.Enable();
    }

    private void OnDisable() {
        _driveInputs.Player.FireRegular.Disable();
    }

    private void Shoot(InputAction.CallbackContext obj)
    {
        if(_lastShootTime + _shootDelay < Time.time) {
            RaycastHit hit;

            Vector3 direction = GetDirection();

            Vector3 endPoint = transform.position + (direction * maxShootDistance);

            TrailRenderer trail = Instantiate(_bulletTrail, transform.position, Quaternion.identity);


            StartCoroutine(SpawnTrail(trail, endPoint));
            _lastShootTime = Time.time;
            if(Physics.Raycast(transform.position, direction, out hit, _range)) {
                // Add damage logic here
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

    private IEnumerator SpawnTrail (TrailRenderer trail, Vector3 endpoint) {
        float time = 0;
        Vector3 startPosition = trail.transform.position;

        while (Vector3.Distance(trail.transform.position, endpoint) > 1) {
            trail.transform.position = Vector3.Lerp(startPosition, endpoint, time);
            time += Time.deltaTime / trail.time;
            yield return null;
        }

        // _anim.SetBool("isShooting", false);
        // Instantiate(_impactSystem, hit.point, Quaternion.LookRotation(hit.normal));

        Destroy(trail.gameObject, trail.time);
    }

}
