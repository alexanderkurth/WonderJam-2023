using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum MessageEnum
{
    DelayStartGame,
    RandomSentenceInGame,
    FinishingAnEnemy,
    Warnings,
    BuyRandomPiece,
    BuyArmorPiece,
    BuyAnInstrument,
}

[System.Serializable]
public struct MessageDatas
{
    public MessageEnum MessageName;
    public List<string> Messages;
}

public class HUDCanvas : AbstractSingleton<HUDCanvas>
{
    [SerializeField]
    private GameObject _mMessageObject = null;

    [SerializeField]
    private TextMeshProUGUI _mMessageText = null;

    [SerializeField]
    private Vector2 _mMinMaxDisplayTimeValue = new Vector2(2, 5);

    [SerializeField]
    private Vector2 _mMinMaxStringSizeForMaxDisplayTime = new Vector2(20, 200);

    [SerializeField]
    private List<MessageDatas> _mMessageDatas = new List<MessageDatas>();

    public void DisplayMessage(MessageEnum messageToDisplay, float delayBeforeDisplay = 0f)
    {
        StartCoroutine(DisplayMessageCoroutine(GetMessageForEnum(messageToDisplay), delayBeforeDisplay));
    }

    private IEnumerator DisplayMessageCoroutine(string text, float delayBeforeDisplay)
    {
        yield return new WaitForSeconds(delayBeforeDisplay);

        float lerp = 0f;

        _mMessageObject.SetActive(true);
        
        while (lerp < 1f)
        {
            lerp += Time.deltaTime / 0.75f;

            _mMessageText.text = text.Substring(0, Mathf.FloorToInt(Mathf.Lerp(0, text.Length, lerp)));

            yield return null;
        }

        yield return new WaitForSeconds(Mathf.Lerp(_mMinMaxDisplayTimeValue.x, _mMinMaxDisplayTimeValue.y, Mathf.InverseLerp(_mMinMaxStringSizeForMaxDisplayTime.x, _mMinMaxStringSizeForMaxDisplayTime.y, text.Length)));

        _mMessageObject.SetActive(false);

        yield break;
    }

    private string GetMessageForEnum(MessageEnum messageToDisplay)
    {
        foreach (MessageDatas item in _mMessageDatas)
        {
            return item.Messages[Random.Range(0, item.Messages.Count)];
        }

        return string.Empty;
    }
}
