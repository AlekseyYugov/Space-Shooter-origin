using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    /// <summary>
    /// Огроничитель позиции. Работает в связке со скриптом LevelBoundary если таковой имеется на сцене.
    /// Кидается на объект который надо огроничить.
    /// </summary>
    public class LevelBoundaryLimiter : MonoBehaviour
    {
        private void Update()
        {
            if (LevelBoundary.Instance == null) return;
            
            var lb = LevelBoundary.Instance;
            var r = lb.Radius;

            if(transform.position.magnitude > r)
            {
                if(lb.LimitMode == LevelBoundary.Mode.Limit)
                {
                    transform.position = transform.position.normalized* r;
                }
                if (lb.LimitMode == LevelBoundary.Mode.Teleport)
                {
                    transform.position = -transform.position.normalized* r;
                }
            }

            
        }
    }
}

