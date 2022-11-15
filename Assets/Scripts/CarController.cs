using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public enum Axel
    {
        Front,
        Rear,
    };

    [Serializable]
    private struct Wheel
    {
        public GameObject model;
        public WheelCollider collider;
        public Axel axel;
        // Dust particle and others related directly to wheels could be stored here
    }

    [SerializeField]
    [NonReorderable]
    private List<Wheel> _wheels;
    

    // Input variables
    private DriveInputs _driveInputs;
    private InputAction _movementAction;
    private InputAction _brakeAction;
    private InputAction _switchWeapon;
    // Acceleration Variables
    [SerializeField] private float _maxAcceleration;
    [SerializeField] private float _brakeAcceleration;
    // Steer Variables
    [SerializeField] private float _turnSensitivity;
    [SerializeField] private float _maxSteerAngle;
    // 
    private Vector3 _centerOfMass;

    private float _throttleInput;
    private float _steerInput;
    private Rigidbody _rb;
    private WeaponInventory _weaponInventory;

    // Boost Variables
    bool _boost = false;
    [SerializeField] float _boostTime = 2f;
    [SerializeField] float _boostMultiplier = 2f;
    float _currentBoostTimer = 0f;

    public bool IsBoosted { get => _boost; }

    void Awake()
    {
        _driveInputs = new DriveInputs();
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass = _centerOfMass;
        _movementAction = _driveInputs.Player.Move;
        _brakeAction = _driveInputs.Player.HandBrake;
        _switchWeapon = _driveInputs.Player.SwitchWeapon;

        _weaponInventory = GetComponent<WeaponInventory>();
    }

    private void OnEnable()
    {
        _movementAction.Enable();
        _brakeAction.Enable();
        _switchWeapon.Enable();
    }

    private void OnDisable()
    {
        _movementAction.Disable();
        _brakeAction.Disable();
        _switchWeapon.Disable();
    }

    void Update()
    {
        GetInputs();
        AnimateWheels();
    }

    private void FixedUpdate() {
        if (_boost)
        {
            CheckBoostTime();
            ApplyBoostForce();
        }
    }

    void LateUpdate()
    {
        Move();
        Steer();
        Brake();
        SwitchWeapon();
    }

    void GetInputs()
    {
        _throttleInput = _movementAction.ReadValue<Vector2>().y;
        _steerInput = _movementAction.ReadValue<Vector2>().x;
    }

    void Move()
    {
        foreach (var wheel in _wheels)
        {
            wheel.collider.motorTorque = _throttleInput * 2200 * _maxAcceleration * Time.deltaTime;
        }
    }

    void Steer()
    {
        foreach (var wheel in _wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                var _steerAngle = _steerInput * _turnSensitivity * _maxSteerAngle;
                wheel.collider.steerAngle = Mathf.Lerp(wheel.collider.steerAngle, _steerAngle, 0.6f);
            }
        }
    }

    void Brake()
    {
        if (_brakeAction.ReadValue<float>() > 0 || _throttleInput == 0)
        {
            foreach (var wheel in _wheels)
            {
                wheel.collider.brakeTorque = 300 * _brakeAcceleration * Time.deltaTime;
            }

        }
        else
        {
            foreach (var wheel in _wheels)
            {
                wheel.collider.brakeTorque = 0;
            }
        }
    }

    void AnimateWheels()
    {
        foreach (var wheel in _wheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.collider.GetWorldPose(out pos, out rot);
            wheel.model.transform.position = pos;
            // rot *= Quaternion.Euler(0, 0, 0); /// this is to rotate wheels in correct direction, perhaps tyler can help with it.
            wheel.model.transform.rotation = rot;
        }
    }

    void SwitchWeapon()
    {
        if (!_switchWeapon.triggered) return;
        _weaponInventory.SwitchWeapon();
    }

    void CheckBoostTime()
    {
        _currentBoostTimer += Time.deltaTime;

        if(_currentBoostTimer > _boostTime){
            _boost = false;
            _currentBoostTimer = 0;
        }
    }

    public void SetBoost(){
        _boost = true;
        _currentBoostTimer = 0;
    }

    private void ApplyBoostForce()
    {
        _rb.AddForce(_rb.velocity.normalized * _boostMultiplier, ForceMode.Impulse);
    }


}
