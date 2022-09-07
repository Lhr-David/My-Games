using UnityEngine;

public class CameraFollowZOnlyBehaviour : MonoBehaviour
{
    Vector3 _offset;

    public Transform target;

    float defaultX;

    private void Start()
    {
        _offset = transform.position - target.position;
        defaultX = transform.position.x;
    }

    private void LateUpdate()
    {
        var newPosition = target.position + _offset;
        newPosition.x = defaultX;
        transform.position = newPosition;
    }
}
