using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum AvailableObject
{
    ArmorBoots = 0,
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

public class BaseObject : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _mObjectRenderer = null;
    [SerializeField]
    private ParticleSystem _mObjectBuyPS = null;
    [SerializeField]
    private AudioSource _mObjectBuySound = null;

    private int _mObjectPrice = 1;
    private ObjectType _mObjectType = ObjectType.Miscellaneous;

    public void IntializeObject(ObjectData objectData)
    {
        _mObjectPrice = SetObjectPrice(objectData.ObjectPrices);
        _mObjectRenderer.sprite = objectData.ObjectSprite;
        _mObjectType = objectData.ObjectType;

        ToggleVisual(true);
    }
    
    private int SetObjectPrice(List<int> price)
    {
        int priceCount = price.Count;

        int randomPriceIndex = Random.Range(0, priceCount + 1);

        return price[randomPriceIndex];
    }

    private void ToggleVisual(bool toEnable)
    {
        _mObjectRenderer.enabled = toEnable;
    }

    public int GetObjectPrice()
    {
        return _mObjectPrice;
    }

    public virtual void BuyItem()
    {
        //TODO : Call code to remove currency for the value _mObjectPrice and warn the Player with the object type;
        _mObjectBuyPS.Play(true);
        _mObjectBuySound.Play();
        ToggleVisual(false);
    }
}
