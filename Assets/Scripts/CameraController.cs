using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform m_player1;
    public Transform m_player2;

    public Transform m_middlePart;
    public Transform m_position;

    float m_MawDistance = 6f;

    void Start()
    {
        m_middlePart.position = new Vector3((m_player1.position.x + m_player2.position.x) / 2, 0f, (m_player1.position.z + m_player2.position.z) / 2);
        Vector3 rotatedVector = Quaternion.Euler(0, 90, 0) * new Vector3(m_player1.position.x - m_player2.position.x, 0f, m_player1.position.z - m_player2.position.z);
        m_position.position = rotatedVector.normalized * m_MawDistance + m_middlePart.position;
    }

    // Update is called once per frame
    void Update()
    {
        m_middlePart.position = new Vector3((m_player1.position.x + m_player2.position.x) / 2, 0f, (m_player1.position.z + m_player2.position.z) / 2);
        Vector3 rotatedVector = Quaternion.Euler(0, 90, 0) * new Vector3(m_player1.position.x - m_player2.position.x, 0f, m_player1.position.z - m_player2.position.z);
        m_position.position = rotatedVector.normalized * m_MawDistance + m_middlePart.position;
    }
}
