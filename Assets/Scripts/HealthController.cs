using System;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;
    public event Action<float> onHealthChange;
    
    // this should be deleted later on
    private void Start() {
    }

    public void ChangeLife(int change) {
        _currentHealth += change;
        float percentageChange = (float) change / _maxHealth;
        if(gameObject.CompareTag("Player")) {
            onHealthChange(percentageChange);
        }
    }
}
