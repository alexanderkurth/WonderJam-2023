using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private Camera m_Camera;

    [SerializeField]
    private Transform m_CameraTransform;

    [SerializeField]
    private Vector3 m_OriginalCameraPosition;

    // Shake Parameters
    [SerializeField]
    private float m_shakeTimer;

    [SerializeField]
    public float m_ShakeDuration = 2.0f;
    [SerializeField]
    public float m_ShakeIntensity = 0.7f;
    [SerializeField]
    public bool canShake = false;


    // Start is called before the first frame update
    void Start()
    {
        if (m_Camera == null)
        {
            m_Camera = GetComponent<Camera>();

            m_CameraTransform = m_Camera.transform;
            m_OriginalCameraPosition = m_CameraTransform.localPosition;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ShakeCamera(1.0f, 5.0f);
        }

        if (canShake)
        {
            StartCameraShakeEffect();
        }
    }

    public void ShakeCamera(float duration, float intensity)
    {
        m_shakeTimer = duration;
        m_ShakeIntensity = intensity;

        canShake = true;
    }

    void StartCameraShakeEffect()
    {
        if (m_shakeTimer > 0)
        {
            m_CameraTransform.localPosition = m_OriginalCameraPosition + Random.insideUnitSphere * m_ShakeIntensity;
            m_shakeTimer -= Time.deltaTime;  
        }
        else
        {
            m_shakeTimer = 0f;
            m_CameraTransform.position = m_OriginalCameraPosition;
            canShake = false;
        }
    }
}
