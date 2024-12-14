using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public PlayerController p1;
    public PlayerController p2;
    void Start()
    {
    }

    void Update()
    {
        if(p1.m_health.isDead() || p2.m_health.isDead())
            SceneManager.LoadScene("Menu");
    }
}
