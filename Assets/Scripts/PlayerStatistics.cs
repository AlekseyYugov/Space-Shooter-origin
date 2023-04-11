using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SpaceShooter
{
    public class PlayerStatistics : MonoBehaviour
    {
        static public int numKills;
        static public int score;
        static public float time;


        static public void Reset()
        {
            numKills = 0;
            score = 0;
            time = 0;
        }
    }
}


