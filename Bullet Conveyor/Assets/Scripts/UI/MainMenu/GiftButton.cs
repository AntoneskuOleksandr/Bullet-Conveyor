using System;
using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class GiftButton : MonoBehaviour
{
    public Button giftButton;
    public int coins;
    public TMP_Text timerText;
    public float giftIntervalMinutes = 20f;

    private DateTime nextGiftTime;

    private void Start()
    {
        giftButton.onClick.AddListener(GiveGift);

        string nextGiftTimeStr = PlayerPrefs.GetString("NextGiftTime", string.Empty);
        if (!string.IsNullOrEmpty(nextGiftTimeStr))
        {
            nextGiftTime = DateTime.Parse(nextGiftTimeStr);
        }

        bool isButtonInteractable = PlayerPrefs.GetInt("IsButtonInteractable", 1) > 0;
        giftButton.interactable = isButtonInteractable;
    }

    private void Update()
    {
        DateTime serverTime = ServerTimeManager.Instance.ServerTime;

        if (serverTime >= nextGiftTime)
        {
            giftButton.interactable = true;
        }

        TimeSpan timeToGift = nextGiftTime - serverTime;
        if (timeToGift.TotalSeconds > 0)
        {
            timerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeToGift.Hours, timeToGift.Minutes, timeToGift.Seconds);
        }
        else
        {
            timerText.text = "GET!!!";
        }
    }

    private void GiveGift()
    {
        CoinMove.Instance.CountCoins(coins, giftButton.transform);

        giftButton.interactable = false;

        PlayerPrefs.SetInt("IsButtonInteractable", 0);

        nextGiftTime = ServerTimeManager.Instance.ServerTime.AddMinutes(giftIntervalMinutes);
        PlayerPrefs.SetString("NextGiftTime", nextGiftTime.ToString());
    }
}
