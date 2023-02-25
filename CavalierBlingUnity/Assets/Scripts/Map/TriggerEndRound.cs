using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEndRound : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        ChevalierMove chevalier = other.gameObject.GetComponent<ChevalierMove>();
        if(chevalier != null)
        {
            Debug.Log("Trigger day end");
            GameMode.Instance.DayEnd();
        }
    }
}
