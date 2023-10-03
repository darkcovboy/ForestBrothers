using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LosePanel : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private CanvasGroupHolder _canvasGroupHolder;

    private IGameLose _gameLose;

    private void OnEnable()
    {
        _canvasGroupHolder.ClosePanels(this.gameObject);
    }

    private void OnDisable()
    {
        _gameLose.OnGameLossing -= OnGameLose;
    }

    [Inject]
    public void Constructor(Player gameLose, Loader loader)
    {
        //Добавить класс связанный с переходами между сценами
        _gameLose = gameLose;
        _gameLose.OnGameLossing += OnGameLose;
    }

    private void OnGameLose() => gameObject.Activate();
}
