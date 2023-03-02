using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicPool : MonoBehaviour
{
    [SerializeField] float _tickTime;
    [SerializeField] float _timeToDestroy;
    GameObject _playerInside;
    GameObject _enemyInside;
    float _playerTimer; 
    float _enemyTimer;

    private void Awake() {
        Invoke("Delete", _timeToDestroy);
    }

    private void Delete() {
        Destroy(gameObject);
    }
    
    private void Update() {
        if(_playerInside) {
            if(_playerTimer < 0) {
                _playerTimer = _tickTime;
                _playerInside.GetComponent<HealthController>().ChangeLife(-5);
            } else {
                _playerTimer -= Time.deltaTime;
            }
        }
        if(_enemyInside) {
            if(_enemyTimer < 0) {
                _enemyTimer = _tickTime;
                _enemyInside.GetComponent<HealthController>().ChangeLife(-5);
            } else {
                _enemyTimer -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            _playerTimer = _tickTime;
            _playerInside = other.gameObject;
        }
        if(other.CompareTag("Enemy")) {
            _enemyTimer = _tickTime;
            _enemyInside = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")) {
            _playerTimer = 0f;
            _playerInside = null;
        }
        if(other.CompareTag("Enemy")) {
            _enemyTimer = 0;
            _enemyInside = null;
        }
        
    }
}
