using SpaceShooter;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ToFindObject : MonoBehaviour
{
    GameObject[] enemy;
    GameObject closest;
    public string nearest;
    float m_FindEnemyPositionX;
    float m_FindEnemyPositionY;

    public GameObject FindClosestEnemy()
    {
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in enemy)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }

        return closest;
    }
    public GameObject FindToRadius()
    {
        float m_SizeArea = 10;
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        float m_FindEnemyPositionX = FindClosestEnemy().transform.position.x;
        float m_FindEnemyPositionY = FindClosestEnemy().transform.position.y;


        if (transform.position.x + m_SizeArea > m_FindEnemyPositionX && transform.position.x - m_SizeArea < m_FindEnemyPositionX &&
            transform.position.y + m_SizeArea > m_FindEnemyPositionY && transform.position.y - m_SizeArea < m_FindEnemyPositionY)
        {
            nearest = FindClosestEnemy().name;
            return FindClosestEnemy();
        }
        else
        {
            nearest = null;
            return null;
        }


    }
    private void Update()
    {
        if (enemy != null) { FindToRadius(); }    

    }

}
