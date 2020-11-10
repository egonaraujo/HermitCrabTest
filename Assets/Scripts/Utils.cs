using System;
using UnityEngine;

public static class Utils
{
    public static string GetFormattedTime(float timer)
    {
        return String.Format("{0}:{1,0:00}:{2,0:000}",
                                            timer > 60 ? Mathf.Floor(timer / 60f) : 0,
                                            Mathf.Floor(timer % 60),
                                            (timer % 1) * 1000);
    }
}
