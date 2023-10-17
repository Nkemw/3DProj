using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public static class TimeManager
{
    public static DateTime ReturnCurrentTime()
    {
        DateTime currentTime = DateTime.Now;
        return currentTime;
    }

    public static int ReturnTimeDiffBySeconds(string pastTime)
    {
        TimeSpan timeDiff = DateTime.Now - DateTime.Parse(pastTime);

        return timeDiff.Seconds;
    }
}
