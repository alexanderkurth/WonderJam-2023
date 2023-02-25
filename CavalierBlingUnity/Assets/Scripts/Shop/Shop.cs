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
    private Dictionary<BuyableItem, ObjectType> _mBuyableItems = null;

    [SerializeField]
    private ObjectsDefinition _mObjectsDefinition = null;

    private void Start()
    {
        //Link self to GameMode.    
    }

    private void CreateShopItems()
    {
        foreach (ShopItemSelection item in _mShopItemSelection)
        {
            GameObject newGO = Instantiate(_mBuyableItemPrefab, item.ItemParentRoot);
            BuyableItem buyableItem = newGO.GetComponent<BuyableItem>();

            _mBuyableItems.Add(buyableItem, item.ItemObjectType);
        }
    }

    public void InitializeShopOfTheDay()
    {
        foreach (KeyValuePair<BuyableItem, ObjectType> item in _mBuyableItems)
        {
            item.Key.IntializeObject(SelectBuyableItem(item.Value));
        }
    }

    public ObjectData SelectBuyableItem(ObjectType objectType)
    {
        List<ObjectData> objectSelection = _mObjectsDefinition.GetObjectsFromType(objectType);
        int selectionCount = objectSelection.Count - 1;

        for (int i = selectionCount; i >= 0; i--)
        {
            ObjectData objectData = objectSelection[i];
            if (objectSelection[i].CanBeBoughtMultipleTime == true || !Inventory.Instance.IsInInventory(objectSelection[i].ObjectType,objectSelection[i].ObjectName))
            {
                continue;   
            }

            objectSelection.RemoveAt(i);
        }

        selectionCount = objectSelection.Count;

        return objectSelection[Random.Range(0, selectionCount)];
    }
}
