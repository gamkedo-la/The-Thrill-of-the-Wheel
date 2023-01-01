using System;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;
    public event Action<float> onHealthChange;
   
    private void ChangeLife(int change) {
        _currentHealth += change;
        float percentageChange = (float) change / _maxHealth;
        onHealthChange(percentageChange);
    }
}
