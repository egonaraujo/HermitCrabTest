using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsController : MonoBehaviour
{
    private static AnalyticsController _instance;

    public static AnalyticsController I { get { return _instance; } }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public void SendGameStart()
    {
        Analytics.CustomEvent("GameStarted");
    }
    public void SendWin(float time)
    {
        Analytics.CustomEvent("PlayerWon", new Dictionary<string, object>() { { "Time", time } });
    }
    public void SendMushroomCollected(int i)
    {
        Analytics.CustomEvent("MushroomCollected",new Dictionary<string,object>(){ { "NumberOfMushrooms", i } } );
    }
    public void SendDie()
    {
        Analytics.CustomEvent("PlayerDied");
    }
}
