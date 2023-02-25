using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : AbstractSingleton<Shop>
{
    [System.Serializable]
    private struct ShopItemSelection
    {        
        public Transform ItemParentRoot;
        public ObjectType ItemObjectType;
    }

    [SerializeField]
    private GameObject _mBuyableItemPrefab;

    [SerializeField]
    private List<ShopItemSelection> _mShopItemSelection = new List<ShopItemSelection>();

    [SerializeField]
    private Dictionary<BuyableItem, ShopItemSelection> _mBuyableItems = new Dictionary<BuyableItem, ShopItemSelection>();

    [SerializeField]
    private ObjectsDefinition _mObjectsDefinition = null;

    private void Start()
    {
        CreateShopItems(); 
        InitializeShopOfTheDay();
    }

    private void CreateShopItems()
    {
        foreach (ShopItemSelection item in _mShopItemSelection)
        {
            GameObject newGO = Instantiate(_mBuyableItemPrefab, item.ItemParentRoot);
            BuyableItem buyableItem = newGO.GetComponent<BuyableItem>();

            _mBuyableItems.Add(buyableItem, item);
        }
    }

    public void InitializeShopOfTheDay()
    {
        foreach (KeyValuePair<BuyableItem, ShopItemSelection> item in _mBuyableItems)
        {
            item.Key.IntializeObject(SelectBuyableItem(item.Value.ItemObjectType));
        }
    }

    public ObjectData SelectBuyableItem(ObjectType objectType)
    {
        List<ObjectData> objectSelection = _mObjectsDefinition.GetObjectsFromType(objectType);
        int selectionCount = objectSelection.Count - 1;

        for (int i = selectionCount; i >= 0; i--)
        {
            ObjectData objectData = objectSelection[i];
            if (objectData.CanBeBoughtMultipleTime == true || !Inventory.Instance.IsInInventory(objectData.ObjectType, objectData.ObjectName))
            {
                continue;   
            }

            objectSelection.RemoveAt(i);
        }

        selectionCount = objectSelection.Count;

        return objectSelection[Random.Range(0, selectionCount)];
    }

    public List<BuyableItem> GetBuyableObjects()
    {
        List<BuyableItem> buyableItemToReturn = new List<BuyableItem>();

        foreach (KeyValuePair<BuyableItem, ShopItemSelection> item in _mBuyableItems)
        {
            if(item.Key.IsBuyable())
            {
                buyableItemToReturn.Add(item.Key);
            }
        }

        return buyableItemToReturn;
    }
}
