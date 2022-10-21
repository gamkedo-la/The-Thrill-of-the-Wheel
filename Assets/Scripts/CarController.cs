using System;
using System.Collections;
using System.Collections.Generic;
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

      void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass = _centerOfMass;

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
        _throttleInput = Input.GetAxis("Vertical");
        _steerInput = Input.GetAxis("Horizontal");
    }

    void Move()
    {
        foreach(var wheel in _wheels)
        {
            wheel.collider.motorTorque = _throttleInput * 1200 * _maxAcceleration * Time.deltaTime;
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
        if (Input.GetKey(KeyCode.Space) || _throttleInput == 0)
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
            rot *= Quaternion.Euler(0, 90, 0);
            wheel.model.transform.rotation = rot;
        }
    }

}
