using UnityEngine;

public class UIMinimapFollower : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float height;
    public void SetTarget(Transform t) {
        target = t;
    }

    private void LateUpdate() {
        transform.position = target.position + new Vector3(0, height, 0);
    }
}