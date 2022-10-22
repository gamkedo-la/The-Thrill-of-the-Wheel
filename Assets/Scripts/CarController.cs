using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public enum Axel {
        Front,
        Rear,
    };

    [Serializable]
    private struct Wheel {
        public GameObject model;
        public WheelCollider collider;
        public Axel axel;
        // Dust particle and others related directly to wheels could be stored here
    }
    // Input variables
    private DriveInputs _driveInputs;
    private InputAction _movementAction;
    private InputAction _brakeAction;
    // Acceleration Variables
    [SerializeField] private float _maxAcceleration;
    [SerializeField] private float _brakeAcceleration;
    // Steer Variables
    [SerializeField] private float _turnSensitivity;
    [SerializeField] private float _maxSteerAngle;
    // 
    private Vector3 _centerOfMass;
    [SerializeField] private List<Wheel> _wheels;
    private float _throttleInput;
    private float _steerInput;
    private Rigidbody _rb;

    void Awake()
    {
        _driveInputs = new DriveInputs();
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass = _centerOfMass;
        _movementAction = _driveInputs.Player.Move;
        _brakeAction = _driveInputs.Player.HandBrake;
    }

    private void OnEnable() {
        _movementAction.Enable();
        _brakeAction.Enable();
    }

    private void OnDisable() {
        _movementAction.Disable();
        _brakeAction.Disable();
    }

    void Update()
    {
        GetInputs();
        AnimateWheels();
    }

    void LateUpdate()
    {
        Move();
        Steer();
        Brake();
    }

    void GetInputs()
    {
        _throttleInput = _movementAction.ReadValue<Vector2>().y;
        _steerInput = _movementAction.ReadValue<Vector2>().x;
    }

    void Move()
    {
        foreach(var wheel in _wheels)
        {
            wheel.collider.motorTorque = _throttleInput * 2200 * _maxAcceleration * Time.deltaTime;
        }
    }

    void Steer()
    {
        foreach(var wheel in _wheels)
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
        foreach(var wheel in _wheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.collider.GetWorldPose(out pos, out rot);
            wheel.model.transform.position = pos;
            rot *= Quaternion.Euler(0, 90, 0); /// this is to rotate wheels in correct direction, perhaps tyler can help with it.
            wheel.model.transform.rotation = rot;
        }
    }

}
