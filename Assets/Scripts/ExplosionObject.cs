using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ExplosionObject : MonoBehaviour
{
    [SerializeField] private GameObject m_explosion;

    [SerializeField] private float m_Timer;
    [SerializeField] private float m_LifeTime;

    public void Ex()
    {
        var ex = Instantiate(m_explosion, transform.position, Quaternion.identity);
        if (ex != null)
        {
            if (m_Timer > m_LifeTime)
            {
                Destroy(ex);
                m_Timer = 0;
            }
        }
    }
    private void Update()
    {
        m_Timer += Time.deltaTime;
    }



}
