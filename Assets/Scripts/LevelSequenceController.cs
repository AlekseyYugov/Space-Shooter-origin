using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace SpaceShooter
{
    public class LevelSequenceController : SingletonBase<LevelSequenceController>
    {
        public static string MainMenuSceneNickname = "main_menu";

        public Episode CurrentEpisode { get; private set; }
        public int CurrentLevel { get; private set; }


        public static SpaceShip PlayerShip { get; set; }


        public void StartEpisode(Episode e)
        {
            CurrentEpisode = e;
            CurrentLevel = 0;
            //сбрасываем статы перед началом эпизода
            SceneManager.LoadScene(e.Levels[CurrentLevel]);
        }

        public void RestertLEvel()
        {
            SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
        }
        public void FinishCurrentLevel(bool success)
        {
            if (success == true)
            {
                AdvanceLevel();
            }
        }

        public void AdvanceLevel()
        {
            CurrentLevel++;
            if (CurrentEpisode.Levels.Length <= CurrentLevel)
            {
                SceneManager.LoadScene(MainMenuSceneNickname);
            }
            else
            {
                SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
            }
        }
    }
}

