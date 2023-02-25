using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleSingletonCaller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ExampleSingleton.Instance.WriteLog("Hello singleton");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
