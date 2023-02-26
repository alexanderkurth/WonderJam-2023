// Make an instrument manager that can be used to play sounds

using UnityEngine;

public class InstrumentEffectManager : MonoBehaviour
{
    private void Update() {
        Inventory inventory = Inventory.Instance;
        AvailableObject currentInstrument = inventory.GetCurrentInstruments();
        if(currentInstrument == AvailableObject.None || currentInstrument == null) return;

        switch (currentInstrument) {
            case AvailableObject.Flute :
                // Play flute sound
                break;
            case AvailableObject.Violin :
                // Play violin sound
                break;
            case AvailableObject.Trumpet :
                // Play luth sound
                break;
            case AvailableObject.Cornemuse :
                // Play cornemuse sound
                break;
            default:
                Debug.LogError("No instrument selected");
                break;
        }
    }
}