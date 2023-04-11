using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace SpaceShooter
{
    public class LevelSequenceController : SingletonBase<LevelSequenceController>
    {
        public static string MainMenuSceneNickname = "main_menu";

        public Episode CurrentEpisode { get; private set; }
        public int CurrentLevel { get; private set; }


        public bool LastLevelResult { get; private set; }

        public static SpaceShip PlayerShip { get; set; }


        private Episode nextEpisode;
        static public Episode currentEpisode;

        [SerializeField] private Episode episode_A;
        [SerializeField] private Episode episode_B;
        [SerializeField] private Episode episode_C;


        public void StartEpisode(Episode e)
        {
            CurrentEpisode = e;
            CurrentLevel = 0;
            //сбрасываем статы перед началом эпизода
            PlayerStatistics.Reset();

            SceneManager.LoadScene(e.Levels[CurrentLevel]);

            Player.NumKills= 0;
            currentEpisode= e;
            nextEpisode = e;
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
            PlayerStatistics.Reset();
            LevelController.m_IsLevelCompleted= false;
            Player.NumKills = 0;
        }
        public void FinishCurrentLevel(bool success)
        {
            LevelController.m_IsLevelCompleted = true;

            LastLevelResult= success;
            CalculateLevellStatistic();

            ResultPanelController.Instance.ShowResult(success);

        }

        public void AdvanceLevel()
        {
            //LevelStatistics.Reset();

            //CurrentLevel++;
            //if (CurrentEpisode.Levels.Length <= CurrentLevel)
            //{
            //    SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
            //}
            //else
            //{
            //    SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
            //}
            if (PlayerStatistics.time <= 30)
            {
                ScoreStats.m_LastScore *= 2;
            }
            if (nextEpisode == episode_A)
            {
                currentEpisode = episode_A;
                nextEpisode = episode_B;                
                Records.FormRecords(LevelSequenceController.currentEpisode, Player.NumKills, ScoreStats.m_LastScore, PlayerStatistics.time);
                SceneManager.LoadScene(episode_B.Levels[CurrentLevel]);
                
            }
            else if (nextEpisode == episode_B)
            {
                currentEpisode = episode_B;
                nextEpisode = episode_C;
                Records.FormRecords(LevelSequenceController.currentEpisode, Player.NumKills, ScoreStats.m_LastScore, PlayerStatistics.time);
                SceneManager.LoadScene(episode_C.Levels[CurrentLevel]);
                
            }
            else if (nextEpisode == episode_C)
            {
                currentEpisode = episode_C;
                nextEpisode = episode_A;
                Records.FormRecords(LevelSequenceController.currentEpisode, Player.NumKills, ScoreStats.m_LastScore, PlayerStatistics.time);
                SceneManager.LoadScene(MainMenuSceneNickname);
                
            }

            Player.NumKills = 0;
            PlayerStatistics.time= 0;
        }
        public void CalculateLevellStatistic()
        {

            PlayerStatistics.score = Player.Instance.Score;
            PlayerStatistics.numKills = Player.NumKills;
            

        }
    }
}

