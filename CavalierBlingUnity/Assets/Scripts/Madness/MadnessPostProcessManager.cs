using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class MadnessPostProcessManager : MonoBehaviour
{
    [SerializeField]
    Volume m_Volume;

    [SerializeField]
    Vignette m_Vignette;

    [SerializeField]
    ColorAdjustments m_ColorAdjustement;

    [SerializeField]
    float min;
    [SerializeField]
    float max;

    [SerializeField]
    FilmGrain m_FilmGrain;

    public Transform m_Target;

    [SerializeField]
    private Camera m_Camera;


    void Start()
    {
        if(m_Camera == null)
        {
            m_Camera = Camera.main;
        }

        m_Volume.profile.TryGet<Vignette>(out m_Vignette);
        m_Volume.profile.TryGet<ColorAdjustments>(out m_ColorAdjustement);
        m_Volume.profile.TryGet<FilmGrain>(out m_FilmGrain);
    }

    private void Update()
    {
        Vector3 objectToScreenPos = m_Camera.WorldToScreenPoint(m_Target.position);
        Debug.Log(objectToScreenPos);
        m_Vignette.center.value = new Vector2(objectToScreenPos.x, objectToScreenPos.z);
    }

    public void UpdateVignette(float intensity)
    {
        m_Vignette.intensity.value = intensity;
    }

    public void UpdateColor(float intensity)
    {
        float scaledValue = (intensity - min) / (max - min);
        m_ColorAdjustement.postExposure.value = intensity * scaledValue ;
    }

    public void UpdateFilmGrain(float intensity)
    {
        m_FilmGrain.intensity.value = intensity;
    }
}
