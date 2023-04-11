using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace SpaceShooter
{
    public class Records : MonoBehaviour
    {
        [SerializeField] private Episode m_Episode_A;
        [SerializeField] private Episode m_Episode_B;
        [SerializeField] private Episode m_Episode_C;

        [SerializeField] private Text m_NumKills_A;
        static public int numKillsA;
        [SerializeField] private Text m_Score_A;
        static public int scoreA;
        [SerializeField] private Text m_Time_A;
        static public float timeA;

        [SerializeField] private Text m_NumKills_B;
        static public int numKillsB;
        [SerializeField] private Text m_Score_B;
        static public int scoreB;
        [SerializeField] private Text m_Time_B;
        static public float timeB;

        [SerializeField] private Text m_NumKills_C;
        static public int numKillsC;
        [SerializeField] private Text m_Score_C;
        static public int scoreC;
        [SerializeField] private Text m_Time_C;
        static public float timeC;

        private void Start()
        {
            m_NumKills_A.text = "numkills: " + numKillsA.ToString();
            m_Score_A.text = "score: " + scoreA.ToString();
            m_Time_A.text = "time: " + timeA.ToString();

            m_NumKills_B.text = "numkills: " + numKillsB.ToString();
            m_Score_B.text = "score: " + scoreB.ToString();
            m_Time_B.text = "time: " + timeB.ToString();

            m_NumKills_C.text = "numkills: " + numKillsC.ToString();
            m_Score_C.text = "score: " + scoreC.ToString();
            m_Time_C.text = "time: " + timeC.ToString();
        }

        static public void FormRecords(Episode episode, int kills, int score, float time)
        {
            if (episode.name == "Episode_A")
            {
                if (kills >= numKillsA && score >= scoreA)
                {
                    numKillsA = kills;
                    scoreA = score;
                    timeA = time;
                }

            }
            if (episode.name == "Episode_B")
            {
                if (kills >= numKillsB && score >= scoreB)
                {
                    numKillsB = kills;
                    scoreB = score;
                    timeB = time;
                }


            }
            if (episode.name == "Episode_C")
            {
                if (kills >= numKillsC && score >= scoreC)
                {
                    numKillsC = kills;
                    scoreC = score;
                    timeC = time;
                }

            }
        }
    }
}

