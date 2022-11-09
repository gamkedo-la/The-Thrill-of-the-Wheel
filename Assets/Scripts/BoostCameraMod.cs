using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostCameraMod : MonoBehaviour
{

    [SerializeField] CarController _carController;
    CameraFollow _cameraFollow;
    float _originalTranslateSpeed;
    bool _boostStarted;

    // Start is called before the first frame update
    void Start()
    {
        _cameraFollow = GetComponent<CameraFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_carController.IsBoosted && !_boostStarted)
        {
            _originalTranslateSpeed = _cameraFollow.TranslateSpeed;
            _cameraFollow.TranslateSpeed = 5;
            _boostStarted = true;
            return;
        }

        if (!_carController.IsBoosted && _boostStarted)
        {
            _cameraFollow.TranslateSpeed = _originalTranslateSpeed;
            _boostStarted = false;
            return;
        }


    }
}
