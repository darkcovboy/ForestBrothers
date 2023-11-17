using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MainScenePanel : MonoBehaviour
{
    [SerializeField] private Button _loadLevel;

    private Loader _loader;

    private void OnEnable()
    {
        _loadLevel.onClick.AddListener(Loader.Instance.LoadNextLevel);
    }

    private void OnDisable()
    {
        _loadLevel.onClick.RemoveListener(Loader.Instance.LoadNextLevel);
    }
}
