using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleSingleton : AbstractSingleton<ExampleSingleton>
{
    public void WriteLog(string message)
    {
        Debug.Log(message);
        Debug.LogWarning(message);
        Debug.LogError(message);
    }
}
