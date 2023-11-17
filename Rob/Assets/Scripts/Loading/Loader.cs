using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class Loader : MonoBehaviour
{
    [SerializeField] private Slider _loadingBar;

    public static Loader Instance { get; private set; }

    private int _nextLevelIndex;
    private bool _canLoad = false;
    private readonly int FirstLevelIndex = 1;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        gameObject.Deactivate();
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetNextLevel(int index)
    {
        Debug.Log("Устанавливаем - " + index);
        _nextLevelIndex = index;
    }

    public void Restart()
    {
        Load(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnLoader()
    {
        _canLoad = true;
    }

    public void LoadNextLevel()
    {
        if (!_canLoad)
            return;

        if ((_nextLevelIndex + 1) > SceneManager.sceneCountInBuildSettings)
        {
            Load(FirstLevelIndex);
        }
        else
        {
            Load(_nextLevelIndex);
            _nextLevelIndex++;
        }
            
    }

    private void Load(int levelIndex)
    {
        Debug.Log("Загрузка " + levelIndex);
        gameObject.Activate();
        StartCoroutine(LoadSceneAsync(levelIndex));
    }

    private IEnumerator LoadSceneAsync(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        _loadingBar.maxValue = 1;

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            _loadingBar.value = progressValue;

            yield return null;
        }

        gameObject.Deactivate();
    }
}
