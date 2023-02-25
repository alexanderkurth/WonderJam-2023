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
    private bool _mPlayOnStart = false;
    [SerializeField]
    private float _mMoveToShopTime = 2f;
    [SerializeField]
    private Vector2 _mNumberOfObjectToLookAt = new Vector2(2, 5);
    [SerializeField]
    private Vector2 _mHesitationTimePerObject = new Vector2(0.25f, 0.5f);
    [SerializeField]
    private float _mSpendingMoveToStartTime = 2f;
    [SerializeField]
    private float _mNoSpendingMoveToStartTime = 4f;

    private void Start()
    {
        if (_mPlayOnStart)
        {
            Vector3 rotation = _mKnightTransform.rotation.eulerAngles;
            rotation.y = 180f;
            _mKnightTransform.rotation = Quaternion.Euler(rotation);
            StartCoroutine(ShoppingLoop());
        }
    }

    public void StartShoppingLoop()
    {
        Vector3 rotation = _mKnightTransform.rotation.eulerAngles;
        rotation.y = 180f;
        _mKnightTransform.rotation = Quaternion.Euler(rotation);
        StartCoroutine(ShoppingLoop());
    }

    private IEnumerator ShoppingLoop()
    {
        yield return MoveKnightToPointCoroutine(_mShopPositionPivot.position, _mMoveToShopTime);

        List<BuyableItem> buyableObjects = Shop.Instance.GetBuyableObjects();

        StartCoroutine(HesitationFlow(buyableObjects));
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

        yield break;
    }

    private IEnumerator HesitationFlow(List<BuyableItem> buyableObjects)
    {
        int objectCount = buyableObjects.Count;
        bool objectIsChosen = objectCount > 0 ? false : true;
        Vector3 rotation = Vector3.zero;
        BuyableItem selectedItem = null;

        yield return new WaitForSeconds(Random.Range(_mHesitationTimePerObject.x, _mHesitationTimePerObject.y));

        if (objectCount > 0)
        {
            int index = 0, numberOfTry = 0;
            List<int> indexes = GenerateIndexToLookAt(objectCount);

            for (int i = 0; i < indexes.Count; i++)
            {
                selectedItem = buyableObjects[index];
                Vector3 direction = (selectedItem.transform.position - _mKnightTransform.position).normalized;
                _mKnightTransform.rotation = Quaternion.LookRotation(direction);

                //C'est dégueux mais on corrige le X pour rester flat.
                rotation = _mKnightTransform.rotation.eulerAngles;
                rotation.x = 0f;
                _mKnightTransform.rotation = Quaternion.Euler(rotation);

                yield return new WaitForSeconds(Random.Range(_mHesitationTimePerObject.x, _mHesitationTimePerObject.y));

                index = (index + 1) % objectCount;
                numberOfTry++;
            }
        }


        rotation = _mKnightTransform.rotation.eulerAngles;
        rotation.y = 0;
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

    private List<int> GenerateIndexToLookAt(int objectCount)
    {
        List<int> toReturn = new List<int>();
        int numberOfObjectToLookAt = objectCount > 1 ? Mathf.RoundToInt(Random.Range(_mNumberOfObjectToLookAt.x, _mNumberOfObjectToLookAt.y)) : 1;
        int selectIndex = Random.Range(0, objectCount);

        for (int i = 0; i < numberOfObjectToLookAt; i++)
        {
            toReturn.Add(selectIndex);

            int valueToAdd = Random.Range(0, 2) > 0 ? 1 : -1;
            selectIndex = (selectIndex + valueToAdd) % objectCount;
        }

        return toReturn;
    }
}
