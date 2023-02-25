using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_OriginalPosition = default;

    [SerializeField]
    [Range(0, 10)]
    private float m_ShakeFrequency = 0;

    [SerializeField]
    private bool m_CanShake = true;

    private void Start()
    {
        m_OriginalPosition = transform.localPosition;
    }

    public void ResetPosition()
    {
        transform.localPosition = m_OriginalPosition;
    }

    private void ShakeCamera()
    {
        float x = m_OriginalPosition.x + Random.insideUnitSphere.x * Time.deltaTime * m_ShakeFrequency;
        float y = m_OriginalPosition.y + Random.insideUnitSphere.y * Time.deltaTime * m_ShakeFrequency;
        float z = m_OriginalPosition.z + Random.insideUnitSphere.z * Time.deltaTime * 1 / m_ShakeFrequency;

        Vector3 pos = new Vector3(x, y, z);

        transform.localPosition = pos;
    }

    private void Update()
    {
        if (m_CanShake  || m_ShakeFrequency != 0)
        {
            ShakeCamera();
        }
    }
}