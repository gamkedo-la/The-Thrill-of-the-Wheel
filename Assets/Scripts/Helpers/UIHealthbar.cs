using UnityEngine;
using UnityEngine.UI;

public class UIHealthbar : MonoBehaviour
{
    private Image _healthBar;

    private void Start()
    {
        _healthBar = GetComponent<Image>();
        FindObjectOfType<HealthController>().onHealthChange += UpdateUI;
    }

    private void UpdateUI(float adjustedChange) {
        _healthBar.fillAmount += adjustedChange;
    }
}
