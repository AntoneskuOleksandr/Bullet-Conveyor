using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance;
    public GameObject coinsGO; 
    public TMP_Text coinsText;
    public float coins;
    public int selectedGunsCount;
    public GameObject[] guns;
    public GameObject levelsParent;
    public UnityEvent onCoinsChanged;

    [Header("Screens")]
    [SerializeField] private GameObject wheelScreen;

    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button giftButton;
    [SerializeField] private Button dailyRewardButton;
    [SerializeField] private Button spinButton;
    [SerializeField] private Button prevLevelButton;
    [SerializeField] private Button nextLevelButton;

    [Header("Scripts")]
    [SerializeField] private LevelDisplay levelDisplay;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        coins = PlayerPrefs.GetFloat("Coins");
        selectedGunsCount = PlayerPrefs.GetInt("SelectedGunsCount");

        UpdateCoinsText();
    }

    public void AddCoins(float amount, int multiplier = 1)
    {
        coins += amount;

        coins *= multiplier;

        PlayerPrefs.SetFloat("Coins", coins);

        onCoinsChanged.Invoke();
        UpdateCoinsText();
    }

    public void SpendCoins(float amount)
    {
        coins -= amount;
        PlayerPrefs.SetFloat("Coins", coins);

        onCoinsChanged.Invoke();
        UpdateCoinsText();
    }

    public void ShowOrHideWheelScrene(bool openWheelScreen)
    {
        dailyRewardButton.gameObject.SetActive(!openWheelScreen);
        giftButton.gameObject.SetActive(!openWheelScreen);
        playButton.gameObject.SetActive(!openWheelScreen);
        prevLevelButton.gameObject.SetActive(!openWheelScreen);
        nextLevelButton.gameObject.SetActive(!openWheelScreen);
        levelsParent.gameObject.SetActive(!openWheelScreen);

        wheelScreen.SetActive(openWheelScreen);
        spinButton.gameObject.SetActive(openWheelScreen);
    }

    private void UpdateCoinsText()
    {
        coinsText.text = coins.ToString();
    }

    public bool IsHasAnyGun()
    {
        return selectedGunsCount > 0;
    }

    public void ChangeSelectedGunsCount(int amount)
    {
        selectedGunsCount += amount;

        PlayerPrefs.SetInt("SelectedGunsCount", selectedGunsCount);

        levelDisplay.UpdateCurrentLevelState();
    }
}
