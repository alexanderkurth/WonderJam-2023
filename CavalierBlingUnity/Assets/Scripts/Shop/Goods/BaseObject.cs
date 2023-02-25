using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AvailableObject
{
    ArmorBoots = 0,
    ArmorLegs = 1,
    ArmorChest = 2,
    ArmorHead = 3,
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
    public int ObjectPrice;
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

    protected int _mObjectPrice = 1;

    public void IntializeObject(ObjectData objectData)
    {
        _mObjectPrice = objectData.ObjectPrice;
        _mObjectRenderer.sprite = objectData.ObjectSprite;

        ToggleVisual(true);
    }

    private void ToggleVisual(bool toEnable)
    {
        _mObjectRenderer.enabled = toEnable;
    }

    public virtual void BuyItem()
    {
        //TODO : Call code to remove currency for the value _mObjectPrice;
        _mObjectBuyPS.Play(true);
        _mObjectBuySound.Play();
        ToggleVisual(false);
    }
}
