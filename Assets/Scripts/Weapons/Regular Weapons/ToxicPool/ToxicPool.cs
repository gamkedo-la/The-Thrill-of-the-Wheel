using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicPool : MonoBehaviour
{
    [SerializeField] float _tickTime;
    bool _playerInside;
    bool _enemyInside;
    float _playerTimer; 
    float _enemyTimer;
    
    private void Update() {
        if(_playerInside) {
            if(_playerTimer < 0) {
                Debug.Log("damage player");
                _playerTimer = _tickTime;
            } else {
                _playerTimer -= Time.deltaTime;
            }
        }
        if(_enemyInside) {
            if(_enemyTimer < 0) {
                Debug.Log("damage enemy");
                _enemyTimer = _tickTime;
            } else {
                _enemyTimer -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")) {
            _playerTimer = _tickTime;
            _playerInside = true;
        }
        if(other.CompareTag("Enemy")) {
            _enemyTimer = _tickTime;
            _enemyInside = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")) {
            _playerTimer = 0f;
            _playerInside = false;
        }
        if(other.CompareTag("Enemy")) {
            _enemyTimer = 0;
            _enemyInside = false;
        }
        
    }
}
