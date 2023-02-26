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

    private Coroutine _mMessageCoroutine = null;

    public bool DisplayMessage(MessageEnum messageToDisplay, float delayBeforeDisplay = 0f)
    {
        if (_mMessageCoroutine == null)
        {
            _mMessageCoroutine = StartCoroutine(DisplayMessageCoroutine(GetMessageForEnum(messageToDisplay), delayBeforeDisplay));
            
            return true;
        }

        return false;
    }

    private IEnumerator DisplayMessageCoroutine(string text, float delayBeforeDisplay)
    {
        if(text == string.Empty)
        {
            yield break;
        }

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

        _mMessageCoroutine = null;

        yield break;
    }

    private string GetMessageForEnum(MessageEnum messageToDisplay)
    {
        foreach (MessageDatas item in _mMessageDatas)
        {
            if (item.MessageName == messageToDisplay)
            {
                return item.Messages[Random.Range(0, item.Messages.Count)];
            }
        }

        return string.Empty;
    }
}
