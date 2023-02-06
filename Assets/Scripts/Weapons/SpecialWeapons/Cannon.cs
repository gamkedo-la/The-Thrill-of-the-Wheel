using UnityEngine.InputSystem;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] Transform _shootingPoint;
    [SerializeField] GameObject _cannonballPrefab;
    [SerializeField] float _cannonForce;
    // Input variables
    private DriveInputs _driveInputs;

    private void Awake() {
        _driveInputs = new DriveInputs();
        _driveInputs.Player.FireSpecial.performed += FireCannonball;
    }

     private void OnEnable()
    {
        _driveInputs.Player.FireSpecial.Enable();
    }

    private void OnDisable()
    {
        _driveInputs.Player.FireSpecial.Disable();
    }

    private void FireCannonball(InputAction.CallbackContext obj)
    {
        GameObject cannonBall = Instantiate(_cannonballPrefab, _shootingPoint.position, Quaternion.identity);
        cannonBall.GetComponent<Rigidbody>().AddForce(transform.forward * _cannonForce, ForceMode.Impulse);
    }
}
