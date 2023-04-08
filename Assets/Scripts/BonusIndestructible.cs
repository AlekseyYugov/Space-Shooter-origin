using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusIndestructible : Powerup
{
    [SerializeField] private BonusSpaceShip m_Bonus;
    protected override void OnPickedUp(SpaceShip ship)
    {
        ship.Invulnerability(m_Bonus.Indestructible);
    }
}
