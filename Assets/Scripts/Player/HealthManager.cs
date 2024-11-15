using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    static Color[] COLORS = {new(0.2f, 0.2f, 0.2f, 0.4f), new(0.96f,0.68f,0.15f, 1.0f), new(0.08f,0.7f,0.33f, 1.0f), new(0.1f,0.45f,0.9f, 1.0f)}; 
    public Slider m_slideBar;
    private Image m_bgImage;
    private Image m_fillImage;
    public float m_maxLife;
    public List<float> m_lifes;
    int m_currentBar;
    void Awake()
    {
        m_bgImage = m_slideBar.transform.Find("Background").GetComponent<Image>();
        m_fillImage = m_slideBar.transform.Find("Fill Area/Fill").GetComponent<Image>();
    }

    void Start()
    {
        InitializeLifeBars();
        UpdateLifeBarUI();
        UpdateColors();
    }

    void Update()
    {
        UpdateLifeBarUI();
    }

    void UpdateLifeBarUI(){
        if(isDead())
            return /* Un joueur mort ne peux pas changer sa bar de vie */;
        m_slideBar.value = m_lifes[m_currentBar];
    }

    void InitializeLifeBars(){
        m_lifes = new() { 0.0f, 0.0f, 0.0f };

        float remainingLife = m_maxLife;
        int i;
        for (i=0; i<m_lifes.Count; i++)
        {
            m_lifes[i] = MathF.Min(100.0f, remainingLife);

            if(m_lifes[i] <= 0.0f) break;
            
            remainingLife -= m_lifes[i];
        }

        m_currentBar = i-1;
    }

    public void Dammage(float damage){
        bool needToChangeBarColor = false;
        while (damage > 0.0f && !isDead())
        {
            if (m_lifes[m_currentBar] > damage)
            {
                m_lifes[m_currentBar] -= damage;
                damage = 0.0f;
            }
            else
            {
                damage -= m_lifes[m_currentBar];
                m_lifes[m_currentBar] = 0.0f;
                m_currentBar--;
                needToChangeBarColor = true;
            }
        }

        if(needToChangeBarColor)
            UpdateColors();
    }

    public void Heal(float heal){
        bool needToChangeBarColor = false;
        while (heal > 0.0f && !isDead())
        {
            if (100f - m_lifes[m_currentBar] > heal)
            {
                m_lifes[m_currentBar] += heal;
                heal = 0.0f;
            }
            else
            {
                heal -= 100f - m_lifes[m_currentBar];
                m_lifes[m_currentBar] = 100.0f;
                m_currentBar++;
                needToChangeBarColor = true;
            }
        }

        if(needToChangeBarColor)
            UpdateColors();
    }

    void UpdateColors(){
        if(isDead()){
            m_fillImage.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        } else {
            m_bgImage.color = COLORS[m_currentBar];
            m_fillImage.color = COLORS[m_currentBar + 1];
        }
    }

    bool isDead(){
        return m_currentBar == -1;
    }
}
