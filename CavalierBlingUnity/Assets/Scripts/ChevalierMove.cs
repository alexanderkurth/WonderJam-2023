using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChevalierMove : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_Direction;
    [SerializeField]
    private float m_Delta;

    // Start is called before the first frame update
    void Start()
    {
        if(m_Delta == 0.0f)
        {
            m_Delta = Time.deltaTime;
        }

        if(m_Direction == Vector3.zero)
        {
            m_Direction = Vector3.forward;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(m_Direction * m_Delta);
    }
}
