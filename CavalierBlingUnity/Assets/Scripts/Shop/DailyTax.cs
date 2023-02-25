using UnityEngine;

public class DailyTax : AbstractSingleton<DailyTax>
{
    private const float DAILY_TAX = 100f;
    private const float TAX_MULTIPLIER = 0.05f;
    private float _currentTax = 0f; 
    
    private float GetDailyTax()
    {
        _currentTax = DAILY_TAX * TAX_MULTIPLIER * GameMode.Instance.dayCount;
        return _currentTax;
    }

    public void DeductTax()
    {
        float currentTax = GetDailyTax();
        Inventory.Instance.ChangeCurrencyValue(currentTax);
    }
}
