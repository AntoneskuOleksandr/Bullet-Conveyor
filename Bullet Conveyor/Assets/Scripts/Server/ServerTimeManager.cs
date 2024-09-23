using System;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ServerTimeManager : MonoBehaviour
{
    public static ServerTimeManager Instance { get; private set; }

    [Serializable]
    public class TimeResponse
    {
        public string datetime;
    }

    public DateTime ServerTime { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            StartCoroutine(GetServerTime());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        ServerTime = ServerTime.AddSeconds(Time.unscaledDeltaTime);
    }

    private IEnumerator GetServerTime()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get("http://worldtimeapi.org/api/timezone/Etc/UTC"))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string jsonResult = webRequest.downloadHandler.text;
                TimeResponse timeResponse = JsonUtility.FromJson<TimeResponse>(jsonResult);
                ServerTime = DateTime.Parse(timeResponse.datetime);
            }
            else
            {
                Debug.Log("Error: " + webRequest.error);
            }
        }
    }
}
