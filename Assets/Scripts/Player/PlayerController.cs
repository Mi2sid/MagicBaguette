using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Animator m_animator;
    Rigidbody m_rigidBody;
    float m_direction;
    float m_speed;
    public Transform centerPoint;

    public PlayerController m_other;
    float m_minDistance = 1.5f;

    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_rigidBody = FindObjectOfType<Rigidbody>();

        m_direction = 0f;
        m_speed = 70f;
    }

    void Update()
    {
        MoveAround();
        transform.LookAt(m_other.transform.position);
    }

    void OnMove(InputValue inputValue){
        m_direction = inputValue.Get<Vector2>().x;
        Debug.Log(this.name);
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
}
