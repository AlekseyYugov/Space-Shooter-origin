using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace SpaceShooter
{
    public class EpisodeSelectionController : MonoBehaviour
    {
        [SerializeField] private Episode m_Episode;

        [SerializeField] private Text m_EpisodeNickname;
        [SerializeField] private Image m_PerviewImage;

        private void Start()
        {
            if (m_Episode != null)
            {
                m_EpisodeNickname.text = m_Episode.EpisodeName;
            }
            if (m_PerviewImage != null)
            {
                m_PerviewImage.sprite = m_Episode.PreviewImage;
            }
        }

        public void OnStartEpisodeButtonClicked()
        {
            LevelSequenceController.Instance.StartEpisode(m_Episode);
        }



    }
}

