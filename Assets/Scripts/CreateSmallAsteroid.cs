using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSmallAsteroid : MonoBehaviour
{
    [SerializeField] private GameObject m_SmallAsteroid;
    
    public void AddAsteroid(int count, Vector3 bigAsteroid)
    {
        for (int i = 0; i < count; i++)
        {
            var m_Asteroid = Instantiate(m_SmallAsteroid, bigAsteroid, Quaternion.identity);
        }
    }

}
