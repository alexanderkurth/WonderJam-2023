using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static GameMode;

public enum MessageEnum
{
    DelayStartGame,
    RandomSentenceInGame,
    FinishingAnEnemy,
    BuyRandomPiece,
    BuyArmorPiece,
    BuyAnInstrument,
    OutOfScreenWarning,
    OutOfScreenLose,
    MadnessWarning,
    MadnessLose,
    NotEnoughMoneyLose,
    Victory
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

    public void DisplayMessage(MessageEnum messageEnum, AvailableObject objectName)
    {
        if (_mMessageCoroutine == null)
        {
            _mMessageCoroutine = StartCoroutine(DisplayMessageCoroutine(GetMessageForEnum(messageEnum, objectName), 0f));
        }
    }

    public void SendVictoryMessage()
    {
        if (_mMessageCoroutine != null)
        {
            StopCoroutine(_mMessageCoroutine);
        }
        _mMessageCoroutine = StartCoroutine(DisplayMessageCoroutine(GetMessageForEnum(MessageEnum.Victory), 0f));
    }

    public void SendGameOverMessage(GameOverCondition gameOverCondition)
    {
        if (_mMessageCoroutine != null)
        {
            StopCoroutine(_mMessageCoroutine);
        }

        MessageEnum messageEnum = MessageEnum.OutOfScreenLose;
        switch (gameOverCondition)
        {
            case GameOverCondition.OutOfScreen:
                {
                    messageEnum = MessageEnum.OutOfScreenLose;
                }
                break;
            case GameOverCondition.Madness:
                {
                    messageEnum = MessageEnum.MadnessLose;
                }
                break;
            case GameOverCondition.NotEnoughMoney:
                {
                    messageEnum = MessageEnum.NotEnoughMoneyLose;
                }
                break;
            default:
                break;
        }

        _mMessageCoroutine = StartCoroutine(DisplayMessageCoroutine(GetMessageForEnum(messageEnum), 0f));
    }

    private IEnumerator DisplayMessageCoroutine(string text, float delayBeforeDisplay)
    {
        if(text == string.Empty)
        {
            yield break;
        }

        yield return new WaitForSecondsRealtime(delayBeforeDisplay);

        float lerp = 0f;

        _mMessageObject.SetActive(true);
        
        while (lerp < 1f)
        {
            lerp += Time.unscaledDeltaTime / 0.5f;

            _mMessageText.text = text.Substring(0, Mathf.FloorToInt(Mathf.Lerp(0, text.Length, lerp)));

            yield return null;
        }

        yield return new WaitForSecondsRealtime(Mathf.Lerp(_mMinMaxDisplayTimeValue.x, _mMinMaxDisplayTimeValue.y, Mathf.InverseLerp(_mMinMaxStringSizeForMaxDisplayTime.x, _mMinMaxStringSizeForMaxDisplayTime.y, text.Length)));

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

    private string GetMessageForEnum(MessageEnum messageEnum, AvailableObject objectName)
    {
        string stringToReturn = string.Empty;
        List<string> messages = new List<string>();

        foreach (MessageDatas item in _mMessageDatas)
        {
            if (item.MessageName == messageEnum)
            {
                messages = item.Messages;
            }
        }

        switch (messageEnum)
        {
            case MessageEnum.BuyRandomPiece:
                {
                    stringToReturn = messages[(int)objectName - (int)AvailableObject.Wine];
                }
                break;
            case MessageEnum.BuyArmorPiece:
                {
                    stringToReturn = messages[(int)objectName];
                }
                break;
            case MessageEnum.BuyAnInstrument:
                {
                    stringToReturn = messages[(int)objectName - (int)AvailableObject.Flute];
                }
                break;
        }

        return stringToReturn;
    }
}
