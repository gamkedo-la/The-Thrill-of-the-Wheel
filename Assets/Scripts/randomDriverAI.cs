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

    ///////////////////////////////////////////////////////////
    // evil enemy car ai begins
    ///////////////////////////////////////////////////////////
    public Transform AI_target;
    public float slowDownWhenThisClose = 16f;
    private float _throttleInput;
    private float _steerInput;
    private float _brakeInput;
    // steering wheel/pedal change percent per second
    private float _lerpSpeed = 0.5f; 
    //private float _fireInput;
    ///////////////////////////////////////////////////////////
    // this is the only function that differs significantly
    // from CarController.cs - so we may want to separate
    // these two components and have this AI only send input
    // state to the same car controller as used by the player
    // so I don't have to copy+paste future enhancements
    ///////////////////////////////////////////////////////////
    void evil_enemy_car_AI()
    {
        /*
        // works great!

        // forward and reverse over time like a fool
        _throttleInput = Mathf.Sin(Time.time*2);
        
        // wobble back and forth over time like a fool
        _steerInput = Mathf.Sin(Time.time*3.11f);

        // never uses the brake
        _brakeInput = 0;

        // unarmed (so far!)
        //_fireInput = 0;

        */

        turn_towards_target(AI_target);
    }

    // hmm there are a lot of advanced steering behaviour AI tutorials online
    // but I don't want to write a thousand line industrial strength version
    void turn_towards_target(Transform where)
    {
        // this changes target to coordinates relative to our view
        Vector3 relativePos = transform.InverseTransformPoint(where.position);
        float targetDistance = Vector3.Distance(where.position, transform.position);
        float throttleAmount = 1f;
        _brakeInput = 0f;
        if (targetDistance < slowDownWhenThisClose) { // getting close to target:
            throttleAmount = targetDistance / slowDownWhenThisClose; // ease up on the gas
            _brakeInput = 1f - (targetDistance / slowDownWhenThisClose); // apply more brakes as we approach target
        }

        if (Mathf.Abs(relativePos.x) < 0.1f)
        {
            //Debug.Log("STRAIGHT");
            _steerInput = Mathf.Lerp(_steerInput,0,_lerpSpeed*Time.deltaTime);
        }
        else if (relativePos.x < 0)
        {
            //Debug.Log("LEFT");
            _steerInput = Mathf.Lerp(_steerInput,-1,_lerpSpeed*Time.deltaTime);
        }
        else if (relativePos.x > 0)
        {
            //Debug.Log("RIGHT");
            _steerInput = Mathf.Lerp(_steerInput,1,_lerpSpeed*Time.deltaTime);
        }

        if (relativePos.z >= 0)
        {
            //Debug.Log(targetDistance.ToString("F1") + " UNITS AHEAD: throttle = " + throttleAmount.ToString("F1") + " brake = " + _brakeInput.ToString("F1"));
            _throttleInput = Mathf.Lerp(_throttleInput,throttleAmount,_lerpSpeed*Time.deltaTime);
        } 
        else
        {
            //Debug.Log(targetDistance.ToString("F1") + " UNITS BEHIND: throttle = " + throttleAmount.ToString("F1") + " brake = " + _brakeInput.ToString("F1"));
            _throttleInput = Mathf.Lerp(_throttleInput,-throttleAmount,_lerpSpeed*Time.deltaTime);
        }
    }
    ///////////////////////////////////////////////////////////
    // evil enemy car ai ends
    ///////////////////////////////////////////////////////////




















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

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.centerOfMass = _centerOfMass;
    }

    private void OnEnable() {
    }

    private void OnDisable() {
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
        evil_enemy_car_AI(); // the only change is here
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
