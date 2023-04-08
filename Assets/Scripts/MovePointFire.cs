using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePointFire : MonoBehaviour
{
    static Vector3 vector1;
    static Vector3 vector2;
    static Vector3 vector3 = vector1;
    private static float m_PointFire = 10f;

    public static Vector3 MakeLead(Vector3 SelectedTarget)
    {     
        if (vector3 == vector1)
        {
            vector1 = SelectedTarget;
            return SelectedTarget;            
        }
        else
        {
            vector2 = SelectedTarget;
        }
        if (vector2.x != vector1.x) 
        { 
            vector3.x = vector2.x + (vector2.x - vector1.x) * m_PointFire;
        }
        if (vector2.y != vector1.y)
        {
            vector3.y = vector2.y + (vector2.y - vector1.y) * m_PointFire;
        }

        vector1 = vector2;

        return vector3;

    }
    
}
