using UnityEngine;
class mMadnessManager : MonoBehaviour
{
    [SerializeField]
    private int madnessCurrentLevel = 0;
    [SerializeField]
    private const int MADNESSMAXLEVEL = 100;
    
    public int GetCurrentMadnessLevel()
    {
        return madnessCurrentLevel;
    }
    public int GetMaxMadnessLevel()
    {
        return MADNESSMAXLEVEL;
    }

    public void IncreaseMadnessLevel(int amount = 1)
    {
        madnessCurrentLevel += amount;
        if (madnessCurrentLevel > MADNESSMAXLEVEL)
        {
            // TODO : ADD GAME OVER
            Debug.Log("GAME OVER");
        }
    }

    public void ReduceCurrentLevel(int amount = 1)
    {
        madnessCurrentLevel -= amount;
        if (madnessCurrentLevel < 0)
        {
            madnessCurrentLevel = 0;
        }
    }
}