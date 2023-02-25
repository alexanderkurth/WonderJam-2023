using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public enum AvailableObject
{
    ArmorArm = 0,
    ArmorLegs = 1,
    ArmorChest = 2,
    ArmorHead = 3,
    Flute = 50,
    Violin,
    Luth,
    Wine = 100,
    Cheese,
    Wood,
    ButterKnife,
    OldSocks,
}

public enum ObjectType
{
    BlingArmorPart,
    MusicalInstrument,
    Miscellaneous
}

[Serializable]
public struct ObjectData
{
    public AvailableObject ObjectName;
    public ObjectType ObjectType;
    public List<int> ObjectPrices;
    public Sprite ObjectSprite;
    public bool CanBeBoughtMultipleTime;
}

public class BuyableItem : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _mObjectRenderer = null;
    [SerializeField]
    private ParticleSystem _mObjectBuyPS = null;
    [SerializeField]
    private AudioSource _mObjectBuySound = null;
    [SerializeField]
    private TextMeshPro _mTmpPrice = null;

    private int _mObjectPrice = 1;
    public int ObjectPrice { get => _mObjectPrice; }
    private ObjectType _mObjectType = ObjectType.Miscellaneous;
    private AvailableObject _mAvailableObject = AvailableObject.ArmorArm;

    public void IntializeObject(ObjectData objectData)
    {
        _mObjectPrice = SetObjectPrice(objectData.ObjectPrices);
        _mTmpPrice.text = _mObjectPrice.ToString();
        _mTmpPrice.color = IsBuyable() ? Color.green : Color.red;
        _mObjectRenderer.sprite = objectData.ObjectSprite;
        _mObjectType = objectData.ObjectType;
        _mAvailableObject = objectData.ObjectName;

        ToggleVisual(true);
    }

    public bool IsBuyable()
    {
        return Inventory.Instance.GetCurrentCurrency() >= _mObjectPrice;
    }
    
    private int SetObjectPrice(List<int> price)
    {
        int priceCount = price.Count;
        int randomPriceIndex = Random.Range(0, priceCount);

        return price[randomPriceIndex];
    }

    private void ToggleVisual(bool toEnable)
    {
        _mObjectRenderer.enabled = toEnable;
        _mTmpPrice.enabled = toEnable;
    }

    public int GetObjectPrice()
    {
        return _mObjectPrice;
    }

    public virtual void BuyItem()
    {
        Inventory.Instance.ChangeCurrencyValue(-_mObjectPrice);
        Inventory.Instance.AddToInventory(_mObjectType, _mAvailableObject);
        _mObjectBuyPS.Play(true);
        _mObjectBuySound.Play();
        ToggleVisual(false);
    }
}
