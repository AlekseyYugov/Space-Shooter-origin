using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    /// <summary>
    /// Уничтожаемый объкт на сцене. То что может иметь хитпоинты.
    /// </summary>
    public class Destructible : Entity
    {
        [SerializeField] private bool m_IsBigAsteroid;
        [SerializeField] private GameObject m_SmallAsteroid;




        [SerializeField] private GameObject m_EmpactEffect;


        #region Properties
        /// <summary>
        /// Объект игнорирует повреждения.
        /// </summary>
        public bool m_Indestructible;

        public BonusSpaceShip m_Bonus;





        /// <summary>
        /// Стартовое кол--во хитпоинтов.
        /// </summary>
        [SerializeField] private int m_HitPoints;

        /// <summary>
        /// Текущие хитпоинты.
        /// </summary>
        private int m_CurrentHitPoints;
        public int HitPoints => m_CurrentHitPoints;

        #endregion



        #region Unity Events
        protected virtual void Start()
        {
            m_CurrentHitPoints = m_HitPoints;
        }
        #endregion

        #region Public API

        /// <summary>
        /// Применение дамага к объекту
        /// </summary>
        /// <param name="damage">Урон наносимый объекту</param>
        public void ApplyDamage(int damage)
        {
            if (m_Bonus != null)
            {
                m_Indestructible = m_Bonus.Indestructible;
            }

            if (m_Indestructible) return;

            m_CurrentHitPoints -= damage;

            if (m_CurrentHitPoints <= 0) OnDeath();
        }
        #endregion

        [SerializeField] private GameObject m_bomb;

        /// <summary>
        /// Переопределяемое событие уничтожения объекта, когда хитпоинты ниже или равны нулю.
        /// </summary>
        /// 




        protected virtual void OnDeath()
        {

            var ex = Instantiate(m_EmpactEffect, transform.position, Quaternion.identity);
            var asteroid = gameObject.transform.position;

            if (Turret.m_DropBomb == true)
            {
                for (int i = 0; i < 72; i++)
                {
                    var bomb = Instantiate(m_bomb, transform.position, Quaternion.Euler(0, 0, 15.0f * i));
                    Destroy(bomb, 1);
                }


                Turret.m_DropBomb = false;
            }
            Destroy(gameObject);
            if (m_IsBigAsteroid)
            {
                var count = Random.Range(2, 5);
                CreateSmallAsteroid(count);


                m_IsBigAsteroid = false;
            }
            Destroy(ex, 1);

            m_EventOnDeath?.Invoke();
        }

        private static HashSet<Destructible> m_AllDestractible;
        public static IReadOnlyCollection<Destructible> AllDestractible => m_AllDestractible;

        protected virtual void OnEnable()
        {
            if (m_AllDestractible == null)
            {
                m_AllDestractible = new HashSet<Destructible>();
            }
            m_AllDestractible.Add(this);
        }
        protected virtual void OnDestroy()
        {
            m_AllDestractible?.Remove(this);
        }

        public const int TeamIdNeutral = 0;

        [SerializeField] private int m_TeamId;
        public int TeamId => m_TeamId;


        private void CreateSmallAsteroid(int count)
        {
            for (int i = 0; i < count; i++)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
                var meteor = Instantiate(m_SmallAsteroid, transform.position, Quaternion.identity);
                if (meteor != null)
                {
                    Rigidbody2D rb = meteor.GetComponent<Rigidbody2D>();
                    rb.velocity = (Vector2)UnityEngine.Random.insideUnitSphere * 8; //Random.Range(3, 4);
                }
            }
        }
        [SerializeField] private UnityEvent m_EventOnDeath;
        public UnityEvent EventOnDeath => m_EventOnDeath;


        #region Score

        [SerializeField] private int m_ScoreValue;

        public int ScoreValue => m_ScoreValue;


        #endregion
    }

}

