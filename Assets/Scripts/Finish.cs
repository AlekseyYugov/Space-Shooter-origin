using SpaceShooter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject m_FinishEffect_01;
    [SerializeField] private GameObject m_FinishEffect_02;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_FinishEffect_01.SetActive(true);
            m_FinishEffect_02.SetActive(true);

            ResultPanelController.Instance.ShowResult(true);
        }
    }
}
