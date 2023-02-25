using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance; 
    
    [SerializeField] 
    private TextMeshProUGUI _currentCurrencyText;

    [SerializeField] 
    private float _baseValue;
    
    private float _currentCurrency;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Init currenCurrency
        _currentCurrency = _baseValue;
        ChangeCurrencyValue(0);
    }

    public void ChangeCurrencyValue(float value)
    {
        _currentCurrency += value;
        _currentCurrencyText.text = _currentCurrency.ToString();

        if (_currentCurrency < 0)
        {
            // Game Over
        }
    }
    
    public float GetCurrentCurrency()
    {
        return _currentCurrency;
    }
}
