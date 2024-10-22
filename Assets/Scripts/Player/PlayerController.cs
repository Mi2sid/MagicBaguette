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
    }

    void OnMove(InputValue inputValue){
        m_direction = inputValue.Get<Vector2>().x;
    }

    void MoveAround(){
        m_animator.SetFloat("Speed", m_direction);
        transform.RotateAround(centerPoint.position, Vector3.up, -m_direction * m_speed * Time.deltaTime);


    }
}
