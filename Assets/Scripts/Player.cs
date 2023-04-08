using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Player : SingletonBase<Player>
    {

        [SerializeField] private int m_NumLives;
        [SerializeField] private SpaceShip m_Ship;
        [SerializeField] private GameObject m_PlayerShipPrefab;
        public SpaceShip ActiveShip => m_Ship;


        [SerializeField] private CameraController m_CameraController;
        [SerializeField] private MovementController m_MovementController;

        bool freezing = false;
        [SerializeField] private float m_Timer;
        [SerializeField] private float m_LifeTime;

        bool first_death = false;


        protected override void Awake()
        {
            base.Awake();
            if (m_Ship != null)
            {
                Destroy(m_Ship.gameObject);
            }
        }

        private void Start()
        {
            Respawn();
            m_Ship.EventOnDeath.AddListener(OnShipDeath);
        }






        private void OnShipDeath()
        {
            m_NumLives--;

            if (m_NumLives > 0)
            {
                freezing = true;
            }
            else
            {
                LevelSequenceController.Instance.FinishCurrentLevel(false);
            }

        }

        private void Update()
        {
            if (freezing == true)
            {
                m_Timer += Time.deltaTime;
                if (m_Timer > m_LifeTime)
                {
                    freezing = false;
                    m_Timer = 0;
                    Respawn();
                }
            }
        }

        private void Respawn()
        {
            if (LevelSequenceController.PlayerShip != null)
            {
                var newPlayerShip = Instantiate(LevelSequenceController.PlayerShip);
                m_Ship = newPlayerShip.GetComponent<SpaceShip>();


                if (first_death)
                {
                    m_Ship.EventOnDeath.AddListener(OnShipDeath);
                }
                first_death= true;


                m_CameraController.SetTarget(m_Ship.transform);
                m_MovementController.SetTargetShip(m_Ship);
            }



        }

        #region Score

        public int Score { get; private set; }
        public int NumKills { get; private set; }

        public void AddKill()
        {
            NumKills++;
        }
        public void AddScore(int num)
        {
            Score += num;
        }


        #endregion
    }
}

