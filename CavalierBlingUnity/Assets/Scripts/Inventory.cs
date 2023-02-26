using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : AbstractSingleton<Inventory>
{
    [SerializeField] 
    private float _baseValue;
    
    private float _currentCurrency;
    private Dictionary<ObjectType, List<AvailableObject>> _inventory = new Dictionary<ObjectType, List<AvailableObject>>();
    private const int MAX_ARMOR_COUNT = 3;

    public static UnityEvent<ObjectData> itemAddedToInventory;
    
    protected override void Awake()
    {
        base.Awake();
        
        // Init currenCurrency
        _currentCurrency = _baseValue;
    }

    private void Start()
    {
        ChangeCurrencyValue(0);
    }

    public void ChangeCurrencyValue(float value)
    {
        _currentCurrency += value;
        CurrencyInformations.Instance.SetCurrencyValue(_currentCurrency);

        if (_currentCurrency < 0)
        {
            GameMode.Instance.GameOver(GameMode.GameOverCondition.NotEnoughMoney);
        }
    }
    
    public float GetCurrentCurrency()
    {
        return _currentCurrency;
    }

    public void AddToInventory(ObjectData objectData)
    {
        if (_inventory.ContainsKey(objectData.ObjectType))
        {
            _inventory[objectData.ObjectType].Add(objectData.ObjectName);
        }
        else
        {
            List<AvailableObject> availableObjects = new List<AvailableObject> { objectData.ObjectName };
            _inventory.Add(objectData.ObjectType, availableObjects);
        }
        
        itemAddedToInventory?.Invoke(objectData);

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
