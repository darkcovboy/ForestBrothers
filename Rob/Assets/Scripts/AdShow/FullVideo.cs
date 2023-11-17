using Agava.YandexGames;
using UnityEngine;
using Zenject;

public class FullVideo
{
    private bool _isAudioOff;
    private Loader _loader;

    [Inject]
    public void Constructor(Loader loader)
    {
        _loader = loader;
    }

    public void ShowVideoNext()
    {
        InterstitialAd.Show(OnOpen, OnCloseNextLevel);
    }

    public void ShowVideoRestart()
    {
        InterstitialAd.Show(OnOpen, OnCloseRestartLevel);
    }

    private void OnOpen()
    {
        _isAudioOff = AudioListener.pause;

        AudioListener.pause = true;
        Debug.Log("Открытие");

        Time.timeScale = 0f;
    }

    private void OnCloseNextLevel(bool canShow)
    {
        if (_isAudioOff == false)
            AudioListener.pause = false;

        Time.timeScale = 1f;
        Debug.Log("Закрытие");
        Loader.Instance.LoadNextLevel();
    }

    private void OnCloseRestartLevel(bool canShow)
    {
        if (_isAudioOff == false)
            AudioListener.pause = false;

        Time.timeScale = 1f;
        Loader.Instance.OnLoader();
        Loader.Instance.Restart();
    }
}
