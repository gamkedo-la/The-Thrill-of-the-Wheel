using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

// this is an experimental HACK of CarController.cs
// still in flux - we might not want to do it this way
// and instead emit "AI Inputs"
public class randomDriverAI : MonoBehaviour
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
    }

    [SerializeField] private float _maxAcceleration;
    [SerializeField] private float _brakeAcceleration;
    [SerializeField] private float _turnSensitivity;
    [SerializeField] private float _maxSteerAngle;
    [SerializeField] private List<Wheel> _wheels;
    
    private Vector3 _centerOfMass;
    private Rigidbody _rb;

    // the AI sets these
    private float _throttleInput;
    private float _steerInput;
    private float _brakeInput;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass = _centerOfMass;
    }

    private void OnEnable() {
        // maybe unpause the ai?
    }

    private void OnDisable() {
        // maybe pause the ai?
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

    // this is the entire AI lol so simple and silly
    void GetInputs()
    {
        _throttleInput = Mathf.Sin(Time.time*2);
        _steerInput = Mathf.Sin(Time.time*3.11f);
        _brakeInput = 0;
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
        if (_brakeInput > 0 || _throttleInput == 0)
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
