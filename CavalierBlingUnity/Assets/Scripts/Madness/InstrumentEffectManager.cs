// Make an instrument manager that can be used to play sounds

using UnityEngine;

public class InstrumentEffectManager : MonoBehaviour
{
    private float m_NextActionTime = 0.0f;
    [SerializeField]
    private float m_MusicEffectTickSeconds = 0.1f;

    [SerializeField]
    private mMadnessManager m_MadnessManager;

    [SerializeField]
    private int m_MadnessMaxAmountPerSecond = 1;

    [SerializeField]
    private float m_MaxDistanceToTrigger = 5.0f;

    [SerializeField]
    private GameObject m_Chevalier;

    [SerializeField]
    private GameObject m_Ecuyer;

    [SerializeField]
    private AvailableObject currentInstrument = AvailableObject.None;

    private void Start()
    {
        if (m_MadnessManager == null)
            m_MadnessManager = FindObjectOfType<mMadnessManager>();

        if (m_Chevalier == null)
            m_Chevalier = GameObject.Find("Chevalier");

        if (m_Ecuyer == null)
            m_Ecuyer = GameObject.Find("PlayerArmature");

        SetCurrentInstrument();
    }

    private void SetCurrentInstrument()
    {
        Inventory inventory = Inventory.Instance;
        currentInstrument = inventory.GetCurrentInstruments();
        if (currentInstrument == AvailableObject.None || currentInstrument == null) return;

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
        if (m_MadnessManager == null || currentInstrument == AvailableObject.None || currentInstrument == null) return;

        if (Time.time > m_NextActionTime)
        {
            m_NextActionTime += m_MusicEffectTickSeconds;
            // Take the distance between the player and the chevalier
            float distance = Vector3.Distance(m_Chevalier.transform.position, m_Ecuyer.transform.position);
            float scalar = (float)distance / (float)m_MaxDistanceToTrigger;
            Debug.Log("Scalar: " + scalar);
            m_MadnessManager.IncreaseMadnessLevel(Mathf.RoundToInt(m_MadnessMaxAmountPerSecond * scalar));
        }
    }
}