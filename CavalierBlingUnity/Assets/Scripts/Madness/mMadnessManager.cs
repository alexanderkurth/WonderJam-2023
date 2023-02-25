using UnityEngine;
class mMadnessManager : MonoBehaviour
{
    [SerializeField]
    private int m_MadnessCurrentLevel = 0;
    [SerializeField]
    private const int m_MadnessMaxLevel = 100;

    [SerializeField]
    private const int m_StartShakingLevel = 50;

    [SerializeField]
    private const int m_MaxShakingIntensity = 50;
    
    public int GetCurrentMadnessLevel()
    {
        return m_MadnessCurrentLevel;
    }
    public int GetMaxMadnessLevel()
    {
        return m_MadnessMaxLevel;
    }

    public void IncreaseMadnessLevel(int amount = 1)
    {
        m_MadnessCurrentLevel += amount;
        if (m_MadnessCurrentLevel > m_MadnessMaxLevel)
        {
            // TODO : ADD GAME OVER
            Debug.Log("GAME OVER");
        }
    }

    public void ReduceCurrentLevel(int amount = 1)
    {
        m_MadnessCurrentLevel -= amount;
        if (m_MadnessCurrentLevel < 0)
        {
            m_MadnessCurrentLevel = 0;
        }
    }

    private void Update()
    {
        if (m_MadnessCurrentLevel > m_StartShakingLevel)
        {
            // Should be between 0 and m_MaxShakingIntensity
            int cameraShakingIntensity = ((m_MadnessCurrentLevel - m_StartShakingLevel) / (m_MadnessMaxLevel - m_StartShakingLevel)) * m_MaxShakingIntensity;
            // TODO : Camera.Shake(cameraShakingIntensity);
        }
    }
}