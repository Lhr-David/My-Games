using UnityEngine;

public class FollowCameraBehaviour : MonoBehaviour
{
    public float m_MinSize = 6.5f;
    public Transform target;
    Vector3 _offset;
    public float idealY;
    private Vector3 m_DesiredPosition;

    void Start()
    {
        _offset = transform.position - target.position;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        m_DesiredPosition = target.position + _offset;
        m_DesiredPosition.y = idealY;
        transform.position = m_DesiredPosition;
    }
}