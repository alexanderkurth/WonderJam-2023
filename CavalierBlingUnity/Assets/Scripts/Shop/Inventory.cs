using TMPro;
using UnityEngine;

public class Inventory : AbstractSingleton<Inventory>
{
    [SerializeField] 
    private TextMeshProUGUI _currentCurrencyText;

    [SerializeField] 
    private float _baseValue;
    
    private float _currentCurrency;

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
            GameMode.Instance.GameOver(GameMode.GameOverCondition.NotEnoughMoney);
        }
    }
    
    public float GetCurrentCurrency()
    {
        return _currentCurrency;
    }
}
