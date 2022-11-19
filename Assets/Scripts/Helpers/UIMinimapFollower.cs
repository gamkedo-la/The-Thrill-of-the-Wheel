using UnityEngine;

public class UIMinimapFollower : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float height;
    private void LateUpdate() {
        transform.position = target.position + new Vector3(0, height, 0);
    }
}