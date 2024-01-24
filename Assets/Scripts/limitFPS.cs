using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limitFPS : MonoBehaviour
{
    public enum limits
    {
        noLimit = 0,
        limit30 = 30,
        limit60 = 60,
        limit90 = 90,
        limit120 = 120,
    }
    public limits limit;
    private void Awake()
    {
        Application.targetFrameRate = (int)limit;
    }
}
