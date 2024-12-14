using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Animator m_animator;
    Rigidbody m_rigidBody;
    float m_direction;
    float m_speed;
    public Transform centerPoint;

    public PlayerController m_other;

    public HealthManager m_health;
    public ManaManager m_manaManager;

    public SpellController m_spellController;

    public bool torment = false;

    float m_minDistance = 1.5f;
    public bool canAct = true;
    public bool takingDmg = false;
    public bool invokeSpell = false;

    public int lastInput = -1;

    public bool spellingMode = false;

    public bool isProtected = false;

    public GameObject stunLogo;
    AuraEffect f;

    void Start()
    {
        m_health = GetComponent<HealthManager>();
        m_animator = GetComponent<Animator>();
        m_rigidBody = FindObjectOfType<Rigidbody>();
        f = GetComponentInChildren<AuraEffect>();


        m_spellController = GetComponentInChildren<SpellController>();
        m_spellController.m_player = this;

        m_direction = 0f;
        m_speed = 40f;
    }

    void Update()
    {
        stunLogo.SetActive(torment);
        f.isAuraActive = isProtected;

        //m_health.Dammage(0.01f);
        if(isProtected){
            if(m_manaManager.m_mana > 5.0f)
                m_manaManager.m_mana -= 5.0f * Time.deltaTime;
            else {
                m_manaManager.m_mana = 0.0f;
                isProtected = false;
            }
        }
        if(takingDmg) return;
        transform.LookAt(m_other.transform.position);

        if(!canAct) return;
        MoveAround();        
    }

    void OnMove(InputValue inputValue){
        float value = inputValue.Get<Vector2>().x;

        if(spellingMode){
            if(((int) value) == 0) return;
            
            if(((int) value) == -1 )
                m_spellController.m_currentSpell = 3;
            else 
                m_spellController.m_currentSpell = 1;
            m_spellController.Fire();
            spellingMode = false;
            return;
        }



        if(invokeSpell){
            if(((int) value) == 0) return;
            lastInput = ((int) value + 4) % 4;
            return;
        }
        m_direction = value;
    }

    void MoveAround(){
        Vector3 difference = m_other.transform.position - transform.position;

        if(difference.magnitude < m_minDistance){
            Vector3 crossProduct = Vector3.Cross(-transform.position, difference);

            if (crossProduct.y > 0f && m_direction > 0f){
                m_animator.SetFloat("Speed", 0f);
                return;
            }
            else if (crossProduct.y < 0f  && m_direction < 0f){
                m_animator.SetFloat("Speed", 0f);
                return;
            }
        }
        m_animator.SetFloat("Speed", m_direction);
        transform.RotateAround(centerPoint.position, Vector3.up, -m_direction * m_speed * Time.deltaTime);
    }

    void OnFire(){
        m_direction = 0f;
        if(invokeSpell){
            lastInput = 0;
            return;
        }
        if(!spellingMode){
            spellingMode = true;
            m_direction = 0f;
            return;
        }
        m_spellController.m_currentSpell = 0;
        m_spellController.Fire();
        spellingMode = false;

    }

    void OnOther(){
        if(invokeSpell){
            lastInput = 2;
            return;
        }
        if(!spellingMode){
            isProtected = false;
            return;
        }
        m_spellController.m_currentSpell = 2;
        m_spellController.Fire();
        spellingMode = false;
    }

    public void LockPlayer()
    {
        canAct = false;
        Invoke("UnlockPlayer", 1f);
    }

    void UnlockPlayer()
    {
        canAct = true; 
        //Debug.Log("Le joueur peut maintenant agir.");
    }

    public void ApplyTorment(float duration)
    {
        StartCoroutine(TormentCoroutine(duration)); 
    }

    private IEnumerator TormentCoroutine(float duration)
    {
        torment = true;
        yield return new WaitForSeconds(duration);
        torment = false;
    }
}
