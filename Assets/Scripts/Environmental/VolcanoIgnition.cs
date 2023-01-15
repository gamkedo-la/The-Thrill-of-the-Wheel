using UnityEngine;

public class VolcanoIgnition : MonoBehaviour
{
    [SerializeField] private float _switchTimer;
    [SerializeField] private GameObject _ignitionElements;
    [SerializeField] private GameObject _coverElements;

    private bool _isIgnited;

    void Start()
    {
        _isIgnited = false;
        InvokeRepeating("SwitchStatus", 10f, _switchTimer);
    }

    void SwitchStatus() {
        _isIgnited = !_isIgnited;
        _ignitionElements.SetActive(_isIgnited);
        _coverElements.SetActive(!_isIgnited);
    }
}
