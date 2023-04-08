using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
    {
        /// <summary>
        /// ����� ��� �������������� ��������� � ������.
        /// </summary>
        [Header("Space ship")]
        [SerializeField] private float m_Mass;

        /// <summary>
        /// ��������� ������ ����.
        /// </summary>
        public float m_Thrust;

        /// <summary>
        /// ��������� ����.
        /// </summary>
        public float m_Mobility;

        /// <summary>
        /// ������������ �������� ��������.
        /// </summary>
        [SerializeField] private float m_MaxLinearVelocity;
        public float MaxLinearVelocity => m_MaxLinearVelocity;

        /// <summary>
        /// ������������ ������������ ��������. � ��������/���.
        /// </summary>
        [SerializeField] private float m_MaxAngularVelocity;
        public float MaxAngularVelocity => m_MaxAngularVelocity;

        /// <summary>
        /// ����������� ������ �� �����.
        /// </summary>
        private Rigidbody2D m_Rigid;

        #region Public API

        /// <summary>
        /// ���������� �������� �����. -1.0 �� +1.0
        /// </summary>
        public float ThrustControl { get; set; }

        /// <summary>
        /// ���������� ������������ �����. -1.0 �� +1.0
        /// </summary>
        public float TorqueControl { get; set; }

        private float m_StandartSpeed;


        [SerializeField] private Sprite m_PreviewImage;
        public Sprite PreviewImage => m_PreviewImage;


        #endregion
        #region Unity Event

        protected override void Start()
        {
            base.Start();

            m_Rigid = GetComponent<Rigidbody2D>();
            m_Rigid.mass = m_Mass;

            m_Rigid.inertia = 1;


            m_StandartSpeed = m_Thrust;

            m_Trace_01.SetActive(false);
            m_Trace_02.SetActive(false);
            m_ShieldEnergy.SetActive(false);


            InitOffensive();
        }

        private void FixedUpdate()
        {
            UpdateRigidBody();
            UpdateEnergyRegen();
        }
        #endregion

        /// <summary>
        /// ����� ����������� ��� ������� ��� ��������
        /// </summary>
        /// 

        [SerializeField] private float m_Time = 0;
        [SerializeField] private GameObject m_Trace_01;
        [SerializeField] private GameObject m_Trace_02;
        [SerializeField] private GameObject m_ShieldEnergy;


        private bool invulBonus = false;
        [SerializeField] private float m_TimeBonus;


        public void SpeedUp(float bonusSpeed)
        {
            m_Time = 0;

            m_Thrust *= bonusSpeed;

            if (bonusSpeed > 1)
            {
                m_Trace_01.SetActive(true);
                m_Trace_02.SetActive(true);
            }

        }
        public void Invulnerability(bool invul)
        {
            invulBonus = true;
            m_Time = 0;

            m_Indestructible = true;
            m_ShieldEnergy.SetActive(true);

        }

        private void Update()
        {
            if (invulBonus == true)
            {
                m_Indestructible = true;
            }

            if (m_Time > m_TimeBonus)
            {
                m_Thrust = m_StandartSpeed;
                m_Trace_01.SetActive(false);
                m_Trace_02.SetActive(false);
                m_Indestructible = false;
                invulBonus = false;
                m_ShieldEnergy.SetActive(false);
            }
            m_Time += Time.deltaTime;
        }

        private void UpdateRigidBody()
        {
            m_Rigid.AddForce(m_Thrust * ThrustControl * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);



            m_Rigid.AddForce(-m_Rigid.velocity * (m_Thrust / m_MaxLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddTorque(TorqueControl * m_Mobility * Time.fixedDeltaTime, ForceMode2D.Force);

            m_Rigid.AddTorque(-m_Rigid.angularVelocity * (m_Mobility / m_MaxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
        }


        [SerializeField] private Turret[] m_Turrets;

        public void Fire(TurretMode mode)
        {
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                if (m_Turrets[i].Mode == mode)
                {
                    m_Turrets[i].Fire();
                }
            }
        }


        [SerializeField] private int m_MaxEnergy;
        [SerializeField] private int m_MaxAmmo;
        [SerializeField] private int m_MaxBomb;
        [SerializeField] private int m_EnergyRegenPerSecond;

        private float m_PrimaryEnergy;
        private int m_SecondaryAmmo;
        private int m_Bomb;

        public void AddEnergy(int e)
        {
            m_PrimaryEnergy += e;
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy + e, 0, m_MaxEnergy);
        }
        public void AddAmmo(int ammo)
        {
            m_SecondaryAmmo = Mathf.Clamp(m_SecondaryAmmo + ammo, 0, m_MaxAmmo);
        }
        public void AddBomb(int bomb)
        {
            m_Bomb = Mathf.Clamp(m_Bomb + bomb, 0, m_MaxBomb);
        }

        private void InitOffensive()
        {
            m_PrimaryEnergy = m_MaxEnergy;
            m_SecondaryAmmo = m_MaxAmmo;
            m_Bomb = m_MaxBomb;
        }

        private void UpdateEnergyRegen()
        {
            m_PrimaryEnergy += (float)m_EnergyRegenPerSecond * Time.fixedDeltaTime;
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy, 0, m_MaxEnergy);
        }
        public bool DrawEnergy(int count)
        {
            if (count == 0)
            {
                return true;
            }
            if (m_PrimaryEnergy >= count)
            {
                m_PrimaryEnergy -= count;
                return true;
            }
            return false;
        }
        public bool DrawAmmo(int count)
        {
            if (count == 0)
            {
                return true;
            }
            if (m_SecondaryAmmo >= count)
            {
                m_SecondaryAmmo -= count;
                return true;
            }
            return false;
        }
        public bool DrawBomb(int count)
        {
            if (count == 0)
            {
                return true;
            }
            if (m_Bomb >= count)
            {
                m_Bomb -= count;
                return true;
            }
            return false;
        }


        public void AssignWeapon(TurretProperties props)
        {
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                m_Turrets[i].AssingLoadout(props);
            }
        }


    }
}

