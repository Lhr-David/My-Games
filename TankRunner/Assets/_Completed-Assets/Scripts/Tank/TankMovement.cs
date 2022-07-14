using UnityEngine;

namespace Complete
{
    public class TankMovement : MonoBehaviour
    {
        public float speedZ = 5f;
        public float speedX = 5f;

        public AudioSource m_MovementAudio;         // Reference to the audio source used to play engine sounds. NB: different to the shooting audio source.
        public AudioClip m_EngineIdling;            // Audio to play when the tank isn't moving.
        public AudioClip m_EngineDriving;           // Audio to play when the tank is moving.

        private float m_OriginalPitch;              // The pitch of the audio source at the start of the scene.
        private ParticleSystem[] m_particleSystems; // References to all the particles systems used by the Tanks

        Place _targetPlace;
        float _centerX;
        float _rightX;
        float _leftX;

        private void OnEnable()
        {
            // We grab all the Particle systems child of that Tank to be able to Stop/Play them on Deactivate/Activate
            // It is needed because we move the Tank when spawning it, and if the Particle System is playing while we do that
            // it "think" it move from (0,0,0) to the spawn point, creating a huge trail of smoke
            m_particleSystems = GetComponentsInChildren<ParticleSystem>();
            for (int i = 0; i < m_particleSystems.Length; ++i)
                m_particleSystems[i].Play();
        }

        private void OnDisable()
        {
            for (int i = 0; i < m_particleSystems.Length; ++i)
            {
                m_particleSystems[i].Stop();
            }
        }

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
            }
            else
            {
                IdleEngineAudio();
            }

            //Debug.Log(xDir);
            Vector3 movement = Vector3.forward * speedZ + xDir * Vector3.right * speedX;
            transform.position += movement * Time.deltaTime;
        }
    }
}