
using UnityEngine.InputSystem;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    [SerializeField] MeshRenderer _cannonModel;
    [SerializeField] GameObject _explosion;

    private void OnCollisionEnter(Collision other) {
        if(!other.transform.CompareTag("Player")) {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            _cannonModel.enabled = false;
            _explosion.SetActive(true);
            Invoke("DestroySelf", .5f);
        }
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.name);
    }

    private void DestroySelf () {
        Destroy(gameObject);
    }
}
