using UnityEngine;
// Make a manager for the madness system
class mMadnessManager : MonoBehaviour
{
    [SerializeField]
    private int madnessCurrentLevel = 0;
    [SerializeField]
    private const int MADNESSMAXLEVEL = 100;
    
    public void ReduceCurrentLevel(int amount = 1)
    {
        madnessCurrentLevel -= amount;
        if (madnessCurrentLevel < 0)
        {
            // TODO : ADD GAME OVER
            madnessCurrentLevel = 0;
        }
    }
}