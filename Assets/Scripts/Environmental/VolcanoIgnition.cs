using UnityEngine;

public class VolcanoIgnition : MonoBehaviour
{
    [SerializeField] private float _switchTimer;
    [SerializeField] private GameObject _ignitionElements;
    private bool _isIgnited;

    void Start()
    {
        _isIgnited = false;
        InvokeRepeating("SwitchStatus", 0f, _switchTimer);
    }

    void SwitchStatus() {
        _isIgnited = !_isIgnited;
        _ignitionElements.SetActive(_isIgnited);
    }
}
