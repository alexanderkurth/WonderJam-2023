using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : AbstractSingleton<Inventory>
{
    [SerializeField] 
    private TextMeshProUGUI _currentCurrencyText;

    [SerializeField] 
    private float _baseValue;
    
    private float _currentCurrency;
    private Dictionary<ObjectType, List<AvailableObject>> _inventory = new Dictionary<ObjectType, List<AvailableObject>>();
    private const int MAX_ARMOR_COUNT = 3;

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

    public void AddToInventory(ObjectType objectType, AvailableObject availableObject)
    {
        if (_inventory.ContainsKey(objectType))
        {
            _inventory[objectType].Add(availableObject);
        }
        else
        {
            List<AvailableObject> availableObjects = new List<AvailableObject> { availableObject };
            _inventory.Add(objectType, availableObjects);
        }

        if (IsFullArmor())
        {
            GameMode.Instance.WinGame();
        }
    }

    public bool IsInInventory(ObjectType objectType, AvailableObject availableObject)
    {
        if (_inventory.ContainsKey(objectType))
        {
            return _inventory[objectType].Contains(availableObject);
        }

        return false; 
    }

    public bool IsFullArmor()
    {
        ObjectType objectType = ObjectType.BlingArmorPart;
        if (_inventory.ContainsKey(objectType))
        {
            return _inventory[objectType].Count == MAX_ARMOR_COUNT; 
        }

        return false; 
    }
}
