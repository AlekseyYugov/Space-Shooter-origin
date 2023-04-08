using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Turret : ToFindObject
    {
        [SerializeField] private TurretMode m_Mode;
        public TurretMode Mode => m_Mode;

        [SerializeField] private TurretProperties m_TurretProperties;

        private float m_RefireTimer;

        public bool CanFire => m_RefireTimer <= 0;

        private SpaceShip m_Ship;

        ToFindObject findObject;

        public static bool m_DropBomb = false;



        #region UnityEvent
        private void Start()
        {
            m_Ship = transform.root.GetComponent<SpaceShip>();
        }
        private void Update()
        {
            if (m_RefireTimer > 0)
            {
                m_RefireTimer -= Time.deltaTime;
            }
            
            
        }
        #endregion

        //Public API
        public void Fire()
        {
            if (m_TurretProperties == null) return;
            if (m_RefireTimer > 0) return;
            if (m_Ship == null)
            {
                return;
            }
            if (m_Ship.DrawEnergy(m_TurretProperties.EnergyUsage)==false)
            {
                return;
            }
            if (m_Ship.DrawAmmo(m_TurretProperties.AmmoUsage) == false)
            {
                return;
            }
            if (m_Ship.DrawBomb(m_TurretProperties.BombUsage) == false)
            {                
                return;
            }


            Projectile projectile = Instantiate(m_TurretProperties.ProjectilePrefab).GetComponent<Projectile>();


            //Vector3 vector = new Vector3(10, 10, 0);


            projectile.transform.position = transform.position;
            projectile.transform.up = transform.up;
            if (FindToRadius()!=null && Input.GetKey(KeyCode.X))
            {
                projectile.transform.up = FindToRadius().transform.position - transform.position;
                
                m_DropBomb = true;

            }
            //if (findObject.FindToRadius() != null)
            //{
            //    Debug.Log("!");
            //}

            //if (findObject.FindToRadius() != null)
            //{
            //    Debug.Log("!");
            //    projectile.transform.up = findObject.FindToRadius().normalized;
            //}





            //изменить направление снаряда через Vector2 и прировнять к transform.up, так мы получим самонаводящие снаряды
            //самонаводящие снаряды должны работать по радиусу
            //projectile.transform.up = transform.up;



            projectile.SetParentShooter(m_Ship);

            m_RefireTimer = m_TurretProperties.RateOffFire;

            {
                //SFX Audio
            }

        }
        public void AssingLoadout(TurretProperties props)
        {
            if (m_Mode != props.Mode)
            {
                return;
            }
            m_RefireTimer = 0;
            m_TurretProperties= props;
        }
    }
}

