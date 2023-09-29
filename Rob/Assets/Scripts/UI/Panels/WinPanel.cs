using UnityEngine;
using Zenject;
using TMPro;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private GameplayPanel _panel;
    [SerializeField] private TMP_Text _earnedMoney;
    [SerializeField] private Button _multiplyMoneyButton;
    [SerializeField] private Button _nextLevelButton;

    private IGameResultHandler _gameResult;
    private Loader _loader;
    private RewardedVideo _rewardedVideo;
    private MoneyCounter _moneyCounter;
    private PlayerSave _playerSave;

    private void OnEnable()
    {
        _panel.gameObject.Deactivate();
        _multiplyMoneyButton.onClick.AddListener(MultiplyMoney);
        _nextLevelButton.onClick.AddListener(NextLevel);
        ShowEarned(_moneyCounter.EarnedMoney);
    }

    private void OnDisable()
    {
        if(_multiplyMoneyButton.gameObject.activeSelf == true)
            _multiplyMoneyButton.onClick.RemoveListener(MultiplyMoney);

        _nextLevelButton.onClick.RemoveListener(NextLevel);
        _gameResult.OnGameWinning -= OnWinGame;
    }

    [Inject]
    public void Constructor(IGameResultHandler gameResultHandler, Loader loader, RewardedVideo rewardedVideo, MoneyCounter moneyCounter, PlayerSave playerSave)
    {
        _gameResult = gameResultHandler;
        _loader = loader;
        _rewardedVideo = rewardedVideo;
        _gameResult.OnGameWinning += OnWinGame;
        _moneyCounter = moneyCounter;
    }

    private void OnWinGame()
    {
        gameObject.Activate();
    }

    private void MultiplyMoney()
    {
        _multiplyMoneyButton.onClick.RemoveListener(MultiplyMoney);
        _multiplyMoneyButton.gameObject.Deactivate();
        _rewardedVideo.MultiplyMoney();
    }

    private void ShowEarned(int earnedMoney) => _earnedMoney.text = $"+{earnedMoney}";
    private void NextLevel()
    {
        _playerSave.CompleteLevel(_moneyCounter.GetMoney());
        _loader.LoadNextLevel();
    }
}
