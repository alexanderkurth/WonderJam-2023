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


    void Start()
    {
        m_Volume.profile.TryGet<Vignette>(out m_Vignette);
        m_Volume.profile.TryGet<ColorAdjustments>(out m_ColorAdjustement);
        m_Volume.profile.TryGet<FilmGrain>(out m_FilmGrain);
    }

    private void Update()
    {
        m_Vignette.center.value = new Vector2(m_Target.position.x, m_Target.position.y);
    }

    void UpdateVignette(float intensity)
    {
        m_Vignette.intensity.value = intensity;
    }
    void UpdateColor(float intensity)
    {
        float scaledValue = (intensity - min) / (max - min);
        m_ColorAdjustement.postExposure.value = intensity * scaledValue ;
    }
    void UpdateFilmGrain(float intensity)
    {
        m_FilmGrain.intensity.value = intensity;
    }
}
