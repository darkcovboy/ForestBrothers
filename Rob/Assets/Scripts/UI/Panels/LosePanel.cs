using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LosePanel : MonoBehaviour
{
    [SerializeField] private Button _restartButton;

    private IGameLose _gameLose;
    private void OnDisable()
    {
        _gameLose.OnGameLossing -= OnGameLose;
    }

   //[Inject]
    public void Constructor(IGameLose gameLose)
    {
        //Добавить класс связанный с переходами между сценами
        _gameLose = gameLose;
        _gameLose.OnGameLossing += OnGameLose;
    }

    private void OnGameLose() => gameObject.Activate();
}
