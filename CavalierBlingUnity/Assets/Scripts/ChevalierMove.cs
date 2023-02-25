using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChevalierMove : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_Direction;
    [SerializeField, Range(0, 10)]
    private float m_Speed = 1.0f;
    [SerializeField]
    private float m_xMin = -10.0f;
    [SerializeField]
    private float m_xMax = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        if(m_Direction == Vector3.zero)
        {
            m_Direction = Vector3.forward;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // WIP



        transform.Translate(m_Direction * Time.fixedDeltaTime * m_Speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            EnemyMove enemy = other.GetComponent<EnemyMove>();
            enemy.m_IsOnGround = true;
            enemy.FallToGround();
        }
    }
}
