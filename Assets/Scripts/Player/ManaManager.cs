using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaManager : MonoBehaviour
{
    public float m_maxMana = 10;
    public float m_mana;
    public Slider m_slideBar;
    void Start()
    {
        m_mana = m_maxMana;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
