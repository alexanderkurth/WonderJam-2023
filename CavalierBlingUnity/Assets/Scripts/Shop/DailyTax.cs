using UnityEngine;

public class DailyTax : MonoBehaviour
{
    private const float DAILY_TAX = 100f;
    private const float TAX_MULTIPLIER = 0.05f;
    private float _currentTax = 0f; 
    
    private float GetDailyTax()
    {
        _currentTax = DAILY_TAX * TAX_MULTIPLIER; // * Day number from game mode
        return _currentTax;
    }

    public void DeductTax()
    {
        float currentTax = GetDailyTax();
        CurrencyManager.Instance.ChangeCurrencyValue(currentTax);
    }
}
