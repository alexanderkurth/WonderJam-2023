using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataHolder : AbstractSingleton<GlobalDataHolder>
{
    private int _mCurrentDay = 0;
    public int CurrentDay { get => _mCurrentDay; }

    public void IncreaseDayCount()
    {
        _mCurrentDay++;
    }

}
