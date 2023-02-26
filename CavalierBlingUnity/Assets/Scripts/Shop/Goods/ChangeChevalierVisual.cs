using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChangeChevalierVisual : MonoBehaviour
{
    [Header("Sprite Renderer")]
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
    
    [Header("Images")]
    [SerializeField] 
    private Image _torsoUImage;
    [SerializeField] 
    private Image _headUImage;
    [SerializeField] 
    private Image _armUImage;
    [SerializeField] 
    private Image _legUImage;
    
    private void OnEnable()
    {
        if (Inventory.itemAddedToInventory == null)
        {
            Inventory.itemAddedToInventory = new UnityEvent<ObjectData>();
        }

        Inventory.itemAddedToInventory.AddListener(ChangeSprite);

        foreach (ObjectData item in Inventory.Instance.GetCurrentObjectListForType(ObjectType.BlingArmorPart))
        {
            ChangeSprite(item);
        }
    }

    private void ChangeSprite(ObjectData item)
    {
        switch (item.ObjectName)
        {
            case AvailableObject.ArmorArm:
                _armSprite.sprite = item.ObjectSprite;
                if (_armUImage)
                {
                    Color tempColor = _armUImage.color;
                    tempColor.a = 1f;
                    _armUImage.color = tempColor;
                    _armUImage.sprite =  item.ObjectSprite;
                }
                break;
            
            case AvailableObject.ArmorLegs:
                _legRightSprite.sprite = item.ObjectSprite;
                _legLeftSprite.sprite = item.ObjectSprite;
                if (_legUImage)
                {
                    Color tempColor = _legUImage.color;
                    tempColor.a = 1f;
                    _legUImage.color = tempColor;
                    _legUImage.sprite =  item.ObjectSprite;
                }
                break;
            
            case AvailableObject.ArmorChest:
                _torsoSprite.sprite = item.ObjectSprite;
                if (_torsoUImage)
                {
                    Color tempColor = _torsoUImage.color;
                    tempColor.a = 1f;
                    _torsoUImage.color = tempColor;
                    _torsoUImage.sprite = item.ObjectSprite;
                }
                break;
            
            case AvailableObject.ArmorHead:
                _headSprite.sprite = item.ObjectSprite;
                if (_headUImage)
                {
                    Color tempColor = _headUImage.color;
                    tempColor.a = 1f;
                    _headUImage.color = tempColor;
                    _headUImage.sprite = item.ObjectSprite;
                }
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
