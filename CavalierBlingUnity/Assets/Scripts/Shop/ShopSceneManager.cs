using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSceneManager : AbstractSingleton<ShopSceneManager>
{
    [SerializeField]
    private Transform _mKnightTransform = null;

    [SerializeField]
    private Transform _mKnightStartPivot = null;

    [SerializeField]
    private Transform _mShopPositionPivot = null;

    [SerializeField]
    private float _mMoveToShopTime = 2f;
    [SerializeField]
    private Vector2 _mHesitationTimePerObject = new Vector2(0.25f, 0.5f);
    [SerializeField]
    private int _mPercentChanceChoseItem = 20;
    [SerializeField]
    private float _mSpendingMoveToStartTime = 2f;
    [SerializeField]
    private float _mNoSpendingMoveToStartTime = 4f;

    private void Start()
    {
        Vector3 rotation = _mKnightTransform.rotation.eulerAngles;
        rotation.y = 0f;
        _mKnightTransform.rotation = Quaternion.Euler(rotation);
        StartCoroutine(MoveKnightToPointCoroutine(_mShopPositionPivot.position, _mMoveToShopTime));
    }

    private IEnumerator MoveKnightToPointCoroutine(Vector3 point, float duration)
    {
        float lerp = 0f;
        Vector3 initialPosition = _mKnightTransform.position;

        while (lerp<1f)
        {
            lerp += Time.deltaTime / duration;

            Vector3 newPosition = Vector3.Lerp(initialPosition, point, lerp);
            _mKnightTransform.position = newPosition;

            yield return null;
        }

        ChoseShopFlow();
        yield break;
    }

    private void ChoseShopFlow()
    {
        List<BuyableItem> buyableObjects = Shop.Instance.GetBuyableObjects();
         
        StartCoroutine(HesitationFlow(buyableObjects));
    }

    private IEnumerator HesitationFlow(List<BuyableItem> buyableObjects)
    {
        int objectCount = buyableObjects.Count;
        bool objectIsChosen = objectCount > 0 ? false : true;

        yield return new WaitForSeconds(Random.Range(_mHesitationTimePerObject.x, _mHesitationTimePerObject.y));

        BuyableItem selectedItem = null;
        int index = 0;
        int numberOfTry = 0;
        Vector3 rotation;

        while (!objectIsChosen)
        {
            selectedItem = buyableObjects[index];
            Vector3 direction = (_mKnightTransform.position - selectedItem.transform.position).normalized;
            _mKnightTransform.rotation = Quaternion.LookRotation(direction);

            //C'est dégueux mais on corrige le X pour rester flat.
            rotation = _mKnightTransform.rotation.eulerAngles;
            rotation.x = -90f;
            _mKnightTransform.rotation = Quaternion.Euler(rotation);

            yield return new WaitForSeconds(Random.Range(_mHesitationTimePerObject.x, _mHesitationTimePerObject.y));

            index = (index + 1) % objectCount;
            numberOfTry++;

            objectIsChosen = Random.Range(0, 101) <= _mPercentChanceChoseItem * numberOfTry;
        }


        rotation = _mKnightTransform.rotation.eulerAngles;
        rotation.y = 180f;
        _mKnightTransform.rotation = Quaternion.Euler(rotation);

        if (selectedItem != null)
        {
            selectedItem.BuyItem();
            StartCoroutine(MoveKnightToPointCoroutine(_mKnightStartPivot.position, _mSpendingMoveToStartTime));
        }
        else
        {
            StartCoroutine(MoveKnightToPointCoroutine(_mKnightStartPivot.position, _mNoSpendingMoveToStartTime));
        }

        yield break;
    }
}
