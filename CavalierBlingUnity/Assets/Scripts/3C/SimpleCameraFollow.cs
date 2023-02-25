using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraFollow : MonoBehaviour
{
    public Camera m_Camera;
    public Transform m_Target;
    public float m_VerticalOffset = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (m_Camera == null || m_Target == null)
              Debug.LogError("Camera or Target not set");

        Vector3 targetPos = m_Target.position;
        targetPos.y += m_VerticalOffset;
        m_Camera.transform.position = targetPos;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = m_Target.position;
        targetPos.y += m_VerticalOffset;
        m_Camera.transform.position = targetPos;
    }
}
