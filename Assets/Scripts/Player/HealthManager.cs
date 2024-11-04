using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Slider m_slideBar;
    public float m_maxLife;
    public float m_lifePercentage;
    void Start()
    {
        m_lifePercentage = 1f;
        m_slideBar.value = m_lifePercentage; 
    }

    // Update is called once per frame
    void Update()
    {
        m_lifePercentage -= 0.001f;
        m_slideBar.value = m_lifePercentage; 

    }
}
