using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLiquid : MonoBehaviour
{
    [SerializeField] float _flowSpeed;
    MeshRenderer _meshRenderer;

    private void Awake() {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        _meshRenderer.material.mainTextureOffset = new Vector2(Mathf.Sin(Time.time * _flowSpeed), Mathf.Cos(Time.time * _flowSpeed));
    }
}
