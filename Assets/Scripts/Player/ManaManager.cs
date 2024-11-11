using System;
using UnityEngine;
using UnityEngine.UI;

public class ManaManager : MonoBehaviour
{
    public float m_maxMana = 50.0f;
    public float m_mana;

    public float m_regenPerSecond;

    public GameObject m_sliderObject;

    RectTransform m_rectTransform;
    Slider m_slideBar;
    void Awake() {
        m_slideBar = m_sliderObject.GetComponent<Slider>();
        m_rectTransform = m_sliderObject.GetComponent<RectTransform>();
    }

    void Start()
    {
        m_rectTransform.sizeDelta = new Vector2(m_maxMana * 2f, m_rectTransform.sizeDelta.y);
        m_rectTransform.anchoredPosition3D = new Vector3(m_rectTransform.sizeDelta.x * m_rectTransform.anchoredPosition3D.x * 0.5f + m_rectTransform.anchoredPosition3D.x * 3f , m_rectTransform.anchoredPosition3D.y, m_rectTransform.anchoredPosition3D.z);

        m_mana = 20;
        m_slideBar.maxValue = m_maxMana;
    }

    // Update is called once per frame
    void Update()
    {
        m_slideBar.value = m_mana;
        ApplyRegeneration(m_regenPerSecond * Time.deltaTime);
    }

    public void ApplyRegeneration(float value) {
        m_mana = MathF.Min(m_maxMana, m_mana + value);
    }

    public bool Use(float value) {
        if(value > m_mana)
            return false;
        m_mana -= value;
        return true;
    }
}
