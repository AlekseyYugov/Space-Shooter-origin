using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace SpaceShooter
{
    public class ScoreStats : MonoBehaviour
    {
        [SerializeField] private Text m_Text;
        static public int m_LastScore;

        private void Update()
        {
            UpdateScore();
        }

        private void UpdateScore()
        {
            int currentScore = Player.Instance.Score;

            if (m_LastScore != currentScore)
            {
                m_LastScore = currentScore;

                m_Text.text = "Score: " + m_LastScore.ToString();
            }
        }
    }
}

