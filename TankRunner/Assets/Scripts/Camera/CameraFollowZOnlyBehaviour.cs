using UnityEngine;

public class CameraFollowZOnlyBehaviour : MonoBehaviour
{
    Vector3 _offset;

    public Transform target;

    float defaultX;
    float defaultY;
    bool resetX;
    bool resetY;
    private void Start()
    {
        _offset = transform.position - target.position;
        defaultX = transform.position.x;
        resetX = true;
        resetY = false;
    }

    public void ResetOffset(Transform relatedTrans)
    {
        transform.position = target.position + relatedTrans.localPosition;
        transform.rotation = relatedTrans.localRotation;
        _offset = relatedTrans.localPosition;
        resetX = false;
        resetY = true;
        defaultY = transform.position.y;
    }

    private void LateUpdate()
    {
        var newPosition = target.position + _offset;
        if (resetX)
            newPosition.x = defaultX;
        if (resetY)
            newPosition.y = defaultY;
        transform.position = newPosition;
    }
}
