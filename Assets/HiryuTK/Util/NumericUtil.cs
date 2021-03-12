using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class NumericUtil
{
    public static int SignAllowingZero (float value)
    {
        if (value > 0.1f)
            return 1;
        else if (value < -0.1f)
            return -1;
        else
            return 0;
    }
}