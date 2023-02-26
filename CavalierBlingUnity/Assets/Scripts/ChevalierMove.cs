using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChevalierMove : MonoBehaviour
{
    [SerializeField, Range(0, 10)]
    public float m_Speed = 1.0f;
    [SerializeField]
    private float m_LearpMin = -50.0f;
    [SerializeField]
    private float m_LearpMax = 50.0f;
    [SerializeField]
    private float m_LerpDuration = 3.0f;
    [SerializeField]
    private GameObject m_TargetPivot;
    [SerializeField]
    private GameObject m_VisualizeRoot;

    private Vector3 m_Direction;
    private float m_TimeElapsed;

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
        float lerpValue = 0.0f;
        if (m_TimeElapsed < m_LerpDuration)
        {
            lerpValue = Mathf.Lerp(m_LearpMin, m_LearpMax, m_TimeElapsed / m_LerpDuration);
            m_TimeElapsed += Time.deltaTime;
        }
        else
        {
            m_TimeElapsed = 0.0f;
            m_LearpMin = -m_LearpMin;
            lerpValue = m_LearpMax;
            m_LearpMax = -m_LearpMax;
        }

        m_Direction = new Vector3(m_TargetPivot.transform.position.x + lerpValue, m_TargetPivot.transform.position.y, m_TargetPivot.transform.position.z).normalized;
        transform.Translate(m_Direction * Time.fixedDeltaTime * m_Speed);
        m_VisualizeRoot.transform.forward = m_Direction;
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
