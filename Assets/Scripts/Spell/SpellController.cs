using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    CapsuleCollider m_spellCollider;
    Animator m_animator;
    public GameObject SpellPrefab;
    Spell spell;
    public PlayerController m_player;
    public bool m_startSpell;

    void Start()
    {
        m_spellCollider = gameObject.GetComponentInChildren<CapsuleCollider>();
        GameObject spellObject = Instantiate(SpellPrefab, transform);
        spell = spellObject.GetComponent<Spell>();
        m_spellCollider.enabled = false;
        m_startSpell = false;
    }

    void Update()
    {
        
    }

    void Use() {
        if (!spell.isOnCooldown)
        {
            if(m_player.m_manaManager.m_mana > spell.ManaCost){
                m_spellCollider.enabled = true;
            } else {
                Debug.Log("Pas assez de mana...");
            }
        }
        else
        {
            Debug.Log("Compétence en cooldown !");
        }
    }

    void OnTriggerEnter(Collider other) {
        PlayerController otherPlayer = other.gameObject.GetComponent<PlayerController>();
        if(otherPlayer != null && otherPlayer != m_player){
            spell.Use(otherPlayer, m_player);
            m_spellCollider.enabled = false;
        }
    }

    public void Fire(){
        m_startSpell = true;
        m_player.LockPlayer();
        Use();
        m_startSpell = false;
    }
}
