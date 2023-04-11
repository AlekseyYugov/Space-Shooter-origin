using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Projectile : MonoBehaviour
    {
        public float m_Velocity;
        [SerializeField] private float m_Lifetime;
        [SerializeField] private int m_Damage;
        [SerializeField] private ImpactEffect m_ImpactEffectPrefab;

        [SerializeField] private float m_Timer;

        [SerializeField] private GameObject m_SmallAsteroid;
        [SerializeField] private GameObject m_BigAsteroid;



        private void Update()
        {
            float stepLenght = Time.deltaTime * m_Velocity;

            Vector2 step = transform.up * stepLenght;


            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLenght);

            if (hit)
            {
                Destructible dest = hit.collider.transform.root.GetComponent<Destructible>();

                if (dest != null && dest != m_Parent)
                {
                    dest.ApplyDamage(m_Damage);
                    if (m_Parent == Player.Instance.ActiveShip || m_Parent == null)
                    {
                        Player.Instance.AddScore(dest.ScoreValue);
                        if (dest.tag == "Enemy" && dest.name != "SmallAsteroid(Clone)" && dest.name != "Asteroid(Clone)")
                        {
                            Debug.Log(dest.name);
                            if (dest.HitPoints == 0)
                            {
                                Player.AddKill(1);
                                Debug.Log("!");
                            }
                            

                        }
                    }
                }
                OnProjectileLifeEnd(hit.collider, hit.point);
            }


            m_Timer += Time.deltaTime;
            if (m_Timer > m_Lifetime)
            {
                Destroy(gameObject);
            }

            transform.position += new Vector3(step.x, step.y, 0);
        }
        private void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
        {
            Destroy(gameObject);
        }
        private Destructible m_Parent;
        public void SetParentShooter(Destructible parent)
        {
            m_Parent = parent;
        }
    }
}


