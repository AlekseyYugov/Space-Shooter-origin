using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu]
public class BonusSpaceShip : ScriptableObject
{
    [SerializeField] private float m_Thrust;
    public float Thrust => m_Thrust;

    [SerializeField] private bool m_Indestructible;
    public bool Indestructible => m_Indestructible;
}
