using UnityEngine;

public class FollowCameraBehaviour : MonoBehaviour
{
    public float m_DampTime = 0.2f;
    public float m_MinSize = 6.5f;
    public Transform target;
    Vector3 _offset;
    public float idealY;

    private Vector3 m_MoveVelocity;
    private Vector3 m_DesiredPosition;


    private void Start()
    {
        _offset = transform.position - target.position;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        m_DesiredPosition = target.position + _offset;
        m_DesiredPosition.y = idealY;
        transform.position = Vector3.SmoothDamp(transform.position, m_DesiredPosition, ref m_MoveVelocity, m_DampTime);
    }
}