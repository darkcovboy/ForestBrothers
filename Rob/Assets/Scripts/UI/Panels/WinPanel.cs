using UnityEngine;
using Zenject;
using TMPro;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _earnedMoney;
    [SerializeField] private Button _multiplyMoneyButton;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private CanvasGroupHolder _canvasGroupHolder;

    private IGameResultHandler _gameResult;
    private RewardedVideo _rewardedVideo;
    private FullVideo _fullVideo;
    private MoneyCounter _moneyCounter;
    private PlayerSave _playerSave;

    private void OnEnable()
    {
        _canvasGroupHolder.ClosePanels(this.gameObject);
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
    public void Constructor(IGameResultHandler gameResultHandler, FullVideo fullVideo, RewardedVideo rewardedVideo, MoneyCounter moneyCounter, PlayerSave playerSave)
    {
        _gameResult = gameResultHandler;;
        _rewardedVideo = rewardedVideo;
        _fullVideo = fullVideo;
        _gameResult.OnGameWinning += OnWinGame;
        _moneyCounter = moneyCounter;
        _playerSave = playerSave;
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

    private void ShowEarned(int earnedMoney) => _earnedMoney.text = $"{earnedMoney}";
    private void NextLevel()
    {
        _playerSave.CompleteLevel(_moneyCounter.GetMoney());
        _fullVideo.ShowVideoNext();
    }
}
