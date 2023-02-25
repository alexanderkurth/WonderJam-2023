using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    public GameObject m_Target;
    [SerializeField, Range(0, 10)]
    private float m_Speed = 1.0f;
    [SerializeField]
    private ParticleSystem m_ParticleSystem;

    private Vector3 m_Direction;

    public bool m_IsOnGround = false;

    // Start is called before the first frame update
    void Start()
    {
        if (m_Target == null)
        {
            m_Direction = Vector3.forward;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!m_IsOnGround)
        {
            if (m_Target != null)
            {
                m_Direction = (m_Target.transform.position - transform.position).normalized;
            }

            transform.Translate(m_Direction * Time.fixedDeltaTime * m_Speed);
        }
    }

    public void Die()
    {
        m_ParticleSystem.Play();
    }
}
