using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI _currentCurrencyText;

    [SerializeField] 
    private int _baseValue;
    
    private int _currentCurrency;

    private void Start()
    {
        // Init currenCurrency
        _currentCurrency = _baseValue;
        ChangeCurrencyValue(0);
    }

    public void ChangeCurrencyValue(int value)
    {
        _currentCurrency += value;
        _currentCurrencyText.text = _currentCurrency.ToString();

        if (_currentCurrency < 0)
        {
            // Game Over
        }
    }

    public int GetCurrentCurrency()
    {
        return _currentCurrency;
    }
}
