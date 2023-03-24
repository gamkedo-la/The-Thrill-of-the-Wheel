using System;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;
    [SerializeField] private GameObject _deathPrefab;
    public event Action<float> onHealthChange;
    // this should be deleted later on
    private void Start() {
    }

    public void ChangeLife(int change) {
        _currentHealth += change;
        _currentHealth = _currentHealth > _maxHealth ? _maxHealth : _currentHealth;
        if(_currentHealth < 1) {
            AudioManager.Instance.PlaySFX("Explosion");
            Instantiate(_deathPrefab, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
            Invoke("ToEndScreen", 2f);
        }
        if(gameObject.CompareTag("Player")) {
            float percentageChange = (float) change / _maxHealth;
            if (onHealthChange!=null) onHealthChange(percentageChange);
        }
    }

    void ToEndScreen () {
        if(gameObject.CompareTag("Player")){
            GameManager.Instance.Lose();
        } else {
            GameManager.Instance.CheckWinCondition();
        }
    }
}
