using System;
using UnityEngine;
using UnityEngine.Events;

public class ChangeChevalierVisual : MonoBehaviour
{
    [SerializeField] 
    private SpriteRenderer _torsoSprite;
    [SerializeField] 
    private SpriteRenderer _headSprite;
    [SerializeField] 
    private SpriteRenderer _armSprite;
    [SerializeField] 
    private SpriteRenderer _legRightSprite;
    [SerializeField] 
    private SpriteRenderer _legLeftSprite;

    private void OnEnable()
    {
        if (Inventory.itemAddedToInventory == null)
            Inventory.itemAddedToInventory = new UnityEvent<ObjectData>();

        Inventory.itemAddedToInventory.AddListener(ChangeSprite);
    }

    private void ChangeSprite(ObjectData item)
    {
        switch (item.ObjectName)
        {
            case AvailableObject.ArmorArm:
                _armSprite.sprite = item.ObjectSprite;
                break;
            
            case AvailableObject.ArmorLegs:
                _legRightSprite.sprite = item.ObjectSprite;
                _legLeftSprite.sprite = item.ObjectSprite;
                break;
            
            case AvailableObject.ArmorChest:
                _torsoSprite.sprite = item.ObjectSprite;
                break;
            
            case AvailableObject.ArmorHead:
                _headSprite.sprite = item.ObjectSprite;
                break;
            
            case AvailableObject.Flute:
                break;
            case AvailableObject.Violin:
                break;
            case AvailableObject.Luth:
                break;
            case AvailableObject.Wine:
                break;
            case AvailableObject.Cheese:
                break;
            case AvailableObject.Wood:
                break;
            case AvailableObject.ButterKnife:
                break;
            case AvailableObject.OldSocks:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
