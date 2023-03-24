using UnityEngine;
using UnityEngine.UI;

public class UIHealthbar : MonoBehaviour
{
    private Image _healthBar;
    bool healthBarSet = false;

    private void Start()
    {
        _healthBar = GetComponent<Image>();
    }

    public void SetHealthbar(HealthController hController) {
        hController.onHealthChange += UpdateUI;
        healthBarSet = true;
    }

    private void UpdateUI(float adjustedChange) {
        if(healthBarSet){
            Debug.Log(adjustedChange);
            _healthBar.fillAmount += adjustedChange;
        }
    }
}
