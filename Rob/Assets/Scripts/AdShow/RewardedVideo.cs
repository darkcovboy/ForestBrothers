using Agava.YandexGames;
using UnityEngine;
using Zenject;

public class RewardedVideo
{
    private const int Multiplier = 3;
    private const int AddMoneyCount = 80;

    private MoneyCounter _moneyCounter;
    private bool _isAudioOff;

    [Inject]
    public void Constructor(MoneyCounter moneyCounter)
    {
        _moneyCounter = moneyCounter;
    }

    public void MultiplyMoney()
    {
        VideoAd.Show(OnOpen, OnRewardMultiply, OnClose);
    }

    public void AddMoney()
    {
        VideoAd.Show(OnOpen, OnRewardAdd, OnClose);
    }

    private void OnOpen()
    {
        _isAudioOff = AudioListener.pause;

        AudioListener.pause = true;

        Time.timeScale = 0f;
    }

    private void OnClose()
    {
        if (_isAudioOff == false)
            AudioListener.pause = false;

        Time.timeScale = 1f;
    }

    private void OnRewardMultiply()
    {
        _moneyCounter.AddMoney(_moneyCounter.EarnedMoney * Multiplier);
    }

    private void OnRewardAdd()
    {
        _moneyCounter.AddMoney(AddMoneyCount);
    }
}
