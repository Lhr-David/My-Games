using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public float speedZ = 5f;
    public float speedX = 5f;

    public AudioSource m_MovementAudio;         // Reference to the audio source used to play engine sounds. NB: different to the shooting audio source.
    public AudioClip m_EngineIdling;            // Audio to play when the tank isn't moving.
    public AudioClip m_EngineDriving;           // Audio to play when the tank is moving.

    private float m_OriginalPitch;              // The pitch of the audio source at the start of the scene.
    public ParticleSystem[] switchingWayParticles;

    Place _targetPlace;
    float _centerX;
    float _rightX;
    float _leftX;

    public bool forceStop;

    private void Start()
    {
        m_OriginalPitch = m_MovementAudio.pitch;
        _targetPlace = Place.Road_Center;

        _centerX = MapSystem.GetXOffset(Place.Road_Center);
        _rightX = MapSystem.GetXOffset(Place.Road_Right);
        _leftX = MapSystem.GetXOffset(Place.Road_Left);
    }

    private void Update()
    {
        if (forceStop)
            return;

        ReceiveInput();
        Move();
    }

    private void IdleEngineAudio()
    {
        if (m_MovementAudio.clip != m_EngineIdling)
        {
            m_MovementAudio.clip = m_EngineIdling;
            m_MovementAudio.Play();
        }
    }

    private void DrivingEngineAudio()
    {
        if (m_MovementAudio.clip != m_EngineDriving)
        {
            m_MovementAudio.clip = m_EngineDriving;
            m_MovementAudio.Play();
        }
    }

    private void ReceiveInput()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            if (_targetPlace == Place.Road_Center)
                _targetPlace = Place.Road_Left;
            else if (_targetPlace == Place.Road_Right)
                _targetPlace = Place.Road_Center;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            if (_targetPlace == Place.Road_Center)
                _targetPlace = Place.Road_Right;
            else if (_targetPlace == Place.Road_Left)
                _targetPlace = Place.Road_Center;
        }
        //  Debug.Log(_targetPlace);
    }

    private void Move()
    {
        float targetX = MapSystem.GetXOffset(_targetPlace);
        float deltaX = targetX - transform.position.x;
        float xDir = 0;

        if (Mathf.Abs(deltaX) > 0.1f)
        {
            DrivingEngineAudio();
            if (deltaX > 0.1f)
                xDir = 1;
            else
                xDir = -1;

            ToggleSwitchingWayPs(true);
        }
        else
        {
            IdleEngineAudio();

            ToggleSwitchingWayPs(false);
        }

        //Debug.Log(xDir);
        Vector3 movement = Vector3.forward * speedZ + xDir * Vector3.right * speedX;
        transform.position += movement * Time.deltaTime;
    }

    void ToggleSwitchingWayPs(bool isPlaying)
    {
        foreach (var ps in switchingWayParticles)
        {
            if (isPlaying && !ps.isPlaying)
            {
                ps.Play();
            }
            else if (!isPlaying && ps.isPlaying)
            {
                ps.Stop();
            }
        }
    }
}