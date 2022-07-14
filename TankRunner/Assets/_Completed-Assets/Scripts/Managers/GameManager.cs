using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Complete
{
    public class GameManager : MonoBehaviour
    {
        public int m_NumRoundsToWin = 5;
        public float m_StartDelay = 3f;
        public float m_EndDelay = 3f;
        public Text m_MessageText;
        public TankManager player;

        private int m_RoundNumber;
        private WaitForSeconds m_StartWait;
        private WaitForSeconds m_EndWait;

        const float k_MaxDepenetrationVelocity = float.PositiveInfinity;

        private void Start()
        {
            // This line fixes a change to the physics engine.
            Physics.defaultMaxDepenetrationVelocity = k_MaxDepenetrationVelocity;

            m_StartWait = new WaitForSeconds(m_StartDelay);
            m_EndWait = new WaitForSeconds(m_EndDelay);

            player.Setup();
            StartCoroutine(GameLoop());
        }

        private void SpawnPlayer()
        {
            player.Setup();
        }

        private IEnumerator GameLoop()
        {
            yield return StartCoroutine(RoundStarting());
            yield return StartCoroutine(RoundPlaying());
            yield return StartCoroutine(RoundEnding());

            StartCoroutine(GameLoop());
        }

        private IEnumerator RoundStarting()
        {
            ResetAllTanks();
            DisableTankControl();
            m_RoundNumber++;
            m_MessageText.text = "Round " + m_RoundNumber;

            yield return m_StartWait;
        }


        private IEnumerator RoundPlaying()
        {
            EnableTankControl();

            m_MessageText.text = string.Empty;

            while (true)
            {
                yield return null;
            }
        }

        private IEnumerator RoundEnding()
        {
            DisableTankControl();

            string message = EndMessage();
            m_MessageText.text = message;

            yield return m_EndWait;
        }

        private string EndMessage()
        {
            string message = "WIN!!";
            return message;
        }

        private void ResetAllTanks()
        {
            player.Reset();
        }

        private void EnableTankControl()
        {
            player.EnableControl();
        }

        private void DisableTankControl()
        {
            player.DisableControl();
        }
    }
}