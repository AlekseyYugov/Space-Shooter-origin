using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SpaceShooter
{
    public class MainMenuController : SingletonBase<MainMenuController>
    {
        [SerializeField] private SpaceShip m_DefaultSpaceShip;
        [SerializeField] private GameObject m_EpisodeSelection;
        [SerializeField] private GameObject m_ShipSelection;
        [SerializeField] private GameObject m_Records;

        private void Start()
        {
            LevelSequenceController.PlayerShip = m_DefaultSpaceShip;
        }

        public void OnButtonStartNew()
        {
            m_EpisodeSelection.SetActive(true);

            gameObject.SetActive(false);
        }

        public void OnSelectShip()
        {
            m_ShipSelection.SetActive(true);
            gameObject.SetActive(false);
        }

        public void OnButtonRecords()
        {
            m_Records.SetActive(true);
            gameObject.SetActive(false);
        }
        public void OnButtonMainMenu()
        {
            m_Records.SetActive(false);
            gameObject.SetActive(true);
        }

        public void OnButtonExit()
        {
            Application.Quit();
        }


    }
}

