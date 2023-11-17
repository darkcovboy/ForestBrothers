using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LosePanel : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private CanvasGroupHolder _canvasGroupHolder;

    private IGameLose _gameLose;
    private FullVideo _fullVideo;

    private void OnEnable()
    {
        _canvasGroupHolder.ClosePanels(this.gameObject);
        _restartButton.onClick.RemoveAllListeners();
        _restartButton.onClick.AddListener(Restart);
    }

    private void OnDisable()
    {
        _restartButton.onClick.RemoveListener(Restart);
        _gameLose.OnGameLossing -= OnGameLose;
    }

    [Inject]
    public void Constructor(Player gameLose, FullVideo fullVideo)
    {
        _gameLose = gameLose;
        _fullVideo = fullVideo;
        _gameLose.OnGameLossing += OnGameLose;
    }

    public void Restart() => _fullVideo.ShowVideoRestart();

    private void OnGameLose() => gameObject.Activate();
}
