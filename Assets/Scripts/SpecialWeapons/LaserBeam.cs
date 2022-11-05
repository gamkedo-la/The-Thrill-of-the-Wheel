using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    [SerializeField] private int _maxLength;
    [SerializeField] private float _growthRate;
    [SerializeField] private float _duration;
    private double _timeAlive;
    private LineRenderer _lineRenderer;
    private BoxCollider _collider;
    
    // Start is called before the first frame update
    void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _collider = GetComponent<BoxCollider>();
        _timeAlive = 0.0;
    }

    // Update is called once per frame
    void Update()
    {
        float lineRendererSize = _lineRenderer.GetPosition(1).z + _growthRate;
        float colliderSize = _collider.size.z;
        float collideroffset = _collider.center.z;
        if (lineRendererSize < _maxLength)
        {
            _lineRenderer.SetPosition(1, new Vector3(0f, 0f, lineRendererSize));
            _collider.size = new Vector3(.2f, .2f, lineRendererSize);
            _collider.center = new Vector3(0,0, lineRendererSize / 2);
        } else if (_timeAlive < _duration) {
            _timeAlive += Time.deltaTime;
        } else {
            Destroy(gameObject);
        }  
    }
}
