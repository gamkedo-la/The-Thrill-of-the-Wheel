using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class SonicBlast : MonoBehaviour, ISpecialWeapon
{

    private struct BlastTarget
    {
        public GameObject Target;
        public float Distance;
        public Rigidbody Rigidbody;
    }

    [SerializeField] float maxSize;
    [SerializeField] float growthSpeed;
    [SerializeField] float force;
    [SerializeField] float coolDownTime;
    [SerializeField] private float upwardsEffect;

    float currentRadius;
    bool firing;
    float currentCooldownTime;

    List<BlastTarget> blastTargets;
    List<BlastTarget> activatedTargets;

    CarController carController;

    public CarController CarController { get => carController; }

    LayerMask layerMask;

    public void Fire()
    {

        if (currentCooldownTime < coolDownTime)
        {
            return;
        }

        Debug.Log("Sonic Blast!!");
        currentCooldownTime = 0;
        firing = true;

        int maxColliders = 20;
        Collider[] hitColliders = new Collider[maxColliders];
        
        int numHits = Physics.OverlapSphereNonAlloc(transform.position, maxSize, hitColliders,layerMask);

        if(numHits > 0){
            foreach (var hit in hitColliders)
            {
                if(hit == null || hit.attachedRigidbody == null || hit.tag == "Player"){
                    continue;
                }
                blastTargets.Add(new BlastTarget{
                    Target = hit.gameObject,
                    Distance = Vector3.Distance(transform.position, hit.ClosestPoint(transform.position)),
                    Rigidbody = hit.attachedRigidbody
                });
            }

            blastTargets.OrderBy((bt) => bt.Distance);

        }
    }

    public void RegisterWeapon()
    {
        CarController.RegisterWeapon(Fire);
    }

    void Awake()
    {
        blastTargets = new List<BlastTarget>();
        activatedTargets = new List<BlastTarget>();
        currentCooldownTime = coolDownTime;
        layerMask = LayerMask.GetMask("EnemyCar");
    }

    // Start is called before the first frame update
    void Start()
    {
        carController = GetComponent<CarController>();
        RegisterWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        currentCooldownTime += Time.deltaTime;

        if (!firing)
        {
            return;
        }

        if (currentRadius >= maxSize)
        {
            firing = false;
            currentRadius = 0;
            return;
        }

        currentRadius += growthSpeed * Time.deltaTime;

        foreach (var target in blastTargets)
        {
            if(currentRadius > target.Distance){
                Debug.Log($"Hit {target.Target.name}");
                StartCoroutine(ApplyForceToTarget(target));
            }
        }

        foreach(var target in activatedTargets){
            blastTargets.Remove(target);
        }

        activatedTargets.Clear();

    }

    private IEnumerator ApplyForceToTarget(BlastTarget target)
    {
        activatedTargets.Add(target);

        target.Rigidbody.AddForce(new Vector3(0,upwardsEffect,0),ForceMode.Impulse);
        yield return new WaitForSeconds(0.1f);
        target.Rigidbody.AddExplosionForce(force,transform.position,maxSize,0,ForceMode.Impulse);

        /* Vector3 forceDir = (target.Target.transform.position - transform.position).normalized;


        Vector3 forceVector = forceDir * force;

        forceVector.y = upwardsEffect;  */

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, currentRadius);
    }
}
