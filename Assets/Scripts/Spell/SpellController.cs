using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpellController : MonoBehaviour
{
    CapsuleCollider m_spellCollider;
    Animator m_animator;
    public List<GameObject> SpellsPrefab;
    public List<Spell> m_spells;
    public PlayerController m_player;
    public bool m_startSpell;

    public int current = 0;
    public int len = 3;
    public int m_currentSpell = 0;

    public GameObject m_directions;

    public List<int> series;
    int spellId = 0;

    int[] m_rotations = {90, 0, 270, 180};

    public GameObject m_offence;

    void Start()
    {
        m_spellCollider = gameObject.GetComponentInChildren<CapsuleCollider>();
        m_spells = new List<Spell>();
        foreach(GameObject prefab in SpellsPrefab){
            GameObject spellObject = Instantiate(prefab, transform);
            Spell spell = spellObject.GetComponent<Spell>();
            if (spell != null) 
                m_spells.Add(spell);
        }
        m_animator = transform.root.GetComponent<Animator>();


        m_spellCollider.enabled = false;
        m_startSpell = false;
        m_directions.SetActive(false);
        m_offence.SetActive(false);

    }

    void Update()
    {
        if(!m_player.invokeSpell) return;


        if(!m_player.takingDmg){
            if(current >= len) {
                m_directions.SetActive(false);

                if(m_spells[m_currentSpell].isOffense){
                    m_offence.SetActive(true);
                }
                m_player.invokeSpell = false;
                m_animator.SetTrigger("Spell");

                m_spells[m_currentSpell].Use(m_player, 1f);
                m_spells[m_currentSpell].ApplyEffectOnPlayer(m_player);

                if(m_spells[m_currentSpell].isOffense){
                    m_spellCollider.enabled = true;
                }

                StartCoroutine(EndSpellCd(spellId));
                return;
            }
            m_directions.transform.rotation = Quaternion.Euler(0, 0, m_rotations[series[current]]);

            if(m_player.lastInput == -1) return;
            if(m_player.lastInput == series[current]){current++;} 
            else {

                m_player.canAct = true;
                m_spells[m_currentSpell].Use(m_player, (float) current/ len);

                m_directions.SetActive(false);
                m_player.invokeSpell = false;

            };
            m_player.lastInput = -1;
            return;
        }
        m_player.canAct = true;
        m_spells[m_currentSpell].Use(m_player, (float) current/ len);

        m_directions.SetActive(false);
        m_player.invokeSpell = false;
    }

    void Use() {
        if (!m_spells[m_currentSpell].isOnCooldown)
        {
            if(m_player.m_manaManager.m_mana > m_spells[m_currentSpell].ManaCost){
                spellId++;
                m_player.canAct = false;
                m_animator.SetFloat("Speed", 0f);

                InputValidate();

            } else {
                //Debug.Log("Pas assez de mana...");
            }
        }
        else
        {
            //Debug.Log("Compétence en cooldown !");
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
        m_offence.SetActive(false);

        m_player.canAct = true;
    }

    void InputValidate() {
        System.Random random = new System.Random();
        series = new List<int>();
        len = m_spells[m_currentSpell].Complexity;
        if(m_player.torment)
            len++;
        for (int i = 0; i < len; i++)
        {
            series.Add(random.Next(0, 4));
        }
        current = 0;

        m_directions.SetActive(true);
        m_player.invokeSpell = true;
        m_player.lastInput = -1;
    }
    

    void OnTriggerEnter(Collider other) {
        PlayerController otherPlayer = other.gameObject.GetComponent<PlayerController>();
        if(otherPlayer != null && otherPlayer != m_player){
            if(!otherPlayer.isProtected){
                m_spells[m_currentSpell].ApplyEffectOnEnemy(otherPlayer, m_player);

                otherPlayer.takingDmg = true;
                otherPlayer.m_animator.SetTrigger("Dammage");
            
                StartCoroutine(UnDamage(otherPlayer));
            } else {
                otherPlayer.isProtected = false;
            }
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
