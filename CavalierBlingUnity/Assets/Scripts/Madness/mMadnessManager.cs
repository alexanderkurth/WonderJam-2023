using UnityEngine;
class mMadnessManager : MonoBehaviour
{
    [SerializeField]
    private int m_MadnessCurrentLevel = 0;
    
    [SerializeField]
    private int m_MadnessMaxLevel = 100;

    [SerializeField]
    private int m_StartShakingLevel = 50;

    [Range(0, 10)] 
    [SerializeField]
    private int m_MaxShakingIntensity = 4;
    
    [System.Serializable]
    public class MadnessAudio{
        [Header("HeartBeat")]
        public int m_StartHeartBeatLevel = 50;
        public AudioSource m_HeartBeatAudioSource;
        public float m_HeartBeatMinPitch = 0.5f;
        public float m_HeartBeatMaxPitch = 3.0f;

        [Header("Breath")]
        public AudioSource m_BreathAudioSource;
        public AudioClip[] m_BreathAudioClips;
    }

    [SerializeField]
    private MadnessAudio m_MadnessAudio;


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

    private void Start() {
        if(m_MadnessAudio.m_HeartBeatAudioSource == null){
            m_MadnessAudio.m_HeartBeatAudioSource = GetComponents<AudioSource>()[0];
        }

        if(m_MadnessAudio.m_BreathAudioSource == null){
            // Get first audio source in game object
            m_MadnessAudio.m_BreathAudioSource = GetComponents<AudioSource>()[1];
        }
    }

    private void Update()
    {
        MadnessPostProcessManager madnessPostProcessManager = GetComponent<MadnessPostProcessManager>();
        madnessPostProcessManager.UpdateVignette((float)m_MadnessCurrentLevel/(float)m_MadnessMaxLevel);
        madnessPostProcessManager.UpdateColor((float)m_MadnessCurrentLevel/(float)m_MadnessMaxLevel);
        madnessPostProcessManager.UpdateFilmGrain((float)m_MadnessCurrentLevel/(float)m_MadnessMaxLevel);

        if (m_MadnessCurrentLevel > m_StartShakingLevel)
        {
            // Should be between 0 and m_MaxShakingIntensity
            float cameraShakingIntensity = ((float)(m_MadnessCurrentLevel - m_StartShakingLevel) / (float)(m_MadnessMaxLevel - m_StartShakingLevel)) * (float)m_MaxShakingIntensity;
            CameraShake cameraShake = GetComponent<CameraShake>();
            cameraShake.SetShakeFrequency(cameraShakingIntensity);

            
        }else{
            CameraShake cameraShake = GetComponent<CameraShake>();
            cameraShake.SetShakeFrequency(0);
        }

        if (m_MadnessCurrentLevel > m_MadnessAudio.m_StartHeartBeatLevel)
        {
            float heartBeatScale = (float)(m_MadnessCurrentLevel - m_MadnessAudio.m_StartHeartBeatLevel) / (float)(m_MadnessMaxLevel - m_MadnessAudio.m_StartHeartBeatLevel);
            // Add audio volume based on current madness level
            m_MadnessAudio.m_HeartBeatAudioSource.volume = heartBeatScale;
            // increase audio speed based on current madness level
            m_MadnessAudio.m_HeartBeatAudioSource.pitch = Mathf.Lerp(m_MadnessAudio.m_HeartBeatMinPitch, m_MadnessAudio.m_HeartBeatMaxPitch, heartBeatScale);

            // Plays a random breath sound effect with a exponentially increase chance of playing
            if (Random.Range(0, 100) < (heartBeatScale * 100))
            {
                if(m_MadnessAudio.m_BreathAudioSource.isPlaying == false){
                    m_MadnessAudio.m_BreathAudioSource.PlayOneShot(m_MadnessAudio.m_BreathAudioClips[Random.Range(0, m_MadnessAudio.m_BreathAudioClips.Length)]);
                }
            }

        }
    }
}