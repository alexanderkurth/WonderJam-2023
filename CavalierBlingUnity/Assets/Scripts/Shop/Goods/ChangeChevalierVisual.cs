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
    
    private void Start()
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
                    _armUImage.gameObject.SetActive(true);
                    _armUImage.sprite =  item.ObjectSprite;
                }
                break;
            
            case AvailableObject.ArmorLegs:
                _legRightSprite.sprite = item.ObjectSprite;
                _legLeftSprite.sprite = item.ObjectSprite;
                if (_legUImage)
                {
                    _legUImage.gameObject.SetActive(true);
                    _legUImage.sprite =  item.ObjectSprite;
                }
                break;
            
            case AvailableObject.ArmorChest:
                _torsoSprite.sprite = item.ObjectSprite;
                if (_torsoUImage)
                {
                    _torsoUImage.gameObject.SetActive(true);
                    _torsoUImage.sprite = item.ObjectSprite;
                }
                break;
            
            case AvailableObject.ArmorHead:
                _headSprite.sprite = item.ObjectSprite;
                if (_headUImage)
                {
                    _headUImage.gameObject.SetActive(true);
                    _headUImage.sprite = item.ObjectSprite;
                }
                break;
            
            case AvailableObject.Flute:
                break;
            case AvailableObject.Trumpet:
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
