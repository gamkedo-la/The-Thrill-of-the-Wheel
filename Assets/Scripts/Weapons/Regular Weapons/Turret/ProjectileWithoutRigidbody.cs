using UnityEngine;

public class ProjectileWithoutRigidbody : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 15f;
    [SerializeField] float _timeToDestroy;
    public GameObject origin;

    public Vector3 target;
    const int DAMAGE = 7;

    private void Awake() {
        Invoke("Delete", _timeToDestroy);
    }

    private void Delete() {
        Destroy(gameObject);
    }
   
    private void Update()   
    {
        transform.position = Vector3.MoveTowards(transform.position, target, projectileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject == origin) {
            return;
        }
        if(other.CompareTag("Player") || other.CompareTag("Enemy")) {
            other.GetComponent<HealthController>().ChangeLife(-DAMAGE);
            Destroy(gameObject);
        }
    }
}
