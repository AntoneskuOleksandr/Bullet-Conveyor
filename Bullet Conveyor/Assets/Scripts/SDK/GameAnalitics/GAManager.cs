using UnityEngine;
using GameAnalyticsSDK;

public class GAManager : MonoBehaviour
{
    public static GAManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More one instance of GA ");
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this);
        instance = this;
    }

    private void Start()
    {
        GameAnalytics.Initialize();
    }

    public void OnLevelComplete(int _level)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "Level completed " + _level);
        Debug.Log("Level completed " + _level);
    }

    public void OnLevelStart(int _level)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "Level started " + _level);
        Debug.Log("Level started " + _level);
    }
}
