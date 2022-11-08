using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _maxDistancePredict = 100;
    [SerializeField] private float _minDistancePredict = 5;
    [SerializeField] private float _maxTimePrediction = 5;
    private Vector3 _standardPrediction, _deviatedPrediction;
    private Rigidbody _rb;

    private void Awake() {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        _rb.velocity = transform.forward * _speed;
        var leadTimePercentage = Mathf.InverseLerp(_minDistancePredict, _maxDistancePredict, Vector3.Distance(transform.position, _target.position));
        PredictMovement(leadTimePercentage);
        RotateRocket();
    }

    private void PredictMovement(float leadTimePercentage) {
        var predictionTime = Mathf.Lerp(0, _maxTimePrediction, leadTimePercentage);
        Rigidbody targetRigidbody = _target.GetComponent<Rigidbody>();
        _standardPrediction = targetRigidbody.position + targetRigidbody.velocity * predictionTime;
    }

    private void RotateRocket() {
        var heading = _standardPrediction - transform.position;
        var rotation = Quaternion.LookRotation(heading);
        _rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, _rotateSpeed * Time.deltaTime));
    }
    
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, _standardPrediction);
    }
}
