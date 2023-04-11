using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



namespace SpaceShooter
{
    public class ResultPanelController : SingletonBase<ResultPanelController>
    {
        [SerializeField] private Text m_Kills;
        [SerializeField] private Text m_Score;
        [SerializeField] private Text m_Time;

        [SerializeField] private Text m_Result;
        [SerializeField] private Text m_ButtonNextText;

        static public bool m_Success = false;

        private void Start()
        {
            gameObject.SetActive(false);
        }

        //private void Update()
        //{
        //    if (m_Success == true) 
        //    {
        //        gameObject.SetActive(true);
        //        ShowResult(m_Success);
        //    }
        //}

        public void ShowResult(bool success)
        {
            gameObject.SetActive(true);

            //LevelSequenceController.Instance.CalculateLevellStatistic();

            m_Success = success;

            m_Result.text = success ? "Win" : "Lose";
            m_ButtonNextText.text = success ? "Next" : "Restart";

            Time.timeScale = 0;

            m_Kills.text = "Kills : " + Player.NumKills.ToString();
            m_Score.text = "Score : " + ScoreStats.m_LastScore.ToString();
            m_Time.text = "Time : " + PlayerStatistics.time.ToString();

            

        }
        public void OnButtonNextAction()
        {
            gameObject.SetActive(false) ;

            Time.timeScale = 1;
            if (m_Success)
            {
                LevelSequenceController.Instance.AdvanceLevel();
            }
            else
            {
                LevelSequenceController.Instance.RestartLevel();
                //Если уровень проигран-----------------------------------------------------
                //LevelSequenceController.Instance.FinishCurrentLevel(m_Success);
                //gameObject.SetActive(true) ;
                //LevelSequenceController.Instance.FinishCurrentLevel(m_Success);
            }
        }
    }
}

