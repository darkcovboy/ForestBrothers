using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    private const int FirstLevelIndex = 1;

    [SerializeField] private Slider _loadingBar;

    public static Loader Instance { get; private set; } 

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }


        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void Restart()
    {
        Load(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        if ((SceneManager.GetActiveScene().buildIndex + 1) > SceneManager.sceneCountInBuildSettings)
            Load(FirstLevelIndex);
        else
            Load((SceneManager.GetActiveScene().buildIndex + 1));
    }

    private void Load(int levelIndex)
    {
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
