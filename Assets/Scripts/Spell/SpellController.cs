using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpellController : MonoBehaviour
{
    CapsuleCollider m_spellCollider;
    Animator m_animator;
    public GameObject SpellPrefab;
    Spell spell;
    public PlayerController m_player;
    public bool m_startSpell;

    public int current = 0;
    public int len = 3;

    public List<int> series;
    int spellId = 0;

    void Start()
    {
        m_spellCollider = gameObject.GetComponentInChildren<CapsuleCollider>();
        GameObject spellObject = Instantiate(SpellPrefab, transform);
        spell = spellObject.GetComponent<Spell>();
        m_animator = transform.root.GetComponent<Animator>();

        m_spellCollider.enabled = false;
        m_startSpell = false;
    }

    void Update()
    {
        if(!m_player.invokeSpell) return;


        if(!m_player.takingDmg){
            if(current >= len) {
                m_player.invokeSpell = false;
                m_animator.SetTrigger("Spell");

                spell.Use(m_player);

                m_spellCollider.enabled = true;

                StartCoroutine(EndSpellCd(spellId));
                return;
            }

            if(m_player.lastInput == -1) return;
            if(m_player.lastInput == series[current]){current++; Debug.Log("target" + series[current]);} 
            else current = 0;
            m_player.lastInput = -1;
            return;
        }
        m_player.invokeSpell = false;
    }

    void Use() {
        if (!spell.isOnCooldown)
        {
            if(m_player.m_manaManager.m_mana > spell.ManaCost){
                spellId++;
                m_player.canAct = false;
                m_animator.SetFloat("Speed", 0f);

                InputValidate();

            } else {
                Debug.Log("Pas assez de mana...");
            }
        }
        else
        {
            Debug.Log("Compétence en cooldown !");
        }
    }

    private IEnumerator EndSpellCd(int id)
    {
        yield return new WaitForSeconds(1f);

        if (id == spellId)
        {
            EndSpell();
        }
    }

    void EndSpell(){
        m_spellCollider.enabled = false;
        m_player.canAct = true;
    }

    void InputValidate() {
        System.Random random = new System.Random();
        series = new List<int>();
        for (int i = 0; i < len; i++)
        {
            series.Add(random.Next(0, 4));
        }
        current = 0;

        Debug.Log("target " + series[current]);

        m_player.invokeSpell = true;
        m_player.lastInput = -1;
    }
    

    void OnTriggerEnter(Collider other) {
        PlayerController otherPlayer = other.gameObject.GetComponent<PlayerController>();
        if(otherPlayer != null && otherPlayer != m_player){
            spell.ApplyEffectOnEnemy(otherPlayer);
            spell.ApplyEffectOnPlayer(m_player);

            otherPlayer.takingDmg = true;
            otherPlayer.m_animator.SetTrigger("Dammage");
            
            StartCoroutine(UnDamage(otherPlayer));

            m_spellCollider.enabled = false;
        }
    }

    private IEnumerator UnDamage(PlayerController enemy)
    {
        yield return new WaitForSeconds(1f);
        enemy.takingDmg = false;
    }

    public void Fire(){
        m_startSpell = true;
        Use();
        m_startSpell = false;
    }
}
