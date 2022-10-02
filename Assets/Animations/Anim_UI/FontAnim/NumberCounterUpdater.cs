using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberCounterUpdater : MonoBehaviour
{
    private NumberCounter numberCounter;    

    public void SetValue(int value) => numberCounter.Value = value;
}
