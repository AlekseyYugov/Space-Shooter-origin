using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpeed : Powerup
{
    [SerializeField] private BonusSpaceShip m_Bonus;
    protected override void OnPickedUp(SpaceShip ship)
    {
        ship.SpeedUp(m_Bonus.Thrust);
    }
    
}
