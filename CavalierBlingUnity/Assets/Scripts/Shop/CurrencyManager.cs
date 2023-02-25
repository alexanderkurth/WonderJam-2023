using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI _currentCurrencyText; 
    
    private int currentCurrency = 0;

    private void Start()
    {
        UpdateCurrencyText();
    }

    public void ChangeCurrencyValue(int value)
    {
        currentCurrency += Mathf.Max(0, value);
        UpdateCurrencyText();
    }

    private void UpdateCurrencyText()
    {
        _currentCurrencyText.text = currentCurrency.ToString();
    }
}
