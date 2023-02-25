using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaking : MonoBehaviour
{
    [SerializeField]
    float shakeAmount = 1.0f;
    [SerializeField]
    float shakeSpeed = 1.0f;

    Vector2 startingPos;

    void Awake()
    {
        startingPos.x = transform.position.x;
        startingPos.y = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 curPos = transform.position;
        curPos.x = startingPos.x + Mathf.Sin(Time.time * shakeSpeed) * shakeAmount/10;
        curPos.z = startingPos.y + Mathf.Sin(Time.time * shakeSpeed) * shakeAmount/10;
        transform.position = curPos;

    }
}
