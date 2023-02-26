// Make an instrument manager that can be used to play sounds

using UnityEngine;

public class InstrumentEffectManager : MonoBehaviour
{
    private float m_NextActionTime = 0.0f;
    private mMadnessManager m_MadnessManager;

    [SerializeField]
    private float m_MusicEffectTickSeconds = 1.0f;

    [SerializeField]
    private int m_MadnessMaxAmountPerSecond = 1;

    [SerializeField]
    private float m_MaxDistanceToTrigger = 170.0f;

    [SerializeField]
    private GameObject m_Chevalier;

    [SerializeField]
    private GameObject m_Ecuyer;

    [SerializeField]
    private AvailableObject currentInstrument = AvailableObject.None;

    private void Start()
    {
        if (m_MadnessManager == null)
            m_MadnessManager = mMadnessManager.Instance;

        if (m_Chevalier == null)
            m_Chevalier = FindObjectOfType<ChevalierMove>().gameObject;

        if (m_Ecuyer == null)
            m_Ecuyer = GameObject.Find("PlayerArmature");

        SetCurrentInstrument();
    }

    private void SetCurrentInstrument()
    {
        Inventory inventory = Inventory.Instance;
        currentInstrument = inventory.GetCurrentInstruments();
        if (currentInstrument == AvailableObject.None) return;

        switch (currentInstrument)
        {
            case AvailableObject.Flute:
                m_MadnessMaxAmountPerSecond = Mathf.Abs(m_MadnessMaxAmountPerSecond) * -1;
                // Play flute sound
                break;
            case AvailableObject.Violin:
                m_MadnessMaxAmountPerSecond = Mathf.Abs(m_MadnessMaxAmountPerSecond) * -1;
                // Play violin sound
                break;
            case AvailableObject.Trumpet:
                m_MadnessMaxAmountPerSecond = Mathf.Abs(m_MadnessMaxAmountPerSecond);
                // Play luth sound
                break;
            case AvailableObject.Cornemuse:
                m_MadnessMaxAmountPerSecond = Mathf.Abs(m_MadnessMaxAmountPerSecond);
                // Play cornemuse sound
                break;
            default:
                Debug.LogError("No instrument selected");
                break;
        }
    }

    private void Update()
    {
        if (m_MadnessManager == null || currentInstrument == AvailableObject.None) return;

        if (Time.time > m_NextActionTime)
        {
            m_NextActionTime += m_MusicEffectTickSeconds;
            // Take the distance between the player and the chevalier world to screen
            Vector3 chevalierPosScreen = Camera.main.WorldToScreenPoint(m_Chevalier.transform.position);
            Vector3 ecuyerPosScreen = Camera.main.WorldToScreenPoint(m_Ecuyer.transform.position);
            float distance = Vector3.Distance(chevalierPosScreen, ecuyerPosScreen);
            if (distance < m_MaxDistanceToTrigger)
            {
                float scalar = Mathf.Round((1.0f - (distance / m_MaxDistanceToTrigger)) * 100.0f) / 100.0f;
                int madnessAmount = Mathf.RoundToInt(Mathf.Abs(m_MadnessMaxAmountPerSecond * scalar));
                if (m_MadnessMaxAmountPerSecond < 0)
                    m_MadnessManager.ReduceMadnessLevel(Mathf.RoundToInt(madnessAmount));
                else
                    m_MadnessManager.IncreaseMadnessLevel(Mathf.RoundToInt(madnessAmount));
            }

        }
    }
}