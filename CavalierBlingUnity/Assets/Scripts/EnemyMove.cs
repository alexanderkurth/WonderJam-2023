using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Target;
    [SerializeField]
    private float m_Delta;
    private Vector3 m_Direction;

    // Start is called before the first frame update
    void Start()
    {
        if (m_Delta == 0.0f)
        {
            m_Delta = Time.deltaTime;
        }

        if (m_Target == null)
        {
            m_Direction = Vector3.forward;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(m_Target != null)
        {
            m_Direction = m_Target.transform.position;
        }

        transform.Translate(m_Direction * m_Delta);
    }
}
