using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace SpaceShooter
{
    public interface ILevelCondition
    {
        bool IsCompleted { get; }
    }
    public class LevelController : MonoBehaviour
    {
        

        [SerializeField] private int m_ReferenceTime;

        public int ReferenceTime => m_ReferenceTime;

        [SerializeField] private UnityEvent m_EventLevelCompleted;

        private ILevelCondition[] m_Conditions;

        private bool m_IsLevelCOmpleted;
        private float m_LevelTime;
        public float LevelTime => m_LevelTime;

        private void Start()
        {
            m_Conditions = GetComponentsInChildren<ILevelCondition>();
        }
        private void Update()
        {
            if (m_IsLevelCOmpleted == false)
            {
                m_LevelTime += Time.deltaTime;

                CheckLevelConditions();
            }
        }

        private void CheckLevelConditions()
        {
            if (m_Conditions == null || m_Conditions.Length == 0)
            {
                return;
            }
            int numCompleted = 0;

            foreach (var item in m_Conditions)
            {
                if (item.IsCompleted)
                {
                    numCompleted++;
                }
            }
            if (numCompleted == m_Conditions.Length)
            {
                m_IsLevelCOmpleted = true;
                m_EventLevelCompleted?.Invoke();

                LevelSequenceController.Instance?.FinishCurrentLevel(true);
            }
        }


    }
}

